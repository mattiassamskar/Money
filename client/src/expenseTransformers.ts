import { Expense } from "./MainContainer";
import { EditExpense } from "./EditContainer";
import { DateTime } from "luxon";

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

export const sumExpensesByMonth = (
  expenses: Expense[],
  minDate: DateTime,
  maxDate: DateTime
) => {
  const result = [] as { x: DateTime; y: number }[];

  for (
    let current = minDate;
    current <= maxDate;
    current = current.plus({ month: 1 })
  ) {
    const sum = expenses
      .filter(
        (expense) =>
          expense.date.hasSame(current, "year") &&
          expense.date.hasSame(current, "month")
      )
      .reduce((prev, curr) => (prev += curr.amount), 0);

    result.push({ x: current, y: sum });
  }
  return result;
};

export const getMinDate = (expenses: Expense[]) => {
  return expenses.toSorted((a, b) => a.date.diff(b.date).milliseconds)[0]?.date;
};

export const getMaxDate = (expenses: Expense[]) => {
  return expenses.toSorted((a, b) => b.date.diff(a.date).milliseconds)[0]?.date;
};
