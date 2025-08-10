# Converts PDFs in docs/_incoming into Markdown pages under docs/gameplay using Tika and Pandoc
param()
$ErrorActionPreference = 'Stop'

function Get-Slug([string]$name) {
    $slug = $name.ToLower()
    $slug = $slug -replace '\s+', '-'
    $slug = $slug -replace '[^a-z0-9\-]', ''
    $slug = $slug -replace '\-+', '-'
    $slug = $slug.Trim('-')
    return $slug
}

function Ensure-Dir([string]$path) {
    if (-not (Test-Path -LiteralPath $path)) {
        New-Item -ItemType Directory -Path $path | Out-Null
    }
}

$repoRoot = (Get-Location).Path
$incomingDir = Join-Path $repoRoot 'docs/_incoming'
$tikaHtmlDir = Join-Path $incomingDir '_tika_html'
$tmpMdDir = Join-Path $incomingDir '_md'
$gameplayDir = Join-Path $repoRoot 'docs/gameplay'

Ensure-Dir $incomingDir
Ensure-Dir $tikaHtmlDir
Ensure-Dir $tmpMdDir
Ensure-Dir (Join-Path $gameplayDir 'images')

# Verify Tika is reachable
try {
    Invoke-WebRequest -UseBasicParsing -Uri 'http://localhost:9998' -Method Get -TimeoutSec 5 | Out-Null
} catch {
    Write-Error 'Tika server not reachable on http://localhost:9998. Start it with: docker run --rm -d -p 9998:9998 --name tika_server apache/tika:2.9.2.0'
    exit 1
}

# Ensure pandoc image exists
$null = (cmd /c "docker image inspect pandoc/core:3.1 >NUL 2>&1 || docker pull pandoc/core:3.1 | cat")

$processedPages = @()

Get-ChildItem -LiteralPath $incomingDir -Filter '*.pdf' | ForEach-Object {
    $pdfPath = $_.FullName
    $fileTitle = [System.IO.Path]::GetFileNameWithoutExtension($_.Name)
    $pageTitle = $fileTitle -replace '^XM\s+Design\s+Bible\s+-\s+', ''
    $slug = Get-Slug $pageTitle

    Write-Host "Processing: $fileTitle -> $slug"

    $htmlOut = Join-Path $tikaHtmlDir ("$slug.html")
    Invoke-WebRequest -UseBasicParsing -Uri 'http://localhost:9998/tika' -Method Post -InFile $pdfPath -ContentType 'application/pdf' | Set-Content -LiteralPath $htmlOut -Encoding UTF8

    $tmpMdOut = Join-Path $tmpMdDir ("$slug.md")
    $dockerPwd = $repoRoot
    $dockerHtml = "/data/docs/_incoming/_tika_html/$slug.html"
    $dockerMd = "/data/docs/_incoming/_md/$slug.md"

    $pandocCmd = "docker run --rm -v `"$dockerPwd`":/data pandoc/core:3.1 -f html -t gfm -o `"$dockerMd`" `"$dockerHtml`""
    Write-Host $pandocCmd
    cmd /c $pandocCmd | Out-Null

    if (-not (Test-Path -LiteralPath $tmpMdOut)) {
        throw "Pandoc failed to produce $tmpMdOut"
    }

    $finalMd = Join-Path $gameplayDir ("$slug.md")
    $header = @(
        "# $pageTitle",
        "",
        "- Source: XM Design Bible → $pageTitle",
        "- Last imported: $(Get-Date -Format 'yyyy-MM-dd')",
        "- Status: Draft",
        "",
        "---",
        ""
    ) -join "`r`n"

    $content = Get-Content -LiteralPath $tmpMdOut -Raw
    Set-Content -LiteralPath $finalMd -Value ($header + $content) -Encoding UTF8

    $processedPages += @{ Title = $pageTitle; Slug = $slug }
}

# Update docs/gameplay/README.md index
$readmePath = Join-Path $gameplayDir 'README.md'
if (Test-Path -LiteralPath $readmePath -PathType Leaf) {
    $readme = Get-Content -LiteralPath $readmePath -Raw
    $indexHeader = '## Index'
    $idx = $readme.IndexOf($indexHeader)
    if ($idx -ge 0) {
        $before = $readme.Substring(0, $idx + $indexHeader.Length)
        $after = "`r`n`r`n" + (($processedPages | Sort-Object Title | ForEach-Object { "- [`$($_.Title)`](./$($_.Slug).md)" }) -join "`r`n") + "`r`n"
        $newReadme = $before + $after
        Set-Content -LiteralPath $readmePath -Value $newReadme -Encoding UTF8
    }
}

Write-Host "Done. Generated pages:" -ForegroundColor Green
$processedPages | Sort-Object Title | ForEach-Object { Write-Host " - $($_.Title) -> docs/gameplay/$($_.Slug).md" }
