import React, {
  forwardRef,
  useImperativeHandle,
  useRef,
  useState,
} from "react";
import ToastMessages from "./ToastMessages";
import { FileUpload } from "primereact/fileupload";

const FileUploads = forwardRef((props, ref) => {
  useImperativeHandle(ref, () => ({
    clearSelectedFile() {
     
      if (fileInputRef.current) {
        fileInputRef.current.clear();
      }
      props.onChange(null); // Notify parent component about file reset
    },
  }));

  const fileInputRef = useRef(null);
  const toastRef = useRef(null);

  const onFileSelect = (event) => {
    const selectedFile =
      event.files && event.files.length > 0 ? event.files[0] : null;

    if (selectedFile) {
      const allowedExtension = props.fileExtension
        .split(",")
        .map((ext) => ext.trim().toLowerCase());
      const selectedfileExtension = selectedFile.name
        .split(".")
        .pop()
        .toLowerCase();
      const fileExtensionIsValid = allowedExtension.includes(
        selectedfileExtension
      );

      if (fileExtensionIsValid) {
        props.onChange(selectedFile);
      } else {
        toastRef.current.showWarrningMessage(
          `Please select a file with following extension ${props.fileExtension}`
        );
        fileInputRef.current.clear();
      }
    } else {
      toastRef.current.showWarrningMessage(`Please select a file.`);
    }

  };

  return (
    <div className="fileupload">
      <FileUpload
        ref={fileInputRef}
        mode={"basic"}
        accept={props.fileExtension}
        onSelect={onFileSelect}
        disabled={props.disable}
      />
      <ToastMessages ref={toastRef} />
    </div>
  );
});

export default FileUploads;
