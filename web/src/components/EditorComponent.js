import { Editor } from "primereact/editor";
import { Knob } from "primereact/knob";
import React, { useEffect, useRef, useState } from "react";
import ToastMessages from "./ToastMessages";

const EditorComponent = ({
  value,
  onTextChanged,
  autoResize,
  headerTemplate,
  disable,
  max,
}) => {
  const toastRef = useRef(null);
  const [orgText, setOrgText] = useState({ txt: "", html: "" });
  const [remainingChars, setRemainingChars] = useState(max);
  const onUpdate = (e) => {
    var oTarget = e.target ? e.target : e,
      txtValue = oTarget.innerText,
      htmlValue = oTarget.innerHTML;

    if (txtValue.length > max) {
      oTarget.innerHTML = orgText.html;
      toastRef.current.showWarrningMessage("Character limit Exceed: "+max);
      return;
    }
    if (txtValue === "" || htmlValue === "<p><br></p>") {
      oTarget.innerHTML = "";
      htmlValue = "";
      txtValue = "";
    }
    setOrgText({ txt: txtValue, html: htmlValue });
    onTextChanged({ text: txtValue, htmlText: htmlValue });
    setRemainingChars(max - txtValue.length);
  };

  return (
    <div
      style={{
        position: "relative",
        // width:"685px"
      }}
    >
      <Editor
        value={value}
        autoResize={autoResize}
        headerTemplate={headerTemplate}
        readOnly={disable}
        onKeyUp={(e) => onUpdate(e)}
        onLoad={(e) => onUpdate(e.root)}
        style={{ height: "220px" }}
        max={max}
      />
      <div
        style={{
          position: "absolute",
          bottom: "7px",
          right: "10px",
          zIndex: "100",
          textAlign: "right",
        }}
      >
        <Knob
          value={max - remainingChars}
          max={max}
          readOnly
          size={50}
          rangeColor="#aaaaaa"
          valueColor="#d32f2e"
          textColor="#000000"
        />
      </div>
      <ToastMessages ref={toastRef} />
    </div>
  );
};

export default EditorComponent;
