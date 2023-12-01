import { useState } from "react";
import { Link } from "react-router-dom";
import PopupDemo from "./PopupDemo";
import '../styles/layout/OpenStatus.css'
 
const ReferenceBodyTemplate = (mrf) => {
  const [visible,setVisible]=useState(false);
   
 if(mrf.roleId==3){
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
else if(mrf.roleId==4){
      if(mrf.mrfStatus === "Received"){
        return (
          <Link to={`/OpenStatus`}>
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
            <PopupDemo visible={visible}
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