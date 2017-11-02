function drop_handler(ev) {
    ev.preventDefault();

    var dt = ev.dataTransfer;
    for (var i = 0; i < dt.files.length; i++) {
        var xhr = new XMLHttpRequest();
        xhr.open("POST", 'upload', true);
        var fd = new FormData();
        fd.append("upload_file", dt.files[i]);
        xhr.send(fd);
    }
}

function dragover_handler(ev) {
    ev.preventDefault();
}

function dragend_handler(ev) {
    ev.dataTransfer.clearData();
}

var app = new Vue({
    el: '#app',
    created: () => {
        var connection = $.hubConnection();
        var contosoChatHubProxy = connection.createHubProxy('chatHub');
        contosoChatHubProxy.on('send', (message) => {
            console.log(message);
            app.expenses.push(message);
        });
        connection.start()
            .done(function () { console.log('Now connected, connection ID=' + connection.id); })
            .fail(function () { console.log('Could not connect'); });
    },
    data: {
        expenses: [],
    }
})