import IndexBets from "./bets/IndexBets";
import CreateEvent from "./events/CreateEvent";
import IndexEvents from "./events/IndexEvents";

const routes = [
    //Events
    {path:'/events/create', element: <CreateEvent/>}, 
    {path:'/events', element: <IndexEvents/>},
    
    //Bets
    {path:'/bets', element: <IndexEvents/>},
    
    //Login+Logout?
    
    {path:'*', element: <IndexBets/>}
];

export default routes;