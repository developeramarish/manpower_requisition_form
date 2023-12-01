import React, { useState } from 'react';

import { Checkbox } from 'primereact/checkbox';

import { InputText } from 'primereact/inputtext';

import { Button } from 'primereact/button';

import '../styles/layout/OpenStatus.css'; // Import your CSS file
import DashboardHeader from './Header';
 
const OpenStatus = () => {

  const [firstChecked, setFirstChecked] = useState(false);

  const [secondChecked, setSecondChecked] = useState(false);

  const [thirdChecked, setThirdChecked] = useState(false);

  const [showTextBox, setShowTextBox] = useState(false);
 
  const handleFirstCheck = () => {

    setFirstChecked(true);

    setSecondChecked(false);

    setThirdChecked(false);

    setShowTextBox(true);

  };
 
  const handleSecondCheck = () => {

    setFirstChecked(false);

    setSecondChecked(true);

    setThirdChecked(false);

    setShowTextBox(false);

  };
 
  const handleThirdCheck = () => {

    setFirstChecked(false);

    setSecondChecked(false);

    setThirdChecked(true);

    setShowTextBox(false);

  };
 
  const handleCancel = () => {

    setFirstChecked(false);

    setSecondChecked(false);

    setThirdChecked(false);

    setShowTextBox(false);

  };
 
  const handleSave = () => {

    // Logic to save data

    setFirstChecked(false);

    setSecondChecked(false);

    setThirdChecked(false);

    setShowTextBox(false);

  };
 
  return (
    <div >
    <DashboardHeader />
    <div className='openstatus'>
    <div className="checkbox-container">
      <div>
        <label htmlFor="first">Is Resubmission Required?</label>

        <Checkbox

          inputId="first"

          onChange={handleFirstCheck}

          checked={firstChecked}

          disabled={secondChecked || thirdChecked}

        />
          {showTextBox && (

<div className="textbox-container">
  <InputText />

</div>

)}

      </div>

      <div>
        <label htmlFor="second">Do You Want to Submit it for HOD approval?</label>
        
        <Checkbox

          inputId="second"

          onChange={handleSecondCheck}

          checked={secondChecked}

          disabled={firstChecked || thirdChecked}

        />

      </div>

      <div>

        <label htmlFor="third">Do You Want to Hold on this MRF?</label>
        
        <Checkbox

          inputId="third"

          onChange={handleThirdCheck}

          checked={thirdChecked}

          disabled={firstChecked || secondChecked}

        />


      </div>

    

      <div className="button-container">

        <Button label="Cancel" className="p-button-danger" onClick={handleCancel} />

        <Button label="Save" className="p-button-primary" onClick={handleSave} />

      </div>

    </div>
    </div>
    </div>
  );

};
 
export default OpenStatus;
