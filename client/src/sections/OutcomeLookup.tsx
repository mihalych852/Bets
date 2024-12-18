import { eventOutcomeDTO } from "../events/DTO/eventOutcomeDTO.model";
import BetForm from "./bets/BetForm";
import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import axios, { AxiosResponse } from "axios";
import { urlBetCreate, urlEventsCreate, urlEventsEdit, urlEventsGetById } from "../endpoints";
import { getCurrentUser } from "../services/auth.service";

//Строчка с исходом, на который можно поставить
export default function OutcomeLookup(props: eventOutcomeDTO){

    const navigate = useNavigate();
    const styleTable = {
        border: "1px solid lightgray"
      }
  const currentUser = getCurrentUser();
    const userLogin = currentUser.userName ?? currentUser.email;
    const bettorId = "b314e08e-5e6c-4215-b8df-506e884960f4";
    
    //еще нужно пересчитывать коэф и перерерисовывать элемент, т.к. он меняется 
    return(
    <tr>
        <td style={styleTable}>{props.description}</td>
        <td style={styleTable}>{props.currentOdd}</td>
        <td style={styleTable}>
            <BetForm  model={{bettorId: bettorId, eventOutcomesId:props.id, createdBy: userLogin}} 
            onSubmit={value => {
            //when the form posted
            console.log(value);
                axios({
                  method: 'POST',
                  url: urlBetCreate,
                  data: value
                })
                  .then(function (res) {
                     console.log(res);
                     
                    })
                    .catch(function (res) {
                      console.log(res)
                    });
                    window.location.reload();
                navigate('../');
              } 
            }
              />
            </td>

</tr>

    ) 
}