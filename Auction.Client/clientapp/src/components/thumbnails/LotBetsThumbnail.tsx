import {FC, useState} from "react";
import {Bet} from "../../models/Bet";
// @ts-ignore
import style from 'LotBetsThumbnail.module.css'

interface LotBetsThumbnailProps {
    Bets : Array<Bet> | null;
}

const LotBetsThumbnail: FC<LotBetsThumbnailProps> = (props: LotBetsThumbnailProps) => {

    return(
        <div style={style}>
            {props.Bets?.map(bet =>
            <strong><li>{bet.User}</li></strong>

            )}
        </div>
    )
}