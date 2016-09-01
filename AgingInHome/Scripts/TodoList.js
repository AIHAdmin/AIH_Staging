$(function () {
    $("#grid").jqGrid({
        url: "/AdminAnalytics/ManageMarkup",
        datatype: 'json',
        myType: 'Get',
        colNames: ['Id', 'Mark Up', "Is Default", "IsActive"],
        colModel: [
            { key: true, hidden: true, name: 'Id', index: 'Id', editable: true },
            { key: true, name: 'Markup1', index: 'Markup1', editable: true },
            { key: true, name: 'IsDefault', index: 'IsDefault', editable: true, formatter: 'select', stype: 'select', edittype: 'select', editoptions: { value: "false:No;true:Yes" } },
            { key: true, name: 'IsActive', index: 'IsActive', editable: true, formatter: 'select', stype: 'select', edittype: 'select', editoptions: { value: "false:No;true:Yes" } }
        ],
        pager: jQuery('#pager'),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        height: '100%',
        width: '60%',
        viewrecords: true,
        caption: 'Mark Up',
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
            url: '/AdminAnalytics/Edit',
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
            url: "/AdminAnalytics/CreateMarkup",
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
            url: "/AdminAnalytics/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete this task?",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        });
});