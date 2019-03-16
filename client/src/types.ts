import moment from "moment";

export interface EditExpense {
id: string;
  date: moment.Moment;
  description: string;
  amount: number;
  askIfDuplicate: boolean;
}

