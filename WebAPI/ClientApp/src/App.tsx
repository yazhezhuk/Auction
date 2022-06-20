import React from 'react';
import './App.css';
import './components/Navbar/Navbar'
import Navbar from "./components/Navbar/Navbar";
import {navigation} from "./NavigationData";
import {BrowserRouter as Router, Route, Routes} from "react-router-dom"
import Editor from "./components/Editor";
import NotesFeed from "./components/NotesFeed";

function App() {
  return (

    <div className="App">
        <Router>
            <Navbar links={navigation.links} current={navigation.current} key={3}/>
            <Routes>
                <Route element={<Editor/>}  key={0}  path="/add"/>
                <Route element={<NotesFeed/>} key={1} path="/all"/>
            </Routes>
        </Router>
    </div>
  );
}

export default App;
