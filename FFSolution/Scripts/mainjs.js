
var AppGlobal = {
    "name": null,
    "id": null,
    "isAccounting": null,
    "isOperation": null,
    "isAdmin": null,
    "isCustomerService": null,
    "isSales": null,
    "isCustomClearance": null,
    "activeTranID": null,
    "activeTranStatusID": null
}


function addRequestVerificationToken(data) {
    data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
    return data;
};

var setValues = function () {
    $.ajax({
        url: '/Account/GetUserCredintionals',
        type: 'GET',
        dataType: 'json',
        cache: false,
        async: false,
        success: function (result) {
            AppGlobal['name'] = result.UserName;
            AppGlobal['isAccounting'] = result.IsAccounting;
            AppGlobal['isAdmin'] = result.IsAdministrator;
            AppGlobal['isCustomerService'] = result.IsCustomerService;
            AppGlobal['isOperation'] = result.IsOperation;
            AppGlobal['isSales'] = result.IsSales;
            AppGlobal['isCustomClearance'] = result.IsCustomClearance;
        },
        error: function (error) {
            console.warn('cannot get account details');
        }
    });
}();

// block UI
$(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);

$(document).ready(function () {

    $.unblockUI();



    $('#MyTable').DataTable({
    });

    $('#MyTable2').DataTable({
    });

    if (AppGlobal['isAdmin'] || AppGlobal['isAccounting']) {

        $('#TransTable').DataTable({
            //"processing": true,
            dom: 'T<"clear">lfrtip',
            tableTools: {
                "sSwfPath": "/Content/swf/copy_csv_xls_pdf.swf",
                "aButtons": [
                    "copy",
                    "csv",
                    "xls",
                    {
                        "sExtends": "pdf",
                        "sPdfOrientation": "landscape",
                        "sPdfMessage": "Your custom message would go here."
                    },
                    "print"
                ]
            }
        });
    } else //if (AppGlobal['isCustomerService'] || AppGlobal['isSales'] || AppGlobal['isOperation'])
    {
        $('#TransTable').DataTable({});
    }

    function transearchByBLNO(BLNO) {
        postData = { blnumber: BLNO };
        $.ajax({
            url: '/Tran/DetailsByBLNumber',
            type: 'GET',
            dataType: 'json',
            data: postData,
            // we set cache: false because GET requests are often cached by browsers
            // IE is particularly aggressive in that respect
            cache: false,
            success: function (result) {
                window.location.href = '/Tran/Details/' + result;
            },
            error: function (error) {
                var htmlForModal = 'Transaction with B/N Number : ' + blnumber + ' is not found, try another number !!!';

                $('#remodelDiv').html(htmlForModal);
                window.location.href = '#modal';
            }
        });
    }

    getNotifications();
});

var getNotifications = function () {
    $.ajax({
        type: "GET",
        async: true,
        url: '/Tran/GetPendingTrans'
        ,dataType: "json"
        , success: function (response) {
            if (response != null || response != 'undefined') {
                if (response.length > 0) {

                    $.each(response, function (index) {
                        $('#TranNotifications')
                                .append('<li><a href="/Tran/Details/' + response[index].Key + '"><span class="tab">' + response[index].Value + '</span></a></li>');
                    });
                    $('#NotificationCounter').text(response.length).show();
                    $('#NotificationButtonLI>a').css("color", "yellow");
                    $('#NotificationButtonLI').show();
                }
                else {
                    $('#NotificationButtonLI').hide();
                }
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.warn('error in the notification bubble : ');
            console.warn(thrownError);
        }
    })
}