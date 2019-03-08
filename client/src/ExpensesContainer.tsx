import React from "react";
import { Expense } from "./App";
import { Table, Row, Col } from "antd";

interface Props {
  expenses: Expense[];
}

export const ExpensesContainer = (props: Props) => {
  const columns = [
    {
      title: "Datum",
      dataIndex: "date",
      key: "date"
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
