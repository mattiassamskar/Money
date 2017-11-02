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
        var proxy = connection.createHubProxy('expenseHub');
        proxy.on('send', (expense) => {
            app.expenses.push(expense);
        });
        connection.start()
            .done(() => console.log('Connected'))
            .fail(() => console.log('Could not connect'));
    },
    data: {
        filter: '',
        expenses: [],
        sum: 0
    },
    methods: {
        filtered: (expenses, filterString) =>
            expenses.filter((expense) =>
                filterString.split(',').filter((f) =>
                    expense.Description.includes(f)).length > 0)
        ,
        totalSum: (expenses, filterString) => {
            const amounts = expenses.filter((expense) =>
                filterString.split(',').filter((f) =>
                    expense.Description.includes(f)).length > 0).map(expense => expense.Amount);
            return amounts.reduce((a, b) => a + b, 0);
        }
    }
})