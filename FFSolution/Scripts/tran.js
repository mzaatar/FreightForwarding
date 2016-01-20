$(document).ready(function () {
    $("#ETA").focusout(function () {
        valTran();
    });


    $("#BookingDate").focusout(function () {
        valTran();
    });

});


function valTran() {
    var eta = $('#ETA').val();
    var bookingDate = $('#BookingDate').val();

    var etaDate = new Date(eta);
    var bookingDateDate = new Date(bookingDate);
    if (eta === '' || eta == undefined || bookingDate === '' || bookingDate == undefined) {

    }
    else if (etaDate <= bookingDateDate) {
        var htmlForModal = '<h4>ETA shoud be greater than Booking Date</h4>';

        $('#remodelDiv').html(htmlForModal);
        window.location.href = '#modal';
        return;
    }
}


function AddCustomer() {
    cname = document.getElementById("NewCustomerName").value;
    postData = { cname: cname };
    //add token
    addRequestVerificationToken(postData);

    //add new customer call
    $.ajax({
        url: '/Customer/CreateByNameOnly',//'@Url.Action("CreateByNameOnly", "Customer")',
        type: 'POST',
        dataType: 'json',
        data: postData,
        cache: false,
        success: function (result) {
            if (result != null) {

                var htmlForModal = '<h4>A new customer has been added to the system named : </h4>' + result.CustomerName;

                $("#ConsigneeID").append("<option value=" + result.CustomerID + ">" + result.CustomerName + "</option>");
                $('#ConsigneeID').trigger("chosen:updated");

                $("#ShipperID").append("<option value=" + result.CustomerID + ">" + result.CustomerName + "</option>");
                $('#ShipperID').trigger("chosen:updated");

                $('#addNewCustomer').trigger("click");

                $('#remodelDiv').html(htmlForModal);
                window.location.href = '#modal';
                return;
            }
        },
        error: function (error) {
            var htmlForModal = '<h4>There is an error adding agent named : ' + result.cname;
            $('#remodelDiv').html(htmlForModal);
            window.location.href = '#modal';
            return;
        }
    });
}
function AddAgent() {
    aname = document.getElementById("NewAgentName").value;
    acountry = document.getElementById("AgentCountry").value;


    postData = { aname: aname, acountry: acountry };
    //add token
    addRequestVerificationToken(postData);

    //add new customer call
    $.ajax({
        url: '/Agent/CreateByNameOnly', //'@Url.Action("CreateByNameOnly", "Agent")',
        type: 'POST',
        dataType: 'json',
        data: postData,
        cache: false,
        success: function (result) {
            if (result != null) {

                var htmlForModal = '<h4>A new agent has been added to the system named : </h4>' + result.AgentName;

                $("#AgentID").append("<option value=" + result.AgentID + ">" + result.AgentName + "</option>");
                $('#AgentID').trigger("chosen:updated");

                $('#addNewAgent').trigger("click");

                $('#remodelDiv').html(htmlForModal);
                window.location.href = '#modal';
                return;
            }
        },
        error: function (error) {

            var htmlForModal = '<h4>There is an error adding a new agent';
            $('#remodelDiv').html(htmlForModal);
            window.location.href = '#modal';
            return;
        }
    });

    
}
function AddPort() {
    pname = document.getElementById("NewPortName").value;
    pcountry = document.getElementById("PortCountry").value;


    postData = { pname: pname, pcountry: pcountry };
    //add token
    addRequestVerificationToken(postData);

    //add new customer call
    $.ajax({
        url: '/Port/CreateByNameOnly', //'@Url.Action("CreateByNameOnly", "Agent")',
        type: 'POST',
        dataType: 'json',
        data: postData,
        cache: false,
        success: function (result) {
            if (result != null) {

                var htmlForModal = '<h4>A new port has been added to the system named : </h4>' + result.PortName;

                $("#POL").append("<option value=" + result.PortID + ">" + result.PortName + "</option>");
                $('#POL').trigger("chosen:updated");

                $("#POD").append("<option value=" + result.PortID + ">" + result.PortName + "</option>");
                $('#POD').trigger("chosen:updated");

                $('#addNewPort').trigger("click");

                $('#remodelDiv').html(htmlForModal);
                window.location.href = '#modal';
                return;
            }
        },
        error: function (error) {

            var htmlForModal = '<h4>There is an error adding a new agent';
            $('#remodelDiv').html(htmlForModal);
            window.location.href = '#modal';
            return;
        }
    });
}


function AddCommodity() {
    cname = document.getElementById("NewCommodityName").value;


    postData = { cname: cname };
    //add token
    addRequestVerificationToken(postData);

    //add new customer call
    $.ajax({
        url: '/Commodity/CreateByNameOnly',//'@Url.Action("CreateByNameOnly", "Commodity")',
        type: 'POST',
        dataType: 'json',
        data: postData,
        cache: false,
        success: function (result) {
            if (result != null) {

                var htmlForModal = '<h4>A new Commodity has been added to the system named : </h4>' + result.CommodityName;

                $("#CommodityID").append("<option value=" + result.AgentID + ">" + result.CommodityName + "</option>");
                $('#CommodityID').trigger("chosen:updated");

                $('#addNewCommodity').trigger("click");

                $('#remodelDiv').html(htmlForModal);
                window.location.href = '#modal';
                return;
            }
        },
        error: function (error) {

            var htmlForModal = '<h4>There is an error adding a new Commodity';
            $('#remodelDiv').html(htmlForModal);
            window.location.href = '#modal';
            return;
        }
    });
}