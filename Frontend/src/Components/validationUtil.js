// Validation function to check if the input is empty
export const isFieldEmpty = (value) => {
    return value.trim() === "";
  };
  
  // Validation function to check if the input is a valid email
  export const isValidEmail = (email) => {
    const emailRegex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i;
    return emailRegex.test(email);
  };
  
  // Validation function to check if the input is a valid number
  export const isValidNumber = (number) => {
    const numberRegex = /^[0-9]+$/;
    return numberRegex.test(number);
  };
  
  // Define error messages
  export const validationMessages = {
    emptyField: "This field is required.",
    invalidEmail: "Invalid email format.",
    invalidNumber: "Please enter a valid number.",
  };
  