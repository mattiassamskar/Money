import { useState } from "react";
import { DateTime } from "luxon";
import { ExpensesContainer } from "./ExpensesContainer";
import { SearchContainer } from "./SearchContainer";
import { ChartContainer } from "./ChartContainer";
import { Flex, Spin } from "antd";
import { Header } from "./HeaderComponent";
import { UploadContainer } from "./UploadContainer";

export interface Expense {
  id: string;
  date: DateTime;
  description: string;
  amount: number;
  notDuplicate: boolean;
}

export interface Filter {
  id: string;
  text: string;
}

export const MainContainer = () => {
  const [expenses, setExpenses] = useState<Array<Expense>>([]);
  const [isLoading, setIsLoading] = useState(false);

  return (
    <Flex gap="middle" vertical>
      <Header />
      <Spin spinning={isLoading}>
        <Flex gap="middle" vertical>
          <SearchContainer setExpenses={setExpenses} />
          {expenses.length > 0 && (
            <>
              <ChartContainer expenses={expenses} />
              <ExpensesContainer expenses={expenses} />
            </>
          )}
          <UploadContainer setIsLoading={setIsLoading} />
        </Flex>
      </Spin>
    </Flex>
  );
};
