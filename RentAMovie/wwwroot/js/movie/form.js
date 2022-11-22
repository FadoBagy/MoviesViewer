const inputs = document.querySelectorAll('.form-group input');
const textarea = document.querySelector('.form-group textarea');
const submitBtn = document.querySelector('.form-group input[type="submit"]');

let isSubmited = false;
submitBtn.addEventListener('click', (e) => {
    isSubmited = true;
});

window.onbeforeunload = () => {
    let isEmpty = true;
    for (const input of inputs) {
        if (input.value != 'Submit' && input.value != 'Create') {
            if (input.value != '') {
                isEmpty = false;
            }
        }
    }
    if (textarea.value != '') {
        isEmpty = false;
    }

    if (!isEmpty && !isSubmited) {
        return "Do you want to exit this page?";
    }
};