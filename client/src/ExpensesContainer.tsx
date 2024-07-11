import { Expense } from "./MainContainer";
import { Table, Row, Col } from "antd";
import { DateTime } from "luxon";

export const ExpensesContainer = ({ expenses }: { expenses: Expense[] }) => {
  const columns = [
    {
      title: "Datum",
      dataIndex: "date",
      key: "date",
      render: (date: DateTime) => <div>{date.toFormat("yyyy-MM-dd")}</div>,
      sorter: (a: Expense, b: Expense) => a.date.diff(b.date).milliseconds,
    },
    {
      title: "Beskrivning",
      dataIndex: "description",
      key: "description",
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
      sorter: (a: Expense, b: Expense) => a.amount - b.amount,
    },
  ];

  return (
    <Row justify="center">
      <Col xs={24} md={20}>
        <Table dataSource={expenses} columns={columns} rowKey="id" />
      </Col>
    </Row>
  );
};
