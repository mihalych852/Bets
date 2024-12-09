//const baseURL = process.env.REACT_APP_API_URL;
const baseURL = 'https://localhost:7127';

//Ссылки на контроллеры в api, используются в axios
export const urlEvents = `${baseURL}/api/events`;
export const urlEventsCreate = `${baseURL}/api/events/create`;
export const urlEventsEdit = `${baseURL}/api/events/update`;
export const urlEventsGetById = `${baseURL}/api/events/`;

export const urlBetCreate = `${baseURL}/Bets/create`;


export const urlLogin = `${baseURL}/user`;
