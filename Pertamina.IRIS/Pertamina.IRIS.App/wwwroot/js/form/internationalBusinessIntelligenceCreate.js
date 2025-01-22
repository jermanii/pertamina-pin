/*Bind data dropdown*/
$(function () {
    document.querySelector('.app-root').dispatchEvent(new Event('closeSpinner'));
    $('#ddlStream').select2({
        ajax: {
            url: url.ddlStreamBusiness,
            method: 'GET',
            data: function (params) {
                return {
                    query: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data.items
                };
            },
            minimumInputLength: 2
        }
    });

    $('#ddlTypeOfStudy').select2({
        ajax: {
            url: url.ddlTypeOfStudy,
            method: 'GET',
            data: function (params) {
                return {
                    query: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data.items
                };
            },
            minimumInputLength: 2
        }
    });

    $("#ddlOwnerEntity").select2({
        ajax: {
            url: url.ddlOwnerEntity,
            method: 'GET',
            data: function (params) {
                return {
                    query: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data.items
                };
            },
            minimumInputLength: 2
        }
    });


    $('#hshDdlCountries').select2({
        multiple: true,
        ajax: {
            url: url.ddlNegaraMitraWithoutKawasan,
            method: 'GET',
            data: function (params) {
                return {
                    query: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data.items
                };
            },
            minimumInputLength: 2
        }
    });

    function formatState(state) {
        if (!state.id) {
            return state.text;  
        }

        var color = state.color; 
        var bullet = $('<span></span>').css({
            'background-color': color,
            'border-radius': '50%',
            'display': 'inline-block',
            'height': '10px',
            'width': '10px',
            'margin-right': '8px'
        });

        return $('<span></span>').append(bullet).append(state.text);
    }

    $('#ddlConfidentiality').select2({
        templateResult: formatState, // Custom formatting for the results list
        templateSelection: formatState, // Custom formatting for the selected item
        ajax: {
            url: url.ddlConfidentiality,
            method: 'GET',
            data: function (params) {
                return {
                    query: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data.items
                };
            },
            minimumInputLength: 2
        }
    });

    /*Function Repeater*/
    var isFirstRepeater = true;
    $('#addRepeaterAuthors').repeater({
        initEmpty: false,

        show: function () {
            $(this).slideDown();
            if (isFirstRepeater) {
                $(this).find('[data-repeater-delete]').prop('disabled', false);
                isFirstRepeater = false;
            }
        },
        hide: function (deleteElement) {
            $(this).slideUp(deleteElement);
        }
    }).find('[data-repeater-delete]').css('display', 'none');

});

