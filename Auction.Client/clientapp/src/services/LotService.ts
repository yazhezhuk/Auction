import {Lot} from "../models/Lot";
import {logger} from "workbox-core/_private";
import {Bet} from "../models/Bet";

export abstract class LotService {
    public static basePath = "/api/lot";

    public static uploadNewLot(lot : Lot) : void {

        logger.log("Trying to upload new lot");
        fetch(this.basePath, {
            method: "POST",
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(lot)
        }).then(res => {
            console.log("Request complete! response:", res);
        }).catch(e => console.log(e));
    }

    public static async fetchLotBets(lot: Lot) : Promise<Array<Bet>>
    {
        const data = await fetch(this.basePath + '/' + lot.Id +'/bets')

        return await data.json()
    }

    public static async fetchLots() : Promise<Array<Lot>>  {

        const response = await this.fetchPath()

        return (response.map(
            (r: {id:number,startDate: Date,endDate: Date,image : string}) =>
                new Lot(r.id,r.startDate,r.endDate,r.image,null))
        )
    }

    private static async fetchPath() : Promise<any> {
        const data = await fetch(LotService.basePath)
        console.log(data)
        return await data.json()
    }
}