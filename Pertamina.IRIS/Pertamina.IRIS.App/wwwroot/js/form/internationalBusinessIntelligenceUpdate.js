var ibiId = '';
var repeaterCounter = 0;
$(function () {

    document.querySelector('.app-root').dispatchEvent(new Event('closeSpinner'));
    ibiId = $("#ibiId").val();

    var date = $("#researchDate").val();
    document.getElementById('dateOfResearchInput').value = date;
    var authorsName = $("#authorsName").val();
    var authors = authorsName ? authorsName.split(',') : [];
    var isFirstRepeater = true;
    $('#addRepeaterAuthors').repeater({
        initEmpty: false,
        show: function () {
            $(this).slideDown();

            repeaterCounter++;
            $(this).find('input').each(function () {
                const uniqueId = `authorsInput-${repeaterCounter}`;
                $(this).attr('id', uniqueId);
            });

            if (isFirstRepeater) {
                $(this).find('[data-repeater-delete]').prop('disabled', false);
                isFirstRepeater = false;
            }
        },
        hide: function (deleteElement) {
            $(this).slideUp(deleteElement);
        }
    });

    authors.forEach(function (author, index) {
        var inputField = $('[data-repeater-list="AuthorName"] .authors').eq(index).find('input');
        if (inputField.length) {
            inputField.val(author);
        } else {
            var repeaterItem = $('<div>', { "data-repeater-item": "" })
                .addClass("authors mb-2")
                .append(
                    $('<div>', { "class": "form-group row" })
                        .append(
                            $('<div>', { "class": "col-md-10" })
                                .append(
                                    $('<input>', {
                                        type: 'text',
                                        value: author, 
                                        class: 'form-control',
                                        id: `authorsInput-${index}`,
                                        placeholder: 'Masukkan Authors'
                                    })
                                )
                        )
                        .append(
                            $('<div>', { "class": "col-md-2" })
                                .append(
                                    $('<button>', {
                                        class: 'border border-secondary btn btn-icon btn-flex btn-danger btn-sm',
                                        'data-repeater-delete': '',
                                        type: 'button'
                                    }).html('<i class="la la-close"></i>')
                                )
                        )
                );

            $('[data-repeater-list="AuthorName"]').append(repeaterItem);
        }
        repeaterCounter = index;
    });

    var countriesIds = $("#countriesIds").val();
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
    if (countriesIds) {
        var idsArray = countriesIds.split(',');

        $.ajax({
            url: url.ddlNegaraMitraWithoutKawasan + "?guid=" + countriesIds,
            method: 'GET',
            success: function (data) {
                if (data.items) {

                    var options = [];
                    $.each(data.items, function (index, item) {
                        var selectedCountry = item
                        if (idsArray.includes(item.id.toLowerCase())) {
                            options.push(new Option(item.text, item.id, true, true));
                        }
                    });

                    $('#hshDdlCountries').append(options).trigger('change');
                }
            }
        });
    }

    var streamBusinessId = $("#streamBusinessId").val();
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
    if (streamBusinessId) {
        $.ajax({
            url: url.ddlStreamBusiness + "?guid=" + streamBusinessId,
            method: 'GET',
            success: function (data) {
                if (data.items && data.items.length > 0) {
                    var selectedItem = data.items.find(x => x.id.toLowerCase() === streamBusinessId);

                    var option = new Option(selectedItem.text, selectedItem.id, true, true);
                    $('#ddlStream').append(option).trigger('change');
                }
            },
            error: function (xhr, status, error) {
                console.error("Error fetching selected item:", error);
            }
        });
    }

    var typeOfStudyId = $("#typeOfStudyId").val();
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
    if (typeOfStudyId) {
        $.ajax({
            url: url.ddlTypeOfStudy + "?guid=" + typeOfStudyId,
            method: 'GET',
            success: function (data) {
                if (data.items && data.items.length > 0) {
                    var selectedItem = data.items.find(x => x.id.toLowerCase() === typeOfStudyId);

                    var option = new Option(selectedItem.text, selectedItem.id, true, true);
                    $('#ddlTypeOfStudy').append(option).trigger('change');
                }
            },
            error: function (xhr, status, error) {
                console.error("Error fetching selected item:", error);
            }
        });
    }

    var entitasPertaminaId = $("#entitasPertaminaId").val();

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
    if (entitasPertaminaId) {
            $.ajax({
                url: url.ddlOwnerEntity + "?guid=" + entitasPertaminaId,
                method: 'GET',
                success: function (data) {
                    if (data.items && data.items.length > 0) {
                        var selectedItem = data.items.find(x => x.id.toLowerCase() === entitasPertaminaId);

                        var option = new Option(selectedItem.text, selectedItem.id, true, true);
                        $('#ddlOwnerEntity').append(option).trigger('change');
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error fetching selected item:", error);
                }
            });
    }

    var confidentialityId = $("#confidentialityId").val();
    function formatState(state) {
        if (!state.id) {
            return state.text;
        }

        var color = state.color ? state.color : $(state.element).data('color');
        var bullet = $(`<span></span>`).css({
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
        templateResult: formatState, 
        templateSelection: formatState,
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
    if (confidentialityId) {
            $.ajax({
                url: url.ddlConfidentiality + "?guid=" + confidentialityId,
                method: 'GET',
                success: function (data) {
                    if (data.items && data.items.length > 0) {
                        var selectedItem = data.items.find(x => x.id.toLowerCase() === confidentialityId);      

                        const newSpan = document.createElement("span");

                        var option = new Option(selectedItem.text, selectedItem.id, true, true);    
                        $(option).data('color', selectedItem.color); 
                        $('#ddlConfidentiality').append(option).trigger('change');
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error fetching selected item:", error);
                }
            });
    }
    const frmIBI = document.getElementById('UpdateIBI');
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

    function updateValidationInput(fileList) {
        if (fileList.length > 0) {
            fileValidationInput.value = 'valid';
        } else {
            fileValidationInput.value = '';
        }
    }

    const fileValidationInput = document.getElementById('fileValidation');
    const submitIBIButton = document.getElementById('btnSubmitIBI');
    submitIBIButton.addEventListener('click', function (e) {
        e.preventDefault();

        var callBackUrlPartial = $(this).data("callbackurl");

        //updateValidationInput(filesArray);
        //if (frmIBIValidator) {
        //    frmIBIValidator.validate().then(function (status) {

        //        if (status == 'Valid') {

        //        }
        //    });
        //}

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
    });

    function uploadDocument(urlUpload, urlSubmit, callBackUrlPartial) {
        const formData = new FormData();
        filesArray.forEach((file, index) => {
            formData.append('files', file);
        });

        fetch(`${urlUpload}?guid=${ibiId}`, {
            method: 'POST',
            body: formData
        }).then(response => response.json())
            .then(data => {
                var model = prepareSaving(data);
                updateIBI(urlSubmit, model, callBackUrlPartial);
            }).catch(error => {
                if (error.name === 'TypeError') {
                    alert("Network error or fetch operation failed");
                } else {
                    alert(`Upload failed: ${error.message}`);
                }
                console.error('Error:', error);
                document.querySelector('.app-root').dispatchEvent(new Event('closeSpinner'));
            });
    }

    function prepareSaving(docFileNameSystem) {
        var model = {
            Id: ibiId,
            Authors: [],
            CountriesCovered: [],
            Documents: []
        };
        model.Title = $("#titleInput").val();
        model.Description = $("#descriptionInput").val();
        $('.authors').each(function (index) {

            var authorName = $(this).find(`#authorsInput-${index}`).val();
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

    function updateIBI(url, params, callBackUrlPartial) {
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

    const dropzone = document.getElementById('dropzone');
    const fileList = document.getElementById('fileList');
    let filesArray = [];

    getFilesAsJson().then(x => {
        var arr = [];
        x.forEach(y => {
            arr.push(convertBase64ToFile(y));
        });
        addFiles(arr);
    });

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
            filesArray.push(file);
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

        fileList.querySelectorAll('button').forEach(button => {
            button.addEventListener('click', removeFile);
        });
    }

    function removeFile(event) {
        const index = event.target.getAttribute('data-index');
        filesArray.splice(index, 1); 
        renderFileList();
    }
});
async function getFilesAsJson() {
    var data = await $.ajax({
        type: "GET",
        url: `${urlGetFilesAsJson}?guid=${ibiId}`
    }).then(x => x);
    return data;
}

function convertBase64ToFile(objFile) {

    const fileName = objFile.FileName;
    const mimeType = objFile.MimeType;  
    const base64String = `data:${mimeType};base64,${objFile.Content}`;
    const base64Data = base64String.split(',')[1]; 

    const byteCharacters = atob(base64Data);
    const byteArrays = [];
    for (let offset = 0; offset < byteCharacters.length; offset += 1024) {
        const slice = byteCharacters.slice(offset, offset + 1024);
        const byteNumbers = new Array(slice.length);
        for (let i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }
        const byteArray = new Uint8Array(byteNumbers);
        byteArrays.push(byteArray);
    }

    const blob = new Blob(byteArrays, { type: mimeType });

    const file = new File([blob], fileName, { type: mimeType });

    return file;
}