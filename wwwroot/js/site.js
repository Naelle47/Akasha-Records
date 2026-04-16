// =====================
// Gestion du thème clair/sombre
// Persistance via localStorage
// =====================
const root = document.documentElement;
const toggleBtn = document.getElementById('theme-toggle');
const storedTheme = localStorage.getItem('theme');

if (storedTheme === 'dark') root.classList.add('theme-dark');

if (toggleBtn) {
    toggleBtn.addEventListener('click', () => {
        const isDark = root.classList.toggle('theme-dark');
        localStorage.setItem('theme', isDark ? 'dark' : 'light');
    });
}

// =====================
// Modale "À propos"
// Fermeture via bouton, clic sur le backdrop ou touche Escape
// =====================
const aboutModal = document.getElementById('about-modal');
const aboutOpen = document.getElementById('about-open');
const aboutClose = document.getElementById('about-close');

function openAbout() {
    aboutModal.classList.add('show');
    aboutModal.setAttribute('aria-hidden', 'false');
}

function closeAbout() {
    aboutModal.classList.remove('show');
    aboutModal.setAttribute('aria-hidden', 'true');
}

if (aboutOpen) aboutOpen.addEventListener('click', openAbout);
if (aboutClose) aboutClose.addEventListener('click', closeAbout);

if (aboutModal) {
    aboutModal.addEventListener('click', (e) => {
        if (e.target === aboutModal || e.target.classList.contains('modal-backdrop')) {
            closeAbout();
        }
    });
}

// Fermeture au clavier (accessibilité)
document.addEventListener('keydown', (e) => {
    if (e.key === 'Escape') closeAbout();
});