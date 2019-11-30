import moment from "moment";
import { Expense } from "./MainContainer";

const fetchExpenses = async (filter?: string) => {
  try {
    const url = filter ? "/expenses?filter=" + filter : "/expenses";
    const result = await fetch(url);
    const expenses = (await result.json()) as Expense[];
    expenses.forEach(expense => (expense.date = moment.utc(expense.date)));
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

const uploadFiles = async (files: FileList) => {
  try {
    var formData = new FormData();
    for (var i = 0; i < files.length; i++) {
      formData.append("files", files[i]);
    }
    await fetch("/upload", {
      method: "POST",
      body: formData
    });
  } catch (error) {
    console.log("Error uploading files: ", error.message);
  }
};

export const api = {
  fetchExpenses,
  deleteExpense,
  uploadFiles
};
