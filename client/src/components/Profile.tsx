//This page gets current User from Local Storage by calling AuthService.getCurrentUser() 
//method and show user information (with token).

import React, { useEffect, useState } from "react";
import { getCurrentUser } from "../services/auth.service";
import NeedToLogin from "./NeedToLogin";
import Button from "./Button";
import { useNavigate, useParams } from "react-router-dom";
import axios from "axios";
import { urlBetGetForUser, urlWalletAdd, urlWalletDebit, urlWalletGetBalance } from "../endpoints";
import WalletAddForm from "../sections/wallet/WalletAddForm";
import WalletMinusForm from "../sections/wallet/WalletMinusForm";
import { betResponseDTO } from "../events/DTO/betResponseDTO.model";
import BetResponse from "../sections/bets/BetRespose";
import BetResponseTable from "../sections/bets/BetResposeTable";

const Profile: React.FC = () => {
  const currentUser = getCurrentUser();
  const navigate = useNavigate();

    const [balanceInfo, setbalanceInfo] = useState();
    const [betList, setbetList] = useState<betResponseDTO[]>([]);

    useEffect(() => {
            if(currentUser.id){
              axios.all([
                axios.get(urlWalletGetBalance+currentUser.id),
                axios.get(urlBetGetForUser+currentUser.id)
              ])            
            .then((response) => {
              console.log(response);              
                setbalanceInfo(response[0].data);
                setbetList(response[1].data);
            }).catch(err => {
                console.log(err)
            });
        }; 

    }, [])
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
          <strong>Баланс: {balanceInfo}</strong>
          </h3>
          <div>
            <WalletAddForm model={{createdBy: currentUser.id, description: "from profile", 
            bettorId: currentUser.id, amount: 0}} 
                        onSubmit={value => {
                        //when the form posted
                        console.log(value);
                        axios({
                          method: 'POST',
                          url: urlWalletAdd,
                          data: value
                        })
                          .then(function (res) {
                              console.log(res);
                                                        axios.get(urlWalletGetBalance+currentUser.id)
                          .then((response) => {
                              console.log(response.data);
                              setbalanceInfo(response.data);
                          }).catch(err => {
                              console.log(err)
                          });
                            })
                            .catch(function (res) {
                              console.log(res)
                            });

                        //window.location.reload();

                          } 
                        }
                        />
          </div>
          <hr />
          <div>
          <WalletMinusForm model={{createdBy: currentUser.id, description: "from profile", 
            bettorId: currentUser.id, amount: 0}} 
                        onSubmit={value => {
                        //when the form posted
                        console.log(value);
                        axios({
                          method: 'POST',
                          url: urlWalletDebit,
                          data: value
                        })
                          .then(function (res) {
                              console.log(res);
                              axios.get(urlWalletGetBalance+currentUser.id)
                            .then((response) => {
                                console.log(response.data);
                                setbalanceInfo(response.data);
                                
                            }).catch(err => {
                                console.log(err)
                            });
                            })
                            .catch(function (res) {
                              console.log(res)
                            });
                            

                          } 
                        }
                        />
          </div>

            </div>

          </div>
        </div>
        
        <div className="row">
          <div className="col-6 border border-3" style={style}>
            <h5>Текущие ставки</h5>
            <BetResponseTable model={betList.filter(x => x.state === 0)} />
          </div>
          <div className="col-6 border border-3" style={style}>
            <h5>Ожидает выплаты</h5>
            <BetResponseTable model={betList.filter(x => x.state === 2)} />
          </div>
        </div>
        <div className="row">
          <div className="col-6 border border-3" style={style}>
            <h5>Завершенные ставки</h5>
            <BetResponseTable model={betList.filter(x => x.state === 3)} />
          </div>
          <div className="col-6 border border-3" style={style}>
            <h5>Отмененные ставки</h5>
            <BetResponseTable model={betList.filter(x => x.state === 1)} />
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