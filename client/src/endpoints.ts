//const baseURL = process.env.REACT_APP_API_URL;
const baseURL = 'https://localhost:7127';
const baseURLWallet = 'https://localhost:7011';
const baseURLUser = 'http://localhost:5000';

//Ссылки на контроллеры в api, используются в axios
export const urlEvents = `${baseURL}/api/events`;
export const urlEventsCreate = `${baseURL}/api/events/create`;
export const urlEventsEdit = `${baseURL}/api/events/update`;
export const urlEventsGetById = `${baseURL}/api/events/`;

export const urlOutComeCreate = `${baseURL}/api/eventOutcomes/create`;

export const urlBetCreate = `${baseURL}/Bets/create`;

export const urlWalletAdd = `${baseURLWallet}/Wallets/CreditAsync`;
export const urlWalletGetBalance = `${baseURLWallet}/Wallets/balance`;
export const urlWalletDebit = `${baseURLWallet}/Wallets/DebitAsync`;


export const urlUserService = `${baseURLUser}/`;
