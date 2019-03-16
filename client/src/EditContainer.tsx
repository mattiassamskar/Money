import React, { useState, useEffect } from "react";
import { Table, Row, Col, Button, Spin } from "antd";
import moment from "moment";
import { getExpenses } from "./api";
import { transformToEditExpenses } from "./expensTransformers";

export interface EditExpense {
  id: string;
  date: moment.Moment;
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
    const expenses = await getExpenses();
    setEditExpenses(transformToEditExpenses(expenses));
    setIsLoading(false);
  };

  const deleteExpense = async (id: string) => {
    await deleteExpense(id);
    fetchExpenses();
  };


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
        {isLoading ? (
          <Spin size="large" />
        ) : (
          <Table dataSource={editExpenses} columns={columns} />
        )}
      </Col>
    </Row>
  );
};
