import React, { useState } from "react";
import { Input, Row, Col, Button } from "antd";
import { Expense } from "./MainContainer";
import { getExpenses } from "./api";

interface Props {
  setExpenses: (expenses: Expense[]) => void;
}

export const SearchContainer = (props: Props) => {
  const [filter, setFilter] = useState("");

  const fetchExpenses = async () => {
    const expenses = await getExpenses(filter);
    props.setExpenses(expenses);
  };

  return (
    <Row type="flex" justify="center" className="container-margin">
      <Col span={8}>
        <Input
          placeholder="Sökord.."
          value={filter}
          onChange={text => setFilter(text.target.value)}
        />
      </Col>
      <Col span={16}>
        <Button type="primary" onClick={fetchExpenses}>
          Sök!
        </Button>
      </Col>
    </Row>
  );
};
