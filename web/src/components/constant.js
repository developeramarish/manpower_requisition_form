// export const APIPath="https://localhost:7128/api/";
export const APIPath = "https://10.22.11.101:90/api/";
export const constantResumePath = "https://10.22.11.101:90/Resume/";
export const constantAssignmentPath = "https://10.22.11.101:90/Assignment/";

export const multiSoftwareSkill = [
  { name: "Visual Studio", code: "Visual Studio" },
  { name: "MS Teams", code: "MS Teams" },
  { name: "MS Office", code: "MS Office" },
  // Add more options as needed
];
export const multiHardwareSkill = [
  { name: "Laptop", code: "Laptop" },
  { name: "Headset", code: "Headset" },
  { name: "Keyboard", code: "Keyboard" },
  { name: "Mouse", code: "Mouse" },
  // Add more options as needed
];

export const mrfStatus = {
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
};

export const Gender = [
  { label: "Male", id: 1 },
  { label: "Female", id: 2 },
  { label: "Other", id: 3 },
];
export const minExperienceOptions = Array.from({ length: 31 }, (_, i) => ({
  label: i.toString(),
  value: i,
}));
export const maxExperienceOptions = Array.from({ length: 31 }, (_, i) => ({
  label: i.toString(),
  value: i,
}));

export const RequisitionType = [
  { name: "FR", code: "FR" },
  { name: "RP", code: "RP" },

  // Add more options as needed
];

export const removeSpaces = (str) => {
  return str.replace(/\s/g, ""); // This regular expression replaces all spaces globally
};
