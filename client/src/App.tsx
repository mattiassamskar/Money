import React, { useState } from "react";
import "./App.css";
import "antd/dist/antd.css";
import { ExpensesContainer } from "./ExpensesContainer";
import { SearchContainer } from "./SearchContainer";
import { Row, Col, Button } from "antd";
import { ChartContainer } from "./ChartContainer";
import moment from "moment";
import "moment/locale/sv";
import { EditContainer } from "./EditContainer";

export interface Expense {
  id: string;
  date: moment.Moment;
  description: string;
  amount: number;
}

const App = () => {
  moment.locale("sv");
  const [isEditing, setIsEditing] = useState<boolean>(true);
  const [expenses, setExpenses] = useState<Array<Expense>>([]);

  return (
    <div className="App">
      <Row>
        <Button onClick={() => setIsEditing(!isEditing)}>Ändra</Button>
        <Col xs={0} md={3} />
        <Col xs={24} md={18}>
          {isEditing ? (
            <EditContainer />
          ) : (
            <div>
              <ChartContainer expenses={expenses} />
              <SearchContainer setExpenses={setExpenses} />
              <ExpensesContainer expenses={expenses} />
            </div>
          )}
        </Col>
        <Col xs={0} md={3} />
      </Row>
    </div>
  );
};

export default App;
