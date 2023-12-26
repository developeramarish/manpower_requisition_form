import { useState } from "react";
import { Link } from "react-router-dom";
import PopupDemo from "./PopupDemo";
import '../styles/layout/OpenStatus.css'
 
const ReferenceBodyTemplate = (mrf) => {
  const [visible,setVisible]=useState(false);
 if(mrf.roleId === 3){
  if (mrf.mrfStatus === "Open") {
    return (
      <Link to={`/EditRequisition/${mrf.mrfId}`}>
        <h4 className="ref-col-cell">{mrf.referenceNo}</h4>
      </Link>
    );
  
  }
    else {
    // If the status is different, navigate to the default EditRequisition page
    return (
      <Link to={`/EditRequisition/${mrf.mrfId}`}>
        <h4 className="ref-col-cell">{mrf.referenceNo}</h4>
      </Link>
    );
  }
}
else if(mrf.roleId === 4){
      if(mrf.mrfStatus === "Received"){
        return (
          <Link to={`/ReceivedStatus/${mrf.mrfId}`}>
            <h4 className="ref-col-cell" >{mrf.referenceNo}</h4>
          </Link>
        );
      }
      else if (mrf.mrfStatus === "Received HOD Approval"){
        return (
          <Link to={`/ReceivedHODApproval/${mrf.mrfId}`}>
            <h4 className="ref-col-cell" >{mrf.referenceNo}</h4>
          </Link>
        );
      }
      else if (mrf.mrfStatus === "Received COO Approval"){
        console.log("mrf",mrf);
        return (
          <Link to={`/ReceivedCOOApproval/${mrf.mrfId}`}>
            <h4 className="ref-col-cell" >{mrf.referenceNo}</h4>
          </Link>
        );
      }
      else if(mrf.mrfStatus === "MRF Re-submission Required"){
        return (
          <div>
            <h4 className="ref-col-cell"  
             onClick={()=>setVisible(true)}
            >{mrf.referenceNo}</h4>
            <PopupDemo content={" This MRF is yet to be resubmitted"} visible={visible}
            onHide={() => setVisible(false)} footer={false}
             />
          </div>
        );
      }
      else if(mrf.mrfStatus === "MRF Rejected"){
        return (
          <div>
            <h4 className="ref-col-cell"  
             onClick={()=>setVisible(true)}
            >{mrf.referenceNo}</h4>
            <PopupDemo content={"This MRF is Rejected"} visible={visible} footer={false}
            onHide={() => setVisible(false)}
             />
          </div>
        );
      }
      else if(mrf.mrfStatus === "Closed by HR"){
        return (
          <div>
            <h4 className="ref-col-cell"  
             onClick={()=>setVisible(true)}
            >{mrf.referenceNo}</h4>
            <PopupDemo content={"This MRF is closed"} visible={visible} footer={false}
            onHide={() => setVisible(false)}
             />
          </div>
        );
      }
      else if(mrf.mrfStatus === "MRF On hold"){

        return (
          <div>
            <h4 className="ref-col-cell"  
             onClick={()=>setVisible(true)}
            >{mrf.referenceNo}</h4>
            <PopupDemo mrfId={mrf.mrfId} content={"Do you want to submit it for HOD approval?"} visible={visible} footer={true}
            onHide={() => setVisible(false)}
             />
          </div>
        );
      }
      else {
        // If the status is different, navigate to the default EditRequisition page
        return (
          <Link to={`/EditRequisition/${mrf.mrfId}`}>
            <h4 className="ref-col-cell">{mrf.referenceNo}</h4>
          </Link>
        );
      }
 }
   
};
  export default ReferenceBodyTemplate;