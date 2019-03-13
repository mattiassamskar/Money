import React from "react";
import { Link } from "react-router-dom";
import { Icon } from "antd";
import "antd/dist/antd.css";

export const Header = () => (
  <div
    style={{
      height: "60px",
      paddingLeft: "10px",
      paddingRight: "10px",
      marginBottom: "10px",
      display: "flex",
      justifyContent: "space-between",
      alignItems: "center",
      background: "#fafafa"
    }}
  >
    <Link to="/" style={{ color: "black" }}>
      <div>
        <Icon type="home" /> Ekonomi
      </div>
    </Link>
    <Link to="/edit" style={{ color: "grey" }}>
      Ändra
    </Link>
  </div>
);
