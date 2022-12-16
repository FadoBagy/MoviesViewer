const pageNumbersElements = document.querySelectorAll('.page-control nav ul li a');

const params = new Proxy(new URLSearchParams(window.location.search), {
	get: (searchParams, prop) => searchParams.get(prop),
});

let currentPageNumber = params.CurrentPage;
if (currentPageNumber <= 0) {
	currentPageNumber = 1;
}

if (currentPageNumber) {
	pageNumbersElements.forEach(element => {
		if (element.textContent == currentPageNumber) {
			element.classList.add('active-page-number');
		}
	});
}