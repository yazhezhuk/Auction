import {Bet} from "../models/Bet";
import {FC, useState} from "react";
import style from "./AuthorizationFrom.module.css"
import {AuthService} from "../services/AuthService";

interface AuthorizationProps {

}

export const AuthorizationForm: FC<AuthorizationProps> = (props: AuthorizationProps) => {
    const [emailText,setEmailText] = useState("")
    const [passwordText,setPasswordText] = useState("")
    const [nameText,setNameText] = useState("")
    const [surnameText,setSurnameText] = useState("")


    return (
        <div id={style.main}>
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
        </div>
    );
}