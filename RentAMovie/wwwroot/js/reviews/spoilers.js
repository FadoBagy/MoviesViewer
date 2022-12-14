const reviewsContentElements = document.querySelectorAll('.review-content');

reviewsContentElements.forEach(e => {
    let currentSpoilersContainerEl = e.querySelector('.spoilers-container');

    if (currentSpoilersContainerEl) {
        let currentReviewTextEl = e.querySelector('.review-text');
        let currentSpoilerControls = e.querySelector('.spoiler-controls');
        let currentSpoilerControlsButton = e.querySelector('.spoiler-controls p:nth-child(2)');

        currentReviewTextEl.style.color = 'transparent';
        currentReviewTextEl.style.textShadow = '0 0 5px rgba(0, 0, 0, 0.5)';
        currentReviewTextEl.style.opacity = '80%';
        currentReviewTextEl.style.userSelect = 'none';

        currentSpoilerControlsButton.addEventListener('click', () => {
            currentSpoilerControls.style.display = 'none';

            currentReviewTextEl.style.color = 'inherit';
            currentReviewTextEl.style.textShadow = 'none';
            currentReviewTextEl.style.opacity = '100%';
            currentReviewTextEl.style.userSelect = 'inherit';
        });
    }
});