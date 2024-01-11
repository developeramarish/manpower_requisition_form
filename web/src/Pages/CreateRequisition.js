import SearchHeader from "./../components/SearchHeader";
import CreateRequisitionBody from "./CreateRequisitionBody";

export default function CreateRequisition({reqId=null,reqRoleId,reqstatus=null,roleId=null,reqstatusId=null}) {
  // const { reqId } = useParams();
  // console.log(reqId)
  // console.log(reqstatus)
  // console.log(reqRoleId)
  // console.log(roleId)
  const title=reqstatus;
  return (
    <>
      <div className="flex bg-gray-200">
        <div className="flex flex-column gap-2 w-full p-3 py-2 h-full ">
        
  
        {title ? <SearchHeader title="MRF Details" /> :<SearchHeader title="Create Requisition"/>}

          <CreateRequisitionBody getReqId={reqId} getReqRoleId={reqRoleId} roleId={roleId} status={reqstatus} mrfStatusId={reqstatusId}/>
        </div>
      </div>
    </>
  );
}
