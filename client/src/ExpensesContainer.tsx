import React, { Component } from "react";
import { Expense } from "./App";
import { Table, Row, Col } from "antd";

interface Props {
  expenses: Expense[];
}

interface State {}

class ExpensesContainer extends Component<Props, State> {
  columns = [
    {
      title: "Datum",
      dataIndex: "date",
      key: "date"
    },
    {
      title: "Beskrivning",
      dataIndex: "description",
      key: "description"
    },
    {
      title: "Belopp",
      dataIndex: "amount",
      key: "amount"
    }
  ];

  render() {
    return (
      <Row>
        <Col span={24} className="container-margin">
          <Table dataSource={this.props.expenses} columns={this.columns} />
        </Col>
      </Row>
    );
  }
}

export default ExpensesContainer;
