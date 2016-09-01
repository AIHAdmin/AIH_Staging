$(function () {
    $("select").selectpicker('refresh');

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

    //Hours Previous Button
    $('#btnPrevHours').click(function () {
        $('#tabservicesareas').trigger('click');
    });
    //channels Previous Button
    $('#btnPrevChannels').click(function () {
        $('#tabHours').trigger('click');
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
            $('#tabHours').trigger('click');
        else
            return flag;
    });
    //End Areas Validation


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

        var index = $(this).data('index');
        $(this).parent().remove();

        count = $(".catgoryserviceDiv").children("div").length;
        count = count + $(".customService").children("div").length

        for (var i = index; i < count ; i++) {
            $("#rmoveCustom_" + (i + 1)).data('index', i);
            $("#ServiceDetailsCustomeService_" + (i + 1)).attr('name', "ServiceDetails[" + i + "].CustomeService");
            $("#ServiceDetailsId_" + (i + 1)).attr('name', "ServiceDetails[" + i + "].Id");
            $("#ServiceDetailsServiceType_" + (i + 1)).attr('name', "ServiceDetails[" + i + "].ServiceType");
            $("#ServiceDetails_" + (i + 1) + "__ServicePrice").attr('name', "ServiceDetails[" + i + "].ServicePrice");
        }

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

        var index = $(this).data('index');
        $(this).parent().remove();
        var count = $("#serviceAreaDiv").children("div").length;

        for (var i = index; i < count ; i++) {
            $("#serviceArea_" + (i + 1)).data('index', i);
            $("#ServiceAreas_" + (i + 1) + "__City").attr('name', "ServiceAreas[" + i + "].City");
            $("#ServiceAreas_" + (i + 1) + "__StateId").attr('name', "ServiceAreas[" + i + "].StateId");
            $("#ServiceAreas_" + (i + 1) + "__ZipCode").attr('name', "ServiceAreas[" + i + "].ZipCode");
            $("#ServiceAreas_" + (i + 1) + "__ServiceRadius").attr('name', "ServiceAreas[" + i + "].ServiceRadius");
        }
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