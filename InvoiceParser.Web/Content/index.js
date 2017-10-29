var req = new XMLHttpRequest();
req.onload = function () {
    var expenses = JSON.parse(req.response);
};
req.open('GET', '/expenses');
req.send();