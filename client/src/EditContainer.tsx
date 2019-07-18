import React, { useState, useEffect } from "react";
import { Table, Row, Col, Button, Spin } from "antd";
import moment from "moment";
import api from "./api";
import { transformToEditExpenses } from "./expensTransformers";
import { EditExpense } from "./types";
import { ColumnProps } from "antd/lib/table";

export const EditContainer = () => {
  const [editExpenses, setEditExpenses] = useState<Array<EditExpense>>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [isUploadingFiles, setIsUploadingFiles] = useState(false);

  useEffect(() => {
    fetchExpenses();
  }, []);

  const fetchExpenses = async () => {
    setIsLoading(true);
    const expenses = await api.getExpenses();
    setEditExpenses(transformToEditExpenses(expenses));
    setIsLoading(false);
  };

  const deleteExpense = async (id: string) => {
    await api.deleteExpense(id);
    fetchExpenses();
  };

  const onDrop = async (e: React.DragEvent<HTMLDivElement>) => {
    e.preventDefault();
    setIsUploadingFiles(true);
    await api.uploadFiles(e.dataTransfer.files);
    setIsUploadingFiles(false);
    await fetchExpenses();
  };

  const columns: ColumnProps<EditExpense>[] = [
    {
      title: "Datum",
      dataIndex: "date",
      key: "date",
      render: (date: moment.Moment) => <div>{date.format("YYYY-MM-DD")}</div>,
      sorter: (a: EditExpense, b: EditExpense) => a.date.diff(b.date)
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
      sorter: (a: EditExpense, b: EditExpense) => a.amount - b.amount
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
      },
      sorter: (a: EditExpense, b: EditExpense) => (a.askIfDuplicate ? 1 : -1)
    }
  ];

  return (
    <Row
      onDragOver={e => e.preventDefault()}
      onDragEnd={e => e.dataTransfer.clearData()}
      onDrop={onDrop}
    >
      <Col span={24}>
        <Table
          dataSource={editExpenses}
          columns={columns}
          rowKey="id"
          loading={isLoading || isUploadingFiles}
        />
      </Col>
    </Row>
  );
};
