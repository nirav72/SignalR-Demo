$(document).ready(function () {

    var con = $.hubConnection();
    var hub = con.createHubProxy("MessageHub");

    con.start().done(function () {

    });

    hub.on("hello", function (msg) {
        vm.msg(msg);
    });

    hub.on("NewTicket", function (data) {
        dump(data);
    });

    var vm = {

        msg: ko.observable("Hello from Knockout"),

        broadcast: function () {
            hub.invoke("Hello", "Hello from the Browser");
        },

        getTicket: function () {
            hub.invoke("Post", "Steve");
        },

        sendTicket: function () {
            var t = {
                name: 'Steve',
                ticket: '11',
                desc: 'This is a ticket from a client'
            }

            hub.invoke("Send", JSON.stringify(t));
        }

    };

    // Activates knockout.js
    ko.applyBindings(vm);
});