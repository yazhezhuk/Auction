import {Lot} from "../models/Lot";
import {logger} from "workbox-core/_private";
import {Bet} from "../models/Bet";
import {User} from "../models/User";
import jwt_decode from "jwt-decode";

interface Token {
    name: string;
    exp: number;
    role: string;
}

export abstract class AuthService {
    public static basePath = "http://localhost:3000/api/auth/";

    public static getUser() : User | null{
        const data = localStorage.getItem("User");
        if (data === null){
            console.log("User not logged in.")
            return null;
        }
        return JSON.parse(data)
    }

    public static getRole(): string | null{
        const token = this.getToken()
        if (token === null) {
            console.log("Token missing.")
            return null
        }
        return jwt_decode<Token>(token).role;
    }
    private static getToken(): string | null{
        return localStorage.getItem("Token")
    }

    public static async logIn(email: string, password : string): Promise<boolean> {
        if (this.getToken() === null) {
            logger.log("Trying to log in");
            await fetch(this.basePath + "logIn/", {
                method: "POST",
                headers: {'Content-Type': 'application/json'},
                body: JSON.stringify({email, password})
            }).then(async res => {
                console.log("Request complete! :", res);
                const responseJson = await res.json();
                const token = responseJson.token;
                const user = responseJson.user;

                console.log("Response token: " + token);
                console.log("Response user: " + user);

                localStorage.setItem("Token", token);
                localStorage.setItem("User", user);
                return true;
            }).catch(e => {
                console.log(e);
                return false;
            });
            return false
        }
        else {
            return true
        }
    }

    public static async signIn(email: string, password : string,firstname: string,surname:string): Promise<boolean> {

        if (localStorage)
            logger.log("Trying to sign in");
        await fetch(this.basePath + "signin/", {
            method: "POST",
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({email,password,firstname,surname})
        }).then(async res => {
            console.log("Request complete! response:", res);
            localStorage.setItem("User", await res.json());
            return true;
        }).catch(e => {
            console.log(e);
            return false;
        });
        return false
    }
}