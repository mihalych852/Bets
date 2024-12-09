import { eventDTO } from "../../events/DTO/eventDTO.model";
import EventLookupRow from "./EventLookupRow";

//Компонент для отрисовки списка событий
export default function EventListForAdmin(props: eventListProps){
    
    return(<>
    <table className="table">
        <thead>
            <tr>
                <th></th>
                <th>Описание</th>
                <th>Дата начала</th>
                <th>Дата завершения</th>
                <th>Исходы</th>
            </tr>
        </thead>
        <tbody>
            {props.events.map(events => <EventLookupRow {...events} key={events.id}/>)}
        </tbody>
    </table>
    </>)
}
interface eventListProps{
    events: eventDTO[];
}