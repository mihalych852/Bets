import { useEffect, useState } from "react";
import { eventDTO } from "../events/DTO/eventDTO.model";
import EventList from "../sections/events/EventList";
import axios, { Axios, AxiosResponse } from "axios";
import { urlEvents } from "../endpoints";
import { getCurrentUser } from "../services/auth.service";

export default function IndexBets(){
    const currentUser = getCurrentUser();
    const [loading, setLoading] = useState<boolean>(true);
    const [data, setData] = useState<eventDTO[]>([])
    
    useEffect(() => {
        axios.get(urlEvents)
            .then((response: AxiosResponse<eventDTO[]>) => {
                console.log(response.data);
                setData(response.data);
                
            })
    }, [])    
    return(
    <>
        <EventList events={data}/>
    </>
)};
