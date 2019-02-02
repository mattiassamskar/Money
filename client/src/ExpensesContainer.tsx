import React, { Component } from "react";
import { Expense } from "./App";
import Table from "antd/lib/table";

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
    return <Table dataSource={this.props.expenses} columns={this.columns} />;
  }
}

export default ExpensesContainer;
