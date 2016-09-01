$(function () {
    $("#frmconsumerregistration").submit(function (e) {
        e.preventDefault();
        if (isFormValid) {
            $("#btnSubmit").hide();
            $(".spanloaderInbox").show();

            $.ajax({
                url: $_UrlConsumerRegistration,
                data: $(this).serialize(),
                method: "post",
                async:true,
                success: function (result) {
                    window.location = $_UrlConsumerDashboard;
                }
            });
        }
    });
    $("#Email").change(function () {
        $.ajax({
            url: $_UrlCheckEmailAvailablity,
            data: { Email: $("#Email").val() },
            success: function (result) {
                if (result == "exists") {
                    $("#spanEmail").closest(".form-group").addClass("has-error");
                    $("#spanEmail").html("Email already exists.");
                    isFormValid = false;
                    isEmailAvailable = false;
                }
                else {
                    isEmailAvailable = true;
                }
            }
        });
    });

    $("#Password").change(function () {
        if (new RegExp("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").test($("#Password").val())) {
            $("#spanPassword").html("");
            $("#spanPassword").closest(".form-group").removeClass("has-error");
        }
    });
    });