// Array to hold the selected files
document.addEventListener('DOMContentLoaded', function () {
    const dateInput = document.getElementById("dateOfResearchInput");

    // Hide date format initially
    if (!dateInput.value) {
        dateInput.style.color = "transparent";
    }

    // Toggle color based on input value
    dateInput.addEventListener("input", function () {
        if (this.value === "") {
            this.style.color = "transparent";
        } else {
            this.style.color = "black";
        }
    });

    // Ensure the placeholder is hidden when the input is not focused
    dateInput.addEventListener("focus", function () {
        this.style.color = "black";
    });

    dateInput.addEventListener("blur", function () {
        if (this.value === "") {
            this.style.color = "transparent";
        }
    });


    const dropzone = document.getElementById('dropzone');
    const fileList = document.getElementById('fileList');
    let filesArray = [];

    dropzone.addEventListener('dragover', function (e) {
        e.preventDefault();
        dropzone.classList.add('dragover');
    });

    dropzone.addEventListener('dragleave', function () {
        dropzone.classList.remove('dragover');
    });

    dropzone.addEventListener('drop', function (e) {
        e.preventDefault();
        dropzone.classList.remove('dragover');
        const files = e.dataTransfer.files;
        addFiles(files);
    });

    dropzone.addEventListener('click', function () {
        const fileInput = document.createElement('input');
        fileInput.type = 'file';
        fileInput.multiple = true;
        fileInput.click();
        fileInput.addEventListener('change', function () {
            addFiles(fileInput.files);
        });
    });

    function addFiles(files) {
        for (const file of files) {
            filesArray.push(file);  // Add files to the array
            renderFileList();
        }
    }

    function renderFileList() {
        fileList.innerHTML = '';
        filesArray.forEach((file, index) => {
            const listItem = document.createElement('div');
            listItem.className = 'file-item';
            listItem.innerHTML = `
                                <span>${file.name} (${(file.size / 1024).toFixed(2)} KB)</span>
                                <button type="button" data-index="${index}">Remove</button>
                            `;
            fileList.appendChild(listItem);
        });

        // Add event listeners to remove buttons
        fileList.querySelectorAll('button').forEach(button => {
            button.addEventListener('click', removeFile);
        });
    }

    function removeFile(event) {
        const index = event.target.getAttribute('data-index');
        filesArray.splice(index, 1);  // Remove file from the array
        renderFileList();
    }

    const fileValidationInput = document.getElementById('fileValidation');
    const frmIBI = document.getElementById('AddIBI');
    var frmIBIValidator = FormValidation.formValidation(
        frmIBI,
        {
            fields: {
                'Title': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'Description': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'Author.Name': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'CountriesCoveredSelected': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'TypeStudyId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'NegaraMitraId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'StreamBusinessId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'ResearchDate': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'ConfidentialityId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'fileValidation': {
                    validators: {
                        notEmpty: {
                            message: 'You must upload at least one file'
                        }
                    }
                }

            },

            plugins: {
                trigger: new FormValidation.plugins.Trigger(),
                bootstrap: new FormValidation.plugins.Bootstrap5({
                    rowSelector: '.fv-row',
                    eleInvalidClass: '',
                    eleValidClass: ''
                })
            }
        }
    ); 
    const submitIBIButton = document.getElementById('btnSubmitIBI');
    submitIBIButton.addEventListener('click', function (e) {
        e.preventDefault();

        var callBackUrlPartial = $(this).data("callbackurl");

        updateValidationInput(filesArray);
        if (frmIBIValidator) {
            frmIBIValidator.validate().then(function (status) {

                if (status == 'Valid') {
                    Swal.fire({
                        text: "Are you sure want to submit this data ?",
                        icon: "info",
                        buttonsStyling: false,
                        showCancelButton: true,
                        confirmButtonText: "Yes, submit it!",
                        cancelButtonText: 'Nope, cancel it',
                        customClass: {
                            confirmButton: "btn btn-primary",
                            cancelButton: 'btn btn-danger'
                        }
                    }).then((result) => {
                        if (result.isConfirmed) {
                            document.querySelector('.app-root').dispatchEvent(new Event('loadSpinner'));
                            submitIBIButton.setAttribute('data-kt-indicator', 'on');
                            submitIBIButton.disabled = true;
                            var urlSubmit = submitIBIButton.dataset.url;
                            var urlUpload = submitIBIButton.dataset.urlupload; 
                            uploadDocument(urlUpload, urlSubmit, callBackUrlPartial); 
                        };
                    });
                }
            });
        }
    });

    function updateValidationInput(fileList) {
        if (fileList.length > 0) {
            fileValidationInput.value = 'valid'; // or some meaningful value
        } else {
            fileValidationInput.value = '';
        }
    }

    function uploadDocument(urlUpload, urlSubmit, callBackUrlPartial) {
        const formData = new FormData();
        filesArray.forEach((file, index) => {
            formData.append('files', file);
        });

        // Send formData to the server 
        fetch(urlUpload, {
            method: 'POST',
            body: formData
        }).then(response => response.json())
            .then(data => {
                /*alert('Files uploaded successfully!');*/
                var model = prepareSaving(data);
                saveIBI(urlSubmit, model, callBackUrlPartial);
                console.log(data);
            }).catch(error => {
                if (error.name === 'TypeError') {
                    // Handle network errors or fetch-related issues
                    alert("Network error or fetch operation failed");
                } else {
                    // Handle all other errors, including non-JSON responses
                    alert(`Upload failed: ${error.message}`);
                }
                document.querySelector('.app-root').dispatchEvent(new Event('loadSpinner'));
                console.error('Error:', error);
            });
    }

    function prepareSaving(docFileNameSystem) {
        var model = {
            Authors: [],
            CountriesCovered: [],
            Documents: []
        };
        model.Title = $("#titleInput").val();
        model.Description = $("#descriptionInput").val();
        $('.authors').each(function () {

            var authorName = $(this).find('#authorsInput').val();
            var objAuthor = {
                Name: authorName,
            };
            model.Authors.push(objAuthor);
        });
        model.CountriesCoveredSelected = $("#hshDdlCountries").val();

        model.TypeStudyId = $("#ddlTypeOfStudy").val();

        model.EntitasPertaminaId = $("#ddlOwnerEntity").val();

        model.StreamBusinessId = $("#ddlStream").val();

        model.ResearchDate = $("#dateOfResearchInput").val();

        model.ConfidentialityId = $("#ddlConfidentiality").val();
        model.Documents = docFileNameSystem;

        return model;
        
    }
    function saveIBI(url, params, callBackUrlPartial) {
        document.querySelector('.app-root').dispatchEvent(new Event('closeSpinner'));
        $.ajax({
            type: "POST",
            url: url,
            data: params,
            success: function (data) {
                if (data.IsError) {
                    submitIBIButton.removeAttribute('data-kt-indicator');
                    submitIBIButton.disabled = false;
                    Swal.fire({
                        text: data.ErrorMessage,
                        icon: 'error',
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary"
                        }
                    });
                }
                else {
                    submitIBIButton.removeAttribute('data-kt-indicator');
                    submitIBIButton.disabled = false;
                    Swal.fire({
                        text: "Form has been successfully submitted!",
                        icon: "success",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary",

                        }
                    }).then((result) => {
                        document.location = callBackUrlPartial;
                    });;
                }
            },
            error: function () {
                alert("Error while inserting data");
            }
        });
    }



});










