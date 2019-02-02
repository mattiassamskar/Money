import React from "react";
import { Input, Row, Col, Button } from "antd";

class SearchContainer extends React.Component {
  render() {
    return (
    <Row gutter={16} type="flex" justify="center" className="container-margin">
        <Col span={8}>
          <Input placeholder="Sökord.." />
        </Col>
        <Col span={8}>
          <Button type="primary">Sök!</Button>
        </Col>
      </Row>
    );
  }
}

export default SearchContainer;
