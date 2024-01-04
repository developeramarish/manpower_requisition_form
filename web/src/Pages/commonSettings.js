import { MRF_STATUS } from "../constants/config";

export const commonSettings = {
  setReadOnly: true,
  setHiringManager: true,
  setSiteHRSPOCApproval: true,
  setHodapproval: true,
  setCooapproval:true,
  setFinanceHeadApproval: true,
};

export function applySettingsBasedOnRoleAndStatus(getReqRoleId, mrfStatusId, roleId) {
  switch (getReqRoleId) {
    case 3: /* MRFOwner */
      //if (mrfStatusId === MRF_STATUS.draft || mrfStatusId === MRF_STATUS.resubReq) {
        applyCommonSettings({
          setReadOnly: false,
          
        });
      //} else {
      //  applyCommonSettings({commonSettings });
      //}
      break;
    case 4:
      switch (mrfStatusId) {
        case MRF_STATUS.submToHr:
        case MRF_STATUS.awaitHodApproval:
        case MRF_STATUS.awaitCooApproval:
        case MRF_STATUS.awaitfinanceHeadApproval:
          applyCommonSettings({
            setReadOnly: true,
            setHodapproval: false,
            setHiringManager: false,
            setSiteHRSPOCApproval: false,
            setFinanceHeadApproval: false,
            setCooapproval: true
          });
          break;
        default:
          applyCommonSettings(commonSettings);
      }
      break;
    
    default:
      if (roleId === 3) {
        applyCommonSettings(commonSettings);
      }
  }
}

function applyCommonSettings(settings) {
  Object.assign(commonSettings, settings);
}
