
const storageService = {
    get: (key) => {
      const value = localStorage.getItem(key);
      return JSON.parse(value);
    },
    set: (key, value) => {
      localStorage.setItem(key, JSON.stringify(value));
    },
    // Additional methods for sessionStorage or other storage mechanisms
  };
  
  export default storageService;
  