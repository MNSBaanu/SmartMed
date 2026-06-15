---
name: Clinical Precision
colors:
  surface: '#f9f9f9'
  surface-dim: '#dadada'
  surface-bright: '#f9f9f9'
  surface-container-lowest: '#ffffff'
  surface-container-low: '#f3f3f3'
  surface-container: '#eeeeee'
  surface-container-high: '#e8e8e8'
  surface-container-highest: '#e2e2e2'
  on-surface: '#1b1b1b'
  on-surface-variant: '#444652'
  inverse-surface: '#303030'
  inverse-on-surface: '#f1f1f1'
  outline: '#747684'
  outline-variant: '#c4c5d4'
  surface-tint: '#3658ba'
  primary: '#001f66'
  on-primary: '#ffffff'
  primary-container: '#003296'
  on-primary-container: '#87a2ff'
  inverse-primary: '#b5c4ff'
  secondary: '#485f87'
  on-secondary: '#ffffff'
  secondary-container: '#b8cffe'
  on-secondary-container: '#425880'
  tertiary: '#242728'
  on-tertiary: '#ffffff'
  tertiary-container: '#3a3c3e'
  on-tertiary-container: '#a5a6a8'
  error: '#ba1a1a'
  on-error: '#ffffff'
  error-container: '#ffdad6'
  on-error-container: '#93000a'
  primary-fixed: '#dce1ff'
  primary-fixed-dim: '#b5c4ff'
  on-primary-fixed: '#00164e'
  on-primary-fixed-variant: '#173ea1'
  secondary-fixed: '#d6e3ff'
  secondary-fixed-dim: '#b0c7f5'
  on-secondary-fixed: '#001b3e'
  on-secondary-fixed-variant: '#30476e'
  tertiary-fixed: '#e1e2e4'
  tertiary-fixed-dim: '#c5c6c8'
  on-tertiary-fixed: '#191c1e'
  on-tertiary-fixed-variant: '#444749'
  background: '#f9f9f9'
  on-background: '#1b1b1b'
  surface-variant: '#e2e2e2'
typography:
  app-title:
    fontFamily: Inter
    fontSize: 18.6px
    fontWeight: '700'
    lineHeight: 24px
    letterSpacing: -0.01em
  section-header:
    fontFamily: Inter
    fontSize: 16px
    fontWeight: '700'
    lineHeight: 20px
  body-regular:
    fontFamily: Inter
    fontSize: 14.6px
    fontWeight: '400'
    lineHeight: 20px
  data-grid:
    fontFamily: Inter
    fontSize: 13px
    fontWeight: '400'
    lineHeight: 18px
  label:
    fontFamily: Inter
    fontSize: 12px
    fontWeight: '600'
    lineHeight: 16px
rounded:
  sm: 0.125rem
  DEFAULT: 0.25rem
  md: 0.375rem
  lg: 0.5rem
  xl: 0.75rem
  full: 9999px
spacing:
  nav-width: 420px
  nav-item-height: 40px
  container-padding: 24px
  gutter: 16px
  stack-sm: 8px
  stack-md: 16px
---

## Brand & Style

The design system is engineered for **SmartMed Pharmacy**, a desktop-class environment where accuracy, speed, and reliability are paramount. The brand personality is professional, clinical, and authoritative, designed to instill confidence in pharmacists and technicians handling sensitive medical data.

The visual style follows a **Corporate / Modern** aesthetic, specifically tailored for a Windows-style desktop application. It prioritizes high legibility and clear information hierarchy over decorative elements. The interface utilizes a "Utility-First" approach: every visual choice supports a workflow, minimizing cognitive load during high-stakes tasks like prescription fulfillment and inventory management.

Key attributes:
- **Efficiency:** Optimized for keyboard navigation and rapid data entry.
- **Trust:** A sober, navy-anchored palette that feels established and medical.
- **Clarity:** Distinct boundaries between navigation, data grids, and action panels.

## Colors

The palette is rooted in a high-contrast, professional range to ensure accessibility and focus.

