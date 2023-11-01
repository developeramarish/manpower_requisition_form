import React, { useRef, useImperativeHandle } from "react";
import { Toast } from "primereact/toast";

const ToastMessages = (props, ref) => {
  const toast = useRef(null);

  // Expose the showSuccessMessage function to the parent component
  useImperativeHandle(ref, () => ({
    showSuccessMessage: () => {
      toast.current.show({
        severity: "success",
        summary: "Success Message",
        detail: "Form submitted successfully!",
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
