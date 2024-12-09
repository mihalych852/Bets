import {useNavigate} from "react-router-dom";
import { Link } from "react-router-dom"
import { useEffect, useState } from "react";
import { eventDTO } from "../../events/DTO/eventDTO.model";
import axios, { Axios, AxiosResponse } from "axios";
import { urlEvents } from "../../endpoints";
import EventList from "../../sections/events/EventList";
import EventListForAdmin from "../../sections/events/EventListForAdmin";


export default function AdministativePanel(){
    const [loading, setLoading] = useState(true);
    const [data, setData] = useState<eventDTO[]>([])

    const navigate = useNavigate();
    useEffect(() => {
        axios.get(urlEvents)
            .then((response: AxiosResponse<eventDTO[]>) => {
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