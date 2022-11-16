let header = document.querySelector('#navbar-header');
let nav = document.querySelector('#navbar-header nav');
let container = document.querySelector('.container');

if (window.screenY > 0) {
    onSrcollActions();
};

window.addEventListener('scroll', () => {
    onSrcollActions();
});

function onSrcollActions() {
    header.classList.toggle('sticky', window.scrollY > 0);
    nav.classList.toggle('no-shadow', window.scrollY > 0);
    container.classList.toggle('maring-top', window.scrollY > 0);
}