import { Expense } from "./MainContainer";
import { EditExpense } from "./EditContainer";

export const transformToEditExpenses = (expenses: Expense[]): EditExpense[] => {
  return expenses.map((expense) => {
    return {
      id: expense.id,
      date: expense.date,
      description: expense.description,
      amount: expense.amount,
      askIfDuplicate:
        expense.notDuplicate !== true && findDuplicates(expense, expenses),
    } as EditExpense;
  });
};

const findDuplicates = (expense: Expense, expenses: Expense[]): boolean =>
  expenses.filter(
    (e) =>
      e.amount === expense.amount &&
      e.description === expense.description &&
      e.date.equals(expense.date)
  ).length > 1;

export const sumExpensesByMonth = (expenses: Expense[]) => {
  return expenses.reduce((a: { [key: number]: number }, b) => {
    const month = parseInt(b.date.toFormat("yyyyMM"));
    a[month] = a[month] || 0;
    a[month] = a[month] + b.amount;
    return a;
  }, []);
};
