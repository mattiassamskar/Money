import React, { useState } from "react";
import moment from "moment";
import { ExpensesContainer } from "./ExpensesContainer";
import { SearchContainer } from "./SearchContainer";
import { ChartContainer } from "./ChartContainer";

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

  return (
    <div>
      <SearchContainer setExpenses={setExpenses} />
      <ChartContainer expenses={expenses} />
      <ExpensesContainer expenses={expenses} />
    </div>
  );
};
