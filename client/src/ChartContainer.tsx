import { Col, Row } from "antd";
import { Expense } from "./MainContainer";
import * as Highcharts from "highcharts";
import HighchartsReact from "highcharts-react-official";
import { sumExpensesByMonth } from "./expenseTransformers";
import { DateTime } from "luxon";

export const ChartContainer = ({ expenses }: { expenses: Expense[] }) => {
  const expensesByMonth = sumExpensesByMonth(expenses);

  const categories = [...Object.keys(expensesByMonth)].map((key) =>
    DateTime.fromFormat(key + "01", "yyyyMMdd").toFormat("MMM yyyy")
  );
  const data = [...Object.values(expensesByMonth)].map((value) => {
    return { y: value };
  });

  const options: Highcharts.Options = {
    chart: {
      type: "column",
      marginTop: 40,
      animation: false,
    },
    title: { text: "" },
    legend: { enabled: false },
    credits: { enabled: false },
    exporting: { enabled: false },
    tooltip: {
      formatter: function () {
        const amount = Intl.NumberFormat("sv-se", {
          style: "currency",
          currency: "SEK",
          minimumFractionDigits: 0,
          maximumFractionDigits: 0,
        }).format(this.y);
        return this.x + " <b>" + amount + "</b>";
      },
    },
    xAxis: {
      type: "category",
      categories,
      tickInterval: 3,
    },
    yAxis: {
      title: undefined,
      labels: {
        format: "{value} kr",
      },
    },
    series: [
      {
        type: "column",
        data,
      },
    ],
  };

  return (
    <Row justify="center">
      <Col xs={24} md={20}>
        <HighchartsReact highcharts={Highcharts} options={options} />
      </Col>
    </Row>
  );
};
