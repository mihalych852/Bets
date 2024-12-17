import { useNavigate, useParams } from "react-router-dom";
import EventEditForm from "../../sections/events/EventEditForm";
import { useEffect, useState } from "react";
import axios, { AxiosResponse } from "axios";
import { urlEventsCreate, urlEventsEdit, urlEventsGetById } from "../../endpoints";
import { eventUpdateDTO } from "../../events/DTO/eventUpdateDTO.model";

export default function EditEvent(){
    const {id} : any = useParams();
    const userLogin = "admin";
    const navigate = useNavigate();
    const par = useParams();

    const [eventInfo, setEventInfo] = useState<eventUpdateDTO>({
        description: "0", id: "0", betsEndTime: new Date(), 
        eventStartTime: new Date(), status: 0, modifyBy:"aaa"  });

    useEffect(() => {
        if(id){
            axios.get(urlEventsGetById+id)
            .then((response: AxiosResponse<eventUpdateDTO>) => {
                console.log(response.data);
                setEventInfo(response.data);
            }).catch(err => {
                console.log(err)
            })
        }else{
            navigate('../events');
        }

    }, [])
    //Надо из параметров все поля заполнить по умолчанию как были
    //b314e08e-5e6c-4215-b8df-506e884960f7
    return(
    <>
    <div className="mb-5">
        <FF model={eventInfo}  />
        <h4>Редактировать событие</h4>
        <input value={eventInfo.id} />
        <input value={eventInfo.description} />
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
        <h5>Редактировать исходы</h5>
        <p>тут должно быть добавление/удаление исходов, ноя пока не умею подгружать актуальные данные</p>

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