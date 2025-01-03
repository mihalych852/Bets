import { StringSchema } from "yup";

declare module 'yup'{
    class StringSchema{
        fieldIsRequire(): this;
    }
}