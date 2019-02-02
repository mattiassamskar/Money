import React, { Component } from "react";
import { Expense } from "./App";

interface Props {
  expenses: Expense[];
}

interface State {}

class ExpensesContainer extends Component<Props, State> {
  render() {
    return <div className="App" />;
  }
}

export default ExpensesContainer;
