import axios from "axios";
import { eventDTO } from "../../events/DTO/eventDTO.model";
import { urlEvents, urlEventsCreate } from "../../endpoints";
import {Link, useNavigate} from "react-router-dom";
import { eventCreationDTO } from "../../events/DTO/eventCreationDTO.model";
import EventForm from "../../sections/events/EventForm";
import { getCurrentUser } from "../../services/auth.service";


export default function CreateEvent(){
    const navigate = useNavigate();
  const currentUser = getCurrentUser();
    //Тут нужноо брать логин пользователя и пихать его в createBy
    const userLogin = currentUser.userName ?? "admin";
    return(
    <>
        <h3>Создать новое событие</h3>
        <EventForm model={{description: '', createdBy: userLogin}} 
            onSubmit={value => {
            //when the form posted
            console.log(value);
                axios({
                  method: 'POST',
                  url: urlEventsCreate,
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
    </>
)};