import {FC, useState} from "react";
import {Bet} from "../../models/Bet";

interface LotThumbnailProps {
    Date : Date
    Bets : Array<Bet> | null;

}

const LotThumbnail: FC<LotThumbnailProps> = (props: LotThumbnailProps) => {

    return(
        <div >
                    <h2>{props.Date.toDateString()}</h2>
        </div>
    )
}