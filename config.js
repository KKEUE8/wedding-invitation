/**
 * ============================================================
 *  WEDDING CONFIGURATION
 *  Update this file to change wedding details across the site.
 * ============================================================
 */
const WEDDING_CONFIG = {
  couple: {
    partner1: "Sarah",
    partner2: "Amogelang",
  },

  date: {
    display: "17 October 2026",
    numeric: "17 · 10 · 2026",
    /** ISO 8601 — South African Standard Time (UTC+2) */
    iso: "2026-10-17T14:00:00+02:00",
  },

  time: {
    display: "From 2:00 PM",
    hour: 14,
  },

  location: {
    city: "Ga-Rankuwa",
    province: "South Africa",

    // ----------------------------------------------------------------
    // TODO: Replace the placeholders below once the venue is confirmed.
    // ----------------------------------------------------------------
    exactVenueName: "70 Phase 8",
    exactVenueAddress: "70 Phase 8, Ga-Rankuwa, 0208",
    googleMapsUrl: "https://maps.app.goo.gl/Hp4k9rDwx1xyfQqH9",
  },

  dressCode: "Traditional Attire",

  rsvp: {
    /**
     * RSVP / Google Forms integration
     * -----------------------------------------------
     * Option A — embed the form directly inside the page:
     *   Set googleFormEmbedUrl to your Google Form's embed URL.
     *   (Google Form → Send → Embed → copy the src URL)
     *
     * Option B — link guests to the form in a new tab:
     *   Set googleFormUrl to the shareable Google Form link.
     *
     * Set useGoogleForm: true to activate Google Forms mode.
     * Set useGoogleForm: false to use the built-in demo form.
     * -----------------------------------------------
     */
    useGoogleForm: true,
    googleFormUrl: "https://docs.google.com/forms/d/e/1FAIpQLSe5ah7d0xP9VktzBPi4JpWlbMKuqffF3iqtoMUciIjPU8GENA/viewform?usp=header",
    googleFormEmbedUrl: "https://docs.google.com/forms/d/e/1FAIpQLSe5ah7d0xP9VktzBPi4JpWlbMKuqffF3iqtoMUciIjPU8GENA/viewform?embedded=true",
  },
};
