import moment from "moment";
import { Expense, Filter } from "./MainContainer";

const fetchExpenses = async (filter?: string) => {
  try {
    const url = filter ? "/expenses?filter=" + filter : "/expenses";
    const result = await fetch(url);
    const expenses = (await result.json()) as Expense[];
    expenses.forEach((expense) => (expense.date = moment.utc(expense.date)));
    return expenses;
  } catch (error) {
    console.log("Error fetching expenses: ", error.message);
    return [];
  }
};

const deleteExpense = async (id: string) => {
  try {
    await fetch("/expenses?id=" + id, { method: "DELETE" });
  } catch (error) {
    console.log("Error deleting expense: ", error.message);
  }
};

const fetchFilters = async () => {
  try {
    const result = await fetch("/filters");
    const expenses = (await result.json()) as Filter[];
    return expenses;
  } catch (error) {
    console.log("Error fetching filters: ", error.message);
    return [];
  }
};

const addFilter = async (text: string) => {
  try {
    await fetch("/filters?text=" + text, { method: "PUT" });
  } catch (error) {
    console.log("Error adding filter: ", error.message);
  }
};

const deleteFilter = async (id: string) => {
  try {
    await fetch("/filters?id=" + id, { method: "DELETE" });
  } catch (error) {
    console.log("Error deleting filter: ", error.message);
  }
};

const uploadFiles = async (files: FileList) => {
  try {
    var formData = new FormData();
    for (var i = 0; i < files.length; i++) {
      formData.append("files", files[i]);
    }
    await fetch("/upload", {
      method: "POST",
      body: formData,
    });
  } catch (error) {
    console.log("Error uploading files: ", error.message);
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
