import React, { Component } from "react";
import "./App.css";
import "antd/dist/antd.css";
import ExpensesContainer from "./ExpensesContainer";
import SearchContainer from "./SearchContainer";

export interface Expense {
  id: string;
  date: string;
  description: string;
  amount: number;
}

class App extends Component {
  expenses: Expense[] = [
    {
      id: "1",
      date: "2019-01-01",
      description: "ICA",
      amount: 2500
    },
    {
      id: "2",
      date: "2019-03-01",
      description: "Coop",
      amount: 990
    },
    {
      id: "3",
      date: "2019-02-01",
      description: "Stadium",
      amount: 35
    }
  ];

  render() {
    return (
      <div className="App">
        <SearchContainer />
        <ExpensesContainer expenses={this.expenses} />
      </div>
    );
  }
}

export default App;
