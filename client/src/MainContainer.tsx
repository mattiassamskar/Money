import React, { useState } from "react";
import moment from "moment";
import { ExpensesContainer } from "./ExpensesContainer";
import { SearchContainer } from "./SearchContainer";
import { ChartContainer } from "./ChartContainer";
import { api } from "./api";
import { Row, Spin } from "antd";

export interface Expense {
  id: string;
  date: moment.Moment;
  description: string;
  amount: number;
  notDuplicate: boolean | null;
}

export interface Filter {
  id: string;
  text: string;
}

export const MainContainer = () => {
  const [expenses, setExpenses] = useState<Array<Expense>>([]);
  const [isLoading, setIsLoading] = useState(false);

  const onDrop = async (e: React.DragEvent<HTMLDivElement>) => {
    e.preventDefault();
    setIsLoading(true);
    await api.uploadFiles(e.dataTransfer.files);
    setIsLoading(false);
  };

  return (
    <Row
      onDragOver={(e) => e.preventDefault()}
      onDragEnd={(e) => e.dataTransfer.clearData()}
      onDrop={onDrop}
    >
      <Spin spinning={isLoading}>
        <SearchContainer setExpenses={setExpenses} />
        <ChartContainer expenses={expenses} />
        <ExpensesContainer expenses={expenses} />
      </Spin>
    </Row>
  );
};
