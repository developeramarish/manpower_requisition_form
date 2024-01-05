// MrfPartialStatusComponents.js
import MrfPartialStatus from "./MrfPartialStatus";
export const renderMrfPartialStatus = (statusId, label, message, formData, roleID, disabled,updateClick=false,textbox=false) => (
    <MrfPartialStatus
      mrfId={formData.getReqId}
      mrfStatusId={statusId}
      label={label}
      message={message}
      formData={formData}
      roleID={roleID}
      disabled={disabled}
      updatedClick={updateClick}
      textbox={textbox}
    />
  );
  