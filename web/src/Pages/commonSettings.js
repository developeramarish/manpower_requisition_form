import { MRF_STATUS } from "../constants/config";

export const commonSettings = {
  setReadOnly: false,
  setHiringManager: false,
  setSiteHRSPOCApproval: false,
  setHodapproval: false,
  setCooapproval: false,
  setFinanceHeadApproval: false,
  awatingFinance: false,
  recievedFinance: false,
};

export function applySettingsBasedOnRoleAndStatus(
  getReqRoleId,
  mrfStatusId,
  roleId
) {
  console.log(getReqRoleId);
  console.log(roleId);
  switch (getReqRoleId) {
    case 3 /* MRFOwner */:
      if (
        mrfStatusId === MRF_STATUS.draft ||
        mrfStatusId === MRF_STATUS.resubReq
      ) {
        applyCommonSettings({
          setReadOnly: false,
        });
      } else {
        applyCommonSettings({
          setReadOnly: true,
          setHodapproval: true,
          setHiringManager: true,
          setSiteHRSPOCApproval: true,
          setFinanceHeadApproval: true,
          setCooapproval: true,
          awatingFinance: true,
          recievedFinance: true,
        });
      }
      break;
    case 4: //HR
      if (mrfStatusId == MRF_STATUS.submToHr) {
        applyCommonSettings({
          setReadOnly: true,
          setHodapproval: false,
          setHiringManager: false,
          setSiteHRSPOCApproval: false,
          setFinanceHeadApproval: true,
          setCooapproval: true,
          awatingFinance: true,
          recievedFinance: true,
        });
      } else if (mrfStatusId == MRF_STATUS.awaitHodApproval) {
        applyCommonSettings({
          setReadOnly: true,
          setHodapproval: true,
          setHiringManager: false,
          setSiteHRSPOCApproval: false,
          setFinanceHeadApproval: true,
          setCooapproval: false,
          awatingFinance: true,
          recievedFinance: true,
        });
      } else if (mrfStatusId == MRF_STATUS.awaitCooApproval) {
        applyCommonSettings({
          setReadOnly: true,
          setHodapproval: true,
          setHiringManager: false,
          setSiteHRSPOCApproval: false,
          setFinanceHeadApproval: false,
          setCooapproval: true,
          awatingFinance: true,
          recievedFinance: true,
        });
      } else if (mrfStatusId == MRF_STATUS.awaitfinanceHeadApproval) {
        applyCommonSettings({
          setReadOnly: true,
          setHodapproval: true,
          setHiringManager: false,
          setSiteHRSPOCApproval: false,
          setFinanceHeadApproval: true,
          setCooapproval: true,
          awatingFinance: false,
          recievedFinance: true,
        });
      } else {
        applyCommonSettings({
          setReadOnly: true,
          setHodapproval: true,
          setHiringManager: true,
          setSiteHRSPOCApproval: true,
          setFinanceHeadApproval: true,
          setCooapproval: true,
          awatingFinance: true,
          recievedFinance: true,
        });
      }

    default:
      if (roleId === 3) {
        // applyCommonSettings(commonSettings);
        applyCommonSettings({
          setReadOnly: false,
          setHodapproval: true,
          setHiringManager: true,
          setSiteHRSPOCApproval: true,
          setFinanceHeadApproval: true,
          setCooapproval: true,
          awatingFinance: true,
          recievedFinance: true,
        });
        break;
      }
  }
}

function applyCommonSettings(settings) {
  Object.assign(commonSettings, settings);
}
