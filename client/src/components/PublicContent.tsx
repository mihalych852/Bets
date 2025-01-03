import { Link } from "react-router-dom";

export default function PublicContent(){
    return(
        <>
            <div>
                <p>Для участия в ставках необходимо пройти войти в <Link className="" to="login">account</Link> или <Link className="" to="register">register</Link>.</p>
            </div>
        </>
    )
}