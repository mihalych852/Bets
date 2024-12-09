import { ErrorMessage, Field, Form, Formik } from "formik";
import Button from "../../components/Button";
import * as Yup from 'yup';
import React from "react"; 

export default function TextField(props: textFieldProps){
    return(
        <div className="mb-3">
        <label htmlFor={props.field}>{props.title}</label>
        <Field name={props.field} id={props.field} className="form-control" />
        <ErrorMessage name={props.field}>{msg => 
            <div className="text-danger">{msg}</div>}
            </ErrorMessage>
        </div>
    )
}

interface textFieldProps{
    field: string;
    title: string;
}