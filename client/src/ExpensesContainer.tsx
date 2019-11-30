import React from "react";
import { Expense } from "./MainContainer";
import { Table, Row, Col } from "antd";
import moment from "moment";

export const ExpensesContainer = ({ expenses }: { expenses: Expense[] }) => {
  const columns = [
    {
      title: "Datum",
      dataIndex: "date",
      key: "date",
      render: (date: moment.Moment) => <div>{date.format("YYYY-MM-DD")}</div>,
      sorter: (a: Expense, b: Expense) => a.date.diff(b.date)
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
      ),
      sorter: (a: Expense, b: Expense) => a.amount - b.amount
    }
  ];

  return (
    <Row>
      <Col span={24}>
        <Table dataSource={expenses} columns={columns} rowKey="id" />
      </Col>
    </Row>
  );
};
