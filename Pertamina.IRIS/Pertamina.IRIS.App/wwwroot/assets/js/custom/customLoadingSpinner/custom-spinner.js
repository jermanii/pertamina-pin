document.querySelector('.app-root').addEventListener('closeSpinner', function () {
    document.documentElement.scrollTop = 0;
    document.querySelector('.spinner-container').style.display = 'none';
});

document.querySelector('.app-root').addEventListener('loadSpinner', function () {
    document.querySelector('.spinner-container').style.display = 'block';
});
