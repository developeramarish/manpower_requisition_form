import './App.css';
import { Home } from './Home';
import { Department } from './Department';
import { Employeedetails } from './Employeedetails';
import { Dashboard } from './Dashboard';
import { Navigation } from './Navigation';
import 'primereact/resources/themes/saga-blue/theme.css';
import "primereact/resources/primereact.css";
import "primeflex/primeflex.css";
import "primeicons/primeicons.css";
import 'primereact/resources/primereact.min.css';

import { BrowserRouter, Routes, Route } from 'react-router-dom';

   


function App() {
  return (
    <BrowserRouter>
      <div className="container">
        
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
