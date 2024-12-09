import { Link } from "react-router-dom";
import Button from "../../components/Button";
import { eventDTO } from "../../events/DTO/eventDTO.model";
import OutcomeLookupForAdmin from "./../OutcomeLookupForAdmin";

//Индивидуальная карточка события с описанием, датой закрытия и исходами
export default function EventLookupRow(props: eventDTO){

    const date = new Date(props.betsEndTime);
    return(<>
    <tr>
        <td>
            <Link to={props.id}>Edit</Link>
        </td>
        <td>{props.description}</td>
        <td></td>
        <td>Открыто до: {date.getDate()}.{date.getMonth()}.{date.getFullYear()}</td>
        <td><ul>{props.eventOutcomes?.map(o => <OutcomeLookupForAdmin {...o} key={o.id}/>)}</ul></td>
    </tr>

    </>)
}