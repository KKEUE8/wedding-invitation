/**
 * app.js — Sarah & Amogelang Wedding Invitation
 * Handles: navigation, countdown timer, scroll animations,
 *          RSVP form (demo + Google Forms routing), location/venue hydration.
 */

'use strict';

/* ================================================================
   INIT — wait for DOM ready
================================================================ */
document.addEventListener('DOMContentLoaded', () => {
  initNavigation();
  initCountdown();
  initScrollAnimations();
  initRsvp();
  initLocation();
});

/* ================================================================
   NAVIGATION
================================================================ */
function initNavigation() {
  const toggle  = document.getElementById('navToggle');
  const menu    = document.getElementById('navMenu');
  const navLinks = document.querySelectorAll('.nav-link');

  if (!toggle || !menu) return;

  // Toggle open/close
  toggle.addEventListener('click', () => {
    const isOpen = menu.classList.toggle('open');
    toggle.setAttribute('aria-expanded', String(isOpen));
  });

  // Close menu when a link is clicked (mobile)
  navLinks.forEach(link => {
    link.addEventListener('click', () => {
      menu.classList.remove('open');
      toggle.setAttribute('aria-expanded', 'false');
    });
  });

  // Close menu on outside click
  document.addEventListener('click', (e) => {
    if (menu.classList.contains('open') && !menu.contains(e.target) && !toggle.contains(e.target)) {
      menu.classList.remove('open');
      toggle.setAttribute('aria-expanded', 'false');
    }
  });

  // Close menu on Escape key
  document.addEventListener('keydown', (e) => {
    if (e.key === 'Escape' && menu.classList.contains('open')) {
      menu.classList.remove('open');
      toggle.setAttribute('aria-expanded', 'false');
      toggle.focus();
    }
  });

  // Highlight active nav link on scroll
  const sections = document.querySelectorAll('section[id], .hero[id]');
  const observerOptions = { rootMargin: '-30% 0px -60% 0px', threshold: 0 };

  const sectionObserver = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
      if (entry.isIntersecting) {
        navLinks.forEach(link => link.classList.remove('active'));
        const activeLink = document.querySelector(`.nav-link[href="#${entry.target.id}"]`);
        if (activeLink) activeLink.classList.add('active');
      }
    });
  }, observerOptions);

  sections.forEach(s => sectionObserver.observe(s));
}

/* ================================================================
   COUNTDOWN TIMER
================================================================ */
function initCountdown() {
  const targetDate = new Date(WEDDING_CONFIG.date.iso);

  const elDays    = document.getElementById('cd-days');
  const elHours   = document.getElementById('cd-hours');
  const elMinutes = document.getElementById('cd-minutes');
  const elSeconds = document.getElementById('cd-seconds');
  const elTimer   = document.getElementById('countdownTimer');
  const elMessage = document.getElementById('countdownMessage');

  if (!elDays || !elHours || !elMinutes || !elSeconds) return;

  function pad(n) { return String(n).padStart(2, '0'); }

  function tick() {
    const now  = new Date();
    const diff = targetDate - now;

    if (diff <= 0) {
      // Wedding day or past
      if (elTimer)   elTimer.hidden   = true;
      if (elMessage) {
        elMessage.hidden = false;
        elMessage.textContent = 'Today is the day! ❤️';
      }
      return; // stop ticking
    }

    const totalSeconds = Math.floor(diff / 1000);
    const days    = Math.floor(totalSeconds / 86400);
    const hours   = Math.floor((totalSeconds % 86400) / 3600);
    const minutes = Math.floor((totalSeconds % 3600) / 60);
    const seconds = totalSeconds % 60;

    elDays.textContent    = pad(days);
    elHours.textContent   = pad(hours);
    elMinutes.textContent = pad(minutes);
    elSeconds.textContent = pad(seconds);

    setTimeout(tick, 1000);
  }

  tick();
}

/* ================================================================
   SCROLL ANIMATIONS (Intersection Observer)
================================================================ */
function initScrollAnimations() {
  // Skip if user prefers reduced motion
  if (window.matchMedia('(prefers-reduced-motion: reduce)').matches) {
    document.querySelectorAll('.animate-on-scroll').forEach(el => {
      el.classList.add('in-view');
    });
    return;
  }

  const observer = new IntersectionObserver(
    (entries) => {
      entries.forEach(entry => {
        if (entry.isIntersecting) {
          entry.target.classList.add('in-view');
          observer.unobserve(entry.target); // animate once
        }
      });
    },
    { threshold: 0.12 }
  );

  document.querySelectorAll('.animate-on-scroll').forEach(el => observer.observe(el));
}

