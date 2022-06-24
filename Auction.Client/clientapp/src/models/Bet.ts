export class Bet {

    constructor(id : number,amount : number,user: string | null){
        this.Id = id;
        this.Amount = amount;
        this.User = user;
    }

    public Id : number
    public Amount : number
    public User : string | null
}