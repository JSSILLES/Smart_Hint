import {React} from 'react'
import FormatDate from '../../components/Utils/Utils';

export default function ClienteItem(props){

    debugger

    console.log("PROPS: " + props)
    
    function sexoLabel(param){
        switch(param){
            case 'Masculino':
            case 'Feminino':
               return param;
            default:
               return 'Não Definido';
        }
    }

    function sexoStyle(param, icone){
        switch(param){
            case 'Masculino':
                return icone? 'person': 'primary';
            case 'Feminino':
                    return icone? 'person-dress': 'info';
            default:
                return 'Não Definido';
        }
    }

    return(
        <>            
            <div key={props.cli.idPessoa} id={props.cli.idPessoa} className={"card mb-2 shadow-sm border-" + sexoStyle(props.cli.sexo)} >
                        
                <div className="card-body">
                    <div className='d-flex justify-content-between'>
                        {/* <p className="card-text"><b>IdPessoa: </b>{props.cli.idPessoa}</p>  */}
                        <p className="card-text"><b>Data Cadastro:</b> {FormatDate(props.cli.dataCadastro)}</p>
                        <p className="card-text"><b>Nome: </b>{props.cli.nome}</p>     
                        <p className="card-text"><b>RG: </b> {props.cli.documento}</p>                     
                    </div>
                    
                    <div className='d-flex justify-content-between'>   
                        <p className="card-text"><b>Email:</b> {props.cli.email}</p>
                        <p className="card-text"><b>Estado Civil:</b> {props.cli.estadoCivil}</p>
                        <h6>
                            <b>Sexo: </b>
                            <span className={'ms-1 text-'+ sexoStyle(props.cli.sexo)}>
                                <i className={'me-1 fas fa-' + sexoStyle(props.cli.sexo, true) }></i>
                                {sexoLabel(props.cli.sexo)}
                            </span>
                        </h6>                          
                    </div>
    
                    <div className='d-flex justify-content-between'>   
                        <p className="card-text"><b>CEP:</b> {props.cli?.endereco?.cep}</p>
                        <p className="card-text"><b>Endereço:</b> {props.cli?.endereco?.rua ? props.cli.endereco.rua : ''}</p>
                        <p className="card-text"><b>Número:</b> {props.cli.endereco ? props.cli.endereco.numero : ''}</p>
                    </div>
    
                    <div className='d-flex justify-content-between'>   
                        <p className="card-text">
                            <b>Complemento:</b> {props.cli.endereco && props.cli.endereco.complemento ? props.cli.endereco.complemento : ''}
                        </p>
                        <p className="card-text"><b>Bairro:</b> {props.cli.endereco ? props.cli.endereco.bairro : ''}</p>
                        <p className="card-text"><b>Cidade:</b> {props.cli.endereco ? `${props.cli.endereco.cidade} - ${props.cli.endereco.estado}` : ''}</p>
                    </div>
                    <div className='d-flex justify-content-end pt-2 m-0 border-top'>
                        <button 
                            className='me-2 btn-sm btn btn-outline-primary'
                            onClick={() => props.pegarCliente(props.cli.idPessoa)}
                        >
                            <i className='fas fa-pen me-2'></i>
                            Editar
                        </button>
                        
                        <button className='btn btn-sm btn-outline-danger' 
                                onClick={() => props. handleConfirmeModalCliente(props.cli.idPessoa)}>
                            <i className='fas fa-trash me-2'></i>
                            Deletar
                        </button>
                    </div>
                </div>
            </div>
                    
        </>
        
        
       
    )
}