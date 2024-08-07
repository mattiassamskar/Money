import { useState, useEffect } from "react";
import { Table, Row, Col, Button, Flex } from "antd";
import { DateTime } from "luxon";
import { api } from "./api";
import { transformToEditExpenses } from "./expenseTransformers";
import { ColumnProps } from "antd/lib/table";
import { Header } from "./HeaderComponent";

export interface EditExpense {
  id: string;
  date: DateTime;
  description: string;
  amount: number;
  askIfDuplicate: boolean;
}

export const EditContainer = () => {
  const [editExpenses, setEditExpenses] = useState<Array<EditExpense>>([]);
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    fetchExpenses();
  }, []);

  const fetchExpenses = async () => {
    setIsLoading(true);
    const expenses = await api.fetchExpenses();
    setEditExpenses(transformToEditExpenses(expenses));
    setIsLoading(false);
  };

  const deleteExpense = async (id: string) => {
    await api.deleteExpense(id);
    fetchExpenses();
  };

  const columns: ColumnProps<EditExpense>[] = [
    {
      title: "Datum",
      dataIndex: "date",
      key: "date",
      render: (date: DateTime) => <div>{date.toFormat("yyyy-MM-dd")}</div>,
      sorter: (a: EditExpense, b: EditExpense) =>
        a.date.toMillis() - b.date.toMillis(),
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
      sorter: (a: EditExpense, b: EditExpense) => a.amount - b.amount,
    },
    {
      title: "Dublett",
      dataIndex: "askIfDuplicate",
      key: "askIfDuplicate",
      render: (askIfDuplicate: boolean, editExpense: EditExpense) => {
        return askIfDuplicate === true ? (
          <Button onClick={() => deleteExpense(editExpense.id)}>Ta bort</Button>
        ) : undefined;
      },
      sorter: (a: EditExpense) => (a.askIfDuplicate ? 1 : -1),
    },
  ];

  return (
    <Flex gap="middle" vertical>
      <Header />
      <Row justify={"center"}>
        <Col xs={24} md={20}>
          <Table
            dataSource={editExpenses}
            columns={columns}
            rowKey="id"
            loading={isLoading}
          />
        </Col>
      </Row>
    </Flex>
  );
};
