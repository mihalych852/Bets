//This page gets current User from Local Storage by calling AuthService.getCurrentUser() 
//method and show user information (with token).

import React from "react";
import { getCurrentUser } from "../services/auth.service";
import NeedToLogin from "./NeedToLogin";
import Button from "./Button";

const Profile: React.FC = () => {
  const currentUser = getCurrentUser();
  const style = {
    border: "1px solid lightgray",
borderRadius: "10px",
padding: "2px"
  }
  if(currentUser){
    return (
      <div className="container">
        <div className="row mb-3">
          <div className="col-6">
            <div className="m-1 border border-3" style={style}>
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
          
          
            </div>

          </div>
          <div className="col-6">
            <div className="m-1 border border-3" style={style}>
                        <h3>
          <strong>Баланс: *Get balance here*</strong>
          </h3>
            <Button children="Пополнить баланс" />
            <br />
            <Button children="Вывести деньги" />

            </div>

          </div>
        </div>
        
        <div className="row">
          <div className="col border border-3" style={style}>
            <h5>Текущие ставки</h5>
          </div>
          <div className="c-6 border border-3" style={style}>
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