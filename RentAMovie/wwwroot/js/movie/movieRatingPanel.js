const ratingPanelEl = document.querySelector('.rating-panel');
const bodyEl = document.body;

let panelIsShown = false;
let hasScrollbar = window.innerWidth > document.documentElement.clientWidth;

document.querySelector('.user-score img').addEventListener('click', () => {
    if (panelIsShown) {
        HideRatingPanel();
    } else {
        ShowRatingPanel();
    }
});
function ShowRatingPanel() {
    bodyEl.style.overflow = 'hidden';
    if (hasScrollbar) {
        bodyEl.style.paddingRight = '15px'
    }
    ratingPanelEl.style.display = 'flex'
    ratingPanelEl.style.top = `${window.scrollY}px`;
    ratingPanelEl.classList.toggle('visible');

    ratingPanelEl.addEventListener('click', RatingPaneClickHandler);
    panelIsShown = true;
};
function HideRatingPanel() {
    ratingPanelEl.classList.toggle('visible');
    setTimeout(() => {
        bodyEl.style.overflow = 'auto';
        if (hasScrollbar) {
            bodyEl.style.paddingRight = '0';
        }
        ratingPanelEl.style.display = 'none';
        RatingClear();
    }, 310);

    ratingPanelEl.removeEventListener('click', RatingPaneClickHandler);
    panelIsShown = false;
};

function RatingPaneClickHandler(e) {
    let clickedEl = e.target;
    CheckIfClickedOutside(clickedEl);
    StarsColoring(clickedEl);
    RatingDisplayChanger();
};
function CheckIfClickedOutside(clickedEl) {
    if (clickedEl.classList.contains('rating-panel')) {
        HideRatingPanel();
    }
}
function StarsColoring(clickedEl) {
    if (clickedEl.classList.contains('fa-star')) {
        RemoveActiveStarClassed();
        clickedEl.parentElement.classList.add('activeStar');
    }
}
function RemoveActiveStarClassed() {
    let starsEls = ratingPanelEl.querySelector('.stars').childNodes;
    starsEls.forEach(el => {
        if (el.tagName == 'LABEL') {
            if (el.classList.contains('activeStar')) {
                el.classList.remove('activeStar');
            }
        }
    });
}

function RatingDisplayChanger() {
    //Check if user had rated
    let ratingNumberEl = ratingPanelEl.querySelector('h5');
    let selectedRatingNumber = ratingPanelEl.querySelector('input[type="radio"]:checked');

    if (selectedRatingNumber) {
        ratingNumberEl.textContent = `${selectedRatingNumber.value}/10`;
    }
}
function RatingClear() {
    let ratingNumberEl = ratingPanelEl.querySelector('h5');
    let selectedRatingNumber = ratingPanelEl.querySelector('input[type="radio"]:checked');

    ratingNumberEl.textContent = '0/10';
    selectedRatingNumber.checked = false;
    RemoveActiveStarClassed();
}