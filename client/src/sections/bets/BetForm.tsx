
import { ErrorMessage, Field, Form, Formik, FormikHelpers } from "formik";
import * as Yup from "yup";
import Button from "../../components/Button";
import NumberField from "../../components/forms/NumberField";
import { betsRequestDTO } from "../../events/DTO/betsRequestDTO.model";

export default function EventForm(props: betFormProps){
    const validationSchema = Yup.object().shape({
        amount: Yup.string().required("This field is required!")
      });
return(
    <Formik 
        initialValues={props.model}
        validationSchema={validationSchema}
        onSubmit={props.onSubmit}
    >
    {(formikProps) => (
        <Form>
            <div className="row m-1">
                <div className="col-8">
                {/* <NumberField field="amount" /> */}
                <div className="mb-3">
                    <Field name="amount" type="number" className="form-control" />
                    {/* <ErrorMessage
                    name="amount"
                    component="div"
                    className="alert alert-danger"
                    /> */}
                </div>

                </div>
<div className="col-2">

        <Button disabled={formikProps.isSubmitting} type="submit" children="+" />
</div>
            </div>
    </Form>
    )}            

    </Formik>
)
    
}

interface betFormProps{
    model: betsRequestDTO;
    onSubmit(values: betsRequestDTO, action: FormikHelpers<betsRequestDTO>): void;
}