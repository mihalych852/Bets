//const baseURL = process.env.REACT_APP_API_URL;
// const baseURL = 'https://localhost:7127';
// const baseURLWallet = 'https://localhost:7011';
// const baseURLUser = 'http://localhost:5000';
 const baseURL = 'http://localhost:5055/bets';
 const baseURLWallet = 'http://localhost:5055/bets/wallet';
 const baseURLUser = 'http://localhost:5055/bets/user';

// const baseURLApiGateway = 'http://localhost:5055/bets';

//const baseURL = 'http://84.201.143.35:5055/bets';
//const baseURLWallet = 'http://84.201.143.35:5055/bets/wallet';
//const baseURLUser = 'http://84.201.143.35:5055/bets/user';

const baseURLApiGateway = 'http://localhost:5055/bets';

//Ссылки на контроллеры в api, используются в axios
export const urlEvents = `${baseURL}/bets/GetAllEvents`;
export const urlEventsCreate = `${baseURL}/bets/AddEvent`;
export const urlEventsEdit = `${baseURL}/bets/UpdateEvent`;
export const urlEventsGetById = `${baseURL}/bets/GetEventById`;

export const urlOutComeCreate = `${baseURL}/bets/AddEventOutcome`;

export const urlBetCreate = `${baseURL}/bets/AddBet`;
export const urlBetGetForUser = `${baseURL}/bets/GetBetsByBettorId/`;

export const urlWalletAdd = `${baseURLWallet}/Credit`;
export const urlWalletGetBalance = `${baseURLWallet}/Balance`;
export const urlWalletDebit = `${baseURLWallet}/Debit`;


export const urlUserServiceLogin = `${baseURLUser}/bets/user/Login`;
export const urlUserServiceRegister = `${baseURLUser}/RegisterUser`;
export const urlUserServiceLogout = `${baseURLUser}/Logout`;
export const urlUserServiceGetInfo = `${baseURLUser}/GetUserInfo`;
