# Sarah & Amogelang — Wedding Invitation Website

A premium digital wedding invitation for **Sarah and Amogelang**, taking place on **17 October 2026** in **Ga-Rankuwa, South Africa**, starting from 1:00 PM.

---

## Project Structure

```
wedding-invitation/
├── index.html      ← Full semantic HTML structure
├── styles.css      ← All styling (Tswana-inspired design, responsive, CSS variables)
├── app.js          ← Countdown, navigation, scroll animations, RSVP form
├── config.js       ← ⭐ Central configuration — update this file
└── README.md       ← This file
```

---

## Running Locally

This is a **pure HTML/CSS/JavaScript project** with no build tools or dependencies.

### Option 1 — Open directly in a browser
```
Double-click index.html
```

### Option 2 — Local dev server (recommended — avoids font CORS issues)

Using VS Code + Live Server extension:
1. Open the `wedding-invitation/` folder in VS Code
2. Right-click `index.html` → **Open with Live Server**

Using Python (no install needed on macOS/Linux):
```bash
cd wedding-invitation
python3 -m http.server 3000
# then open http://localhost:3000
```

Using Node.js:
```bash
npx serve wedding-invitation
```

---

## Deploying the Website

### Option A — GitHub Pages (Free)
1. Push this folder to a GitHub repository
2. Go to **Settings → Pages**
3. Set source to `main` branch, `/ (root)` folder
4. Your site will be live at `https://yourusername.github.io/repo-name`

### Option B — Netlify (Free, easiest for sharing via WhatsApp)
1. Go to [netlify.com](https://netlify.com)
2. Drag the entire `wedding-invitation/` folder onto the Netlify dashboard
3. Your site is instantly live with a shareable URL

### Option C — Vercel
```bash
npx vercel wedding-invitation
```

### Option D — Any static hosting
Upload all four files (`index.html`, `styles.css`, `app.js`, `config.js`) to any web host.

---

## Configuration — `config.js`

All wedding details live in one place. Open `config.js` to update:

```js
const WEDDING_CONFIG = {
  couple: { partner1: "Sarah", partner2: "Amogelang" },
  date: {
    display: "17 October 2026",
    iso: "2026-10-17T13:00:00+02:00",   // ← Do NOT change this (used by countdown)
  },
  location: {
    city: "Ga-Rankuwa",
    // ▼▼▼ UPDATE THESE WHEN VENUE IS CONFIRMED ▼▼▼
    exactVenueName:    null,   // e.g. "The Grand Ballroom"
    exactVenueAddress: null,   // e.g. "123 Main Road, Ga-Rankuwa, 0208"
    googleMapsUrl:     null,   // e.g. "https://maps.google.com/?q=..."
  },
  rsvp: {
    useGoogleForm:      false,   // Set to true to activate Google Forms mode
    googleFormUrl:      null,    // Shareable Google Form link
    googleFormEmbedUrl: null,    // Embed URL from Google Form → Send → Embed
  },
};
```

---

## ➕ Adding the Venue

When the venue is confirmed, open `config.js` and update:

```js
exactVenueName:    "Your Venue Name",
exactVenueAddress: "Full street address, Ga-Rankuwa, 0208",
googleMapsUrl:     "https://maps.google.com/?q=Your+Venue+Name",
```

That's it — no other file needs to change.

---

## ➕ Connecting Google Forms (RSVP)

### Step 1 — Create your Google Form
1. Go to [forms.google.com](https://forms.google.com)
2. Create your RSVP form with the desired fields
3. Your Google Sheet will automatically collect responses via **Tools → Create spreadsheet**

### Step 2 — Get your form URLs

**For the link button (Option B — opens form in new tab):**
- In your Google Form, click **Send → Link icon** → Copy the URL
- Paste it into `config.js`:
  ```js
  googleFormUrl: "https://forms.gle/xxxxxxxxxxxxxxxx",
  ```

**For embedding the form directly (Option A — shows inside the website):**
- In your Google Form, click **Send → Embed icon** → Copy the `src=` URL from the `<iframe>` code
- Paste it into `config.js`:
  ```js
  googleFormEmbedUrl: "https://docs.google.com/forms/d/e/XXXXXXXX/viewform?embedded=true",
  ```

### Step 3 — Enable Google Forms mode
```js
useGoogleForm: true,
```

### Step 4 — Save and deploy
The demo form will automatically be hidden and replaced with the Google Form.

---

## Data Flow (Google Forms)

```
Guest visits website
        ↓
Clicks RSVP / fills embedded form
        ↓
Google Form (your form)
        ↓
Google Sheets (automatic)
        ↓
Your wedding guest list ✓
```

---

## Accessibility

- Semantic HTML (`<header>`, `<nav>`, `<main>`, `<section>`, `<footer>`, `<article>`)
- All form inputs have `<label>` elements
- ARIA attributes on interactive components
- Keyboard-navigable navigation (Escape closes menu)
- `aria-live` regions for countdown and form errors
- `prefers-reduced-motion` respected — no animations if the user has this set
- Focus-visible outlines on all interactive elements

---

## Browser Support

Works in all modern browsers (Chrome, Firefox, Safari, Edge).
Mobile-first responsive design — optimised for phones receiving the link via WhatsApp.

---

## Notes

- No JavaScript frameworks, build tools or npm packages required
- Fonts loaded from Google Fonts (Cormorant Garamond + Jost) — requires an internet connection
- For a fully offline version, download and self-host the font files
