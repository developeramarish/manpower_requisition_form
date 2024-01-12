import { MRF_STATUS } from "../constants/config";

export const commonSettings = {
  setReadOnly: true,
  setHiringManager: true,
  setSiteHRSPOCApproval: true,
  setHodapprovalName: true,
  setHodapprovalDate: true,
  setCooapprovalName: true,
  setCooapprovalDate: true,
  setFinanceHeadApprovalName: true,
  setFinanceHeadApprovalDate: true,
};

export function applySettingsBasedOnRoleAndStatus(
  getReqRoleId,
  mrfStatusId,
  roleId
) {
  
  switch (getReqRoleId) {
    case 3 /* MRFOwner */:
      if (
        mrfStatusId === MRF_STATUS.draft ||
        mrfStatusId === MRF_STATUS.resubReq
      ) {
        applyCommonSettings({
          setReadOnly: false,
          setHiringManager: true,
          setSiteHRSPOCApproval: true,
          setHodapprovalName: true,
          setHodapprovalDate: true,
          setCooapprovalName: true,
          setCooapprovalDate: true,
          setFinanceHeadApprovalName: true,
          setFinanceHeadApprovalDate: true,
          awatingFinance: true,
          recievedFinance: true,
        });
      } else {
        applyCommonSettings({
          setReadOnly: true,
          setHiringManager: true,
          setSiteHRSPOCApproval: true,
          setHodapprovalName: true,
          setHodapprovalDate: true,
          setCooapprovalName: true,
          setCooapprovalDate: true,
          setFinanceHeadApprovalName: true,
          setFinanceHeadApprovalDate: true,
          awatingFinance: true,
          recievedFinance: true,
        });
      }
      break;
    case 4: //HR
      if (mrfStatusId == MRF_STATUS.submToHr) {
        applyCommonSettings({
          setReadOnly: true,
          setHiringManager: false,
          setSiteHRSPOCApproval: false,
          setHodapprovalName: false,
          setHodapprovalDate: true,
          setCooapprovalName: true,
          setCooapprovalDate: true,
          setFinanceHeadApprovalName: true,
          setFinanceHeadApprovalDate: true,
          awatingFinance: true,
          recievedFinance: true,
        });
      } else if (mrfStatusId == MRF_STATUS.awaitHodApproval) {
        applyCommonSettings({
          setReadOnly: true,
          setHiringManager: false,
          setSiteHRSPOCApproval: false,
          setHodapprovalDate: false,
          setHodapprovalName: true,
          setCooapprovalName: true,
          setCooapprovalDate: true,
          setFinanceHeadApprovalName: true,
          setFinanceHeadApprovalDate: true,
          awatingFinance: true,
          recievedFinance: true,
        });
      } else if (mrfStatusId == MRF_STATUS.hodapproval) {
        applyCommonSettings({
          setReadOnly: true,
          setHiringManager: false,
          setSiteHRSPOCApproval: false,
          setHodapprovalName: true,
          setHodapprovalDate: true,
          setCooapprovalName: true,
          setCooapprovalDate: true,
          setFinanceHeadApprovalName: false,
          setFinanceHeadApprovalDate: true,
          awatingFinance: true,
          recievedFinance: true,
        });
      } else if (mrfStatusId == MRF_STATUS.awaitCooApproval) {
        applyCommonSettings({
          setReadOnly: true,
          setHiringManager: false,
          setSiteHRSPOCApproval: false,
          setHodapprovalName: true,
          setHodapprovalDate: true,
          setCooapprovalName: true,
          setCooapprovalDate: false,
          setFinanceHeadApprovalName: true,
          setFinanceHeadApprovalDate: true,
          awatingFinance: true,
          recievedFinance: true,
        });
      } else if (mrfStatusId == MRF_STATUS.cooapproval) {
        applyCommonSettings({
          setReadOnly: true,
          setHiringManager: true,
          setSiteHRSPOCApproval: true,
          setHodapprovalName: true,
          setHodapprovalDate: true,
          setCooapprovalName: true,
          setCooapprovalDate: true,
          setFinanceHeadApprovalName: true,
          setFinanceHeadApprovalDate: true,
          awatingFinance: true,
          recievedFinance: true,
        });
      } else if (mrfStatusId == MRF_STATUS.awaitfinanceHeadApproval) {
        applyCommonSettings({
          setReadOnly: true,
          setHiringManager: false,
          setSiteHRSPOCApproval: false,
          setHodapprovalName: true,
          setHodapprovalDate: true,
          setCooapprovalName: true,
          setCooapprovalDate: true,
          setFinanceHeadApprovalName: true,
          setFinanceHeadApprovalDate: false,
        });
      } else if (mrfStatusId == MRF_STATUS.recivedfinanceHeadApproval) {
        applyCommonSettings({
          setReadOnly: true,
          setHiringManager: false,
          setSiteHRSPOCApproval: false,
          setHodapprovalName: true,
          setHodapprovalDate: true,
          setCooapprovalName: false,
          setCooapprovalDate: true,
          setFinanceHeadApprovalName: true,
          setFinanceHeadApprovalDate: true,
        });
      }else if (mrfStatusId == MRF_STATUS.bypassFinanceHeadApproval) {
        applyCommonSettings({
          setReadOnly: true,
          setHiringManager: false,
          setSiteHRSPOCApproval: false,
          setHodapprovalName: true,
          setHodapprovalDate: true,
          setCooapprovalName: false,
          setCooapprovalDate: true,
          setFinanceHeadApprovalName: true,
          setFinanceHeadApprovalDate: true,
        });
      }else {
        applyCommonSettings({ setReadOnly: true,
          setHiringManager: true,
          setSiteHRSPOCApproval: true,
          setHodapprovalName: true,
          setHodapprovalDate: true,
          setCooapprovalName: true,
          setCooapprovalDate: true,
          setFinanceHeadApprovalName: true,
          setFinanceHeadApprovalDate: true,});
      }

    default:
      if (roleId === 3) {
        // applyCommonSettings(commonSettings);
        applyCommonSettings({ setReadOnly: false });
        break;
      }
  }
}

function applyCommonSettings(settings) {
  Object.assign(commonSettings, settings);
}
