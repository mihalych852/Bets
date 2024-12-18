//This page gets current User from Local Storage by calling AuthService.getCurrentUser() 
//method and show user information (with token).

import React from "react";
import { getCurrentUser } from "../services/auth.service";
import NeedToLogin from "./NeedToLogin";

const Profile: React.FC = () => {
  const currentUser = getCurrentUser();

  if(currentUser){
    return (
      <div className="container">
        <header className="jumbotron">
          <h3>
          <strong>Профиль</strong>
          </h3>
        </header>
        <p>
          <strong>Имя пользователя:</strong> {currentUser.userName ?? "Не указано"}
        </p>
        <p>
          <strong>Email:</strong> {currentUser.email}
        </p>
        <strong>Authorities:</strong>
        <ul>
          {currentUser.rolles &&
            currentUser.rolles.map((role: string, index: number) => <li key={index}>{role}</li>)}
        </ul>
        <div className="row">
          <div className="col-6">
            <h5>Текущие ставки</h5>
          </div>
          <div className="col-6">
            <h5>История ставок</h5>
          </div>
        </div>
      </div>
    );
  }
  else{
    return(<>
      <NeedToLogin />
    </>)
  }


};

export default Profile;