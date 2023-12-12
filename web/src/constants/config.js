export const APP_KEY = "MRF_v1.0";

export const API_URL = {
    MRF_STATUS_SUMMARY: "https://10.22.11.101:90/api/Dashboard/GetMrfStatusSummary",
    RESUME_SUMMARY: "https://10.22.11.101:90/api/Dashboard/GetMrfResumeSummary/Count",
    INTERVIEW_SUMMARY: "https://10.22.11.101:90/api/Dashboard/GetMrfInterviewSummary/Count"
}

export const ROUTES = {
    /* DASHBOARD: "dashboard",
    CREATE_REQUISITION: "create_requisition",
    MY_REQUISITION: "my_requisition" */
    dashboard: "dashboard",
    my_requisition: "my_requisition",
    create_requisition: "create_requisition",
    edit_requisition: "edit_requisition"
}