---
title: Jobs Catalog
updated: 2025-08-11
category: design
---

### Sources (canonical TSVs in `docs/_incoming/`)

- `XM Design Bible - Beastmaster.tsv`
- `XM Design Bible - Brawler.tsv`
- `XM Design Bible - Elementalist.tsv`
- `XM Design Bible - Hunter.tsv`
- `XM Design Bible - Keeper.tsv`
- `XM Design Bible - Mender.tsv`
- `XM Design Bible - Nightstalker.tsv`
- `XM Design Bible - Techweaver.tsv`

Machine-readable YAML mirrors in `docs/design/data/jobs/` (each includes full TSV under `tsv:`):

- `beastmaster.yaml`
- `brawler.yaml`
- `elementalist.yaml`
- `hunter.yaml`
- `keeper.yaml`
- `mender.yaml`
- `nightstalker.yaml`
- `techweaver.yaml`

YAML structure per job:

- job: `name`, `role`, `armor`
- weaponProficiencies: array of `{ type, grade }`
- attributeGrades: `{ hp, ep, might, perception, vitality, agility, willpower, social, evasion }` as letter grades
- abilities: array with `{ level, name, type, description, ep, castingTime, cooldown, resist, resonanceCost, devStatus, notes }`


