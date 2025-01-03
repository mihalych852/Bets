import { eventOutcomeDTO } from "../../events/DTO/eventOutcomeDTO.model";

export default function OutcomeLookupForList(props: eventOutcomeDTO){
    //сделать через bootstrap/mui красиво потом
    const mystyle = {
        margin: "1rem",
        padding: "0px"
      };const styleTable = {
        border: "1px solid lightgray",
        innerWidth: "50%"
      };
      const styleHeader = {
        //backgroundColor: "#dce0f6",
        backgroundColor: "#3e4a8b",
        color: "fff"
      };
      const styleTitle = {
        //backgroundColor: "#dce0f6",
        color: "fff"
      };
    return(
    <>
    <tr>
        <td>{props.description}</td><td>{props.currentOdd}</td><td>{props.betsClosed}</td>
    </tr>
    </>)
}
