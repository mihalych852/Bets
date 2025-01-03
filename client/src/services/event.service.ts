import axios from "axios";
import { urlOutComeCreate } from "../endpoints";


export const createOutcome = (description: string, eventId: string, createdBy: string) => {
  console.log(eventId, createdBy, description);
  return axios
    .post(urlOutComeCreate, {
        description, 
        eventId,
        createdBy
    })
    .then((response) => {
      console.log(response);
      return response.data;
    })
    .catch((error) => console.error(error));
};