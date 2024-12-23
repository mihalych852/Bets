//(Authentication service)

import axios from "axios";
import { NavigateFunction, useNavigate } from 'react-router-dom';
import { urlUserServiceGetInfo, urlUserServiceLogin, urlUserServiceLogout, urlUserServiceRegister } from "../endpoints";

export const register = (username: string, email: string, password: string) => {
  return axios.post(urlUserServiceRegister, {
    username,
    email,
    password,
  });
};

export const login = (email: string, password: string) => {
  return axios
    .post(urlUserServiceLogin, {
      email,
      password,
    })
    .then((response) => {
      if (response.data.token) {
        localStorage.setItem("token", response.data.token);
        //localStorage.setItem("token", JSON.stringify(response.data));
        getUserInfo();
      }

      return response.data;
    })
    .catch((error) => console.error(error));
};

export const getUserInfo = () => {
  return axios
    .get(urlUserServiceGetInfo)
    .then((response) => {
        localStorage.setItem("user", JSON.stringify(response.data));
        console.log(response.data);
        window.location.reload();
      return response.data;
    })
    .catch((error) => console.error(error));
};

export const logout = async () => {
  try{
    // localStorage.removeItem("user");
    // localStorage.removeItem("token");
      const res = await axios({
      url: urlUserServiceLogout,
      method: "POST",
    })
    .then(() => {
      localStorage.removeItem("user");
      localStorage.removeItem("token");
      window.location.reload();
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
  const userStr = localStorage.getItem("user");  
  if (userStr) 
    return true;
  else
    return false;
};