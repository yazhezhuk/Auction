import {Bet} from "./Bet";

export class Lot{
    constructor(id : number,startDate : Date,endDate : Date,image: string,bets : Array<Bet> | null){
        this.Id = id;
        this.Bets = bets;
        this.ImageBase64 = image;
        this.StartDate = startDate;
        this.EndDate = endDate;
    }

    public Id : number;
    public StartDate : Date;
    public EndDate : Date;
    public ImageBase64 : string;
    public Bets : Array<Bet> | null;
}