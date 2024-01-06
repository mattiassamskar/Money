import { Link } from "react-router-dom";
import { HomeOutlined } from "@ant-design/icons";
import "./header.css";
import { Col, Row } from "antd";

export const Header = () => (
  <Row justify={"center"}>
    <Col xs={24} md={20}>
      <div className="header-container">
        <Link to="/" className="header-homelink">
          <div>
            <HomeOutlined /> Ekonomi
          </div>
        </Link>
        <Link to="/edit" className="header-editlink">
          Ändra
        </Link>
      </div>
    </Col>
  </Row>
);
