// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
$('body').on('focus', ".date_timer", function () {
    $(this).datetimepicker({
        dateFormat: 'mm-dd-yy',
        timeFormat: "HH:mm",
        //minDate: 0,
        showButtonPanel: true,
        onSelect: function (dateText, inst) {
            //var fromdate = new Date(dateText);
            //$(this).val(moment(dateText).format('DD-MM-YYYY hh:mm:ss A'));
        }
    });
});
//This Methods and enum to show message in Case add or update any entity.
function ShowOperationResultMessage(message, messageType) {
    $('#divMessage').attr('class', ''); //without paramters to remove all css classes
    switch (messageType) {
        case ResponseStatusEnum.Success:
            $('#divMessage').addClass('alert alert-success');
            break;
        case ResponseStatusEnum.Error:
            $('#divMessage').addClass('alert alert-danger');
            break;
        case ResponseStatusEnum.Warning:
            $('#divMessage').addClass('alert alert-warning');
            break;
    }
    document.getElementById('messageText').innerText = message;
    $('#divMessage').show();
}
function CloseMessage(divMessage) {
    $('#' + divMessage).fadeOut("slow");
}
$('#ServicesDDL').on("change","#ddlServices",function (e) {
    let serviceId = $(this).val();
    $.ajax({
        url: '/Round/GetRoundsSearchSection',
        type: 'GET',
        data: { "serviceId": serviceId },
        success: function (res) {
            $('#RoundsNumbers').html(res);
            $('#RoundsNumbers').fadeIn();
        },
        error: function (res) {
            $('#RoundsNumbers').fadeOut(function () {
                $('#RoundsNumbers').html(res);
            });
        }
    });
    $('#RoundTableContainer').jtable('load', {
        serviceId: $(this).val(),
    });
});
$("#RestOfForm").on("change", "#ddlRounds", function () {
    let roundId = $("#ddlRounds").val();
    let sendeingdateIsNotEmpty = false;
    $(".sendingDate").each(function () {
        if ($(this).val()) {
            sendeingdateIsNotEmpty = true;
        }
    });
    if (roundId && roundId != "0" && sendeingdateIsNotEmpty) {
        $("#refreshdialscount").prop("disabled", false);
        $("#refreshdialscount").css('cursor', 'pointer');
    }
    else {
        $("#refreshdialscount").prop("disabled", true);
        $("#refreshdialscount").css('cursor', 'not-allowed');
    }
});
$("#RestOfForm").on("change", "#ddlPreviousCampaign", function () {
    let previousCampaignId = $("#ddlPreviousCampaign").val();
    
    if (previousCampaignId && previousCampaignId != "0") {
        $("#refreshdialscount").prop("disabled", false);
        $("#refreshdialscount").css('cursor', 'pointer');
    }
    else {
        $("#refreshdialscount").prop("disabled", true);
        $("#refreshdialscount").css('cursor', 'not-allowed');
    }
});
$("#SmsTextList").on("change",".sendingDate",function (e) {
    let roundId = $("#ddlRounds").val();
    let sendeingdateIsNotEmpty = false;
    $(".sendingDate").each(function () {
        if ($(this).val()) {
            sendeingdateIsNotEmpty = true;
        }
    });
    if (roundId && roundId != "0" && sendeingdateIsNotEmpty) {
        $("#refreshdialscount").prop("disabled", false);
        $("#refreshdialscount").css('cursor', 'pointer');
    }
    else {
        $("#refreshdialscount").prop("disabled", true);
        $("#refreshdialscount").css('cursor', 'not-allowed');
    }
});
$("#SmsTextList").on("click", "#AddNewText", function () {
    debugger;
    let smsmCounts = $('.campaigntexts').length;
    let anotherSMStextField = `
                            <div class="row campaigntexts">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Sending Date</label>
                                        <input class="form-control date_timer sendingDate" id="SendingDate`+ smsmCounts + `" name="MutlipleText[` + smsmCounts + `].SendingDate" readonly="readonly" required="Required" type="text" value="" />
                                    </div>
                                </div>
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <label>Message Text</label>
                                        <div class="form-group">
                                            <textarea class="form-control SMSText" rows="1" name="MutlipleText[`+ smsmCounts + `].SMSText"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <div type="button" id="removeSMSText" style="font-size:125px;color:red">
                                            <span class="material-icons">
                                                clear
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>`;
    $("#beforeSMSTextDetails").before(anotherSMStextField);
});
$("#SmsTextList").on("click", "#removeSMSText", function () {
    $(this).parents(".campaigntexts").remove();
    let smsmCounts = $('.campaigntexts').length;
    for (var i = 1; i < smsmCounts; i++) {
        $('.campaigntexts:eq(' + i + ') .sendingDate').attr("name","MutlipleText["+i+"].SendingDate");
        $('.campaigntexts:eq(' + i + ') .sendingDate').attr("id","SendingDate"+i);
        $('.campaigntexts:eq(' + i + ') .SMSText').attr("name","MutlipleText["+i+"].SMSText");

    }

});
$("#RestOfForm").on('focusout', ".categorydialCount", function () {
    var inputVal = $(this).val();
    if (!$.isNumeric(inputVal))
    {
        $(this).val(0);
    }
    
});

$(function () {

    $('.list-group-item').on('click', function () {
        $('.fa', this)
            .toggleClass('fa-angle-right')
            .toggleClass('fa-angle-down');
    });

});