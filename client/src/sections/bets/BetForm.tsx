
import { ErrorMessage, Field, Form, Formik, FormikHelpers } from "formik";
import Button from "../../components/Button";
import NumberField from "../../components/forms/NumberField";
import { betsRequestDTO } from "../../events/DTO/betsRequestDTO.model";

export default function EventForm(props: betFormProps){
return(
    <Formik 
        initialValues={props.model}
        onSubmit={props.onSubmit}
    >
    {(formikProps) => (
        <Form>
            <div className="row m-1">
                <div className="col-8">
        <NumberField field="amount" />

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