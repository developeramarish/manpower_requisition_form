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
        detail: message,
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
    showInfoMessage: (message) => {
      toast.current.show({
        severity: "info",
        summary: "Info Message",
        detail: message,
      });
    },
    showWarrningMessage: (message) => {
      toast.current.show({
        severity: "warn",
        summary: "Warning Message",
        detail: message,
      });
    },
  }));

  return (
    <div>
      <Toast ref={toast} position="bottom-center" />
    </div>
  );
};

export default React.forwardRef(ToastMessages);
