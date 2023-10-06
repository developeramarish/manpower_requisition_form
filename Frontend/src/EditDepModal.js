import React, { useState } from 'react';
import { Dialog } from 'primereact/dialog';
import { Button } from 'primereact/button';
import { InputText } from 'primereact/inputtext';
import { Dropdown } from 'primereact/dropdown';

const EditDeptModel = () => {
  const [visible, setVisible] = useState(false);
  const [formData, setFormData] = useState({ name: '', age: '', gender: '' });

  const showDialog = () => {
    setVisible(true);
  };

  const hideDialog = () => {
    setVisible(false);
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({ ...prevData, [name]: value }));
  };

  const handleSubmit = () => {
    // Handle form submission logic
    hideDialog();
  };

  const genderOptions = [
    { label: 'Male', value: 'male' },
    { label: 'Female', value: 'female' },
  ];

  return (
    <div>
      <Button label="Open Modal" onClick={showDialog} />

      <Dialog visible={visible} onHide={hideDialog}>
        <div>
          <h2>Your Form</h2>
          <form>
            <label>Name:</label>
            <InputText name="name" value={formData.name} onChange={handleInputChange} />

            <label>Age:</label>
            <InputText name="age" value={formData.age} onChange={handleInputChange} />

            <label>Gender:</label>
            <Dropdown
              name="gender"
              value={formData.gender}
              options={genderOptions}
              onChange={handleInputChange}
              optionLabel="label"
              placeholder="Select Gender"
            />

            <Button label="Submit" onClick={handleSubmit} />
          </form>
        </div>
      </Dialog>
    </div>
  );
};

export default EditDeptModel;
