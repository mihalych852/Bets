export default function Button(props: buttonProps){
    const Button = ({    type: "button",
        disabled: false});
    return <button className="btn btn-primary" 
    disabled={props.disabled}
    type={props.type}
    onClick={props.onClick}>
        {props.children}
    </button>
}

interface buttonProps{
    children: React.ReactNode;
    onClick?(): void;
    type: "button" | "submit";
    disabled: boolean;

}

Button.defaultProps = {
    type: "button",
    disabled: false,
}