$(document).ready(function () {
    $('#contactEmailInput').on('blur', function () {
        var email = $(this).val();
        var regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;



        if (!regex.test(email)) {
            $('#emailError').text('Lütfen geçerli bir e-posta adresi girin.');
            $('#emailError').css('padding', '3px 15px');
        } else {
            $('#emailError').text('');
            $('#emailError').css('padding', '0px');
        }
    });
});
