import React, { useState } from "react";
import "./App.css";
import "antd/dist/antd.css";
import ExpensesContainer from "./ExpensesContainer";
import SearchContainer from "./SearchContainer";
import { Row, Col } from "antd";

export interface Expense {
  id: string;
  date: string;
  description: string;
  amount: number;
}

const App = () => {
  const [expenses, setExpenses] = useState<Array<Expense>>([]);

  return (
    <div className="App">
      <Row>
        <Col xs={0} md={3} />
        <Col xs={24} md={18}>
          <SearchContainer setExpenses={setExpenses}/>
          <ExpensesContainer expenses={expenses}/>
        </Col>
        <Col xs={0} md={3} />
      </Row>
    </div>
  );
};

export default App;
