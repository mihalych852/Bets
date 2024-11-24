import { useEffect } from "react"
import { eventDTO } from "./DTO/eventDTO.model"
import EventLookup from "./EventLookup";

export default function IndexEvent(){
    //useEffect(() => {
        //axios.get()
    //});
    const testEvent: eventDTO = {
        id:1,
        description: "test event",
        closingDate: new Date(2024, 7, 12)
    };
    return(
    <>
        <h1>Create Event</h1>
        <EventLookup {...testEvent}/>
    </>
)};
