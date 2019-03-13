import React, { useState, useEffect } from "react";
import { Expense } from "./App";
import { Table, Row, Col, Button } from "antd";
import moment from "moment";
import { getExpenses } from "./api";

interface EditExpense {
  id: string;
  date: moment.Moment;
  description: string;
  amount: number;
  askIfDuplicate: boolean;
}

export const EditContainer = () => {
  const [editExpenses, setEditExpenses] = useState<Array<EditExpense>>([]);

  useEffect(() => {
    fetchExpenses();
  }, []);

  const fetchExpenses = async () => {
    const expenses = await getExpenses();
    setEditExpenses(transformExpenses(expenses));
  };

  const deleteExpense = async (id: string) => {
    await deleteExpense(id);
    fetchExpenses();
  };

  const transformExpenses = (expenses: Expense[]): EditExpense[] => {
    return expenses.map(expense => {
      return {
        id: expense.id,
        date: moment.utc(expense.date),
        description: expense.description,
        amount: expense.amount,
        askIfDuplicate:
          expense.notDuplicate !== true && findDuplicates(expense, expenses)
      } as EditExpense;
    });
  };

  const findDuplicates = (expense: Expense, expenses: Expense[]): boolean =>
    expenses.filter(
      e =>
        e.amount === expense.amount &&
        e.description === expense.description &&
        e.date.isSame(expense.date)
    ).length > 1;

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
    },
    {
      title: "Dublett",
      dataIndex: "askIfDuplicate",
      key: "askIfDuplicate",
      render: (askIfDuplicate: boolean, editExpense: EditExpense) => {
        return askIfDuplicate === true ? (
          <Button onClick={() => deleteExpense(editExpense.id)}>Ta bort</Button>
        ) : (
          undefined
        );
      }
    }
  ];

  return (
    <Row>
      <Col span={24} className="container-margin">
        <Table dataSource={editExpenses} columns={columns} />
      </Col>
    </Row>
  );
};
