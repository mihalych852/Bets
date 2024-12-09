import IndexBets from "./pages/IndexBets";
import CreateEvent from "./pages/Events/CreateEvent";
import EditEvents from "./pages/Events/EditEvent";
import AdministativePanel from "./pages/Events/AdministativePanel";

const routes = [
    //Events
    {path:'/events/create', element: <CreateEvent/>}, 
    {path:'/events/:id', element: <EditEvents/>},
    {path:'/events', element: <AdministativePanel/>},
    
    //Bets
    {path:'/bets', element: <IndexBets/>},
    
    //Login+Logout?
    
    {path:'*', element: <IndexBets/>}
];

export default routes;