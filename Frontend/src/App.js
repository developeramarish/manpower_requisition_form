import "./App.css";
import "./styles/layout/theme.css";
import "primereact/resources/primereact.css";
import "primeflex/primeflex.css";
import "primeicons/primeicons.css";
import "primereact/resources/primereact.min.css";
import  "./styles/layout/InputComponents.css";
import Dashboard from "./Dashboard";
import MyRequisitions from "./MyRequisitions";
import CreateRequisition from "./Pages/CreateRequisition";
import MyReumes from "./Pages/MyReumes";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import EmployeDetails from "./Pages/EmployeDetails";
import Candidate from "./Pages/Candidate";
import EmployeDetailsCreate from "./Pages/EmployeDetailsCreate";
import EmployeeDtailsEdit from "./Pages/EmployeeDtailsEdit";
import AddCandidate from "./Pages/AddCandidate";

function App() {
  return (
    <BrowserRouter>
      <div className="container">
        <Routes>
          <Route path="/Dashboard" element={<Dashboard />} />
          <Route path="/MyRequisitions" element={<MyRequisitions />} />
          <Route path="/CreateRequisition" element={<CreateRequisition />} />
          <Route path="/EditRequisition/:reqId" element={<CreateRequisition />} />
          <Route path="/MyReumes" element={<MyReumes />} />
          <Route path="/EmployeDetails" element={<EmployeDetails />}></Route>
          <Route path="/Candidate" element={<Candidate />} />
          <Route path="/AddCandidate" element={<AddCandidate />} />
          <Route
            path="/EmployeDetailsCreate"
            element={<EmployeDetailsCreate />}
          />
          <Route
            path="/EmployeeDtailsEdit/:id"
            element={<EmployeeDtailsEdit />}
          />
          <Route index element={<Dashboard />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
