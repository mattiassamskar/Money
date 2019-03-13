import React  from "react";
import { Row } from "antd";
import { Expense } from "./MainContainer";
import * as Highcharts from "highcharts";
import HighchartsReact from "highcharts-react-official";
import moment from "moment";

interface Props {
  expenses: Expense[];
}

export const ChartContainer = (props: Props) => {
  const sumExpensesByMonth = () => {
    return props.expenses.reduce((a: { [key: number]: number }, b) => {
      const month = parseInt(b.date.format("YYYYMM"));
      a[month] = a[month] || 0;
      a[month] = a[month] + b.amount;
      return a;
    }, []);
  };

  const expensesByMonth = sumExpensesByMonth();
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
    tooltip: { valueDecimals: 2 },
    xAxis: {
      type: "category",
      categories
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
