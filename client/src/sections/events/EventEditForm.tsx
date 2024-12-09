import {Link, useNavigate} from "react-router-dom";
import { ErrorMessage, Field, Form, Formik, FormikHelpers } from "formik";
import Button from "../../components/Button";
import * as Yup from 'yup';
import TextField from "../../components/forms/TextField";
import { eventUpdateDTO } from "../../events/DTO/eventUpdateDTO.model";

export default function EventEditForm(props: eventEditFormProps){
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
        <div className="mb-3">

        <Field className="form-select" as="select" name="status">
             <option value="0">Ожидается</option>
             <option value="1">В процессе</option>
             <option value="2">Завершено</option>
             <option value="3">Отменено</option>
           </Field>
           </div>
        <Button disabled={formikProps.isSubmitting} type="submit" children="Сохранить" />
        <Link className="btn btn-secondary" to="/events">Отмена</Link>
    </Form>
    )}            

    </Formik>
)
    
}

interface eventEditFormProps{
    model: eventUpdateDTO;
    onSubmit(values: eventUpdateDTO, action: FormikHelpers<eventUpdateDTO>): void;
}