import { Link } from "react-router-dom";
import Button from "../../components/Button";
import { eventDTO } from "../../events/DTO/eventDTO.model";
import OutcomeLookupForAdmin from "./../OutcomeLookupForAdmin";
import { eventFullDTO } from "../../events/DTO/eventFullDTO.model";
import { eventsStatus } from "../../events/DTO/eventStatus";
import logo from './icons8-edit-50.png';

//Индивидуальная карточка события с описанием, датой закрытия и исходами
export default function EventLookupRow(props: eventFullDTO){

    const date = new Date(props.betsEndTime);
    const dateStart = new Date(props.eventStartTime);
    return(<>
    <tr>
        <td>
            <Link to={props.id}><img width={25} src={logo} /></Link>
            
        </td>
        <td>{props.description}</td>
        <td>{dateStart.getDate()}.{dateStart.getMonth()+1}.{dateStart.getFullYear()}</td>
        <td>{date.getDate()}.{date.getMonth()+1}.{date.getFullYear()}</td>
        <td><ul>{props.eventOutcomes?.map(o => <OutcomeLookupForAdmin {...o} key={o.id}/>)}</ul></td>
        <td>{eventsStatus[props.status]} </td>
    </tr>

    </>)
}