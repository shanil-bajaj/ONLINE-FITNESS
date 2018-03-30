
// Convert first character capital
function capFirst(oTextBox) {
    oTextBox.value = oTextBox.value[0].toUpperCase() + oTextBox.value.substring(1);
}

//
function RestrictSpaceSpecial(e) {
    try {
        if (window.event) {
            var charCode = window.event.keyCode;
        }
        else if (e) {
            var charCode = e.which;
        }
        else { return true; }
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
    catch (err) {
        alert(err.Description);
    }
}
