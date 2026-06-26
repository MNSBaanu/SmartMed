# SmartMed Specialist Agents

Subagents in `.cursor/agents/` are picked **automatically** by the main agent (see `.cursor/rules/subagent-delegation.mdc`). You do not need to name them unless you want a specific one.

## Subagents

| Subagent | Auto-triggers on |
|----------|------------------|
| `smartmed-architect` | New features, refactors, layer/architecture questions |
| `winforms-ui` | `SmartMed/Forms/`, `SmartMed/UI/`, designer, grids |
| `sql-repository` | `Database/`, `SmartMed/Data/`, SQL, repositories |
| `business-logic` | `SmartMed/Services/`, validation, pharmacy rules |
| `qa-testing` | Review, testing, verify (runs in background after changes) |
| `documentation` | Report, diagrams, `Docs/`, assignment writing |

## Usage

Just ask normally:

- *"Add prescription upload to PlaceOrderForm"* → architect plans, then sql + business-logic + winforms-ui
- *"Fix the expiry alert on Manage Medicines"* → business-logic + winforms-ui
- *"Review my order changes"* → qa-testing

Override anytime: *"Use only sql-repository for this"* or *"Don't delegate, do it here"*.

## Optional: `@` skills

`.cursor/skills/` offers the same roles for manual `@winforms-ui` invoke in chat without subagent delegation.

## Project layers

```
SmartMed/Forms/     → Presentation
SmartMed/UI/        → Theme
SmartMed/Services/  → Business logic
SmartMed/Data/      → Data access
SmartMed/Models/    → Entities
Database/           → SQL scripts
```
