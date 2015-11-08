function ValidateEmail(mail) {

    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(mail)) {
        return (true)
    }
    return (false)
}

function ValidateText(text) {

    if ((/^[A-Za-z\u00C0-\u017F]+$/.test(text))) {
        return (true)
    }
    return (false)
}

function ValidateCreditCard(cc) {

    if ((/^(?:3[47][0-9]{13})$/.test(cc))) {
        return (true)
    }
    return (false)
}

function ValidateCVC(cvc) {

    if ((/^[0-9]{3,4}$/.test(cvc))) {
        return (true)
    }
    return (false)
}


$(document)
.on('click', 'form button[type=submit]', function (e) {
    var isValid = $(e.target).parents('form').isValid();
    if (!isValid) {
        e.preventDefault(); //prevent the default action
    }
});


