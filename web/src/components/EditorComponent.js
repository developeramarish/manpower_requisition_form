import { Editor } from "primereact/editor";
import React, { useEffect, useState } from "react";

const EditorComponent = ({
  value,
  onTextChanged,
  autoResize ,
  headerTemplate,
  disable,
  max
}) => {
  const [text, setText] = useState("");
  const onUpdate = (e) => {
    // console.log(e);
    if(e && e.length<=max){
     
      setText(e);
    }else{
      return;
    }
    
  };
  useEffect(() => {
    onTextChanged(text);
  }, [text]);
  return (
    <Editor
      value={value}
      autoResize={autoResize}
      headerTemplate={headerTemplate}
      readOnly={disable}
      onTextChange={(e) => onUpdate(e.htmlValue)}
      style={{ height: '170px' }}
      max={max}
    />
  );
};

export default EditorComponent;
