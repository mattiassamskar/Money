import { describe, it, expect } from "vitest";
import { DateTime } from "luxon";
import {
  getMaxDate,
  getMinDate,
  sumExpensesByMonth,
} from "./expenseTransformers";
import { Expense } from "./MainContainer";

describe("getMinDate", () => {
  it("should get the earliest date", () => {
    const expenses: Expense[] = [
      {
        id: "1",
        date: DateTime.fromISO("2024-03-15"),
        description: "",
        amount: 100,
        notDuplicate: true,
      },
      {
        id: "2",
        date: DateTime.fromISO("2024-01-10"),
        description: "",
        amount: 50,
        notDuplicate: true,
      },
      {
        id: "3",
        date: DateTime.fromISO("2024-01-10"),
        description: "",
        amount: 50,
        notDuplicate: true,
      },
    ];

    const result = getMinDate(expenses);
    expect(result.toISODate()).toBe("2024-01-10");
  });

  it("should handle an empty array", () => {
    const expenses: Expense[] = [];
    const result = getMinDate(expenses);
    expect(result).toEqual(undefined);
  });
});

describe("getMaxDate", () => {
  it("should get the latest date", () => {
    const expenses: Expense[] = [
      {
        id: "1",
        date: DateTime.fromISO("2024-03-15"),
        description: "",
        amount: 100,
        notDuplicate: true,
      },
      {
        id: "2",
        date: DateTime.fromISO("2024-01-10"),
        description: "",
        amount: 50,
        notDuplicate: true,
      },
      {
        id: "3",
        date: DateTime.fromISO("2024-03-15"),
        description: "",
        amount: 50,
        notDuplicate: true,
      },
    ];

    const result = getMaxDate(expenses);
    expect(result.toISODate()).toBe("2024-03-15");
  });

  it("should handle an empty array", () => {
    const expenses: Expense[] = [];
    const result = getMaxDate(expenses);
    expect(result).toEqual(undefined);
  });
});

describe("sumByMonth", () => {
  it("should group expenses by month", () => {
    const expenses: Expense[] = [
      {
        id: "1",
        date: DateTime.fromISO("2025-01-10"),
        description: "",
        amount: 50,
        notDuplicate: true,
      },
      {
        id: "2",
        date: DateTime.fromISO("2025-02-15"),
        description: "",
        amount: 30,
        notDuplicate: true,
      },
      {
        id: "3",
        date: DateTime.fromISO("2025-02-27"),
        description: "",
        amount: 5,
        notDuplicate: true,
      },
      {
        id: "4",
        date: DateTime.fromISO("2025-04-01"),
        description: "",
        amount: 10,
        notDuplicate: true,
      },
    ];

    const result = sumExpensesByMonth(
      expenses,
      DateTime.fromISO("2025-01-01"),
      DateTime.fromISO("2025-05-01")
    );

    expect(result).toEqual([
      {
        x: DateTime.fromISO("2025-01-01"),
        y: 50,
      },
      {
        x: DateTime.fromISO("2025-02-01"),
        y: 35,
      },
      {
        x: DateTime.fromISO("2025-03-01"),
        y: 0,
      },
      {
        x: DateTime.fromISO("2025-04-01"),
        y: 10,
      },
      {
        x: DateTime.fromISO("2025-05-01"),
        y: 0,
      },
    ]);
  });
});
