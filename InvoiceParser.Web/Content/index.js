window.onload = function () {


    document.getElementById('fileinput').addEventListener('change', function () {
        var file = this.files[0];

        var xhr = new XMLHttpRequest();
        xhr.open("POST", 'upload', true);
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
                console.log(xhr.responseText);
            }
        };
        var fd = new FormData();
        fd.append("upload_file", file);
        xhr.send(fd);
    }, false);
}


// var req = new XMLHttpRequest();
// req.onload = function () {
//     var expenses = JSON.parse(req.response);
// };
// req.open('GET', '/expenses');
// req.send();