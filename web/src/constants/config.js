export const APP_KEY = "MRF_v1.0";
//const APIPath="https://localhost:7128/api/";
const APIPath="https://10.22.11.101:90/api/";
export const API_URL = {
	
	MRF_STATUS_SUMMARY:
	`${APIPath}Dashboard/GetMrfStatusSummary`,
	RESUME_SUMMARY:
	`${APIPath}Dashboard/GetMrfResumeSummary/Count`,
	INTERVIEW_SUMMARY:
	`${APIPath}Dashboard/GetMrfInterviewSummary/Count`,
	INTERVIEW_SUMMARY_POPUP:
	`${APIPath}Mrfinterviewermap/GetInterviewDetails/GetInterviewDetails`,
	MRF_STATUS_POPUP:
	`${APIPath}Mrfdetail/GetMrfDetails/GetMrfDetails?`,
	MY_REQUISITION:
	`${APIPath}Mrfdetail/GetMrfDetails/GetMrfDetails`,
	RESUME_SUMMARY_POPUP: 
	`${APIPath}Mrfresumereviewermap/GetResumeStatusDetails/GetResumeStatusDetails?`,
	RESUME_SUMMARY_POST:
	 `${APIPath}Candidatedetail/Put/`,
	INTERVIEW_FEEDBACK: 
	`${APIPath}CandidateInterviewFeedback`,
	MRF_PARTIAL_STATUS_UPDATE:
	`${APIPath}Mrfdetail/PartialUpdateMRFStatus/`,
	GET_CREATE_REQUISITION_DEPARTMENT:
	`${APIPath}Subdepartment/GetInfo/`,
	GET_CREATE_REQUISITION_DEATILS:
	`${APIPath}Mrfdetail/GetRequisition/`,
	GET_CREATE_REQUISITION_DROPDOWN:
	`${APIPath}Mrfdetail/GetMRFDropdownlist`,
	
};

export const FILE_URL = {
	RESUME: `${APIPath}Resume`,
	ASSIGNMENT: `${APIPath}Assignment`,

};

export const ROUTES = {
	/* DASHBOARD: "dashboard",
    CREATE_REQUISITION: "create_requisition",
    MY_REQUISITION: "my_requisition" */
	dashboard: "dashboard",
	my_requisition: "my_requisition",
	create_requisition: "create_requisition",
	edit_requisition: "edit_requisition",
	add_candidate: "add_candidate",
};

export const MRF_STATUS = {
	draft: 1,
  submToHr: 2,
  resubReq: 3,
  hodapproval: 4,
  cooapproval: 5,
  open: 6,
  onHold: 7,
  rejected: 8,
  withdrawn: 9,
  closed: 10,
  awaitHodApproval:11,
  awaitCooApproval:12,
  mrfTransferToNew:12,
};


export const ROLES = {
	superAdmin: 1,
	admin: 2,
	mrfOwner: 3,
	hr: 4,
	resumeReviwer: 5,
	interviewer: 6,
	hiringManager: 7,
};
