import {Link, useNavigate} from "react-router-dom";
import { ErrorMessage, Field, Form, Formik, FormikHelpers } from "formik";
import Button from "../../components/Button";
import * as Yup from 'yup';
import TextField from "../../components/forms/TextField";
import { eventCreationDTO } from "../../events/DTO/eventCreationDTO.model";

export default function EventForm(props: genreFormProps){
return(
    <Formik 
        initialValues={props.model}
        onSubmit={props.onSubmit}
        validationSchema={Yup.object({
            description: Yup.string().required('Поле обязательно для заполнения')
        })}
    >
    {(formikProps) => (
        <Form>
        <TextField title="Описание события" field="description" />
        <div className="row">
            <div className="mb-3 col-6">
                <label htmlFor="eventStartTime">Дата начала события</label>
                <Field name="eventStartTime" type="date" className="form-control" />
            </div>
            <div className="mb-3 col-6">
                <label htmlFor="betsEndTime">Дата завершения события</label>
                <Field name="betsEndTime" type="date" className="form-control" />
            </div>
        </div>
        <Button disabled={formikProps.isSubmitting} type="submit" children="Создать событие" />
        <Link className="btn btn-secondary" to="/events">Отмена</Link>
    </Form>
    )}            

    </Formik>
)
    
}

interface genreFormProps{
    model: eventCreationDTO;
    onSubmit(values: eventCreationDTO, action: FormikHelpers<eventCreationDTO>): void;
}