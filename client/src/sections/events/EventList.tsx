import { eventDTO } from "../../events/DTO/eventDTO.model";
import EventLookup from "./EventLookup";
import css from './EventList.module.css';

//Компонент для отрисовки списка событий
export default function EventList(props: eventListProps){
    
    return(<>
        <div className={css.div}>
            {props.events.map(events => <EventLookup {...events} key={events.id}/>)}
        </div>
    </>)
}
interface eventListProps{
    events: eventDTO[];
}