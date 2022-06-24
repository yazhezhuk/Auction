export class User {

    constructor(id : number,email : string,token : string, balance : number){
        this.Id = id;
        this.Balance = balance;
        this.Email = email;
    }

    public Id : number
    public Balance : number
    public Email : string | null
}