import SearchHeader from "./../components/SearchHeader";
import CreateRequisitionBody from "../containers/CreateRequistionBody";
import "../css/CreateRequistion.css";

export default function CreateRequisition({reqId=null,reqRoleId,reqstatus=null,roleId=null,reqstatusId=null}) {
  const title=reqstatus;
  return (
    <>
      <div className=" create_requistion">
        <div className=" create_requistn_section ">

        {title ?  <h3 className="create_requistion_title">MRF Details</h3> : <h3 className="create_requistion_title">Create Requisition</h3>}

          <CreateRequisitionBody getReqId={reqId} getReqRoleId={reqRoleId} roleId={roleId} status={reqstatus} mrfStatusId={reqstatusId}/>
        </div>
      </div>
    </>
  );
}
