var app = new Vue({
  el: '#app',
  mounted: function () {
    var ctx = document.getElementById("chart").getContext('2d');
    this.chart = new Chart(ctx, {
      type: 'line',
      options: {
        legend: {
          display: false
        },
        elements: {
          line: {
            tension: 0
          }
        },
        scales: {
          yAxes: [{
            ticks: {
              beginAtZero: true
            }
          }]
        }
      }
    });
  },
  data: {
    filter: '',
    expenses: [],
    sum: 0,
    chart: '',
    hideSpinner: true
  },
  methods: {
    totalSum: function () {
      return this.expenses.reduce((a, b) => a + b.amount, 0);
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
    dragover: function (ev) { ev.preventDefault(); },
    dragEnd: function (ev) { ev.dataTransfer.clearData(); },
    updateChart: function (ev) {
      var self = this;
      self.hideSpinner = false;
      var req = new XMLHttpRequest();
      req.onload = function () {
        self.expenses = (JSON.parse(req.response));
        var result = self.expenses.reduce((a, b) => {
          var month = new Date(b.date).toLocaleDateString('sv-SE').substring(0, 7);
          a[month] = a[month] || 0;
          a[month] = a[month] + b.amount;
          return a;
        }, []);
        self.chart.data.labels = [...Object.keys(result)];
        self.chart.data.datasets = [{ data: [...Object.values(result)] }];
        self.chart.update();
        self.hideSpinner = true;
      };
      req.open('GET', '/expenses' + '?filter=' + self.filter);
      req.send();
    }
  }
});