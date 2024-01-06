import { useEffect, useState } from "react";
import { Input, Col, Button, Tag, Row } from "antd";
import { Expense, Filter } from "./MainContainer";
import { api } from "./api";
import { SaveOutlined } from "@ant-design/icons";

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
    setExpenses([]);
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
    <>
      <Row justify="center">
        <Col>
          <Button
            icon={<SaveOutlined />}
            loading={isLoadingFilters}
            onClick={async () => {
              await api.addFilter(text);
              await getFilters();
            }}
          />
        </Col>
        <Col xs={16} md={10}>
          <Input.Search
            enterButton
            value={text}
            loading={isLoadingExpenses}
            onChange={(text) => setText(text.target.value)}
            onSearch={() => getExpenses(text)}
          />
        </Col>
      </Row>
      <Row justify="center">
        <Col xs={24} md={20} flex={"none"}>
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
        </Col>
      </Row>
    </>
  );
};
