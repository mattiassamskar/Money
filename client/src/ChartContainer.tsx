import React, { useState } from "react";
import { Input, Row, Col, Button } from "antd";
import { Expense } from "./App";
import * as Highcharts from "highcharts";
import HighchartsReact from "highcharts-react-official";

interface Props {
  expenses: Expense[];
}

export const ChartContainer = (props: Props) => {
  const options: Highcharts.Options = {
    chart: {
      type: "column",
      marginTop: 40
    },
    title: { text: "" },
    credits: { enabled: false },
    exporting: { enabled: false },
    series: [
      {
        type: "line",
        data: props.expenses.map(d => d.amount)
      }
    ]
  };

  return (
    <Row>
      <HighchartsReact highcharts={Highcharts} options={options} />
    </Row>
  );
};
