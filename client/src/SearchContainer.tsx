import React, { useState } from "react";
import { Input, Row, Col, Button } from "antd";
import { Expense } from "./App";

interface Props {
  setExpenses: (expenses: Expense[]) => void;
}

export const SearchContainer = (props: Props) => {
  const [filter, setFilter] = useState("");

  const fetchExpenses = () => {
    fetch("/expenses?filter=" + filter)
      .then(result => result.json())
      .then(expenses => props.setExpenses(expenses));
  };

  return (
    <Row gutter={16} type="flex" justify="center" className="container-margin">
      <Col span={8}>
        <Input
          placeholder="Sökord.."
          value={filter}
          onChange={text => setFilter(text.target.value)}
        />
      </Col>
      <Col span={8}>
        <Button type="primary" onClick={fetchExpenses} >Sök!</Button>
      </Col>
    </Row>
  );
};
