$(function () {


    //Previous Button

    //service previous button
    $('#btnPrevServices').click(function () {
        $('#tabCompany').trigger('click');
    });
    //Service area previous button
    $('#btnPrevServicesAreas').click(function () {
        $('#tabService').trigger('click');
    });
    //Account Previous Button
    $('#btnPrevAccount').click(function () {
        $('#tabservicesareas').trigger('click');
    });
    //Account Previous Button
    $('#btnPrevAccount').click(function () {
        $('#tabservicesareas').trigger('click');
    });
    //Hours Previous Button
    $('#btnPrevHours').click(function () {
        $('#tabAccount').trigger('click');
    });
    //channels Previous Button
    $('#btnPrevChannels').click(function () {
        $('#tabHours').trigger('click');
    });
    //channels Previous Button
    $('#btnPrevAboutUs').click(function () {
        $('#tabChannels').trigger('click');
    });

    //End Previous Button

    //Client Side validation

    function isValidEmailAddress(emailAddress) {
        var pattern = new RegExp("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$");
        return pattern.test(emailAddress);
    };

    function isValidPassword(password) {
        var pattern = new RegExp("^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@/#$%&/=?_.,:;\\-]).+$");
        return pattern.test(password);
    };

    function IsProviderAlreadyExists(email) {
        var isEmailExists = false;
        //setInterval(function () {
        //    $.ajax({
        //        url: UserEmailAlreadyExistUrl,
        //        data: { PrimaryEmail: countlist },
        //        success: function (result) {
        //            isEmailExists = result;
        //        }
        //    })
        //},5000);
        alert(isEmailExists);
        return isEmailExists;
    }

    //Company validation
    $('#btnCompany').click(function () {

        var flag = true;
        //company
        if ($("#CompanyName").val() == '') {
            $('#spanCompanyName').html('Company Name is required');
            flag = false;
        }
        else {
            $('#spanCompanyName').html('');
        }
        //Provider Address
        if ($("#ProviderAddress").val() == '') {
            $('#spanProviderAddress').html('Provider Address is required');
            flag = false;
        }
        else {
            $('#spanProviderAddress').html('');
        }
        //City
        if ($("#City").val() == '') {
            $('#spanCity').html('City is required');
            flag = false;
        }
        else {
            $('#spanCity').html('');
        }
        //State
        if ($("#StateId option:selected").val() == '') {
            $('#spanStateId').html('State is required');
            flag = false;
        }
        else {
            $('#spanStateId').html('');
        }
        //City
        if ($("#Zipcode").val() == '') {
            $('#spanZipcode').html('Zip code is required');
            flag = false;
        }
        else {
            $('#spanZipcode').html('');
        }
        //Provider Contact Number
        if ($("#ProviderContactNumber").val() == '') {
            $('#spanProviderContactNumber').html('Contact Number is required');
            flag = false;
        }
        else {
            $('#spanProviderContactNumber').html('');
        }
        if (flag)
            $('#tabService').trigger('click');
        else
            return flag;

    });
    //End Company validation

    //Services Validation
    $('#btnservices').click(function () {

        var flag = true;
        //Category
        if ($("#CategoryId option:selected").val() == '') {
            $('#spanCategoryId').html('Select Service');
            flag = false;
        }
        else {
            $('#spanCategoryId').html('');
        }
        //Experience
        if ($("#Experience option:selected").val() == '') {
            $('#spanExperience').html('Experience is required');
            flag = false;
        }
        else {
            $('#spanExperience').html('');
        }

        if (flag)
            $('#tabservicesareas').trigger('click');
        else
            return flag;
    });
    //End Services Validation

    //Service Areas Validation
    $('#btnServicesAreas').click(function () {

        var flag = true;
        //City
        if ($("#ServiceAreas_0__City").val() == '') {
            $('#spanServiceCity').html('City is required.');
            flag = false;
        }
        else {
            $('#spanServiceCity').html('');
        }
        //State
        if ($("#ServiceAreas_0__StateId option:selected").val() == '') {
            $('#spanServiceStateId').html('State is required.');
            flag = false;
        }
        else {
            $('#spanServiceStateId').html('');
        }
        //ZipCode
        if ($("#ServiceAreas_0__ZipCode").val() == '') {
            $('#spanServiceZipCode').html('ZipCode is required.');
            flag = false;
        }
        else {
            $('#spanServiceZipCode').html('');
        }
        //Redius
        if ($("#ServiceAreas_0__ServiceRadius option:selected").val() == '') {
            $('#spanServiceRadius').html('Radius is required');
            flag = false;
        }
        else {
            $('#spanServiceRadius').html('');
        }

        if (flag)
            $('#tabAccount').trigger('click');
        else
            return flag;
    });
    //End Areas Validation

    //Account Validation
    $('#btnAccount').click(function (e) {
        var flag = true;

        if ($("#PrimaryEmail").val() == '') {
            $('#spanPrimaryEmail').html('Primary Email is required.');
            flag = false;
        }
        else if (!isValidEmailAddress($("#PrimaryEmail").val())) {
            $('#spanPrimaryEmail').html('Primary Email is not valid.');
            flag = false;
        }
        else {
            $('#spanPrimaryEmail').html('');
        }

        //Password
        if ($("#Password").val() == '') {
            $('#spanPassword').html('Password is required.');
            flag = false;
        }
        else if (!isValidPassword($("#Password").val())) {
            $('#spanPassword').html('Your Password must have one capital letter one number and one special character.');
            flag = false;
        }
        else {
            $('#spanPassword').html('');
        }

        //Confirm Password
        if ($("#ConfirmPassword").val() == '') {
            $('#spanConfirmPassword').html('Confirm Password is required.');
            flag = false;
        }
        else if ($("#Password").val() != $("#ConfirmPassword").val()) {
            $('#spanConfirmPassword').html('Password did not match.');
            flag = false;
        }
        else {
            $('#spanConfirmPassword').html('');
        }

        if (flag)
            $('#tabHours').trigger('click');
        else
            return flag;




    });
    //End Account Validation

    //Hours Validation
    $('#btnHours').click(function () {
        var flag = true;
        //Monday Start Time
        if ($("#hourOfOperation_MonStart option:selected").val() == '') {
            $('#spanMonStart').html('Monday start time is required.');
            flag = false;
        }
        else {
            $('#spanMonStart').html('');
        }
        //Monday End Time
        if ($("#hourOfOperation_MonEnd option:selected").val() == '') {
            $('#spanMonEnd').html('Monday end time is required.');
            flag = false;
        }
        else {
            $('#spanMonEnd').html('');
        }

        if (flag)
            $('#tabChannels').trigger('click');
        else
            return flag;
    });
    //End Hours Validation

    //Channels Validation
    $('#btnChannels').click(function () {
        $('#tabAboutUs').trigger('click');
    });
    //EndChannels Validation

    //About Us Validation
    $('#btnAboutUs').click(function () {
        var flag = true;
        
        $('#WhoWeAre').text(encodeURIComponent($("._DivsubmitWhoweAre").find(".Editor-editor").html()));
        $('#WhyWeDo').text(encodeURIComponent($("._DivsubmitWhyWeDo").find(".Editor-editor").html()));
        $('#WhatWeDo').text(encodeURIComponent($("._DivsubmitWhatWeDo").find(".Editor-editor").html()));


        //WhoWeAre
        if ($("#WhoWeAre").val() == '') {
            $('#spanWhoWeAre').html('Who We Are is required.');
            flag = false;
        }
        else {
            $('#spanWhoWeAre').html('');
        }

        //WhyWeDo
        if ($("#WhyWeDo").val() == '') {
            $('#spanWhyWeDo').html('Why Choose Us is required.');
            flag = false;
        }
        else {
            $('#spanWhyWeDo').html('');
        }

        //WhyWeDo
        if ($("#WhatWeDo").val() == '') {
            $('#spanWhatWeDo').html('Our Mission is required.');
            flag = false;
        }
        else {
            $('#spanWhatWeDo').html('');
        }
        return flag;
    });
    //End About Us Validation

    //End Client Side validation







    $('._liveSearch').attr("data-live-search", true).height(203);
    setInterval(function () { $('._liveSearch .dropdown-menu').addClass('contentScroll mCustomScrollbar'); $("select").selectpicker('refresh'); }, 5000);

    $("[name='CategoryId']").change(function () {
        var catid = $(this).val();

        $.ajax({
            url: LoadCategoryServiceUrl,
            data: { categoryId: catid },
            success: function (partialview) {
                $(".catgoryserviceDiv").empty();
                $(".catgoryserviceDiv").append(partialview);
                $("select").selectpicker('refresh');
            }
        })
        //
    });



    $("select").selectpicker('refresh');
    //$('#ProviderContactNumber').mask('(000)000-0000')
    if ($('#hdnCityError').val() != '' && $('#hdnCityError').val() != undefined) {
        $('#CategoryId').val('');
        alert($('#hdnCityError').val());
    }

    $('.big-search').addClass('hidden-xs');
    $('header').addClass('brd-btm');

    //Time validation for monday
    $("#hourOfOperation_MonEnd").change(function () {
        var EndValMon = $('option:selected', this).text();
        var hrEnd = EndValMon.substring(EndValMon.length - 2, EndValMon.length);
        var EndTime = EndValMon.match(/\d+/);

        var startValMon = $('#hourOfOperation_MonStart :selected').text();
        var hrstartMon = startValMon.substring(startValMon.length - 2, startValMon.length);
        var numberstartMon = startValMon.match(/\d+/);
        if (hrstartMon == 'AM' && hrEnd == 'AM' || hrstartMon == 'PM' && hrEnd == 'PM') {
            if (parseInt(EndTime) < parseInt(numberstartMon)) {
                alert('Close time should be greater than start time');
                $('#hourOfOperation_MonEnd').val('');
            }
        }
        if (hrstartMon == 'PM' && hrEnd == 'AM') {
            alert('Close time should be greater than start time');
            $('#hourOfOperation_MonEnd').val('');
        }

    });

    //Time validation for tuesday
    $("#hourOfOperation_TueEnd").change(function () {
        var EndValTue = $('option:selected', this).text();
        var hrEnd = EndValTue.substring(EndValTue.length - 2, EndValTue.length);
        var EndTime = EndValTue.match(/\d+/);

        var startValTue = $('#hourOfOperation_TueStart :selected').text();
        var hrstartTue = startValTue.substring(startValTue.length - 2, startValTue.length);
        var numberstartTue = startValTue.match(/\d+/);
        if (hrstartTue == 'AM' && hrEnd == 'AM' || hrstartTue == 'PM' && hrEnd == 'PM') {
            if (parseInt(EndTime) < parseInt(numberstartTue)) {
                alert('Close time should be greater than start time');
                $('#hourOfOperation_MonEnd').val('');
            }
        }
        if (hrstartTue == 'PM' && hrEnd == 'AM') {
            alert('Close time should be greater than start time');
            $('#hourOfOperation_TueEnd').val('');
        }

    });

    //Time validation for wednesday
    $("#hourOfOperation_WedEnd").change(function () {
        var EndValWed = $('option:selected', this).text();
        var hrEnd = EndValWed.substring(EndValWed.length - 2, EndValWed.length);
        var EndTime = EndValWed.match(/\d+/);

        var startValWed = $('#hourOfOperation_WedStart :selected').text();
        var hrstartWed = startValWed.substring(startValWed.length - 2, startValWed.length);
        var numberstartWed = startValWed.match(/\d+/);
        if (hrstartWed == 'AM' && hrEnd == 'AM' || hrstartWed == 'PM' && hrEnd == 'PM') {
            if (parseInt(EndTime) < parseInt(numberstartWed)) {
                alert('Close time should be greater than start time');
                $('#hourOfOperation_MonEnd').val('');
            }
        }
        if (hrstartWed == 'PM' && hrEnd == 'AM') {
            alert('Close time should be greater than start time');
            $('#hourOfOperation_WedEnd').val('');
        }

    });

    //Time validation for thursday
    $("#hourOfOperation_ThuEnd").change(function () {
        var EndValThu = $('option:selected', this).text();
        var hrEnd = EndValThu.substring(EndValThu.length - 2, EndValThu.length);
        var EndTime = EndValThu.match(/\d+/);

        var startValThu = $('#hourOfOperation_ThuStart :selected').text();
        var hrstartThu = startValThu.substring(startValThu.length - 2, startValThu.length);
        var numberstartThu = startValThu.match(/\d+/);
        if (hrstartThu == 'AM' && hrEnd == 'AM' || hrstartThu == 'PM' && hrEnd == 'PM') {
            if (parseInt(EndTime) < parseInt(numberstartThu)) {
                alert('Close time should be greater than start time');
                $('#hourOfOperation_MonEnd').val('');
            }
        }
        if (hrstartThu == 'PM' && hrEnd == 'AM') {
            alert('Close time should be greater than start time');
            $('#hourOfOperation_MonEnd').val('');
        }

    });

    //Time validation for friday
    $("#hourOfOperation_FriEnd").change(function () {
        var EndValFri = $('option:selected', this).text();
        var hrEnd = EndValFri.substring(EndValFri.length - 2, EndValFri.length);
        var EndTime = EndValFri.match(/\d+/);

        var startValFri = $('#hourOfOperation_FriStart :selected').text();
        var hrstartFri = startValFri.substring(startValFri.length - 2, startValFri.length);
        var numberstartFri = startValFri.match(/\d+/);
        if (hrstartFri == 'AM' && hrEnd == 'AM' || hrstartFri == 'PM' && hrEnd == 'PM') {
            if (parseInt(EndTime) < parseInt(numberstartFri)) {
                alert('Close time should be greater than start time');
                $('#hourOfOperation_FriEnd').val('');
            }
        }
        if (hrstartFri == 'PM' && hrEnd == 'AM') {
            alert('Close time should be greater than start time');
            $('#hourOfOperation_FriEnd').val('');
        }

    });

    //Time validation for saturday
    $("#hourOfOperation_SatEnd").change(function () {
        var EndValSat = $('option:selected', this).text();
        var hrEnd = EndValSat.substring(EndValSat.length - 2, EndValSat.length);
        var EndTime = EndValSat.match(/\d+/);

        var startValSat = $('#hourOfOperation_SatStart :selected').text();
        var hrstartSat = startValSat.substring(startValSat.length - 2, startValSat.length);
        var numberstartSat = startValSat.match(/\d+/);
        if (hrstartSat == 'AM' && hrEnd == 'AM' || hrstartSat == 'PM' && hrEnd == 'PM') {
            if (parseInt(EndTime) < parseInt(numberstartSat)) {
                alert('Close time should be greater than start time');
                $('#hourOfOperation_SatEnd').val('');
            }
        }
        if (hrstartSat == 'PM' && hrEnd == 'AM') {
            alert('Close time should be greater than start time');
            $('#hourOfOperation_SatEnd').val('');
        }

    });

    //Time validation for sunday
    $("#hourOfOperation_SunEnd").change(function () {
        var EndValSun = $('option:selected', this).text();
        var hrEnd = EndValSun.substring(EndValSun.length - 2, EndValSun.length);
        var EndTime = EndValSun.match(/\d+/);

        var startValSun = $('#hourOfOperation_SunStart :selected').text();
        var hrstartSun = startValSun.substring(startValSun.length - 2, startValSun.length);
        var numberstartSun = startValSun.match(/\d+/);
        if (hrstartSun == 'AM' && hrEnd == 'AM' || hrstartSun == 'PM' && hrEnd == 'PM') {
            if (parseInt(EndTime) < parseInt(numberstartSun)) {
                alert('Close time should be greater than start time');
                $('#hourOfOperation_SunEnd').val('');
            }
        }
        if (hrstartSun == 'PM' && hrEnd == 'AM') {
            alert('Close time should be greater than start time');
            $('#hourOfOperation_SunEnd').val('');
        }

    });

    var size;
    if (parseInt($("[name='Zipcode']").val()) == 0) {
        $("[name='Zipcode']").val("");
    }

    //$("#ProviderContactNumber").mask("(000) 000-0000");
    var countlist = 0;
    $(".fa-plus-square-o").hide();
    // $("table").hide();
    $(".fa-plus-square-o").click(function () {
        $(".fa-minus-square-o").show();
        $(".table").show();
        $(this).hide();
    });
    $(".fa-minus-square-o").click(function () {
        $(".fa-plus-square-o").show();
        $(".table").hide();
        $(this).hide();
    });

    $("._customServiceclick").click(function (e) {
        countlist = $(".catgoryserviceDiv").children("div").length;
        countlist = countlist + $(".customService").children("div").length;
        e.preventDefault();
        $.ajax({
            url: LoadCustomServiceUrl,
            data: { count: countlist },
            success: function (partialView) {
                $(".customService").append(partialView);
                $("select").selectpicker('refresh');
                $('._addNewCustomer').last().find('._textcustSer').focus();
            }
        })
    });
    $(document).on("click", ".customeremove", function () {
        $(this).parent().remove();
    });

    $("#AddOther").click(function (e) {
        //countlist = countlist + 1;
        var count = $("#serviceAreaDiv").children("div").length;
        e.preventDefault();
        $.ajax({
            url: ServiceAreaPartialUrl,
            data: { count: count },
            success: function (partialView) {
                $("#serviceAreaDiv").append(partialView);
                $("select").selectpicker('refresh');
                $('._AddNewProvideritem').last().find('._textServiceCity').focus();
            }
        })
    });
    $(document).on("click", ".customeOther", function () {
        $(this).parent().remove();
    });
    $("#file").change(function () {
        //if ($(this)[0].files[0].size > 528385) {
        //    alert("Image Size should not be greater than 500Kb");
        //    return false;
        //}
        if ($(this)[0].files[0].type.indexOf("image") == -1) {
            alert("Invalid Type");
            return false;
        }
        var reader = new FileReader();
        reader.onload = function () {
        }
        size = parseFloat($(this)[0].files[0].size / 1024).toFixed(2);

    });

    ///*****User for Image croping*****///

    YUI().use('node', 'crop-box', function (Y) {
        var options =
        {
            imageBox: '.imageBox',
            thumbBox: '.thumbBox',
            spinner: '.spinner',
            imgSrc: ''
        }
        var cropper;
        Y.one('#file').on('change', function () {

            var reader = new FileReader();

            if (size < 4000) {
                reader.onloadend = function (e) {
                    options.imgSrc = e.target.result;

                    cropper = new Y.CropBox(options);
                }
                reader.readAsDataURL(this.get('files')._nodes[0]);

                this.get('files')._nodes = [];
                $("#myModal").modal({ backdrop: false, keyboard: false });

                $("#myModal").modal('show');
            }
            else {
                alert("your current image size is " + size + " KB.your image should be less than 4000 KB")
            }
        })
        Y.one('#btnCrop').on('click', function (e) {
            var img = cropper.getAvatar();
            $("#Bitimagestring").val(img);
            $('#getImgPreview').attr("src", img);
            $("#myModal").modal('hide');
            //Y.one('.cropped').append('<img src="' + img + '">');
        })
        Y.one('#btnZoomIn').on('click', function () {
            cropper.zoomIn();
        })
        Y.one('#btnZoomOut').on('click', function () {
            cropper.zoomOut();
        })
    })


    YUI.add('crop-box', function (Y) {
        Y.CropBox = Y.Base.create('crop-box', Y.Base, [],
            {
                initializer: function (options) {
                    this.options = options;
                    this.state = {};
                    this.render();
                },
                render: function () {
                    var self = this;
                    this.imageBox = Y.one(this.options.imageBox);
                    this.thumbBox = this.imageBox.one(this.options.thumbBox);
                    this.spinner = this.imageBox.one(this.options.spinner);

                    this.initObject();
                    return this;
                },
                initObject: function () {
                    var self = this;
                    this.spinner.show();

                    this.image = new Image();
                    this.image.onload = function () {
                        self.spinner.hide();
                        self.setBackground();

                        //event handler
                        self.imageBox.on('mousedown', self.imgMouseDown, self);
                        self.imageBox.on('mousemove', self.imgMouseMove, self);
                        self.mouseup = Y.one('body').on('mouseup', self.imgMouseUp, self);

                        Y.UA.gecko > 0 ?
                            self.imageBox.on('DOMMouseScroll', self.zoomImage, self) :
                            self.imageBox.on('mousewheel', self.zoomImage, self);
                    };
                    this.image.src = this.options.imgSrc;
                },
                setBackground: function () {
                    if (!this.ratio) this.ratio = 1;

                    var w = parseInt(this.image.width) * this.ratio;
                    var h = parseInt(this.image.height) * this.ratio;

                    var pw = (this.imageBox.get('clientWidth') - w) / 2;
                    var ph = (this.imageBox.get('clientHeight') - h) / 2;

                    this.imageBox.setAttribute('style',
                        'background-image: url(' + this.image.src + '); ' +
                        'background-size: ' + w + 'px ' + h + 'px; ' +
                        'background-position: ' + pw + 'px ' + ph + 'px; ' +
                        'background-repeat: no-repeat');
                },
                imgMouseDown: function (e) {
                    e.stopImmediatePropagation();
                    this.state.dragable = true;
                    this.state.mouseX = e.clientX;
                    this.state.mouseY = e.clientY;
                },
                imgMouseMove: function (e) {
                    e.stopImmediatePropagation();
                    if (this.state.dragable) {
                        var x = e.clientX - this.state.mouseX;
                        var y = e.clientY - this.state.mouseY;

                        var bg = this.imageBox.getStyle('backgroundPosition').split(' ');

                        var bgX = x + parseInt(bg[0]);
                        var bgY = y + parseInt(bg[1]);

                        this.imageBox.setStyle('backgroundPosition', bgX + 'px ' + bgY + 'px');

                        this.state.mouseX = e.clientX;
                        this.state.mouseY = e.clientY;
                    }
                },
                imgMouseUp: function (e) {
                    e.stopImmediatePropagation();
                    this.state.dragable = false;
                },
                zoomImage: function (e) {
                    e.wheelDelta > 0 ? this.ratio *= 1.1 : this.ratio *= 0.9;
                    this.setBackground();
                },
                getAvatar: function () {
                    var self = this,
                        width = this.thumbBox.get('clientWidth'),
                        height = this.thumbBox.get('clientHeight'),
                        canvas = document.createElement("canvas"),
                        dim = this.imageBox.getStyle('backgroundPosition').split(' '),
                        size = this.imageBox.getStyle('backgroundSize').split(' '),
                        dx = parseInt(dim[0]) - this.imageBox.get('clientWidth') / 2 + width / 2,
                        dy = parseInt(dim[1]) - this.imageBox.get('clientHeight') / 2 + height / 2,
                        dw = parseInt(size[0]);
                    dh = parseInt(size[1]);
                    sh = parseInt(this.image.height);
                    sw = parseInt(this.image.width);

                    canvas.width = width;
                    canvas.height = height;
                    var context = canvas.getContext("2d");
                    context.drawImage(this.image, 0, 0, sw, sh, dx, dy, dw, dh);
                    var imageData = canvas.toDataURL('image/jpeg');

                    return imageData;
                },
                zoomIn: function () {
                    this.ratio *= 1.1;
                    this.setBackground();
                },
                zoomOut: function () {
                    this.ratio *= 0.9;
                    this.setBackground();
                },
                destructor: function () {
                    if (this.mouseup) this.mouseup.detach()
                }
            });
    }, '1.0',
    {
        requires: ['node', 'base']
    });



    $("body").on("keydown", ".numriconly", function (e) {

        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });



});