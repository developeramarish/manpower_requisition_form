import { variables } from "../Variables";

const baseUrl = variables.APP_API;
//'Dashboard/GetMrfResumeSummary'
export const fetchData = async (endpoint) => {
  try {
    const response = await fetch(`${baseUrl}/${endpoint}`);
    const jsonData = await response.json();
    return jsonData;
  } catch (error) {
    console.error("Error fetching data:", error);
    throw error; // Propagate the error to the calling function
  }
};
