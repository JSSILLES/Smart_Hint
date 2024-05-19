import {useState, useEffect} from 'react';
import {Button, Modal} from 'react-bootstrap';
import TitlePage from '../../components/TitlePages';
import ClienteForm from './ClienteForm';
import ClienteLista from './ClienteLista';

import api from '../../api/aplicacao';

export default function Cliente(){
    const [showClienteModal, setShowClienteModal] = useState(false);
    const [smShowConfirmeModal, setSmShowClienteModal] = useState(false);
    const [clientes,setClientes]= useState([]);
    const [cliente,setCliente]= useState({id: 0});

    useEffect(() => {
        const carregarClientes = async () => {
            try {
                const response = await api.get('cliente');
                setClientes(response.data);
            } catch (error) {
                console.error('Erro ao carregar clientes:', error);
            }
        };

        carregarClientes();
    }, []); 

    const handleClienteModal = () => {
        setShowClienteModal(!showClienteModal);    
        // window.location.reload();
    }

    const handleConfirmeModalCliente = (id) => {

        if(id !== 0 && id !== undefined) {
            const cliente = clientes.filter((cliente) => cliente.idCliente === id);
            setCliente(cliente[0]);   
        }
        else{
            setCliente({id: 0})
        }
        setSmShowClienteModal(!smShowConfirmeModal);    
    }

    const novoCliente = () => {

        debugger

        setCliente({ idCliente: 0});
        handleClienteModal();
    }

    const addCliente = async (cliente) =>{  

        debugger

        handleClienteModal();          
        const response = api.post('cliente', cliente);
        setClientes([...clientes, response.data]);
    }

    function cancelarCliente(){
        setCliente({ id: 0});
        handleClienteModal();
    }

    const atualizarCliente = async(cli) => {

        handleClienteModal();

       try {
            const response = await api.put(`cliente/${cli.idPessoa}`, cli);

            const updatedCliente = response.data;

            // Atualize o estado dos clientes com o cliente atualizado
            setClientes(clientes.map(item => item.idPessoa === updatedCliente.idPessoa ? updatedCliente : item));
            setCliente(updatedCliente);

            setShowClienteModal(!showClienteModal);
        } catch (error) {
            console.error('Erro ao atualizar cliente:', error);
        }
    }

    const deletarCliente = async (idCliente) => {

        handleConfirmeModalCliente(0);

        const response = await api.delete(`cliente/${idCliente}`);

        if (response)
        {
          const clientesFiltrados = clientes.filter(
            (clientes) => clientes.idPessoa !== idCliente
          );
          setClientes([...clientesFiltrados]);
        }
    }

    function pegarCliente(idCliente){         
        const cliente = clientes.filter((cliente) => cliente.idPessoa === idCliente);
                
        setCliente(cliente[0]);
        handleClienteModal();       

    }


    return (
        <>
           <TitlePage title={'Cliente ' + (cliente && (cliente.idCliente !== 0 && cliente.idCliente !== undefined)?  cliente.idCliente :'' )}>
                <Button variant="outline-secondary" onClick={novoCliente}>
                    <i className='fas fa-plus'></i>
                </Button> 
           </TitlePage>

            <div>
                <ClienteLista
                    clientes={clientes}
                    pegarCliente = {pegarCliente}
                    handleConfirmeModalCliente={handleConfirmeModalCliente}
                />                                               
              
            </div>
                      
            <Modal show={showClienteModal} onHide={handleClienteModal}>
                <Modal.Header closeButton>
                    <Modal.Title>
                        Cliente {cliente.idCliente !== 0 ? cliente.idCliente : ''} 
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <ClienteForm
                        addCliente={addCliente}
                        cancelarCliente={cancelarCliente}
                        atualizarCliente={atualizarCliente}
                        cliSelecionado={cliente}
                        clientes={clientes}
                    />                    
                </Modal.Body>
            </Modal>

            <Modal 
                size='sm'
                show={smShowConfirmeModal} 
                onHide={handleConfirmeModalCliente}
            >
                <Modal.Header closeButton>
                    <Modal.Title>
                        Excluindo Cliente {cliente.idCliente !== 0 ? cliente.idCliente : ''}
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    Tem certeza que deseja excluir o Cliente {cliente.idCliente}? 
                </Modal.Body>
                <Modal.Footer className='d-flex justify-content-between'>
                    <button className='btn btn-outline-success me-2'
                            onClick={() => deletarCliente(cliente.idCliente)}>
                        <i className='fas fa-check me-2'></i>
                        Sim
                    </button>
                    <button className='btn btn-danger me-2'
                            onClick={() => handleConfirmeModalCliente(0)}>
                    <i className='fas fa-times me-2'></i>
                        Não
                    </button>
                </Modal.Footer>
            </Modal>

        </>
    )
}