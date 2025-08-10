# Gameplay Documentation

This section houses player-facing documentation for Xenomech gameplay systems. It mirrors the structure of the gameplay Excel workbook. Each worksheet (page) in the workbook maps to a Markdown page here.

## Structure
- Each Excel sheet becomes one Markdown file in this folder.
- Use kebab-case for filenames (e.g., `weapon-skills.md`).
- Place any images for a page under `docs/gameplay/images/<page-name>/`.
- Keep code examples out of these docs; link to code files instead.

## How to add a new page
1. Copy `TEMPLATE.md` to a new file named after the sheet (e.g., `attributes.md`).
2. Fill in the sections. When referring to implementation, link to the relevant files (paths in this repo) rather than pasting code.
3. If the sheet contains tables, render them as GitHub Markdown tables.
4. Add the new page to the index below.

## Naming guidance
- Filenames: kebab-case, concise, stable (avoid renames).
- Headings: Start with a single H1 matching the page title; use H2/H3 for sections.
- Terminology: Prefer in-game terms as they appear to players.

## Cross-references
When a gameplay rule is implemented in code, add a short note with links to the files, for example:

- Progression rules: `XM.Shared.Progression/` (specific file links where applicable)
- Combat systems: `XM.Plugin.Combat/`
- Items and equipment: `XM.Plugin.Item/`
- Quests and journal: `XM.Plugin.Quest/`

## Index
Add links to gameplay pages here as they are created:

- (empty for now)


