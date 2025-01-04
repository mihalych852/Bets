import {Link, useNavigate} from "react-router-dom";
import { ErrorMessage, Field, Form, Formik, FormikHelpers } from "formik";
import Button from "../../components/Button";
import * as Yup from 'yup';
import TextField from "../../components/forms/TextField";
import { eventCreationDTO } from "../../events/DTO/eventCreationDTO.model";
import { wallerAddDTO } from "../../events/DTO/walletAddDTO.model";

export default function WalletMinusForm(props: walletAddFormProps){
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
            <div className="row m-1">
                <div className="col-4">
                <div className="mb-3">
                    <Field name="amount" type="number" className="form-control" />
                </div>

                </div>
<div className="col-8">

        <Button type="submit" children="Вывести деньги" />
</div>
            </div>
    </Form>
    )}            

    </Formik>
)
    
}

interface walletAddFormProps{
    model: wallerAddDTO;
    onSubmit(values: wallerAddDTO, action: FormikHelpers<wallerAddDTO>): void;
}