import {NavLink} from "react-router-dom";
import styles from "./navbar.module.css"
import {AuthService} from "../../services/AuthService";


export const Navbar = (props : {current : {name : string,nav_to : string,for : string},
    links : Array<{name : string,nav_to : string,for: string}>}) => {
    return (
        <div id={styles["scrollBar"]}>
            {props.links
                .map(link =>
                    link.for === AuthService.getRole() || link.for === "All"
                        ? <NavLink className={styles.navbarMenuOption} to={link.nav_to}>{link.name}</NavLink>
                        : null
                )
            }
        </div>
    )
}

