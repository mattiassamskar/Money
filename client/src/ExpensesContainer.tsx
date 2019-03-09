import React from "react";
import { Expense } from "./App";
import { Table, Row, Col } from "antd";
import moment from "moment";

interface Props {
  expenses: Expense[];
}

export const ExpensesContainer = (props: Props) => {
  const columns = [
    {
      title: "Datum",
      dataIndex: "date",
      key: "date",
      render: (date: moment.Moment) => <div>{date.format("YYYY-MM-DD")}</div>
    },
    {
      title: "Beskrivning",
      dataIndex: "description",
      key: "description"
    },
    {
      title: "Belopp",
      dataIndex: "amount",
      key: "amount"
    }
  ];

  return (
    <Row>
      <Col span={24} className="container-margin">
        <Table dataSource={props.expenses} columns={columns} />
      </Col>
    </Row>
  );
};
