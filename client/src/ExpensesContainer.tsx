import React from "react";
import { Expense } from "./MainContainer";
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
      key: "amount",
      render: (amount: number) => (
        <div style={{ display: "flex", justifyContent: "flex-end" }}>
          {Intl.NumberFormat("sv-se", { minimumFractionDigits: 2 }).format(
            amount
          )}
        </div>
      )
    }
  ];

  return (
    <Row>
      <Col span={24}>
        <Table dataSource={props.expenses} columns={columns} />
      </Col>
    </Row>
  );
};
