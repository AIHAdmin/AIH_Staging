var resltData = "";

function getAllSelectOptions() {
        $.ajax({
            type: 'Get',
            url: '/ProviderListing/GetProviderListing',
            dataType: 'json',
            async:false,
            success: function (data) {
                var items = data.result;
                var x = "";
                for (var i = 0; i < (items.length - 1) ; i++) {
                    x += items[i].ProviderListingId + ":" + items[i].CompanyName + ";";
                }
                resltData = x.slice(1, -1);
            },
            error: function (ex) {
                var r = jQuery.parseJSON(response.responseText);
                alert("Message: " + r.Message);
                alert("StackTrace: " + r.StackTrace);
                alert("ExceptionType: " + r.ExceptionType);
            }
        });
        return resltData;
    }

$(function () {

    var parmData = getAllSelectOptions();
    bindGrid(parmData);
});


function bindGrid(parmData) {
        $("#grid").jqGrid({
            url: "/AdminAnalytics/ManageProviderAnalytic",
            datatype: 'json',
            myType: 'Get',
            colNames: ['Id', 'Provider', 'ProfileID', 'ViewId', 'IsActive'],
            colModel: [
                { key: true, hidden: true, name: 'Id', index: 'Id', editable: true },
                { key: true, name: 'ProviderListingId', index: 'ProviderListingId', editable: true, formatter: 'select', stype: 'select', edittype: 'select', editoptions: { value: parmData } },
                { key: true, name: 'ProfileID', index: 'ProfileID', editable: true },
                { key: true, name: 'ViewID', index: 'ViewID', editable: true },
                { key: true, name: 'IsActive', index: 'IsActive', editable: true, formatter: 'select', stype: 'select', edittype: 'select', editoptions: { value: "false:No;true:Yes"} },
            ],
            pager: jQuery('#pager'),
            rowNum: 10,
            rowList: [10, 20, 30, 40],
            height: '100%',
            width: '60%',
            viewrecords: true,
            caption: 'Provider Analytics Listing',
            emptyrecords: 'No records to display',
            jsonReader: {
                root: "Data",
                page: "page",
                total: "total",
                records: "records",
                repeatitems: false,
                Id: "0"
            },
            autowidth: true,
            multiselect: false
        }).navGrid('#pager', { edit: true, add: true, del: true, search: false, refresh: true },
                {
                    // edit options
                    zIndex: 100,
                    url: '/AdminAnalytics/EditProviderAnalytics',
                    closeOnEscape: true,
                    closeAfterEdit: true,
                    recreateForm: true,
                    afterComplete: function (response) {
                        if (response.responseText) {
                            alert(response.responseText);
                        }
                    }
                },
                {
                    // add options
                    zIndex: 100,
                    url: "/AdminAnalytics/AddProviderAnalytics",
                    closeOnEscape: true,
                    closeAfterAdd: true,
                    afterComplete: function (response) {
                        if (response.responseText) {
                            alert(response.responseText);
                        }
                    }
                },
                {
                    // delete options
                    zIndex: 100,
                    url: "/AdminAnalytics/DeleteProviderAnalytics",
                    closeOnEscape: true,
                    closeAfterDelete: true,
                    recreateForm: true,
                    msg: "Are you sure you want to delete this task?",
                    afterComplete: function (response) {
                        if (response.responseText) {
                            alert(response.responseText);
                        }
                    }
                })
    }

