import React, { useState } from "react";
import { Button } from 'primereact/button';
import { Dialog } from 'primereact/dialog';
import SearchText from "./SearchText";
import { InputTextarea } from 'primereact/inputtextarea';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { MultiSelect } from 'primereact/multiselect';


const MyResumeDetail = () => {
    const [visible, setVisible] = useState(true);
    const [myresumeDetail, setmyresumeDetail] = useState([
        { SrNo: "1", ResumeName: "Ramksrishna" , UploadedOn:"28-04-2023", UploadedBy:"Sweta" ,ResumeReviewer:"Dummy data",ResumeStatus:"Shortlisted",Reason:<InputTextarea placeholder='Enter Reason'  rows={1} cols={25} />},
        { SrNo: "2", ResumeName: "Ashutosh M" , UploadedOn:"28-04-2023", UploadedBy:"Sweta" ,ResumeReviewer:"Dummy data",ResumeStatus:"Shortlisted",Reason:<InputTextarea placeholder='Enter Reason'  rows={1} cols={25} />},
        { SrNo: "3", ResumeName: "Ram" , UploadedOn:"28-04-2023", UploadedBy:"Sweta" ,ResumeReviewer:"Dummy data",ResumeStatus:"Shortlisted",Reason:<InputTextarea placeholder='Enter Reason'  rows={1} cols={25} />},
        { SrNo: "4", ResumeName: "Ramksrishna" , UploadedOn:"28-04-2023", UploadedBy:"Sweta" ,ResumeReviewer:"Dummy data",ResumeStatus:"Shortlisted",Reason:<InputTextarea placeholder='Enter Reason'  rows={1} cols={25} />},
        { SrNo: "5", ResumeName: "ksrishna" , UploadedOn:"28-04-2023", UploadedBy:"Sweta" ,ResumeReviewer:"Dummy data",ResumeStatus:"Shortlisted",Reason:<InputTextarea placeholder='Enter Reason'  rows={1} cols={25} />},
        { SrNo: "6", ResumeName: "Dev Anand" , UploadedOn:"28-04-2023", UploadedBy:"Sweta" ,ResumeReviewer:"Dummy data",ResumeStatus:"Shortlisted",Reason:<InputTextarea placeholder='Enter Reason'  rows={1} cols={25} />},
        { SrNo: "7", ResumeName: "Ramksrishna" , UploadedOn:"28-04-2023", UploadedBy:"Sweta" ,ResumeReviewer:"Dummy data",ResumeStatus:"Shortlisted",Reason:<InputTextarea placeholder='Enter Reason'  rows={1} cols={25} />},
        { SrNo: "8", ResumeName: "Ramksrishna" , UploadedOn:"28-04-2023", UploadedBy:"Sweta" ,ResumeReviewer:"Dummy data",ResumeStatus:"Shortlisted",Reason:<InputTextarea placeholder='Enter Reason'  rows={1} cols={25} />},
        { SrNo: "9", ResumeName: "Prat" , UploadedOn:"28-04-2023", UploadedBy:"sweta",ResumeReviewer:"Dummy data",ResumeStatus:"Shortlisted",Reason:<InputTextarea placeholder='Enter Reason'  rows={1} cols={25} />},
       
      ]);


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
        <div>MyResumeDetail</div>
         {/* <Button label="Show" icon="pi pi-external-link" onClick={() => setVisible(true)} /> */}

         <Dialog header="MRF ID(Resume Summary)" visible={true} style={{ width: '90vw' }} onHide={() => setVisible(false)} >
{/* <SearchText/> */}

                
<DataTable value={myresumeDetail} paginator rows={5} rowsPerPageOptions={[5, 10, 25, 50]} tableStyle={{ minWidth: '50rem' }}>
            <Column field='SrNo' header="Sr.No"  bodyClassName="font-bold" sortable style={{ width: '20%' }} ></Column>
            <Column field='ResumeName' header="Resume" sortable  bodyClassName="font-bold text-red-500" style={{ width: '20%' }}></Column>
            <Column field='UploadedOn' header="UploadedOn" sortable   bodyClassName="font-bold"   style={{ width: '20%' }} ></Column>
            <Column field='UploadedBy' header="UploadedBy" bodyClassName="font-bold"  sortable style={{ width: '20%' }}></Column>
            <Column field="ResumeReviewer" header="ResumeReviewer" body={FilterDemo} bodyClassName="font-bold" sortable style={{ width: '20%' }}></Column>
            <Column field='ResumeStatus' header="ResumeStatus" bodyClassName="font-bold"  sortable style={{ width: '20%' }}></Column>
            <Column field='Reason' header="Resaon" sortable style={{ width: '20%' }}></Column>

          </DataTable>   
                
                
                
                
            </Dialog>
    </div>
    
  )
}

export default MyResumeDetail