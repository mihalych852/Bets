import { eventDTO } from "../../events/DTO/eventDTO.model";
import { eventOutcomeDTO } from "../../events/DTO/eventOutcomeDTO.model";
import OutcomeLookup from "../OutcomeLookup";
import css from './EventList.module.css';

//Индивидуальная карточка события с описанием, датой закрытия и исходами
export default function EventLookup(props: eventDTO){
    //сделать через bootstrap/mui красиво потом
    const mystyle = {
        margin: "1rem",
      };const styleTable = {
        border: "1px solid lightgray",
        innerWidth: "50%"
      };
    const date = new Date(props.betsEndTime);

    return(<>
        {/* <div style={mystyle}>
            <h3>{props.description}</h3>
            <p>Открыто до:{props.closingDate.getDay()}.{props.closingDate.getMonth()}.{props.closingDate.getFullYear()}</p>
            <table><tbody>{props.eventOutcomes?.map(o => <OutcomeLookup {...o} key={o.id}/>)}</tbody></table>
        </div> */}
        
        <div className="col-6">
            <div className="card text-center" style={mystyle}>
                <div className="card-body">
                    <h5 className="card-title">{props.description}</h5>
                </div>
                <ul className="list-group list-group-flush">
                    <table>
                        <thead>
                            <tr style={styleTable}>
                                <th style={styleTable} className="w-50">Описание</th>
                                <th style={styleTable}>Коэф.</th>
                                <th style={styleTable} className="">Исходы</th>
                            </tr>
                        </thead>
                        <tbody>
                            {props.eventOutcomes?.map(o => <OutcomeLookup {...o} key={o.id}/>)}
                            </tbody>
                            </table>

                </ul>
                <div className="card-footer text-muted">
                    <p>Открыто до: {date.getDate()}.{date.getMonth()}.{date.getFullYear()}</p>
                </div>
            </div>
        </div>

    </>)
}

interface eventLookupProps{
    eventDTO: eventDTO;
    outcomes: eventOutcomeDTO[];
}