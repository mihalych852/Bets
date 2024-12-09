import { eventOutcomeDTO } from "../events/DTO/eventOutcomeDTO.model";

//Строчка с исходом, на который можно поставить
export default function OutcomeLookupForAdmin(props: eventOutcomeDTO){
    //тут еще надо как-то id передавать, на что мы ставим
    //кнопку сделать сабмитной и вызывать контроллер createBet?
    //еще нужно пересчитывать коэф и перерерисовывать элемент, т.к. он меняется 
    return(
    <li>{props.description} Odds:{props.currentOdd}

</li>

    ) 
}