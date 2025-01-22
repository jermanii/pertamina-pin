function redirectToDashboard() {
    $.ajax({
        url: url.getLinkPowerBi,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response && response.url) {
                window.open(response.url, '_blank');
            }
        },
        error: function () {
            console.log('Failed to retrieve dashboard URL from record.');
        }
    });
}

function downloadUserManual() {
    $.ajax({
        url: url.getDownloadUserManual,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response && response.url) {
                window.open(response.url, '_blank');
            }
        },
        error: function () {
            console.log('Failed to retrieve download User Manual.');
        }
    });
}