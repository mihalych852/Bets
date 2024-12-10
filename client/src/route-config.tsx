import IndexBets from "./pages/IndexBets";
import CreateEvent from "./pages/Events/CreateEvent";
import EditEvents from "./pages/Events/EditEvent";
import AdministativePanel from "./pages/Events/AdministativePanel";
import Home from "./components/Home";
import Login from "./components/Login";
import Register from "./components/Register";
import Profile from "./components/Profile";

const routes = [
    //Events
    {path:'/events/create', element: <CreateEvent/>}, 
    {path:'/events/:id', element: <EditEvents/>},
    {path:'/events', element: <AdministativePanel/>},
    
    //Bets
    {path:'/bets', element: <IndexBets/>},
    
    //Login+Logout?
    {path:'/register', element: <Register/>},
    {path:'/login', element: <Login/>},
    {path:'/profile', element: <Profile/>},

    
    {path:'*', element: <Home/>}
];

export default routes;