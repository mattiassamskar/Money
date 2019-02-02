import React, { Component } from "react";
import "./App.css";

export interface Expense {
  id: string;
  date: string;
  description: string;
  amount: number;
}

class App extends Component {
  render() {
    return <div className="App" />;
  }
}

export default App;
