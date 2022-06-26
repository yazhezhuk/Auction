import {FC, useState} from "react";
import style from "./AuthorizationFrom.module.css"
import {AuthService} from "../services/AuthService";

interface AuthorizationProps {

}

export const AuthorizationForm: FC<AuthorizationProps> = (props: AuthorizationProps) => {
    const [signinButtonClicked,setSigninButtonClicked] = useState(true)
    const [emailText,setEmailText] = useState("")
    const [passwordText,setPasswordText] = useState("")
    const [nameText,setNameText] = useState("")
    const [surnameText,setSurnameText] = useState("")


    return (
        <div id={style.main}>
            {!signinButtonClicked ?
                <>
            <label title={"Email"}>
                Email
            <input onChange={(event) => {
                setEmailText(event.currentTarget.value)
            }} type="text"/>
            </label>
            <label title={"Password"}>
            <input onChange={(event) => {
                setPasswordText(event.currentTarget.value)
            }} type="text"/>
            </label>
            <label title={"First name"}>
                <input onChange={(event) => {
                    setNameText(event.currentTarget.value)
                }} type="text"/>
            </label>
            <label title={"Surname"}>
                <input onChange={(event) => {
                    setSurnameText(event.currentTarget.value)
                }} type="text"/>
            </label>

            <button onClick={() =>{
                AuthService.signIn(emailText, passwordText, nameText, surnameText)
                    .then(r  => {
                        if (r)
                            console.log("Sucessfully registered")
                    } );
            }}>Register</button>
            </> : <>
                <label title={"Email"}/>
            Email
            <input onChange={(event) => {
                setEmailText(event.currentTarget.value)
            }} type="text"/>
            <label title={"Password"}/>
            Password
                <input onChange={(event) => {
                    setPasswordText(event.currentTarget.value)
                }} type="text"/>
            <button onClick={() =>{
                AuthService.logIn(emailText, passwordText)
                    .then(r  => {
                        if (r)
                            console.log("Sucessfully logged in")
                    } );
            }}>Log In</button>
            <button onClick={() => setSigninButtonClicked(false)}>Sign In</button>
                </>}
        </div>
    );
}