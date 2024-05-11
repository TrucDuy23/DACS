var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('#btnAddNew').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);

            var username = $('#m_username').val();
            var name = $('#m_name').val();
            var password = $('#m_password').val();
            var address = $('#m_address').val();
            var email = $('#m_email').val();
            var phone = $('#m_phone').val();

            if (username === "" || name === "" || password === "") {
                bootbox.alert("Chưa nhập thông tin cần thiết");
                return;
            }

            $.ajax({
                url: "/Admin/User/AddUserAjax",
                data: {
                    username: username,
                    name: name,
                    password: password,
                    address: address,
                    email: email,
                    phone: phone
                },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status) {
                        bootbox.alert({
                            message: "Thêm tài khoản thành công!",
                            size: 'medium',
                            closeButton: false
                        });
                        // Xóa dữ liệu đã nhập sau khi thêm thành công
                        $('#m_username').val("");
                        $('#m_name').val("");
                        $('#m_password').val("");
                        $('#m_address').val("");
                        $('#m_email').val("");
                        $('#m_phone').val("");
                        // Chuyển hướng về trang quản lý người dùng
                        window.location.href = "/Admin/User";
                    } else {
                        bootbox.alert("Thêm tài khoản thất bại");
                    }
                },
                error: function (xhr, status, error) {
                    bootbox.alert("Lỗi: " + xhr.responseText);
                }
            });
        });



        $('.abclaice').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);

            var listid = btn.data("listid");
            var name = btn.data('name');
            var address = btn.data('address');
            var email = btn.data('email');
            var phone = btn.data('phone');

            var valuename = document.getElementById(name);
            var valueaddress = document.getElementById(address);
            var valueemail = document.getElementById(email);
            var valuephone = document.getElementById(phone);
            

            $.ajax({
                url: "/Admin/User/UpdateUserAjax",
                data:
                    {
                        userid: listid,
                        name: valuename.value,                      
                        address: valueaddress.value,
                        email: valueemail.value,
                        phone : valuephone.value
                    },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        bootbox.alert({
                            message: "Cập nhật thành công!",
                            size: 'medium',
                            closeButton: false
                        });

                        window.location.href = "/Admin/User";
                    }
                    else {
                        bootbox.alert(
                            {
                                message: "Cập nhật tài khoản lỗi",
                                closeButton: false
                            }
                           );
                    }
                }
            });
        });
    }
}
user.init();