window.addEventListener('load', () => {
    const redAlert = document.querySelector('.red-alert');
    const greenAlert = document.querySelector('.green-alert');
    const redAlertBtn = document.querySelector('.red-alert img');
    const greenAlertBtn = document.querySelector('.green-alert img');

    if (redAlert) {
        redAlertBtn.addEventListener('click', () => {
            redAlert.style.transform = 'translateY(-300%)';
        });
    }
    if (greenAlert) {
        greenAlertBtn.addEventListener('click', () => {
            greenAlert.style.transform = 'translateY(-300%)';
        });
    }
});
