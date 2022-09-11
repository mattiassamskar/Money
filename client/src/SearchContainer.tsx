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
  const [isLoadingExpenses, setIsLoadingExpenses] = useState(false);
  const [isLoadingFilters, setIsLoadingFilters] = useState(false);
  const [filters, setFilters] = useState<Filter[]>([]);

  useEffect(() => {
    getFilters();
  }, []);

  const getExpenses = async (text: string) => {
    setIsLoadingExpenses(true);
    const expenses = await api.fetchExpenses(text);
    setExpenses(expenses);
    setIsLoadingExpenses(false);
  };

  const getFilters = async () => {
    setIsLoadingFilters(true);
    const filters = await api.fetchFilters();
    setFilters(filters);
    setIsLoadingFilters(false);
  };

  return (
    <div>
      <Row type="flex" justify="center" gutter={8}>
        <Col>
          <Button
            icon="save"
            loading={isLoadingFilters}
            onClick={async () => {
              await api.addFilter(text);
              await getFilters();
            }}
          />
        </Col>
        <Col xs={16} md={10}>
          <Input.Search
            placeholder="Sökord.."
            enterButton
            value={text}
            loading={isLoadingExpenses}
            onChange={(text) => setText(text.target.value)}
            onSearch={() => getExpenses(text)}
          />
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
