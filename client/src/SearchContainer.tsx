import React, { useState } from "react";
import { Input, Row, Col, Button } from "antd";
import { Expense } from "./MainContainer";
import { getExpenses } from "./api";

interface Props {
  setExpenses: (expenses: Expense[]) => void;
}

export const SearchContainer = (props: Props) => {
  const [filter, setFilter] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const fetchExpenses = async () => {
    setIsLoading(true);
    const expenses = await getExpenses(filter);
    props.setExpenses(expenses);
    setIsLoading(false);
  };

  return (
    <Row type="flex" justify="center">
      <Col xs={16} md={10}>
        <Input
          placeholder="Sökord.."
          value={filter}
          onChange={text => setFilter(text.target.value)}
        />
      </Col>
      <Col>
        <Button
          icon="search"
          type="primary"
          loading={isLoading}
          onClick={fetchExpenses}
        >
          Sök
        </Button>
      </Col>
    </Row>
  );
};
