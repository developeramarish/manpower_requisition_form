// RequisitionButtons.js
import React from 'react';
import ButtonC from "./../components/Button";
import { useDispatch } from "react-redux";
import { PAGE_ACTIONS } from "../reducers/Page_r";



export const CancelButton = ({ handleCancel, disable }) => (
    <ButtonC
      label="CANCEL"
      className="w-2 surface-hover border-red-600 text-red-600"
      onClick={handleCancel}
      outlined="true"
      disabled={disable}
    />
  );
  
  export const AddResumeButton = ({ dispatch, navigateTo, getReqId, formData }) => (
    <>
      
        <ButtonC
          label="Add Resume"
          className="w-2 bg-red-600 border-red-600"
          onClick={() => {
            dispatch(
              PAGE_ACTIONS.setParams({
                params: {
                  mrfId: getReqId,
                  referenceNo: formData.referenceNo,
                },
              })
            );
            navigateTo("add_candidate");
          }}
        />
     
    </>
  );