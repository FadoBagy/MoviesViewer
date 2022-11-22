const genresInputEl = document.getElementById('Genres');
const genreEls = document.querySelectorAll('.genre-item');
const clearEl = document.getElementById('clear');

let genres = [];
if (genresInputEl.value != '') {
    let genreNames = genresInputEl.value.split(', ');
    genreNames.forEach(genreName => {
        genreEls.forEach(genre => {
            if (genre.firstElementChild.textContent == genreName) {
                genre.classList.add('active');
                genres.push(' ' + genre.firstElementChild.textContent);
            }
        });
    });
}

for (const genre of genreEls) {
    genre.addEventListener('click', () => {
        if (!genre.classList.contains('active')) {
            if (SelectLimitCheck()) {
                genre.classList.add('active');
                genres.push(' ' + genre.firstElementChild.textContent);
            }
        } else {
            genre.classList.remove('active');
            genres = genres.filter(word => word != ' ' + genre.firstElementChild.textContent);
        }
        genresInputEl.value = genres;
    });
}

function SelectLimitCheck() {
    let index = 0;

    for (const genre of genreEls) {
        if (genre.classList.contains('active')) {
            index++;
            if (index == 5) {
                return false;
            }
        }
    }
    return true;
}

clearEl.addEventListener('click', () => {
    genresInputEl.value = '';
    genres = [];
    for (const genre of genreEls) {
        genre.classList.remove('active');
    }
});