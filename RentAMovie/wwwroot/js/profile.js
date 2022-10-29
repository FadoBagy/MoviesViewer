const actionBtnEl = document.querySelector('.header-button-section p');
const formEl = document.getElementById('profile-form');
const formSubmitBtnEl = document.getElementById('update-profile-button');
const changeEls = document.querySelectorAll('.change-link');

if (actionBtnEl.textContent == 'Edit') {
    formSubmitBtnEl.style.display = 'none';

    let index = 1;
    for (const child of formEl.children) {
        if (child.classList.contains('form-floating') && index > 3) {
            child.children[0].setAttribute('disabled', '');
        }
        index++;
    }
    for (const link of changeEls) {
        link.style.display = 'none';
    }
}

actionBtnEl.addEventListener('click', (e) => {
    if (e.target.textContent == 'Edit') {
        formSubmitBtnEl.style.display = 'block';
        actionBtnEl.textContent = 'Cancel';

        let index = 1;
        for (const child of formEl.children) {
            if (child.classList.contains('form-floating') && index > 3) {
                child.children[0].removeAttribute('disabled');
            }
            index++;
        }
        for (const link of changeEls) {
            link.style.display = 'block';
        }
    } else {
        formSubmitBtnEl.style.display = 'none';
        actionBtnEl.textContent = 'Edit';

        let index = 1;
        for (const child of formEl.children) {
            if (child.classList.contains('form-floating') && index > 1) {
                child.children[0].setAttribute('disabled', '');
            }
            index++;
        }
        for (const link of changeEls) {
            link.style.display = 'none';
        }
    }
});