import moment from "moment";
import { Expense } from "./MainContainer";

export const getExpenses = async (filter?: string) => {
  try {
    const url = filter ? "/expenses?filter=" + filter : "/expenses";
    const result = await fetch(url);
    const expenses = (await result.json()) as Expense[];
    expenses.forEach(expense => (expense.date = moment.utc(expense.date)));
    return expenses;
  } catch (error) {
    return [];
  }
};

export const deleteExpense = async (id: string) => {
  try {
    await fetch("/expenses?id=" + id, { method: "DELETE" });
  } catch (error) {}
};
