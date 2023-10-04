import './App.css';
import { Home } from './Home';
import { Department } from './Department';
import { Employeedetails } from './Employeedetails';
import { Dashboard } from './Dashboard';
import { Navigation } from './Navigation';

import { BrowserRouter, Routes, Route } from 'react-router-dom';

   


function App() {
  return (
    <BrowserRouter>
      <div className="container">
        <h3 className="m-3 d-flex justify-content-center">Manpower Requisition Form </h3>
        <Navigation />

        <Routes>
          <Route path="/Home" element={<Home />} />
          <Route path="/Department" element={<Department />} />
          <Route path="/Employeedetails" element={<Employeedetails />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
