const resetBtn = document.querySelector('.search-btns input[value="Reset"]');
const searchBar = document.querySelector('.search input[type="text"]');
const filters = document.querySelectorAll('.filter-section .form-select');

resetBtn.addEventListener('click', (e) => {
    e.preventDefault();

    searchBar.value = '';
    filters.forEach(filter => {
        filter.value = 'Select';
    });
});