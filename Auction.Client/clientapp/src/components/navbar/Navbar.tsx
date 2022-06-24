import {NavLink} from "react-router-dom";
import styles from "./navbar.module.css"


export const Navbar = (props : {current : {name : string,nav_to : string},
    links : Array<{name : string,nav_to : string}>}) => {
    return (
        <div id={styles["scrollBar"]}>
            {props.links
                .map(link =>
                    <NavLink className={styles.navbarMenuOption} to={link.nav_to}>{link.name}</NavLink>)
            }
        </div>
    )
}

