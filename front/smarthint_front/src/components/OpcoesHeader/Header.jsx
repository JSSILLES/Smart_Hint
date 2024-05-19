import React from 'react';
import { Nav } from 'react-bootstrap';
import { NavLink } from 'react-router-dom';

function Header() {
  return (
      <Nav className="me-auto">
          <Nav.Link activeClassName='active' as={NavLink} to="/cliente/lista" style={{ color: "white" }}>Clientes</Nav.Link>         
      </Nav>

  );
}

export default Header;