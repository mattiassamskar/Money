var app = new Vue({
    el: '#app',
    mounted: function () {
        var self = this;
        setTimeout(function () {
            var connection = $.hubConnection();
            var proxy = connection.createHubProxy('expenseHub');
            proxy.on('send', function(expenses) {
                self.expenses.push(...expenses);
            });
            connection.start();
        }, 1000);
        var ctx = document.getElementById("chart").getContext('2d');
        this.chart = new Chart(ctx, { type: 'bar' });
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
            var hej = self.expenses.filter(function (expense) {
                return self.filter.split(',').filter(function (f) {
                    return expense.Description.includes(f)
                }).length > 0
            });
            return hej;
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
            this.chart.data.labels.pop();
            this.chart.data.datasets = [];
            var labels = this.filtered().map(function(e) {return e.Date});
            var data = this.filtered().map(function (e) { return e.Amount });

            this.chart.data.labels.push(labels);
            this.chart.data.datasets.push({data: data });
            this.chart.update();
        }
    }
})