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
