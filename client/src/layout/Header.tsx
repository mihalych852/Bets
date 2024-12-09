import React from "react";
export default function Header(){
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
            </div>
        </nav>
)};