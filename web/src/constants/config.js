export const APP_KEY = "MRF_v1.0";
const APIPath="https://10.22.11.101:90/api/";
export const API_URL = {
	
	MRF_STATUS_SUMMARY:
		APIPath+"Dashboard/GetMrfStatusSummary",
	RESUME_SUMMARY:
		APIPath+"Dashboard/GetMrfResumeSummary/Count",
	INTERVIEW_SUMMARY:
		APIPath+"Dashboard/GetMrfInterviewSummary/Count",
	INTERVIEW_SUMMARY_POPUP:
		APIPath+"Mrfinterviewermap/GetInterviewDetails/GetInterviewDetails",
	MRF_STATUS_POPUP:
		APIPath+"/Mrfdetail/GetMrfDetails/GetMrfDetails?",
	MY_REQUISITION:
		APIPath+"Mrfdetail/GetMrfDetails/GetMrfDetails",
	RESUME_SUMMARY_POPUP: APIPath+`Mrfresumereviewermap/GetResumeStatusDetails/GetResumeStatusDetails?`,
	RESUME_SUMMARY_POST: APIPath+`Candidatedetail/Put/`,
	INTERVIEW_FEEDBACK: APIPath+"CandidateInterviewFeedback",
};

export const FILE_URL = {
	RESUME: "https://10.22.11.101:90/Resume/",
	ASSIGNMENT: "https://10.22.11.101:90/Assignment/",
};

export const ROUTES = {
	/* DASHBOARD: "dashboard",
    CREATE_REQUISITION: "create_requisition",
    MY_REQUISITION: "my_requisition" */
	dashboard: "dashboard",
	my_requisition: "my_requisition",
	create_requisition: "create_requisition",
	edit_requisition: "edit_requisition",
};

export const MRF_STATUS = {
	draft: 2,
	submToHr: 3,
	resubReq: 4,
	open: 9,
	rejected: 11,
	closed: 13,
	withdrawn: 12,
	onHold: 10,
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
