//(Authentication service)

import axios from "axios";
import { NavigateFunction, useNavigate } from 'react-router-dom';
import { urlUserService } from "../endpoints";


//const API_URL = "http://localhost:5000/api/v1/Auth/";
const API_URL = urlUserService + "api/v1/Auth/";

export const register = (username: string, email: string, password: string) => {
  return axios.post(API_URL + "signup", {
    username,
    email,
    password,
  });
};

export const login = (email: string, password: string) => {
  return axios
    .post(API_URL + "login", {
      email,
      password,
    })
    .then((response) => {
      if (response.data.token) {
        localStorage.setItem("jwt", response.data.token);
        localStorage.setItem("token", JSON.stringify(response.data));
        getUserInfo();
      }

      return response.data;
    })
    .catch((error) => console.error(error));
};

const getUserInfo = () => {
  return axios
    .get(API_URL + "GetUserInfo")
    .then((response) => {
        localStorage.setItem("user", JSON.stringify(response.data));
      return response.data;
    })
    .catch((error) => console.error(error));
};

export const logout = async () => {
  try{
      const res = await axios({
      url: API_URL + "logout",
      method: "POST",
    })
    .then(() => {
        localStorage.removeItem("user");
        localStorage.removeItem("jwt");
        //window.location.reload();
    })
    .catch((error) => console.error(error));
  } catch(error){
    console.log(error);
  }
};

export const getCurrentUser = () => {
  const userStr = localStorage.getItem("user");
  if (userStr){

    return JSON.parse(userStr);
  } 

  return null;
};

export const isUserLoggedIn = () => {
  const userStr = localStorage.getItem("token");  
  if (userStr) 
    return true;
  else
    return false;
};