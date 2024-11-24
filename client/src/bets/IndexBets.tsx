import { eventDTO } from "../events/DTO/eventDTO.model";
import EventList from "../events/EventList";

export default function IndexBets(){
    const testList: eventDTO[] = [
        {
            id:1,
            description: "Реал Мадрид - Барселона",
            closingDate: new Date("13-12-2024")
        }
    ]
    return(
    <>
        <h1>Список ставок</h1>
        <p>Нужен список событий, которые уже открыты и не закрыты с исходами внутри</p>
        <EventList events={testList}/>
    </>
)};