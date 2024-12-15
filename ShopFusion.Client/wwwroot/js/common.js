window.ShowToastrNotification = (type, message) => {
    if (type == "success") {
        toastr.success(message, 'Success', { timeOut: 5000 });
    }
    if (type == "error") {
        toastr.error(message, 'Error', { timeOut: 5000 });
    }
}

window.ShowSweetAlertNotification = (icon, title, message) => {
    swal.fire({ icon: icon, title: title, text: message});
}

function ShowDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('show');
}

function HideDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('hide');
}