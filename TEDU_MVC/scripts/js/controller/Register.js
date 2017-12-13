var contact = {
    init: function () {
        contact.registerEvents();
    },
    registerEvents: function () {
        $('#btnSend').off('click').on('click', function () {
            var name = $('#inputName').val();
            var email = $('#inputEmail').val();
            var password = $('#inputPasswordConfirm').val();
            var phone = $('#inputPhone').val();
            var address = $('#inputAddress').val();

            $.ajax({
                url: '/User/Send',
                type: 'POST',
                dataType: 'json',
                data: {
                    name: name,
                    email: email,
                    password: password,
                    phone: phone,
                    address: address
                   

                   
                },
                success: function (res) {
                    if (res.status === true) {
                        window.alert('Tạo Tài Khoản Thành Công');
                        contact.resetForm();
                    }
                }
            });
        });
    },
    resetForm: function () {
        $('#inputName').val('');
        $('#inputEmail').val('');
        $('#inputPasswordConfirm').val('');
        $('#inputPhone').val('');
        $('#inputAddress').val('');
    }
}
contact.init();