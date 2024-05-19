import './App.css';
import Cliente from './pages/Clientes/Cliente';
import { Switch, Route } from 'react-router-dom';
import Dashboard from './pages/Dashboard/Dashboard';
import PageNotFound from './pages/PageNotFound';

export default function App(){
    
    return (
        <Switch>  
            <Route path='/' exact component={Dashboard}/>
            <Route path='/cliente/lista' component={Cliente}/>            
            <Route component={PageNotFound}/>

        </Switch>
    );
}
