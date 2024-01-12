import { useEffect, useState,useRef } from "react";
import { Dialog } from 'primereact/dialog';
import DropdownComponent from "./../components/Dropdown";
import "../css/InterviewFeedback.css";
import { postData,getData } from "../constants/Utils";
import InputTextareaComponent from "./../components/InputTextarea";
import { API_URL } from "../constants/config";
import ButtonC from "./../components/Button";
import { storageService } from "../constants/storage";
import ToastMessages from "./../components/ToastMessages";
const FeedbackForm = ({ visible, onHide, onSubmit, count,candidateId=null })=> {
  const [interviewRound, setRound] = useState('');
  const [evaluationFeedBackId,setevaluationFeedBackId] = useState('');
  const [comments,Setcomments] = useState('');
  const [FeedData, setFeedData] = useState();
  const roundLabels = ['Round 1', 'Round 2', 'Round 3'];
  const interviewDetailsData = {
    //id,
    candidateId,
    evaluationFeedBackId,
    interviewRound,
    comments,
    //createdByEmployeeId,
    //createdOnUtc,
    //updatedByEmployeeId,
    //updatedOnUtc
  };
  
  const [formData, setFormData] = useState({
    ...interviewDetailsData
  });
  
  const RedAsterisk = () => <span className="text-red-500">*</span>;
  const toastRef = useRef(null);
  const onLoad = () => {
    fetch(API_URL.INTERVIEW_FEEDBACK_MASTER)
    .then((response) => {
        console.log(response);
            return response.json();
          })
          .then((json) => {
            setFeedData(json["result"]);
            
          })
    
          .catch((error) => console.log(error));
      
};

useEffect(() => {
    onLoad();
}, [candidateId]);


  const handleSubmit = (e) => {
    e.preventDefault();
    
    //onSubmit({ round1, round2, round3, comments });
  };

      
  
	  const AddForm = async (data) => {
		console.log(data);
        const id= data.id;
        const candidateId=candidateId;
        const evaluationFeedBackId=0;
        const interviewRound= interviewRound;
        const comments= data.comments;
        const createdByEmployeeId=storageService.getData("profile").employeeId;
        const createdOnUtc=new Date().toISOString();
        const	updatedByEmployeeId=storageService.getData("profile").employeeId;
		const	updatedOnUtc=new Date().toISOString();

		try {
		
		let response = await postData(`${API_URL.INTERVIEW_FEEDBACK_POST}`,interviewDetailsData);
		
		  if (response.ok) {
			const responseData = response.json();
			if (responseData.statusCode === 409) {
			  toastRef.current.showConflictMessage(responseData.message);
			} else {
				
			  toastRef.current.showSuccessMessage(
				"Interview Feedback updated successfully!"
			  );
			}
		  } else {
			console.error("Request failed with status:", response.status);
			const errorData = await response.text();
			console.error("Error Data:", errorData);
			if (response.status === 400) {
			  toastRef.current.showBadRequestMessage(
				"Bad request: " + response.url
			  );
			}
		  }
		} catch (error) {
		  console.error("Error:", error);
		}
	  };

  return (
    <Dialog header="Interview Feedback" visible={visible} onHide={onHide} draggable={false} className="feed-popup">
	<form>
      {roundLabels.slice(count - 1).map((roundLabel, index) => (
        <div key={index} className="flex gap-2 dvinputFeedback">
          <label htmlFor={`round${index + count}`}>{roundLabel}:</label>
          <label htmlFor="FeedbackType" className="font-bold text-sm">
            Feedback Type <RedAsterisk />
          </label>
          <DropdownComponent
            id={`dround${index + count}`}
            optionLabel="description"
            optionValue="id"
            type="evaluationFeedBack"
            className="dropdown-custom"
            options={FeedData}
            value={formData.evaluationFeedBackId}
            onChange={(e) =>
              setFormData({
                ...formData,
                evaluationFeedBackId: e.target.value,
              })
            }
          />
          <label htmlFor={`round${index + count}`} className="font-bold text-sm">
            Comments <RedAsterisk />
          </label>
          <InputTextareaComponent
            id={`round${index + count}`}
            value={interviewRound[index + count - 1]}
            onChange={(e) => setRound((prevRound) => [...prevRound.slice(0, index + count - 1), e.target.value, ...prevRound.slice(index + count)])
            }
            className="inputRound bg-gray-100"
            rows={2}
            cols={60}
          />
        </div>
      ))}
      <div className="dvAddFeedback">
        <ButtonC label="Submit Feedback" className="w-15 bg-red-600 border-red-600 BtnAddFeedback" outlined="true" />
      </div>
      <ToastMessages ref={toastRef} />
    </form>
	</Dialog>
  );
};

export default FeedbackForm;
