import React, { useState } from "react";
import "./App.css";
import "antd/dist/antd.css";
import { ExpensesContainer } from "./ExpensesContainer";
import { SearchContainer } from "./SearchContainer";
import { Row, Col } from "antd";
import { ChartContainer } from "./ChartContainer";
import moment from "moment";

export interface Expense {
  id: string;
  date: moment.Moment;
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
          <ChartContainer expenses={expenses} />
          <SearchContainer setExpenses={setExpenses} />
          <ExpensesContainer expenses={expenses} />
        </Col>
        <Col xs={0} md={3} />
      </Row>
    </div>
  );
};

export default App;
