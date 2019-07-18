import React from "react";
import { Row } from "antd";
import { Expense } from "./MainContainer";
import * as Highcharts from "highcharts";
import HighchartsReact from "highcharts-react-official";
import moment from "moment";
import { sumExpensesByMonth } from "./expensTransformers";

interface Props {
  expenses: Expense[];
}

export const ChartContainer = (props: Props) => {
  const expensesByMonth = sumExpensesByMonth(props.expenses);

  const categories = [...Object.keys(expensesByMonth)].map(key =>
    moment.utc(key + "01").format("MMM YYYY")
  );
  const data = [...Object.values(expensesByMonth)].map(value => {
    return { y: value };
  });

  const options: Highcharts.Options = {
    chart: {
      type: "column",
      marginTop: 40,
      animation: false
    },
    title: { text: "" },
    legend: { enabled: false },
    credits: { enabled: false },
    exporting: { enabled: false },
    tooltip: {
      formatter: function() {
        const amount = Intl.NumberFormat("sv-se", {
          style: "currency",
          currency: "SEK",
          minimumFractionDigits: 0,
          maximumFractionDigits: 0
        }).format(this.y);
        return this.x + " " + "<b>" + amount + "</b>";
      }
    },
    xAxis: {
      type: "category",
      categories,
      tickInterval: 3
    },
    yAxis: {
      title: undefined,
      labels: {
        format: "{value} kr"
      }
    },
    series: [
      {
        type: "column",
        data
      }
    ]
  };

  return (
    <Row>
      <HighchartsReact highcharts={Highcharts} options={options} />
    </Row>
  );
};
