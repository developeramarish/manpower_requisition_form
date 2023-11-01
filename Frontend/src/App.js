import "./App.css";
import "./styles/layout/theme.css";
import "primereact/resources/primereact.css";
import "primeflex/primeflex.css";
import "primeicons/primeicons.css";
import "primereact/resources/primereact.min.css";
import  Dashboard  from "./Dashboard";
import MyRequisitions from "./MyRequisitions";
import CreateRequisition from "./Pages/CreateRequisition";
import MyReumes from "./Pages/MyReumes";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import EmployeDetails from "./Pages/EmployeDetails";

function App() {
  return (
    <BrowserRouter>
      <div className="container">
        

        <Routes>
          <Route path="/Dashboard" element={<Dashboard />} />
          <Route path="/MyRequisitions" element={<MyRequisitions />} />
          <Route path="/CreateRequisition" element={<CreateRequisition />} />
          <Route path="/MyReumes" element={<MyReumes />} />
          <Route path="/EmployeDetails" element={<EmployeDetails />} />
          <Route index element={<Dashboard />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
