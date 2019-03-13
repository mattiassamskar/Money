import React from "react";
import { BrowserRouter, Route } from "react-router-dom";
import { Row, Col } from "antd";
import "antd/dist/antd.css";
import { Header } from "./HeaderComponent";
import { MainContainer } from "./MainContainer";
import { EditContainer } from "./EditContainer";
import moment from "moment";
import "moment/locale/sv";

const App = () => {
  moment.locale("sv");

  return (
    <BrowserRouter>
      <div className="App">
        <Row>
          <Col xs={0} md={3} />
          <Col xs={24} md={18}>
            <Header />
            <Route path="/" exact component={MainContainer} />
            <Route path="/edit" component={EditContainer} />
          </Col>
          <Col xs={0} md={3} />
        </Row>
      </div>
    </BrowserRouter>
  );
};

export default App;
