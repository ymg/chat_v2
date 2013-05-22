/* File Created: July 24, 2012 */

/// <reference path="jquery-2.0.0.min.js" />
/// <reference path="shortcut.js" />
/// <reference path="bootbox.min.js" />


/**********************/
/*   Chat methods     */
/**********************/

var Chathub = $.connection.chatHub;

Chathub.client.retrieveInput = function (val) {
    $('.chatList').append(val);
    $(function () {
        var height = $('.chatBox')[0].scrollHeight;
        $('.chatBox').scrollTop(height);
    });
    $(function () {
        var height = $('#chat')[0].scrollHeight;
        $('#chat').scrollTop(height);
    });
};

Chathub.client.currentUsers = function (list) {
    $('.userList').empty();
    var json = $.parseJSON(list);
    $.each(json, function (name, value) {
        $('.userList').append("<li>" + value + "</li>");
    });
};

/**********************/
/* Chat functionality */
/**********************/

$.connection.hub.start().done(function () {

    $('.chatInput').click(function () {
        if (!localStorage.getItem('nickName')) {
            $('.chatInput').attr('disabled', 'disabled');
            window.bootbox.prompt('Enter nickname please', function (temp) {
                if (temp) {
                    if (temp.length <= 10) {
                        localStorage.setItem('nickName', temp);
                        Chathub.server.addInUserList(temp, $.connection.hub.id);
                        $('.chatInput').unbind();
                    } else {
                        window.bootbox.alert("nickname must be 10 characters or less");
                    }
                }
            });
            $('.chatInput').removeAttr('disabled', 'disabled');
        } else {
            Chathub.server.addInUserList(localStorage.getItem('nickName'), $.connection.hub.id);
            $('.chatInput').unbind();
        }
    });

    $('.submitChat').click(function () {
        if ($('.chatInput').val() != '' && localStorage.getItem('nickName')) {
            Chathub.server.addText($('.chatInput').val());
            $('.chatInput').val('');
        }
    });

});

shortcut.add("Enter", function () {
    $('.submitChat').click();
});

$.connection.hub.stateChanged(function (change) {
    if (change.newState === $.signalR.connectionState.reconnecting) {
        $('.chatBox').addClass("Tilt");
        $('.overlay').css('display', 'block');


    } else if (change.newState === $.signalR.connectionState.connected) {
        $('.chatBox').removeClass("Tilt");
        $('.overlay').css('display', 'none');
    }
});
