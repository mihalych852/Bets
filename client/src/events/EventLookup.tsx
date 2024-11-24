import { eventDTO } from "./DTO/eventDTO.model";

//Индивидуальная карточка события с описанием, датой закрытия и исходами
export default function EventLookup(props: eventDTO){
    //сделать через bootstrap/mui красиво потом
    const mystyle = {
        backgroundColor: "lightgray",
        padding: "10px",
        margin: "10px",
        fontFamily: "Arial",
        width: "250px",
        height: "200px"
      };
    return(<>
        <div style={mystyle}>
            <h3>{props.description}</h3>
            <p>Открыто до:{props.closingDate.getDay()}.{props.closingDate.getMonth()}.{props.closingDate.getFullYear()}</p>
            <p>list of outcomes</p>
        </div>
    </>)
}