import { Link } from "react-router-dom";
const referenceBodyTemplate = (mrf) => {
   
 if(mrf.roleId==4){
  if (mrf.mrfStatus === "Open") {
      
    return (
      <Link to={`/OpenStatus`}>
        <h4 className="ref-col-cell">{mrf.referenceNo}</h4>
      </Link>
    );
  } else {
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
          <Link to={`/EditRequisition/${mrf.mrfId}`}>
            <h4 className="ref-col-cell">{mrf.referenceNo}</h4>
          </Link>
        );
      
      }
 }
   
};
  export default referenceBodyTemplate;