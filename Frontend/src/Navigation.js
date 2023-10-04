import React,{Component} from 'react';
import {NavLink} from 'react-router-dom';

import { ScrollPanel } from 'primereact/scrollpanel';
import Dashboard from './Dashboard';
export class Navigation extends Component{

    render(){
        return(
            <Dashboard></Dashboard>
        )
    }
}