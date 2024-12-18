import axios from "axios";
import { NavigateFunction, useNavigate } from 'react-router-dom';
import { urlOutComeCreate } from "../endpoints";
import { getCurrentUser } from "./auth.service";


export const createOutcome = (description: string, eventId: string) => {
    const user = getCurrentUser();
    const createBy = user.email;
  return axios
    .post(urlOutComeCreate, {
        description, 
        eventId,
        createBy
    })
    .then((response) => {
      console.log(response);
      return response.data;
    })
    .catch((error) => console.error(error));
};