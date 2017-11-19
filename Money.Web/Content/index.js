var app = new Vue({
    el: '#app',
    mounted: function () {
        var self = this;
        var connection = $.hubConnection();
        var proxy = connection.createHubProxy('expenseHub');
        proxy.on('send', function(expenses) {
            self.expenses.push(...expenses);
        });
        connection.start();
        var ctx = document.getElementById("chart").getContext('2d');
        this.chart = new Chart(ctx, { type: 'line', options: { legend: { display: false } } });
    },
    data: {
        filter: '',
        expenses: [],
        sum: 0,
        chart: ''
    },
    methods: {
        filtered: function () {
            var self = this;
            return self.expenses.filter(function (expense) {
                return self.filter.split(',').filter(function (f) {
                    return expense.Description.includes(f)
                }).length > 0
            });
        },
        totalSum: function () {
            return this.filtered().reduce((a, b) => a + b.Amount, 0);
        },
        drop: function (ev) {
            ev.preventDefault();
            var dt = ev.dataTransfer;
            for (var i = 0; i < dt.files.length; i++) {
                var xhr = new XMLHttpRequest();
                xhr.open("POST", 'upload', true);
                var fd = new FormData();
                fd.append("upload_file", dt.files[i]);
                xhr.send(fd);
            }
        },
        dragover: function (ev) { ev.preventDefault() },
        dragEnd: function (ev) { ev.dataTransfer.clearData() },
        updateChart: function (ev) {
            var result = this.filtered().reduce((a, b) => {
                a[b.Date.substring(0, 7)] = a[b.Date.substring(0, 7)] || 0;
                a[b.Date.substring(0, 7)] = a[b.Date.substring(0, 7)] + b.Amount;
                return a;
            }, []);

            this.chart.data.labels = [...Object.keys(result)];
            this.chart.data.datasets = [{ data: [...Object.values(result)] }];
            this.chart.update();
        }
    }
})