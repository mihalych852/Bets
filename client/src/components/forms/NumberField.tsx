import { ErrorMessage, Field, Form, Formik } from "formik";
import Button from "../Button";
import * as Yup from 'yup';
import React from "react"; 

export default function NumberField(props: numberFieldProps){
    return(
        <div className="mb-2">
        {/* <label htmlFor={props.field}>{props.title}</label> */}
        <Field type="number" name={props.field} id={props.field} className="form-control" />
        {/* <ErrorMessage name={props.field}>{msg => 
            <div className="text-danger">{msg}</div>}
            </ErrorMessage> */}
        </div>
    )
}

interface numberFieldProps{
    field: string;
    //name: string;
    title?: string;
}