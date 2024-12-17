import React, { useState } from "react";
import { getCurrentUser, isUserLoggedIn, logout } from "../services/auth.service";
import { Link, NavigateFunction, useNavigate } from "react-router-dom";
import { hasFormSubmit } from "@testing-library/user-event/dist/utils";
export default function Header(){
    // let navigate: NavigateFunction = useNavigate();

    // const [loading, setLoading] = useState<boolean>(false);
    // const [message, setMessage] = useState<string>("");
    // const handleLogout = () => {
    //     setMessage("");
    //     setLoading(true);
    
    //     logout().then(
    //       () => {
    //         navigate("../");
    //         window.location.reload();
    //       },
    //       (error) => {
    //         const resMessage =
    //           (error.response &&
    //             error.response.data &&
    //             error.response.data.message) ||
    //           error.message ||
    //           error.toString();
    
    //         setLoading(false);
    //         setMessage(resMessage);
    //       }
    //     );
    //   };
    return(
        <nav className="navbar navbar-expand-lg navbar-light bg-light mb-3">
            <div className="container-fluid">
                <a className="navbar-brand" href="/">CoolBet</a>
                <div className="collapse navbar-collapse">
                    <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                        <li className="nav-item">
                            <a className="nav-link" href="/events">Панель администратора</a>
                        </li>
                    </ul>
                </div>
                <Greeting  />
            </div>
        </nav>
)};

export function UserUnknown(){
    return(
        <>
            <Link className="" to="login">Login</Link><span>  | </span><Link className="" to="register">   Register</Link>    
        </>
    )
}

export function UserKnown(){
  const currentUser = getCurrentUser();
    return(
        <>
            <Link className="" to="profile">UserName</Link><span>  | 
                </span> <Link className="" to=""><span onClick={() => logout().then(() => {window.location.reload();})}>LogOut</span> </Link>   
        </>
    )
}

function Greeting() {
    const isLoggedIn = isUserLoggedIn();
    if (isLoggedIn) {
      return <UserKnown />;
    }
    return <UserUnknown />;
  };
  interface userKnownProps{
    onSubmit(): void; 
  }