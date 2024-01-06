import moment from "moment";
import { Expense, Filter } from "./MainContainer";

const fetchExpenses = async (filter?: string) => {
  try {
    const url = filter ? "/api/expenses?filter=" + filter : "/api/expenses";
    const result = await fetch(url);
    const expenses = (await result.json()) as Expense[];
    expenses.forEach((expense) => (expense.date = moment.utc(expense.date)));
    return expenses;
  } catch (error) {
    console.log("Error fetching expenses: ", error);
    return [];
  }
};

const deleteExpense = async (id: string) => {
  try {
    await fetch("/api/expenses?id=" + id, { method: "DELETE" });
  } catch (error) {
    console.log("Error deleting expense: ", error);
  }
};

const fetchFilters = async () => {
  try {
    const result = await fetch("/api/filters");
    const expenses = (await result.json()) as Filter[];
    return expenses;
  } catch (error) {
    console.log("Error fetching filters: ", error);
    return [];
  }
};

const addFilter = async (text: string) => {
  try {
    await fetch("/api/filters?text=" + text, { method: "PUT" });
  } catch (error) {
    console.log("Error adding filter: ", error);
  }
};

const deleteFilter = async (id: string) => {
  try {
    await fetch("/api/filters?id=" + id, { method: "DELETE" });
  } catch (error) {
    console.log("Error deleting filter: ", error);
  }
};

const uploadFiles = async (files: FileList) => {
  try {
    var formData = new FormData();
    for (var i = 0; i < files.length; i++) {
      formData.append("files", files[i]);
    }
    await fetch("/api/upload", {
      method: "POST",
      body: formData,
    });
  } catch (error) {
    console.log("Error uploading files: ", error);
  }
};

export const api = {
  fetchExpenses,
  deleteExpense,
  fetchFilters,
  addFilter,
  deleteFilter,
  uploadFiles,
};
