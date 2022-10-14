const biographyPartsEl = document.querySelector('.parts');
const biographyEl = document.querySelector('.biography');

if (biographyPartsEl.clientHeight > 300) {
    biographyPartsEl.style.height = '300px';

    let moreBtn = document.createElement('p');
    moreBtn.textContent = 'Read More';
    biographyEl.appendChild(moreBtn);
}

const readMoreEl = document.querySelector('.biography p');
let isExpanded = false;
readMoreEl.addEventListener('click', (e) => {
    if (!isExpanded) {
        isExpanded = true;
        biographyPartsEl.style.overflow = 'visible';
        biographyPartsEl.style.height = 'fit-content';
    } else {
        isExpanded = false;
        biographyPartsEl.style.overflow = 'hidden';
        biographyPartsEl.style.height = '300px';
    }
})