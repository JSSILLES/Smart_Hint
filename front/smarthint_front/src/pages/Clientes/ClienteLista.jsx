import React from 'react';
import ClienteItem from './ClienteItem';
import Pesquisa from '../../components/Pesquisa/Pesquisar';


export default function ClienteLista(props) {
  
    debugger

    return (
      
      <>
      <div className="col-md-12 d-flex"  style={{marginbotton: "50px" }}>
          <Pesquisa/>
        </div>
        <div className='container'>
          {props.clientes && props.clientes.length > 0 ? (
            props.clientes.map(cliente => (
              <ClienteItem
                key={cliente.idCliente}
                cli={cliente}  
                pegarCliente={props.pegarCliente}   
                handleConfirmeModalCliente={props.handleConfirmeModalCliente}            
              />  
            ))
          ) : (
            <p>Nenhum cliente encontrado</p>
          )}
        </div>        
      </>  
    );
  }
  