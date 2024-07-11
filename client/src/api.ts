import { DateTime } from "luxon";
import { Expense, Filter } from "./MainContainer";

interface ExpensesResponseData {
  id: string;
  date: string;
  description: string;
  amount: number;
}

const fetchExpenses = async (filter?: string) => {
  try {
    const url = filter ? "/api/expenses?filter=" + filter : "/api/expenses";
    const response = await fetch(url);
    const result = (await response.json()) as ExpensesResponseData[];

    const expenses: Expense[] = result.map((item) => ({
      id: item.id,
      amount: item.amount,
      description: item.description,
      date: DateTime.fromISO(item.date),
      notDuplicate: true,
    }));

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
