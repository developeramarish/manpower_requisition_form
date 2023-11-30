import React, { useState } from 'react';
import { Checkbox } from 'primereact/checkbox';
import { Button } from 'primereact/button';
import { InputText } from 'primereact/inputtext';
import DashboardHeader from './Header';
import LeftPanel from './LeftPanel';
import SearchText from './SearchText';
import { Toolbar } from 'primereact/toolbar';
import DataTableComponents from '../Components/DataTableComponent';
import ButtonC from '../Components/Button';
import InputTextCp from '../Components/Textbox';

function OpenStatus() {
  const [checked, setChecked] = useState(false);
  const [showTextbox, setShowTextbox] = useState(false);
  const [disableCheckbox, setDisableCheckbox] = useState(false);

  const handleCheckboxChange = (event) => {
    setChecked(event.checked);
    setShowTextbox(event.checked);
    setDisableCheckbox(event.checked);
  };

  const handleOkClick = () => {
    console.log('OK clicked');
  };

  const handleCancelClick = () => {
    console.log('Cancel clicked');
  };
  return (
    <div >
    <DashboardHeader />
   
      
      <div
      className="border-round-lg bg-white text-black-alpha-90 p-3 flex flex-column justify-content-between"
      style={{ width: "210vh" } }
    >
      {/* <Checkbox
        checked={checked}
        onChange={handleCheckboxChange}
        inputId="checkbox"
      />
      <label htmlFor="checkbox" className="p-checkbox-label">
        Checkbox
      </label>
      {showTextbox && (
        <InputText placeholder="Enter text here" className="p-mt-2" />
      )}
      <Checkbox
        disabled={disableCheckbox}
        inputId="checkbox2"
        className="p-mt-2"
      />
      <label htmlFor="checkbox2" className="p-checkbox-label">
        Disabled Checkbox
      </label>
      <div className="p-mt-2">
        <Button label="OK" className="p-button-success" onClick={handleOkClick} />
        <Button label="Cancel" className="p-button-danger p-ml-2" onClick={handleCancelClick} />
      </div>
    </div> */}
      
       
    
      <h3 className="text-xl my-2"></h3>
      <section
        className="flex flex-column flex-nowrap gap-3 border-y-2 border-gray-300 py-3 px-1 overflow-y-scroll"
        style={{ height: "70%" }}
      >
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            
            <label htmlFor="checkbox" className="text-xl my-2">Is Resubmission Required 
             <Checkbox
        checked={checked}
        onChange={handleCheckboxChange}
        inputId="checkbox"
      />
      {showTextbox && (
        <InputText placeholder="Enter text here" className="p-mt-2" />
      )}
       </label>
       
      <label htmlFor="checkbox2" className="text-xl my-2">Do You Want to Submit it for HOD approval??
             <Checkbox
        checked={disableCheckbox}
        inputId="checkbox2"
        className="p-mt-2"
      />
       </label>
        
            
             
          </div>
        </div>
         {<div className="flex flex-wrap justify-content-end gap-5 mt-3">
         <Button label="OK" className="p-button-success" onClick={handleOkClick} />
        <Button label="Cancel" className="p-button-danger p-ml-2" onClick={handleCancelClick} />
      </div>}
         
        </section>
      </div>  
      </div>
      
    
  );
  
}

export default OpenStatus;