/* ================================================================
   RSVP
================================================================ */
function initRsvp() {
  const cfg = WEDDING_CONFIG.rsvp;

  if (cfg.useGoogleForm) {
    activateGoogleFormMode(cfg);
  } else {
    activateDemoFormMode();
  }
}

/* ---- Google Forms mode ---- */
function activateGoogleFormMode(cfg) {
  const demoWrapper   = document.getElementById('rsvpDemoMode');
  const googleWrapper = document.getElementById('rsvpGoogleMode');

  if (demoWrapper)   demoWrapper.hidden = true;
  if (googleWrapper) googleWrapper.hidden = false;

  // Option A: embed
  if (cfg.googleFormEmbedUrl) {
    const embedContainer = document.getElementById('rsvpEmbedContainer');
    if (embedContainer) {
      embedContainer.hidden = false;
      const iframe = document.createElement('iframe');
      iframe.src = cfg.googleFormEmbedUrl;
      iframe.title = 'RSVP Form';
      iframe.setAttribute('allowfullscreen', '');
      iframe.setAttribute('loading', 'lazy');
      embedContainer.appendChild(iframe);
    }
  }

  // Option B: link button
  if (cfg.googleFormUrl && !cfg.googleFormEmbedUrl) {
    const linkContainer = document.getElementById('rsvpLinkContainer');
    if (linkContainer) {
      linkContainer.hidden = false;
      const link = document.createElement('a');
      link.href = cfg.googleFormUrl;
      link.target = '_blank';
      link.rel = 'noopener noreferrer';
      link.className = 'btn btn--gold';
      link.textContent = 'RSVP NOW';
      link.setAttribute('aria-label', 'Open RSVP form (opens in new tab)');
      linkContainer.appendChild(link);
    }
  }
}

/* ---- Demo form mode ---- */
function activateDemoFormMode() {
  const form       = document.getElementById('rsvpForm');
  const successDiv = document.getElementById('rsvpSuccess');
  const resetBtn   = document.getElementById('rsvpResetBtn');

  if (!form) return;

  // Show/hide attending-only fields based on attendance choice
  const attendanceRadios  = form.querySelectorAll('input[name="attendance"]');
  const attendingFields   = document.getElementById('attendingFields');
  const attendingExtras   = document.getElementById('attendingFieldsExtra');

  function toggleAttendingFields() {
    const val = form.querySelector('input[name="attendance"]:checked')?.value;
    const show = val === 'yes';
    if (attendingFields)  attendingFields.style.display  = show ? '' : 'none';
    if (attendingExtras)  attendingExtras.style.display  = show ? '' : 'none';
  }

  // Fallback for browsers without CSS :has() support (older Safari / Firefox ESR)
  function updateRadioStyles() {
    form.querySelectorAll('.radio-option').forEach(opt => {
      const radio = opt.querySelector('input[type="radio"]');
      if (radio) opt.classList.toggle('radio-option--checked', radio.checked);
    });
  }

  attendanceRadios.forEach(r => r.addEventListener('change', () => {
    toggleAttendingFields();
    updateRadioStyles();
  }));
  toggleAttendingFields(); // run on load
  updateRadioStyles();

  // Guest count: toggle guest-names textarea visibility
  const guestCountSelect  = document.getElementById('guestCount');
  const guestNamesGroup   = document.getElementById('guestNamesGroup');

  function toggleGuestNames() {
    if (!guestCountSelect || !guestNamesGroup) return;
    const val = guestCountSelect.value;
    guestNamesGroup.style.display = (val === '1') ? 'none' : '';
  }

  if (guestCountSelect) guestCountSelect.addEventListener('change', toggleGuestNames);
  toggleGuestNames();

  // Form submission
  form.addEventListener('submit', (e) => {
    e.preventDefault();
    if (!validateRsvpForm(form)) return;

    const data = collectFormData(form);
    submitRsvp(data, form, successDiv);
  });

  // Reset button
  if (resetBtn) {
    resetBtn.addEventListener('click', () => {
      form.reset();
      toggleAttendingFields();
      toggleGuestNames();
      clearAllErrors(form);
      if (successDiv) successDiv.hidden = true;
      form.hidden = false;
      form.querySelector('[id]')?.focus();
    });
  }
}

/* ---- Validation ---- */
function validateRsvpForm(form) {
  let isValid = true;
  clearAllErrors(form);

  // Full name
  const name = form.querySelector('#guestName');
  if (name && !name.value.trim()) {
    showError('guestNameError', 'Please enter your full name.');
    isValid = false;
  }

  // Phone
  const phone = form.querySelector('#guestPhone');
  if (phone && !phone.value.trim()) {
    showError('guestPhoneError', 'Please enter your phone number.');
    isValid = false;
  }

  // Attendance
  const attendance = form.querySelector('input[name="attendance"]:checked');
  if (!attendance) {
    showError('attendanceError', 'Please let us know whether you can attend.');
    isValid = false;
  }

  if (!isValid) {
    // Scroll to first error
    const firstError = form.querySelector('.form-error:not(:empty)');
    if (firstError) firstError.scrollIntoView({ behavior: 'smooth', block: 'center' });
  }

  return isValid;
}

