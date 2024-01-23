import { useDispatch } from "react-redux";
import { PAGE_ACTIONS } from "../reducers/Page_r";
import { navigateTo } from "../constants/Utils";
import "../css/MrfRefStatus.css";

const MrfLink = ({ mrfRef, mrfId = null, status = null, role = null ,statusId=null }) => {
  // const [visible, setVisible] = useState(false);
  const dispatch = useDispatch();

  // console.log(message)
  const handleAClick = (id, status, role,statusId) => {
    dispatch(
      PAGE_ACTIONS.setParams({
        params: { id: id, statusForTitle: status, roleId: role, mrfstatusId:statusId},
        // statusForTitle: status,
        // roleId: role,
        // statusId
      })
    );
    // console.log(params)
    navigateTo("edit_requisition");
  };

  return (
    <div className="mrf-ref-cell">
      <button
        onClick={(e) => handleAClick(mrfId, status, role,statusId)}
        className="mrf-ref-link"
      >
        {mrfRef}
      </button>
    </div>
  );
};

const ReferenceBodyTemplate = (mrf) => {
  // const roleId = storageService.getData("profile").roleId;
  // const roleId = mrf.roleId;
  const mrfRef = mrf.referenceNo;

return (
  <MrfLink
    mrfRef={mrfRef}
    mrfId={mrf.mrfId}
    role={mrf.roleId}
    status={mrf.mrfStatus}
    statusId={mrf.mrfStatusId}
  />
);
  
};

export default ReferenceBodyTemplate;
