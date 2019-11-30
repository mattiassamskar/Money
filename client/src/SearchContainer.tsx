import React, { useState } from "react";
import { Input, Row, Col, Button } from "antd";
import { Expense } from "./MainContainer";
import { api } from "./api";

export const SearchContainer = ({
  setExpenses
}: {
  setExpenses: (expenses: Expense[]) => void;
}) => {
  const [filter, setFilter] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const getExpenses = async () => {
    setIsLoading(true);
    const expenses = await api.fetchExpenses(filter);
    setExpenses(expenses);
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
          onClick={getExpenses}
        >
          Sök
        </Button>
      </Col>
    </Row>
  );
};
