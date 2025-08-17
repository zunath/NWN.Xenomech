---
title: Armor Catalog
updated: 2025-08-11
category: design
---

### Canonical source

- `docs/_incoming/XM Design Bible - Armor.tsv` (very large table)
- YAML mirror of full sheet: `docs/design/data/armor.yaml` (tsv block)

### Schema (columns)

Required columns: `Type`, `Level`, `Item Name`, `Source`, `DEF`

Optional stats when present: `HP`, `EP`, `ACC`, `ATK`, `EVA`, `MGT`, `PER`, `VIT`, `WIL`, `AGI`, `SOC`

Job tags: `BST`, `BRW`, `ELM`, `HNT`, `KPR`, `MDR`, `NGT`, `TEC` (x indicates compatibility)

### Slot shards

- `docs/design/data/armor/shield.yaml`
- `docs/design/data/armor/head.yaml`
- `docs/design/data/armor/body.yaml`
- `docs/design/data/armor/hands.yaml`
- `docs/design/data/armor/feet.yaml`
- `docs/design/data/armor/neck.yaml`
- `docs/design/data/armor/ring.yaml`
- `docs/design/data/armor/back.yaml`
- `docs/design/data/armor/waist.yaml`

### Notes

- Items frequently appear with +1/+2 variants; treat them as distinct SKUs.
- For machine use, prefer the per-slot YAMLs above or the full TSV mirror when you need the entire dataset.


