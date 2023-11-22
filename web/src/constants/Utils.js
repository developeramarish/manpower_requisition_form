import { storageService } from "./storage";

/**** 
    Description: Sets application state for "currentDevice" depending on screen width. The "currentDevice" state gets added on "App" element which helps to apply css for different resolution.

     case 1: screen max-width = 767px "isMobile" is set.
    case 2: screen max-width = 1024px "isIpad" is set.
    default: screen width greater then 1024px "isDesktop" is set.      
****/
export const detectDevice = () => {
    if (window.matchMedia("(max-width: 767px)").matches) {
        return "mobile";
    } else if (window.matchMedia("(max-width: 1024px)").matches) {
        return "ipad";
    } else {
        return "desktop";
    }
}

/**** 
     Description: Sets the state "isTouchDevice" to "true" or "false" of the application.
****/
export const isTouchDevice = () => {
    if (window.matchMedia("(pointer: coarse)").matches) {
        return true;
        // console.log("Touch screen detected");
    }
    return false;
}

export async function getData (url) {
    const accessToken = storageService.getData('token');
    const headers = new Headers();
    const bearer = `Bearer ${accessToken}`;

    headers.append("Authorization", bearer);

    const options = {
        method: "GET",
        headers: headers
    };
    // console.log(accessToken);
    return fetch(url, options)
    .then(response => response.json())
    .catch(error => error);
}

export const getKeyFromLocation = () => {
    return window.location.hash.split("#/")[1];
  }

export const navigateTo = (sPageKey) =>{
    window.location.hash = "#/" + sPageKey;
}