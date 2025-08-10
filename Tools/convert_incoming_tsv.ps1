# Converts TSVs in docs/_incoming into Markdown pages under docs/gameplay
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
$gameplayDir = Join-Path $repoRoot 'docs/gameplay'
Ensure-Dir $incomingDir
Ensure-Dir $gameplayDir

$processedPages = @()

Get-ChildItem -LiteralPath $incomingDir -Filter '*.tsv' | ForEach-Object {
    $tsvPath = $_.FullName
    $fileTitle = [System.IO.Path]::GetFileNameWithoutExtension($_.Name)
    $pageTitle = $fileTitle -replace '^XM\s+Design\s+Bible\s+-\s+', ''
    $slug = Get-Slug $pageTitle

    Write-Host "Processing TSV: $fileTitle -> $slug"

    $rows = Import-Csv -LiteralPath $tsvPath -Delimiter "`t"
    if ($rows.Count -eq 0) {
        Write-Warning "Skipping empty TSV: $fileTitle"
        return
    }

    $columns = @()
    $first = $rows | Select-Object -First 1
    foreach ($p in $first.PSObject.Properties) { $columns += $p.Name }

    # Build markdown table
    $sb = New-Object System.Text.StringBuilder
    [void]$sb.AppendLine("# $pageTitle")
    [void]$sb.AppendLine()
    [void]$sb.AppendLine("- Source: XM Design Bible → $pageTitle")
    [void]$sb.AppendLine("- Last imported: $(Get-Date -Format 'yyyy-MM-dd')")
    [void]$sb.AppendLine("- Status: Draft")
    [void]$sb.AppendLine()
    [void]$sb.AppendLine("---")
    [void]$sb.AppendLine()
    $relTsv = "../_incoming/" + ($_.Name -replace ' ', '%20')
    [void]$sb.AppendLine("Download original TSV: [`docs/_incoming/$($_.Name)`]($relTsv)")
    [void]$sb.AppendLine()
    [void]$sb.AppendLine("## Table")
    [void]$sb.AppendLine()

    $header = '| ' + ($columns -join ' | ') + ' |'
    $divider = '| ' + (@($columns | ForEach-Object { '---' }) -join ' | ') + ' |'
    [void]$sb.AppendLine($header)
    [void]$sb.AppendLine($divider)

    foreach ($row in $rows) {
        $cells = @()
        foreach ($col in $columns) {
            $val = ''
            if ($row.PSObject.Properties.Match($col)) { $val = [string]$row.$col }
            # Escape pipes
            $val = $val -replace '\|', '\\|'
            $cells += $val
        }
        [void]$sb.AppendLine('| ' + ($cells -join ' | ') + ' |')
    }

    $outPath = Join-Path $gameplayDir ("$slug.md")
    Set-Content -LiteralPath $outPath -Value $sb.ToString() -Encoding UTF8

    $processedPages += @{ Title = $pageTitle; Slug = $slug }
}

# Update docs/gameplay/README.md index with processed pages only
$readmePath = Join-Path $gameplayDir 'README.md'
if (Test-Path -LiteralPath $readmePath -PathType Leaf -and $processedPages.Count -gt 0) {
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
