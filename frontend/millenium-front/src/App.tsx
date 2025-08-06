import React from "react";
import "./App.css";
import RestComponent from "./RestComponent";
import SoapComponent from "./SoapComponent";

function App() {
  return (
    <div>
      <h1>REST and SOAP for millenium</h1>
      <RestComponent />
      <SoapComponent />
    </div>
  );
}

export default App;
