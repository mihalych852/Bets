import { useNavigate, useParams } from "react-router-dom";
import EventEditForm from "../../sections/events/EventEditForm";
import { useEffect, useState } from "react";
import axios, { AxiosResponse } from "axios";
import { urlEventsCreate, urlEventsEdit, urlEventsGetById } from "../../endpoints";
import { eventUpdateDTO } from "../../events/DTO/eventUpdateDTO.model";
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Modal from 'react-bootstrap/Modal';
import OutcomeForm from "../../sections/events/OutcomeForm";
import { createOutcome } from "../../services/event.service";
import { eventOutcomeRequestDTO } from "../../events/DTO/eventOutcomeRequestDTO.model";
import { eventDTO } from "../../events/DTO/eventDTO.model";
import { eventOutcomeDTO } from "../../events/DTO/eventOutcomeDTO.model";
import OutcomeLookup from "../../sections/OutcomeLookup";
import OutcomeLookupForList from "../../sections/events/OutcomeLookupForList";
import { Table } from "react-bootstrap";

export default function EditEvent(){
    const {id} : any = useParams();
    const userLogin = "admin";
    const navigate = useNavigate();
    const par = useParams();

    const [eventInfo, setEventInfo] = useState<eventUpdateDTO>({
        description: "0", id: "0", betsEndTime: new Date(), 
        eventStartTime: new Date(), status: 0, modifyBy:"aaa"  });

    const [outComes, setData] = useState<eventOutcomeDTO[]>([]);

    useEffect(() => {
            if(id){
            axios.get(urlEventsGetById+id)
            .then((response: AxiosResponse<eventUpdateDTO>) => {
                console.log(response.data);
                setEventInfo(response.data);
                //setData(response.data.) get outcomes
            }).catch(err => {
                console.log(err)
            });
        }; 

    }, [])

    const handleSave = (formValue: eventOutcomeRequestDTO) => {
        const { description, eventId } = formValue;
        createOutcome(description, eventId).then(
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
        <FF model={eventInfo}  />
        <h4>Редактировать событие</h4>
        <input className="form-control" value={eventInfo.id} />
        <input className="form-control"  value={eventInfo.description} />
        <EventEditForm model= {eventInfo
        //     {modifyBy: "admin",
        //     description: eventInfo?.description ?? "", 
        // id: id, 
        // status: eventInfo?.status ?? 0, 
        // eventStartTime: eventInfo?.eventStartTime ?? new Date(), 
        // betsEndTime: eventInfo?.betsEndTime ?? new Date()}
    } 
        onSubmit={value => {
            //when the form posted
            console.log(value);
                axios({
                  method: 'POST',
                  url: urlEventsEdit,
                  data: value
                })
                  .then(function (res) {
                     console.log(res)
                  })
                  .catch(function (res) {
                     console.log(res)
                });
                navigate('../events');
              }
            }
        />

    </div>
    <div>
        <h5>Список исходов</h5>
        <div className="row">
            <OutcomeForm model={{description: '', createdBy: userLogin, eventId: eventInfo.id}} onSubmit={handleSave}/>
        </div>
        <div className="mb-3">
          <Table striped className="w-100">
            <thead>
              <tr><th>Описание</th><th>Коэф.</th><th>Статус</th></tr>
            </thead>
            <tbody>
              {eventInfo.eventOutcomes?.map(events => <OutcomeLookupForList {...events} key={events.id}/>)}

            </tbody>
          </Table>
          
        </div>

    </div>


    </>
)}; 

export function FF(props: ieventUpdateDTO){
    return(
        <>
                    <input value={props.model.id} />

        </>
    )
}
interface ieventUpdateDTO{
    model: eventUpdateDTO;
}