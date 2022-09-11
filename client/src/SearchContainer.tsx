import React, { useEffect, useState } from "react";
import { Input, Row, Col, Button, Tag } from "antd";
import { Expense, Filter } from "./MainContainer";
import { api } from "./api";

export const SearchContainer = ({
  setExpenses,
}: {
  setExpenses: (expenses: Expense[]) => void;
}) => {
  const [text, setText] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const [filters, setFilters] = useState<Filter[]>([]);

  useEffect(() => {
    getFilters();
  }, []);

  const getExpenses = async (text: string) => {
    setIsLoading(true);
    const expenses = await api.fetchExpenses(text);
    setExpenses(expenses);
    setIsLoading(false);
  };

  const getFilters = async () => {
    setIsLoading(true);
    const filters = await api.fetchFilters();
    setFilters(filters);
    setIsLoading(false);
  };

  return (
    <div>
      <Row type="flex" justify="center" gutter={8}>
        <Col>
          <Button
            icon="save"
            onClick={async () => {
              await api.addFilter(text);
              await getFilters();
            }}
          />
        </Col>
        <Col xs={16} md={10}>
          <Input
            placeholder="Sökord.."
            value={text}
            onChange={(text) => setText(text.target.value)}
          />
        </Col>
        <Col>
          <Button
            icon="search"
            type="primary"
            loading={isLoading}
            onClick={() => getExpenses(text)}
          >
            Sök
          </Button>
        </Col>
      </Row>
      <Row type="flex" justify="center" gutter={4}>
        {filters.map((filter) => (
          <Tag
            key={filter.id}
            style={{ marginTop: 8 }}
            closable
            onClick={async () => {
              setText(filter.text);
              await getExpenses(filter.text);
            }}
            onClose={async () => {
              await api.deleteFilter(filter.id);
              await getFilters();
            }}
          >
            {filter.text}
          </Tag>
        ))}
      </Row>
    </div>
  );
};
