import { Link } from "react-router-dom";
import { eventDTO } from "../../events/DTO/eventDTO.model";
import OutcomeLookupForAdmin from "./../OutcomeLookupForAdmin";
import logo from './icons8-edit-50.png';

//Индивидуальная карточка события с описанием, датой закрытия и исходами
export default function EventLookupRow(props: eventDTO){

    const date = new Date(props.betsEndTime);
    return(<>
    <tr>
        <td>
            <Link to={props.id}><img width={25} src={logo} /></Link>
            
        </td>
        <td>{props.description}</td>
        <td></td>
        <td>Открыто до: {date.getDate()}.{date.getMonth()}.{date.getFullYear()}</td>
        <td><ul>{props.eventOutcomes?.map(o => <OutcomeLookupForAdmin {...o} key={o.id}/>)}</ul></td>
    </tr>

    </>)
}