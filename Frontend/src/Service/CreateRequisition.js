import { variables } from '../Variables';
const baseUrl = variables.APP_API;

export const postData = async (endpoint, data) => {
    try {
      const response = await fetch(`${baseUrl}/${endpoint}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
      });
      const jsonData = await response.json();
      return jsonData;
    } catch (error) {
      console.error('Error making POST request:', error);
      throw error;
    }
  };