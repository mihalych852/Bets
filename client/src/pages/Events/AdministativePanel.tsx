import {useNavigate} from "react-router-dom";
import { Link } from "react-router-dom"
import { useEffect, useState } from "react";
import axios, { Axios, AxiosResponse } from "axios";
import { urlEvents } from "../../endpoints";
import EventListForAdmin from "../../sections/events/EventListForAdmin";
import { eventFullDTO } from "../../events/DTO/eventFullDTO.model";


export default function AdministativePanel(){
    const [loading, setLoading] = useState<boolean>(true);
    const [data, setData] = useState<eventFullDTO[]>([])

    const navigate = useNavigate();
    useEffect(() => {

        axios.get(urlEvents)
            .then((response: AxiosResponse<eventFullDTO[]>) => {
                console.log(response.data);
                setData(response.data);
                setLoading(false);
            })
    }, [])
    // const testList: eventFullDTO[] = [
    //     {
    //         id: "b314e08e-5e6c-4215-b8df-506e884960f7",
    //         description: "Реал Мадрид - Барселона",
    //         betsEndTime: new Date("10-12-2024"),
    //         eventStartTime: new Date("11-23-2024"),
    //         eventOutcomes: [{
    //             id: "b314e08e-5e6c-4215-b8df-506e884960f8",
    //             description: "outcome 1",
    //             currentOdd: 2
    //         }, {
    //             id: "b314e08e-5e6c-4215-b8df-506e884960f9",
    //             description: "outcome 2",
    //             currentOdd: 4
    //         },
    //         ],
    //         status: undefined
    //     },
    //     {
    //         id: "2",
    //         description: "Победа в выборах",
    //         betsEndTime: new Date("10-12-2024"),
    //         eventStartTime: new Date("11-23-2024"),
    //         eventOutcomes: [{
    //             id: "3",
    //             description: "Трапм",
    //             currentOdd: 2
    //         }, {
    //             id: "4",
    //             description: "Кто-то там еще",
    //             currentOdd: 4
    //         }, {
    //             id: "456456",
    //             description: "Кто-то там еще",
    //             currentOdd: 4
    //         }, {
    //             id: "786753",
    //             description: "Кто-то там еще",
    //             currentOdd: 4
    //         },
    //         ],
    //         status: undefined
    //     }
    // ]
    return(
        <>
            <div className="mb-3">
                <Link className="btn btn-primary" to="create">Добавить событие</Link>
            </div>
            {loading && (
                  <span className="spinner-border spinner-border"></span>
                )}
            <EventListForAdmin events={data}/>
            
        </>
    )
}