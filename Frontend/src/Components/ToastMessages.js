import React, { useRef, useImperativeHandle } from "react";
import { Toast } from "primereact/toast";

const ToastMessages = (props, ref) => {
  const toast = useRef(null);

  // Expose the showSuccessMessage function to the parent component
  useImperativeHandle(ref, () => ({
    showSuccessMessage: (message) => {
      toast.current.show({
        severity: "success",
        summary: "Success Message",
        detail:message,
      });
    },
    showConflictMessage: (message) => {
      toast.current.show({
        severity: "error",
        summary: "Conflict Message",
        detail: message, 
      });
    },
    showBadRequestMessage: (message) => {
      toast.current.show({
        severity: "error",
        summary: "Bad Request Message",
        detail: message,
      });
    },
  
  }));

  return (
    <div>
      <Toast ref={toast} />
    </div>
  );
};

export default React.forwardRef(ToastMessages);
