//const baseURL = process.env.REACT_APP_API_URL;
// const baseURL = 'https://localhost:7127';
// const baseURLWallet = 'https://localhost:7011';
// const baseURLUser = 'http://localhost:5000';
const baseURL = 'https://localhost:5055/Bets/bets';
const baseURLWallet = 'https://localhost:5055/Bets/wallet';
const baseURLUser = 'http://localhost:5055/Bets/user';

const baseURLApiGateway = 'http://localhost:5055/Bets';

//Ссылки на контроллеры в api, используются в axios
export const urlEvents = `${baseURL}/api/events`;
export const urlEventsCreate = `${baseURL}/api/events/create`;
export const urlEventsEdit = `${baseURL}/api/events/update`;
export const urlEventsGetById = `${baseURL}/api/events/`;

export const urlOutComeCreate = `${baseURL}/api/eventOutcomes/create`;

export const urlBetCreate = `${baseURL}/Bets/create`;
export const urlBetGetForUser = `${baseURL}/Bets/forBettor/`;

export const urlWalletAdd = `${baseURLWallet}/Wallets/CreditAsync`;
export const urlWalletGetBalance = `${baseURLWallet}/Wallets/balance/`;
export const urlWalletDebit = `${baseURLWallet}/Wallets/DebitAsync`;


export const urlUserServiceLogin = `${baseURLUser}/api/v1/Auth/login`;
export const urlUserServiceRegister = `${baseURLUser}/api/v1/Auth/signup`;
export const urlUserServiceLogout = `${baseURLUser}/api/v1/Auth/logout`;
export const urlUserServiceGetInfo = `${baseURLUser}/api/v1/Auth/GetUserInfo`;
