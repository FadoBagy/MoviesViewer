const reviewTextElements = document.querySelectorAll('.review-text');
const reviewTextProfileElements = document.querySelector('.review-text-profile');

reviewTextElements.forEach(element => {
    let elementText = element.textContent.trim();
    if (elementText.length > 510) {
        element.textContent = elementText.substring(0, 510) + ' ...';
    }
});

if (reviewTextProfileElements) {
    let elementText = reviewTextProfileElements.textContent.trim();
    if (elementText.length > 180) {
        reviewTextProfileElements.textContent = elementText.substring(0, 180) + ' ...'
    }
}