function showError(id, message) {
  const el = document.getElementById(id);
  if (el) el.textContent = message;
}

function clearAllErrors(form) {
  form.querySelectorAll('.form-error').forEach(el => (el.textContent = ''));
}

/* ---- Collect data ---- */
function collectFormData(form) {
  return {
    name:           form.querySelector('#guestName')?.value.trim()  || '',
    phone:          form.querySelector('#guestPhone')?.value.trim() || '',
    attendance:     form.querySelector('input[name="attendance"]:checked')?.value || '',
    guestCount:     form.querySelector('#guestCount')?.value        || '1',
    guestNames:     form.querySelector('#guestNames')?.value.trim() || '',
    dietary:        form.querySelector('#dietary')?.value.trim()    || '',
    accessibility:  form.querySelector('#accessibility')?.value.trim() || '',
    message:        form.querySelector('#message')?.value.trim()    || '',
    songRequest:    form.querySelector('#songRequest')?.value.trim() || '',
    submittedAt:    new Date().toISOString(),
  };
}

/* ---- Submit ---- */
function submitRsvp(data, form, successDiv) {
  /**
   * Demo mode: stores the response in localStorage and shows a thank-you.
   * Replace this function body with a real fetch() call to your backend,
   * or remove it entirely when switching to Google Forms mode.
   */
  try {
    const existing = JSON.parse(localStorage.getItem('wedding_rsvp_submissions') || '[]');
    existing.push(data);
    localStorage.setItem('wedding_rsvp_submissions', JSON.stringify(existing));
  } catch (_) {
    // localStorage not available — silently continue
  }

  // Show success message
  if (successDiv) {
    const msgEl = document.getElementById('rsvpSuccessMsg');
    if (msgEl) {
      if (data.attendance === 'yes') {
        msgEl.textContent =
          `Thank you, ${data.name}! We are so excited to celebrate with you on 17 October 2026. ` +
          `We will be in touch with full venue details once confirmed.`;
      } else {
        msgEl.textContent =
          `Thank you for letting us know, ${data.name}. ` +
          `You will be missed, but we appreciate you taking the time to respond.`;
      }
    }
    successDiv.hidden = false;
    successDiv.scrollIntoView({ behavior: 'smooth', block: 'center' });
  }

  form.hidden = true;
}

/* ================================================================
   LOCATION — hydrate venue details from config
================================================================ */
function initLocation() {
  const loc = WEDDING_CONFIG.location;

  const venueDetails  = document.getElementById('venueDetails');
  const venueName     = document.getElementById('venueName');
  const venueAddress  = document.getElementById('venueAddress');
  const locationNote  = document.getElementById('locationNote');
  const directionsBtn = document.getElementById('directionsBtn');
  const mapContainer  = document.getElementById('mapContainer');

  // Show venue name / address when provided
  if (loc.exactVenueName || loc.exactVenueAddress) {
    if (venueDetails) venueDetails.hidden = false;
    if (venueName    && loc.exactVenueName)    venueName.textContent    = loc.exactVenueName;
    if (venueAddress && loc.exactVenueAddress) venueAddress.textContent = loc.exactVenueAddress;
    if (locationNote) locationNote.hidden = true;
  }

  // Activate directions button when Google Maps URL is available
  if (loc.googleMapsUrl && directionsBtn) {
    directionsBtn.href = loc.googleMapsUrl;
    directionsBtn.target = '_blank';
    directionsBtn.rel = 'noopener noreferrer';
    directionsBtn.removeAttribute('aria-disabled');
    directionsBtn.setAttribute('aria-label', 'Get directions to the venue (opens in Google Maps)');
  }

  // Embed Google Maps iframe using a search query embed
  if (mapContainer) {
    const query = encodeURIComponent(
      (loc.exactVenueAddress || '') + ' ' + (loc.city || '') + ' ' + (loc.province || '')
    ).trim();
    const embedUrl = `https://maps.google.com/maps?q=${query}&output=embed`;
    mapContainer.innerHTML = '';
    const iframe = document.createElement('iframe');
    iframe.src = embedUrl;
    iframe.title = 'Venue location map';
    iframe.style.cssText = 'width:100%;height:100%;min-height:320px;border:none;border-radius:4px;';
    iframe.setAttribute('allowfullscreen', '');
    iframe.setAttribute('loading', 'lazy');
    iframe.setAttribute('referrerpolicy', 'no-referrer-when-downgrade');
    mapContainer.appendChild(iframe);
  }
}
