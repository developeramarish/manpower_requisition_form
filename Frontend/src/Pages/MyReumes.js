import React, { useState } from 'react'
import SearchText from './SearchText';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import DashboardHeader from './Header';
import LeftPanel from './LeftPanel';
import {Dropdown} from 'primereact/dropdown';
import { MultiSelect } from 'primereact/multiselect';

import { InputTextarea } from 'primereact/inputtextarea';
        

const MyReumes = () => {

  const [resumeData, setResumeData] = useState([
    { SrNo: "1", ResumeName: "Ramksrishna" , Status:"DummyDataforDroDown",ForwardTo:"DummyForForward",Reason:<InputTextarea placeholder='Enter Reason'  rows={1} cols={25} />},
    { SrNo: "2", ResumeName: "Ashutosh m" , Status:"DummyDataforDroDown",ForwardTo:"DummyForForward",Reason:<InputTextarea placeholder='Enter Reason'  rows={1} cols={25} /> },
    { SrNo: "3", ResumeName: "Gaurav"  , Status:"DummyDataforDroDown",ForwardTo:"DummyForForward",Reason:<InputTextarea placeholder='Enter Reason'  rows={1} cols={25} />},
    { SrNo: "4", ResumeName: "Aditya" , Status:"DummyDataforDroDown",ForwardTo:"DummyForForward",Reason:<InputTextarea placeholder='Enter Reason'  rows={1} cols={25} /> },
    { SrNo: "5", ResumeName: "priyanka" , Status:"DummyDataforDroDown",ForwardTo:"DummyForForward",Reason:<InputTextarea placeholder='Enter Reason'  rows={1} cols={25} />},
    { SrNo: "6", ResumeName: "Manish"  , Status:"DummyDataforDroDown",ForwardTo:"DummyForForward",Reason:<InputTextarea placeholder='Enter Reason'  rows={1} cols={25} />},
    { SrNo: "7", ResumeName: "Pratey"  , Status:"DummyDataforDroDown",ForwardTo:"DummyForForward",Reason:<InputTextarea placeholder='Enter Reason'  rows={1} cols={25} />}
  ]);

  const StatusDropdown = ({ value, onChange }) => {
    const statusOptions = [
      { label: 'Active', value: 'active' },
      { label: 'Inactive', value: 'inactive' },
      // Add more status options as needed
    ];
    return (
      <Dropdown
            value={value}
            options={statusOptions}
            onChange={(e) => onChange(e.value)}
            placeholder="Select a status"
          />
        );
      };

  const statusTemplate = (rowData, column) => {
    return (<StatusDropdown value={rowData.status} onChange={(value) => handleStatusChange(rowData, value)} />);
  };



  const handleStatusChange = (rowData, value) => {
    const updatedData = resumeData.map((item) =>
      item.id === rowData.id ? { ...item, status: value } : item);
      setResumeData(updatedData);
  };



  {/*for ForwardTo*/}



  const FilterDemo=() =>{
    const [selectedCities, setSelectedCities] = useState(null);
    const cities = [
        { name: 'Manotosh', code: 'NY' },
        { name: 'Zia', code: 'RM' },
        { name: 'Arun Goyel', code: 'LDN' },
        { name: 'IAmeya', code: 'IST' },
        { name: 'Ntin', code: 'PRS' },
        { name: 'Raj', code: 'PRS' }
    ];
    return (
        <div className="card flex justify-content-center">
            <MultiSelect value={selectedCities} onChange={(e) => setSelectedCities(e.value)} options={cities} optionLabel="name" 
                filter placeholder="Select Reviewer" maxSelectedLabels={3} className="w-full md:w-20rem" />
        </div>
    );
}
  

  return (
    <div>



      <div className='MyDashBoard'>
        <div className='containerH'>
          <div className='box'>
            <label>My Resumes</label>
          </div>
          <SearchText />
        </div>
        <div className='bar'>
          <DataTable value={resumeData} paginator rows={5} rowsPerPageOptions={[5, 10, 25, 50]} tableStyle={{ minWidth: '50rem' }}>
            <Column field='SrNo' header="Sr.No" sortable style={{ width: '20vw' }} ></Column>
            <Column field='ResumeName' header="Resume" sortable style={{ width: '20vw' }}></Column>
            <Column field='Status' header="Status" sortable   body={statusTemplate}  style={{ width: '40vw' }} ></Column>
            <Column field='ForwardTo' header="Forward To"  body={FilterDemo} sortable style={{ width: '20vw' }}></Column>
            <Column field='Reason' header="Resaon" sortable style={{ width: '20vw' }}></Column>

          </DataTable>


        </div>
      </div >

    </div>


  );
};

export default MyReumes