import React from "react";
import { Link, useNavigate } from "react-router-dom";

const GridMenuHeader = ({ headerTitle }) => {
  const navigate = useNavigate();

  return (
    <div className="grid-3">
      <div className="item">
        <div className="back" onClick={() => navigate("/menu")}>
          &lt;Powrót
        </div>
      </div>
      <div className="item">
        <h1>{headerTitle}</h1>
      </div>
      <div className="item"></div>
    </div>
  );
};

export default GridMenuHeader;
