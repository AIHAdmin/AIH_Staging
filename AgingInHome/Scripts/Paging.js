$(function () {
    $(".tblGrid").on('click', 'th', function () {
        
        if ($(this).data('isort') == true) {
            var sortOrder = $('#sortOrder').val();
            if (sortOrder == 'asc') {
                $('#sortOrder').val('desc');              
            }
            else {
                $('#sortOrder').val('asc');               
            }
            $('#sortCol').val($(this).data('sort'));
            Sorting();
        }
    });
});

//for arrow buttonsk
function Pagging(val) {    
    var sortCol = $("#sortCol").val() == undefined ? '' : $("#sortCol").val();
    var sortOrd = $("#sortOrder").val();

    var totalrecord = $("#hdnTotalRecord").val();
    var PageNumber = $("#currentPage").val();
    var PerPageRecord = $("#hdnPerPageRecord").val();
    var totalpage = $("#hdnTotalPage").val();
    var islast = false;
    if (val == '0') {
        if (parseInt(PageNumber, 10) != 1) {
            PageNumber = 1;
        }
        else
            PageNumber = 0;
    }
    else if (val == '-1') {
        PageNumber = parseInt(PageNumber, 10) - 1;
    }
    else if (val == '+1') {
        PageNumber = parseInt(PageNumber, 10) + 1;
    }
    else if (val == '2') {
        islast = true;
        if (parseInt(PageNumber, 10) == parseInt(totalpage, 10)) {
            PageNumber = parseInt(PageNumber, 10) + 1;
        }
        else
            PageNumber = parseInt(totalpage, 10);
    }

    if (parseInt(PageNumber, 10) > 0 && parseInt(totalpage, 10) >= PageNumber) {        
        loadPartialView(PageNumber, PerPageRecord, islast, sortOrd, sortCol);
    }

}

//for drop down
function Pagging1(perpage) {
    
    $("#hdnPerPageRecord").val(perpage);
    $('.fillter_popup').hide();
    var totalrecord = $("#hdnTotalRecord").val();
    var PageNumber = 1;
    var PerPageRecord = $("#hdnPerPageRecord").val();
    
    loadPartialView(PageNumber, PerPageRecord, false, 'desc', '');
    
}

//for pagenuber text box
function Pagging2(val) { 

    var sortCol = $("#sortCol").val() == undefined ? '' : $("#sortCol").val();
    var sortOrd = $("#sortOrder").val();

    var totalrecord = $("#hdnTotalRecord").val();
    var PageNumber = $("#currentPage").val();
    var PerPageRecord = $("#hdnPerPageRecord").val();
    var islast = false;
    if (val == '0') {
        PageNumber = 1;
    }
    else {
        PageNumber = (parseInt(PageNumber, 10));
    }
    loadPartialView(PageNumber, PerPageRecord, islast, sortOrd, sortCol);
}

//for searching
function SearchRecord() {
     
    Pagging1($("#hdnPerPageRecord").val());
}

//for sorting
function Sorting() {     
    //Paging;SS
    var sortCol = $("#sortCol").val() == undefined ? '' : $("#sortCol").val();
    var sortOrd = $("#sortOrder").val();

    var totalrecord = $("#hdnTotalRecord").val();
    var PageNumber = 1;
    var PerPageRecord = $("#hdnPerPageRecord").val();

    loadPartialView(PageNumber, PerPageRecord, false, sortOrd, sortCol);
}