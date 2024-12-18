import {Link, useNavigate} from "react-router-dom";
import { ErrorMessage, Field, Form, Formik, FormikHelpers } from "formik";
import Button from "../../components/Button";
import * as Yup from 'yup';
import TextField from "../../components/forms/TextField";
import { eventCreationDTO } from "../../events/DTO/eventCreationDTO.model";
import { eventOutcomeDTO } from "../../events/DTO/eventOutcomeDTO.model";
import { eventOutcomeRequestDTO } from "../../events/DTO/eventOutcomeRequestDTO.model";

export default function OutcomeForm(props: outcomeFormProps){
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
        <div className="row">
            <div className="mb-3 col-10">
                <label htmlFor="description">Описание</label>
                <Field name="description" type="textarea" className="form-control" />
            </div>
            <div className="mb-1 mt-4 col-2">
                <label htmlFor=""><br /></label>
                <Button disabled={formikProps.isSubmitting} type="submit" children="+" />
            </div>
        </div>
    </Form>
    )}            

    </Formik>
)
    
}

interface outcomeFormProps{
    model: eventOutcomeRequestDTO;
    onSubmit(values: eventOutcomeRequestDTO, action: FormikHelpers<eventOutcomeRequestDTO>): void;
}