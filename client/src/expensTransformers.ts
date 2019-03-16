import { Expense } from "./MainContainer";
import { EditExpense } from "./EditContainer";
import moment from "moment";

export const transformToEditExpenses = (expenses: Expense[]): EditExpense[] => {
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
