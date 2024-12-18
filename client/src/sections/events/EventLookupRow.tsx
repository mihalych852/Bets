import { Link } from "react-router-dom";
import OutcomeLookupForAdmin from "./../OutcomeLookupForAdmin";
import { eventFullDTO } from "../../events/DTO/eventFullDTO.model";
import { eventsStatus } from "../../events/DTO/eventStatus";

//Индивидуальная карточка события с описанием, датой закрытия и исходами
export default function EventLookupRow(props: eventFullDTO){

    const date = new Date(props.betsEndTime);
    const dateStart = new Date(props.eventStartTime);
    return(<>
    <tr>
        <td>
            <Link to={props.id}>Edit</Link>
        </td>
        <td>{props.description}</td>
        <td>{dateStart.getDate()}.{dateStart.getMonth()}.{dateStart.getFullYear()}</td>
        <td>{date.toLocaleString()}</td>
        <td><ul>{props.eventOutcomes?.map(o => <OutcomeLookupForAdmin {...o} key={o.id}/>)}</ul></td>
        <td>{eventsStatus[props.status]} </td>
    </tr>

    </>)
}