import React from "react";
import { betResponseDTO } from "../../events/DTO/betResponseDTO.model";
import BetResponse from "./BetRespose";

export default function BetResponseTable(props: betResponseTableProps){
    if(props.model.length === 0){
        return(<><p className="text-muted">Ставок нет</p></>)
    }
    return(<>
        <table className="w-100 table">
            <thead>
                <tr>
                    <th>Описание события</th>
                    <th>Ставка</th>
                    <th>Статус</th>
                </tr>
            </thead>
            <tbody>
            {props.model.map(bet => <BetResponse {...bet} key={bet.id}/>)}                
            </tbody>
    </table>
        </>)
}

export interface betResponseTableProps{
    model: betResponseDTO[]
}