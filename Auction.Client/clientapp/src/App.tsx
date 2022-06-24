import './App.css';
import './components/navbar/Navbar'
import {Navbar} from "./components/navbar/Navbar";
import {navigation} from "./NavigationData";
import {BrowserRouter as Router, Route, Routes} from "react-router-dom"
import  "./routes/SecuredRoute";
import {AuthorizationForm} from "./components/AuthorizationForm";
import {ProtectedRouteProps} from "./routes/SecuredRoute";
import {AuthService} from "./services/AuthService";


const defaultProtectedRouteProps: Omit<ProtectedRouteProps, 'outlet'> = {
    isAuthenticated: AuthService.getUser() !== null,
    authenticationPath: '/login',
};
function App() {
  return (

    <div className="App">
        <Router>
            <Navbar links={navigation.links} current={navigation.current}/>
            <Routes>
                <Route element={<AuthorizationForm/>} path="/"/>
            </Routes>
        </Router>
    </div>
  );
}

export default App;
