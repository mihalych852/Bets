import {useNavigate} from "react-router-dom";
import { Link } from "react-router-dom"
import { useEffect, useState } from "react";
import axios, { Axios, AxiosResponse } from "axios";
import { urlEvents } from "../../endpoints";
import EventListForAdmin from "../../sections/events/EventListForAdmin";
import { eventFullDTO } from "../../events/DTO/eventFullDTO.model";


export default function AdministativePanel(){
    const [loading, setLoading] = useState(true);
    const [data, setData] = useState<eventFullDTO[]>([])

    const navigate = useNavigate();
    useEffect(() => {
        axios.get(urlEvents)
            .then((response: AxiosResponse<eventFullDTO[]>) => {
                console.log(response.data);
                setData(response.data);
            })
    }, [])
    return(
        <>
            <div className="mb-3">
                <Link className="btn btn-primary" to="create">Добавить событие</Link>
            </div>
            <EventListForAdmin events={data}/>
        </>
    )
}