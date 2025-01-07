import { Form, Formik, FormikHelpers } from "formik";
import { eventOutcomeDTO } from "../../events/DTO/eventOutcomeDTO.model";
import Button from "../../components/Button";
import axios from "axios";
import { urlOutComeUpdate } from "../../endpoints";
import { eventsStatus } from "../../events/DTO/eventStatus";

export default function OutcomeLookupForList(props: outcomeProps){
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
          <td>{props.model.description}</td>
          <td>{props.model.currentOdd}</td>
          {/* <td>{props.isHappened?.toString()}</td> */}
          <td><RenderBtnForOutcome model={props.model} status={props.status} /></td>
      </tr>
    </>)
}

function RenderBtnForOutcome(props: outcomeProps) {
  if(props.status != 2){
    return <MessageStatusNotOk />;
  }
    if (props.model.isHappened) {
      return <BtnForSetLoserOutcome model={props.model} 
      onSubmit={async value => {
        console.log(value);
        SetHappenedAndUpdateOutcome(props.model, false);
        }
      } />;
    }
    return <BtnForSetWinnerOutcome model={props.model} 
    onSubmit={async value => {
      console.log(value);
      SetHappenedAndUpdateOutcome(props.model, true);
      }
     } />;
  };

  async function SetHappenedAndUpdateOutcome(outcome:eventOutcomeDTO, isHappened: boolean) {
    try{
      const newEv = {...outcome};
      newEv.isHappened = isHappened;
      await axios.post(urlOutComeUpdate, newEv);
    }
    catch(error){
        console.log(error);
    }
  }

export function BtnForSetLoserOutcome(props: outcomeBtnFormProps){
  return(
      <Formik 
        initialValues={props.model}
        onSubmit={props.onSubmit}
    >
    {(formikProps) => (
        <Form>
          <button className="btn btn-outline-danger" type="submit">
            Set As Loser
        </button>
    </Form>
    )}            

    </Formik>
  )
}
export function BtnForSetWinnerOutcome(props: outcomeBtnFormProps){
  return(
    <Formik 
      initialValues={props.model}
      onSubmit={props.onSubmit}
  >
  {(formikProps) => (
      <Form>
        <button className="btn btn-outline-success" type="submit">
          Set As Winner
      </button>
  </Form>
  )}            

  </Formik>
)
}
export function MessageStatusNotOk(){
  return(
    <p>Ожидание завершения события</p>
  )
}
  interface outcomeBtnFormProps{
      model: eventOutcomeDTO;
      onSubmit(values: eventOutcomeDTO, action: FormikHelpers<eventOutcomeDTO>): void;
  }
  interface outcomeProps{
      model: eventOutcomeDTO;
      status?: eventsStatus;
  }