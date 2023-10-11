import './App.css';
import { Home } from './Home';
import { Department } from './Department';
import { Employeedetails } from './Employeedetails';
import { Dashboard } from './Dashboard';
import { Navigation } from './Navigation';
import './styles/layout/theme.css';
import "primereact/resources/primereact.css";
import "primeflex/primeflex.css";
import "primeicons/primeicons.css";
import 'primereact/resources/primereact.min.css';

import DashboardPage from './Dashboard'; // Import your Dashboard page component
import MyRequisitions from './MyRequisitions';
import CreateRequisition from './Pages/CreateRequisition';
import MyReumes from './Pages/MyReumes';

import { BrowserRouter, Routes, Route } from 'react-router-dom';

   


function App() {
  return (
    <BrowserRouter>
      <div className="container">
        
        <Navigation />

        <Routes>
         
          
          <Route path="/MyRequisitions" element={<MyRequisitions />} />
          <Route path="/CreateRequisition" element={<CreateRequisition />} />
          <Route path="/MyReumes" element={<MyReumes />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
