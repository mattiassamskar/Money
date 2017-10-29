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

// var req = new XMLHttpRequest();
// req.onload = function () {
//     var expenses = JSON.parse(req.response);
// };
// req.open('GET', '/expenses');
// req.send();