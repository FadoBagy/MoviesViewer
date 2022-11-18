let nav = document.querySelector('#navbar-header');

if (window.pageYOffset > 0) {
    onSrcollActions();
};

window.addEventListener('scroll', () => {
    onSrcollActions();
});

function onSrcollActions() {
    nav.classList.toggle('sticky', window.scrollY > 0);
    nav.classList.toggle('no-shadow', window.scrollY > 0);
};