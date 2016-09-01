$(document).ready(function () {

    var navListItems = $('div.stepwizard div a'),
            allWells = $('.step-content'),
            allNextBtn = $('.nextBtn'),
            allPrevBtn = $('.prevBtn');

    allWells.hide();

    navListItems.click(function (e) {
        e.preventDefault();
        var $target = $($(this).attr('href')),
                $item = $(this);

        if (!$item.hasClass('disabled')) {
            navListItems.removeClass('active');
            $item.addClass('active').addClass('complete');
            allWells.hide();
            $target.show();
            $target.find('input:eq(0)').focus();
        }
    });

    allNextBtn.click(function () {
        var curStep = $(this).closest(".step-content"),
            curStepBtn = curStep.attr("id"),
            nextStepWizard = $('div.stepwizard div a[href="#' + curStepBtn + '"]').parent().next().children("a"),
            curInputs = curStep.find("input[type='text'],input[type='url'],select,input[type=password]").not("[name=AlterPhone]"),
            isValid = true;
        isFormValid = true;
        $(".form-group").removeClass("has-error");
        for (var i = 0; i < curInputs.length; i++) {
            $("#span" + $(curInputs[i]).attr("name")).html("");
                if ($(curInputs[i]).val() == "") {
                    isValid = false;
                    isFormValid = false;
                    $("#span" + $(curInputs[i]).attr("name")).html("*"+ $(curInputs[i]).attr("errorkey") + " is required.");
                    $(curInputs[i]).closest(".form-group").addClass("has-error");
                }

                if ($(curInputs[i]).val() != "" && $(curInputs[i]).attr("name") == "Email") {
                    if (!new RegExp("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$").test($(curInputs[i]).val())) {
                        isValid = false;
                        isFormValid = false;
                        $("#span" + $(curInputs[i]).attr("name")).html("Please enter valid email.");
                        $(curInputs[i]).closest(".form-group").addClass("has-error");
                    }
                    if ($("#Email").val() != "") {
                        $.ajax({
                            url: $_UrlCheckEmailAvailablity,
                            data: { Email: $("#Email").val() },
                            async:true,
                            success: function (result) {
                                if (result == "exists") {
                                    $("#spanEmail").closest(".form-group").addClass("has-error");
                                    $("#spanEmail").html("Email already exists.");
                                    isValid = false;
                                    isFormValid = false;
                                }
                            }
                        });
                    }
                  
                }

                if ($("#Password").val() != "" && $(curInputs[i]).attr("name") == "ConfirmPassword") {
                    if (!new RegExp("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").test($("#Password").val())) {
                        $("#spanPassword").html("Your Password must have one capital letter one number and one special character with min. 8 characters.");
                        $("#Password").closest(".form-group").addClass("has-error");
                    }
                    else if ($(curInputs[i]).val()!="" && $(curInputs[i]).val() != $("#Password").val()) {
                        isValid = false;
                        isFormValid = false;
                        $("#span" + $(curInputs[i]).attr("name")).html("The password and confirmation password do not match.");
                        $(curInputs[i]).closest(".form-group").addClass("has-error");
                    }
                }

            if (!curInputs[i].validity.valid){
                isValid = false;
                isFormValid = false;
                $(curInputs[i]).closest(".form-group").addClass("has-error");
            }

        }

        if (isValid && isFormValid && $('form').valid())
            nextStepWizard.removeAttr('disabled').trigger('click');
    });

    allPrevBtn.click(function(){
        var curStep = $(this).closest(".step-content"),
            curStepBtn = curStep.attr("id"),
            prevStepWizard = $('div.stepwizard div a[href="#' + curStepBtn + '"]').parent().prev().children("a");

        $(".form-group").removeClass("has-error");
        prevStepWizard.removeAttr('disabled').trigger('click');
    });

    $('div.stepwizard div a.active').trigger('click');
});