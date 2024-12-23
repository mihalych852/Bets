import { betResponseDTO } from "../../events/DTO/betResponseDTO.model";
import { betStatus } from "../../events/DTO/betStatus";

export default function BetResponse(props: betResponseDTO){
    return(<>
    <tr>
        <td>{props.description}</td>
        <td>{props.amount}</td>
        <td>{betStatus[props.state]} </td>
    </tr>

    </>)
}