- **Primary Navy (#003296):** Used exclusively for institutional branding, application titles, and high-level section headers. It provides the "anchor" for the UI.
- **Accent Blue (#B4CBF9):** The primary action color. It is used for primary buttons, active navigation states, and highlighting selected elements. Its softness contrasts with the deep Navy to indicate interactivity without causing eye fatigue.
- **Neutral Black (#000000):** Reserved for body text and data entry to provide maximum contrast against the white background.
- **Background White (#FFFFFF):** The standard surface for all workspaces, ensuring a sterile and clean medical feel.
- **System Gray (#E5E7EB):** Used for secondary actions, borders, and disabled states.

## Typography

This design system uses **Inter** as the primary typeface, serving as a highly legible, systematic alternative to Segoe UI. It excels in data-dense environments where clarity of numerals and special characters is essential.

- **Hierarchical Scale:** The system uses a tight scale to maximize information density on 1080p and 1440p desktop displays.
- **Readability:** Body text is set at 11pt (approx 14.6px) to maintain standard pharmacy software conventions while improving line-height for better scanning.
- **Emphasis:** Navy is applied only to the App Title and Section Headers. All other text remains black or dark gray to focus the user's attention on the data.

## Layout & Spacing

The layout follows a **Fixed Sidebar** model typical of robust enterprise desktop applications.

- **Navigation Rail:** A fixed-width left sidebar (420px) houses the primary application modules. Navigational items are full-width within this rail, providing a large hit area for rapid switching between tasks.
- **Content Area:** A fluid workspace that expands to fill the remaining screen real estate. This area is dedicated to DataGrids, forms, and patient profiles.
- **Rhythm:** The system uses an 8px grid. Most internal component spacing (padding/margins) should be multiples of 4px or 8px to ensure a structured, Windows-aligned appearance.
- **Alignment:** All form labels are left-aligned above their respective input fields to facilitate a natural top-to-bottom scanning pattern.

## Elevation & Depth

This design system avoids dramatic shadows in favor of **Low-contrast outlines** and **Tonal layers**. This maintains the "flat" professional look of modern desktop interfaces.

- **Level 0 (Floor):** The main application background (#FFFFFF).
- **Level 1 (Sub-surface):** Navigation rail and sidebar areas, often distinguished by a subtle 1px border (#E5E7EB) rather than a shadow.
- **Level 2 (Modals/Dialogs):** Centered dialogs use a medium ambient shadow (0px 4px 12px rgba(0,0,0,0.1)) to lift them above the workspace, accompanied by a semi-transparent dark overlay to dim the background.
- **Interactions:** Hover states on rows or buttons are indicated by a subtle fill change (typically a 5% tint of the primary or secondary color) rather than an increase in elevation.

## Shapes

The design system employs a **Soft** shape language.

- **Standard Radius:** 0.25rem (4px) is applied to buttons, input fields, and container corners. This provides a modern, approachable feel while remaining structured and professional.
- **Tables:** DataGrid corners are sharp (0px) where they meet container edges to maximize usable space and reinforce the "systematic" grid nature of the application.
- **Selection Brushes:** Full-row selection in tables uses a 0px radius to create a continuous horizontal bar across the grid.

## Components

### Buttons
- **Primary Buttons:** Solid Accent Blue (#B4CBF9) fill with Black text. Height 36px or 40px. 
- **Secondary Buttons:** Medium Gray (#E5E7EB) fill with Black text.
- **Navigation Buttons:** Full-width (420px), 40px height, left-aligned text. When active, they use the Accent Blue fill to highlight the current module.

### Input Fields
- **Text Inputs:** White background with a 1px Gray border. On focus, the border transitions to Primary Navy. Labels sit directly above the field in the "label" typography style.
- **Password Masking:** Standard bullet characters with a "show" toggle icon in the right-aligned suffix position.

### DataGrids (Tables)
- **Selection:** Full-row selection using the Accent Blue (#B4CBF9) at 50% opacity to highlight the active record.
- **Columns:** Auto-fit width based on content. Headers are sticky and use the Navy color for text on a light gray background.
- **Density:** Tight vertical padding (8px) to maximize the number of visible rows.

### Dialogs & Modals
- **Placement:** Always centered in the viewport.
- **Sizing:** Fixed widths (e.g., 400px, 600px) depending on content type (Alert vs. Entry Form).
- **Header:** Contains the Navy App Title style and a close "X" icon.

### Chips & Status Indicators
- Used for "In Stock", "Out of Stock", or "Pending". These use a small font size and 2px rounded corners, with colors corresponding to status (Green, Red, Amber).