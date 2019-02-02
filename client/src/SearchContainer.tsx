import React from "react";
import { Input, Row, Col, Button } from "antd";

class SearchContainer extends React.Component {
  render() {
    return (
    <Row gutter={16} className="container-margin">
        <Col span={12}>
          <Input placeholder="Sökord.." />
        </Col>
        <Col span={12}>
          <Button type="primary">Sök!</Button>
        </Col>
      </Row>
    );
  }
}

export default SearchContainer;
