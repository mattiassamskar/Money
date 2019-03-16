import React from "react";
import { Link } from "react-router-dom";
import { Icon } from "antd";
import "antd/dist/antd.css";
import "./header.css";

export const Header = () => (
  <div className="header-container">
    <Link to="/" className="header-homelink">
      <div>
        <Icon type="home" /> Ekonomi
      </div>
    </Link>
    <Link to="/edit" className="header-editlink">
      Ändra
    </Link>
  </div>
);
