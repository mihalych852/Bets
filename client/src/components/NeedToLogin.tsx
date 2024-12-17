import { Link } from "react-router-dom";

export default function NeedToLogin(){
    return(
        <>
            <div>
                <p>Для участия в ставках необходимо пройти войти в <Link className="" to="../login">аккаунт</Link> или <Link className="" to="../register">зарегистрироваться</Link>.</p>
            </div>
        </>
    )
}