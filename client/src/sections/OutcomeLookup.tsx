import { eventOutcomeDTO } from "../events/DTO/eventOutcomeDTO.model";
import BetForm from "./bets/BetForm";
import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import axios, { AxiosResponse } from "axios";
import { urlBetCreate,urlWalletDebit,urlWalletAdd, urlEventsCreate, urlEventsEdit, urlEventsGetById } from "../endpoints";
import { getCurrentUser } from "../services/auth.service";
import { betsRequestDTO } from "../events/DTO/betsRequestDTO.model";
import {wallerAddDTO} from "../events/DTO/walletAddDTO.model";

//Строчка с исходом, на который можно поставить
export default function OutcomeLookup(props: eventOutcomeDTO){

    const navigate = useNavigate();
    const styleTable = {
        border: "1px solid lightgray"
      }
  const currentUser = getCurrentUser();
    const userLogin = currentUser?.userName ?? currentUser?.email;
    const bettorId = currentUser?.id;
    
    const handleSubmit = async (value: betsRequestDTO) => {
    try {

        // Создаем объект для транзакции списания
        const debitTransaction: wallerAddDTO = {
          bettorId: bettorId,
          amount: value.amount ?? 0,
          description: "Списание за ставку",
          createdBy: userLogin
      };
        const debitResponse = await axios.post(urlWalletDebit, debitTransaction);
        if (debitResponse.status === 200) {
          // 2. Если транзакция списания успешна, отправляем запрос на создание ставки
          const betResponse = await axios.post(urlBetCreate, value);
          if (betResponse.status === 200) {
            // 3. Если ставка успешна, переходим на главную страницу
            navigate('../');
          }
        }
        else {
           // 4. Если ставка не удалась, отправляем запрос на создание транзакции начисления
           const creditTransaction: wallerAddDTO = {
            bettorId,
            amount: value.amount ?? 0,
            description: "Возврат за неудачную ставку",
            createdBy: userLogin
        };
           await axios.post(urlWalletAdd, creditTransaction);
            alert("Не удалось создать ставку. Сумма была возвращена.");
        }
    }
    catch (error) {
      console.error("Ошибка:", error);
      alert("Не удалось создать ставку.");
    }
  }

    return (
      <tr>
          <td style={styleTable}>{props.description}</td>
          <td style={styleTable}>{props.currentOdd}</td>
          <td style={styleTable}>
              <BetForm model={{ bettorId, eventOutcomesId: props.id, createdBy: userLogin }} onSubmit={handleSubmit} />
          </td>
      </tr>
    );
  }