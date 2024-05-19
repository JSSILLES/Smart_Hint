import {Navbar, Container, Nav, NavDropdown} from 'react-bootstrap';
import { NavLink } from 'react-router-dom';
import logoSmart from '../../src/img/logoSmart.jpeg';
import Header from './OpcoesHeader/Header';

const estiloTitle = {
    backgroundColor: '#005844',
    color: '#ffffff', // Define a cor do texto para branco para melhor contraste
    padding: '20px 0', // Adiciona um espaçamento interno superior e inferior de 20px
};


export default function Menu() {
  return (    
        <Navbar expand="lg" style={estiloTitle} >
            <Container>
                <Navbar.Brand as={NavLink} to='/'>
                    <img src={logoSmart} alt="Logo da Smart"
                        className='d-inline-block align-top' 
                        width='80' height='40' />
                </Navbar.Brand>
                
                <Navbar.Toggle aria-controls="responsive-navbar-nav" style={{ backgroundColor: "white" }}/>

                <Navbar.Collapse id="responsive-navbar-nav"> 
                    <Header/>
                </Navbar.Collapse>
            </Container>
        </Navbar>
  )
}
