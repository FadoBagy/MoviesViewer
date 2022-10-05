const infoBtnEl = document.querySelectorAll('.card .info');
const darkEl = document.getElementById('dark');

for (var infoBtn of infoBtnEl) {
    infoBtn.addEventListener('click', (e) => {
        if (darkEl.style.display != 'block') {
            ShowInfoBoard();
        } else {
            HideInfoBoard();
        }
    });
}

function ShowInfoBoard() {
    darkEl.style.display = 'block';
    $("body").css("overflow", "hidden");
}

function HideInfoBoard() {
    darkEl.style.display = 'none';
    $("body").css("overflow", "auto");
}
