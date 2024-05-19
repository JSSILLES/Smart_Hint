import React, { useState, useEffect } from 'react';
import api from '../../api/aplicacao';


import TitlePage from '../../components/TitlePages';


export default function Dashboard(props) {

  const [clientesCadastrados, setClientesCadastrados] = useState([]);
  
   useEffect(() => {
        const carregarClientes = async () => {
            try {
                const response = await api.get('cliente');
                setClientesCadastrados(response.data);
                console.log(response)

            } catch (error) {
                console.error('Erro ao carregar clientes:', error);
            }
        };
        carregarClientes();
    }, []); 


  return (
    <>
        <div className='container'>
            <TitlePage title='Dashboard'/>
            <br /><br />
        </div> 

        <div className='container'>           
             <br />         
            <div className='row justify-content-center no-gutters'>
            
                <div className='col-12 col-md-4'>
                    <div className='card border-success border-2'>
                        <div className='card-header'>Clientes</div>
                        <div className='card-body'>
                            <h1 className='text-center'>{clientesCadastrados.length}</h1>
                        </div>
                    </div>
                </div>                
            </div>
        </div>
        
    </>
  )
}
