import { useNavigate, useParams } from "react-router-dom";
import EventEditForm from "../../sections/events/EventEditForm";
import { useEffect, useState } from "react";
import axios, { AxiosResponse } from "axios";
import { urlEventsEdit, urlEventsGetById } from "../../endpoints";
import { eventUpdateDTO } from "../../events/DTO/eventUpdateDTO.model";
import { createOutcome } from "../../services/event.service";
import { eventOutcomeRequestDTO } from "../../events/DTO/eventOutcomeRequestDTO.model";
import { eventOutcomeDTO } from "../../events/DTO/eventOutcomeDTO.model";
import OutcomeLookupForList from "../../sections/events/OutcomeLookupForList";
import { Table } from "react-bootstrap";
import { getCurrentUser } from "../../services/auth.service";
import AdministativePanel from "./AdministativePanel";
import OutcomeForm from "../../sections/events/OutcomeForm";

export default function EditEvent(){
    const {id} : any = useParams();
      const currentUser = getCurrentUser();    
    const userLogin = currentUser?.email;
    const navigate = useNavigate();
    const par = useParams();

    const [loading, setLoading] = useState<boolean>(true);
    const [eventInfo, setEventInfo] = useState<eventUpdateDTO>();
    const [outComes, setOutcomes] = useState<eventOutcomeDTO[]>([]);

    useEffect(() => {
            axios.get(urlEventsGetById+id)
            .then((response) => {
                console.log(response.data);
                //setEventInfo(p => ({...response.data, modifiedBy: userLogin}));                
                setEventInfo(response.data);                
                setOutcomes(response.data.eventOutcomes);
            }).catch(err => {
                console.log(err)
            });
    }, [id])

    async function editEvent(eventToEdit: eventUpdateDTO) {
      eventToEdit.modifiedBy = userLogin;
      //cast enum to number or get error
      switch(eventToEdit.status.toString()){
        case '0':
          eventToEdit.status = 0;
          break;
          case '1':
            eventToEdit.status = 1;
            break;
            case '2':
              eventToEdit.status = 2;
              break;
              case '3':
                eventToEdit.status = 4;
                break;
      }
      try{
        await axios.post(urlEventsEdit, eventToEdit); 
        navigate('../events');
      }
      catch(error){
          console.log(error);
      }
    }

    const handleSave = (formValue: eventOutcomeRequestDTO) => {
        const { description, eventId, createdBy } = formValue;
        createOutcome(description, eventId, createdBy).then(
          () => {
            window.location.reload();
          },
          (error) => {
            const resMessage =
              (error.response &&
                error.response.data &&
                error.response.data.message) ||
              error.message ||
              error.toString();
          }
        );
      };
  
    //Надо из параметров все поля заполнить по умолчанию как были
    //b314e08e-5e6c-4215-b8df-506e884960f7
    return(
    <>
    <div className="mb-5">
        <h4>Редактировать событие</h4>
        {eventInfo ? <EventEditForm model= {eventInfo} 
        onSubmit={async value => {
              console.log(value);
                editEvent(value);
              }
            }
        /> :
          <span className="spinner-border spinner-border"></span>
        }


    </div>
    <div>
        <h5>Список исходов</h5>
        <div className="row">
          {outComes ? 
            <OutcomeForm model={{description: '', eventId: id, createdBy: userLogin}}  
            onSubmit={handleSave}/>
            :
            <span className="spinner-border spinner-border"></span>
          }
        </div>
        <div className="mb-3">
          <Table striped className="w-100">
            <thead>
              <tr><th>Описание</th><th>Коэф.</th><th>Статус</th></tr>
            </thead>
            <tbody>
              {outComes?.map(events => <OutcomeLookupForList {...events} key={events.id}/>)}

            </tbody>
          </Table>
          
        </div>

    </div>


    </>
)}; 

// export function FF(props: ieventUpdateDTO){
//     return(
//         <>
//                     <input value={props.model.id} />

//         </>
//     )
// }
interface ieventUpdateDTO{
    model: eventUpdateDTO;
}