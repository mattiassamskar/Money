import React, { useState, useEffect } from "react";
import { Expense } from "./App";
import { Table, Row, Col } from "antd";
import moment from "moment";

export interface EditExpense {
  id: string;
  date: moment.Moment;
  description: string;
  amount: number;
  isDuplicate: boolean;
}

export const EditContainer = () => {
  const [expenses, setExpenses] = useState<Array<Expense>>([]);

  useEffect(() => {
    fetch("/expenses")
      .then(result => result.json())
      .then((expenses: Expense[]) => setExpenses(transformExpenses(expenses)));
  }, []);

  const transformExpenses = (expenses: Expense[]): EditExpense[] => {
    const editExpenses = expenses.map(expense => {
      return {
        id: expense.id,
        date: moment.utc(expense.date),
        description: expense.description,
        amount: expense.amount
      } as EditExpense;
    });

    editExpenses.forEach(
      editExpense =>
        (editExpense.isDuplicate = findDuplicates(editExpenses, editExpense))
    );

    return editExpenses;
  };

  const findDuplicates = (
    editExpenses: Expense[],
    editExpense: EditExpense
  ): boolean =>
    editExpenses.filter(
      expense =>
        expense.date.isSame(editExpense.date) &&
        expense.description === editExpense.description &&
        expense.amount === editExpense.amount
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
      dataIndex: "isDuplicate",
      key: "isDuplicate",
      render: (value: boolean) => <div>{value.toString()}</div>
    }
  ];

  return (
    <Row>
      <Col span={24} className="container-margin">
        <Table dataSource={expenses} columns={columns} />
      </Col>
    </Row>
  );
};
