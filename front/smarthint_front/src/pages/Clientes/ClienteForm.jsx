import {useEffect, useState} from 'react';
import {useForm} from 'react-hook-form';

const clienteInicial ={
  tipoCliente: 0,  
  nome: '',
  documento: '',
  estadoCivil: '',
  sexo: '',
  removido: false,
  email: '',
  
  // cep: '',  
  // rua:'',
  // numero: '',
  // complemento: '',
  // estado: '',
  // cidade: '',
  // bairro: '',
}

export default function ClienteForm(props) {

  debugger

  const dataAtual = new Date(); 

  const dia = String(dataAtual.getDate()).padStart(2, '0'); // Obtém o dia com zero à esquerda, se necessário
  const mes = String(dataAtual.getMonth() + 1).padStart(2, '0'); // Obtém o mês com zero à esquerda, se necessário
  const ano = dataAtual.getFullYear(); // Obtém o ano
  
  const dataFormatada = `${dia}/${mes}/${ano}`;  

  const [cliente, setCliente] = useState({
    // idCliente: 0,
    removido: false,    
  });

  const {register, setValue } = useForm();

  const handleChange = (event) => {        
    const { name, value } = event.target;
    setCliente(prevCliente => ({
        ...prevCliente,
        endereco: {
            ...prevCliente.endereco,
            [name]: value
        }
    }));
  };
  
  const formatPhoneNumber = (phoneNumber) => {
    const cleaned = ('' + phoneNumber).replace(/\D/g, '');
    const match = cleaned.match(/^(\d{2})(\d{5})(\d{4})$/);
    if (match) {
      return '(' + match[1] + ') ' + match[2] + '-' + match[3];
    }
    return phoneNumber;
  };

  const inputTextHandlerTelefone = (event) => {
    const { name, value } = event.target;
    // Se o nome do campo de entrada for 'numeroTelefone', formatamos o valor do número de telefone
    const newValue = name === 'numeroTelefone' ? formatPhoneNumber(value) : value;
    setCliente(prevCliente => ({
      ...prevCliente,
      [name]: newValue
    }));
  };  

  const handleCancelar = (e) => {
    e.preventDefault();
    props.cancelarCliente();
    setCliente({ ...clienteInicial, endereco: {} });
  };

  useEffect(() => {
    if (props.cliSelecionado && props.cliSelecionado.idPessoa !== 0) {
      setCliente(props.cliSelecionado);
    }
  }, [props.cliSelecionado]);


  const preencheEndereco = (e) => {
    const cep = e.target.value.replace(/\D/g, '');

    fetch(`http://viacep.com.br/ws/${cep}/json/`)
      .then((resp) => resp.json())
      .then((data) => {
        setCliente((prevCliente) => ({
          ...prevCliente,
          endereco: {
            ...prevCliente.endereco,
            rua: data.logradouro,
            complemento: data.complemento,
            estado: data.uf,
            cidade: data.localidade,
            bairro: data.bairro,
          },
        }));
      })
      .catch((error) => {
        console.error('Erro na requisição: ' + error);
      });
  };


  const inputTextHandler = (e) =>{
        const {name, value} = e.target;

    setCliente(prevCliente => ({
      ...prevCliente,
      [name]: value
    }));
  };

  const handleSubmit = (e) => {
      e.preventDefault();
      if (props.cliSelecionado.idCliente !== 0) props.atualizarCliente(cliente);
      else props.addCliente(cliente);
      setCliente({ ...clienteInicial, endereco: {} });
  };

  return (
    <>
      <form className='row g-3' onSubmit={handleSubmit}>          
          <div className="col-md-12 d-flex align-items-center justify-content-end">
            <label className="form-label me-4">Data Cadastro</label>
            <p className="mb-0">{dataFormatada}</p>
          </div>
          <div className="col-md-12">
            <label className="form-label">Nome Completo*</label>
            <input 
              required
              name='nome'
              value={cliente.nome}
              onChange={inputTextHandler}
              id='nome' 
              type='text' 
              maxLength={80}
              className="form-control"
            />
          </div>

          <div className="col-md-4">
            <label className="form-label">Documento</label>
            <input 
               required
               name='documento'
               value={cliente.documento}
               onChange={inputTextHandler}
               id='documento' 
               type='text' 
               maxLength={9}               
               className="form-control"      
            />
          </div>

          <div className="col-md-4">
            <label className="form-label">Sexo</label>
            <select 
              name='sexo'
              value={cliente.sexo}
              onChange={inputTextHandler}
              id="sexo" 
              className="form-select"
            >
                <option defaultValue='0'>Selecionar...</option>
                <option value='Masculino'>Masculino</option>
                <option value='Feminino'>Feminino</option>
            </select>
          </div>

          <div className="col-md-4">
            <label className="form-label">Estado Civil</label>
            <select 
              name='estadoCivil'
              value={cliente.estadoCivil}
              onChange={inputTextHandler}
              id="estadoCivil" 
              className="form-select"
            >
                <option defaultValue='0'>Selecionar...</option>
                <option value='Solteiro(a)'>Solteiro(a)</option>
                <option value='Casado(a)'>Casado(a)</option>
                <option value='Divorciado(a)'>Divorciado(a)</option>
                <option value='Viúvo(a)'>Viúvo(a)</option>

            </select>
          </div>

          <div className="col-md-4">
            <label className="form-label">Telefone</label>
            <input 
              required
              name='numeroTelefone'
              value={cliente.numeroTelefone}
              onChange={inputTextHandlerTelefone}
              id='numeroTelefone' 
              type='text' 
              maxLength={14}
              className="form-control"      
            />
          </div>

          <div className="col-md-8">
            <label className="form-label">E-mail</label>
            <input 
              name='email'
              value={cliente.email}
              onChange={handleChange}
              type='email' 
              className="form-control"
            />
          </div>

          <div className="col-md-4">
          <label className="form-label">CEP*</label>
          <input
            name='cep'
            value={props.cli?.idPessoa !== 0 ? cliente.endereco?.cep : ''}           
            onBlur={preencheEndereco}
            onChange={handleChange}  // Atribuindo a função handleChange ao evento onChange
            id='cep'
            type='text'
            maxLength={8}
            className="form-control"
          />
        </div>

        <div className="col-md-8">
            <label className="form-label">Endereço</label>
            <input 
                name='rua'
                value={props.cli?.idPessoa !== 0 ? cliente.endereco?.rua : ''}
                onChange={handleChange}
                id='rua' 
                type='text' 
                maxLength={70}
                className="form-control"              
            />
          </div>

          <div className="col-md-3">
            <label className="form-label">Número*</label>
            <input 
              name='numero'  
              value={props.cli?.idPessoa !== 0 ? cliente.endereco?.numero : ''}
              onChange={handleChange}
              id='numero' 
              type='text' 
              maxLength={5}
              className="form-control"
            />
          </div>

          <div className="col-md-4">
            <label className="form-label">Complemento</label>
            <input 
                name='complemento'
                value={props.cli?.idPessoa !== 0 ? cliente.endereco?.complemento : ''}
                onChange={handleChange}
                id='complemento' 
                type='text' 
                maxLength={70}
                className="form-control"
            />
          </div>

          <div className="col-md-5">
            <label className="form-label">Bairro</label>
            <input 
              name='bairro'
              value={props.cli?.idPessoa !== 0 ? cliente.endereco?.bairro : ''}
              onChange={handleChange}
              {...register("bairro")}
              id='bairro' 
              type='text' 
              maxLength={50}
              className="form-control"
          />
          </div>

          <div className="col-md-7">
            <label className="form-label">Cidade</label>
            <input 
                name='cidade'
                value={props.cli?.idPessoa !== 0 ? cliente.endereco?.cidade : ''}
                {...register("cidade")}
                onChange={handleChange}
                id='cidade' 
                type='text' 
                maxLength={50}
                className="form-control"
            />
          </div>

          <div className="col-md-5">
            <label className="form-label">Estado</label>
            <input 
                name='estado'  
                value={ props.cli?.idPessoa !== 0 ? cliente.endereco?.estado : ''}
                {...register("estado")}
                onChange={handleChange}
                id='estado' 
                type='text' 
                maxLength={2}
                className="form-control"
            />
          </div>

          <hr/>

          <div className="col-12 mt-0">
          {props.cliSelecionado?.idCliente  === 0 ? (
            <>
              <button className='btn btn-outline-success me-3' type='submit'>
                <i className='fas fa-plus me-1'></i>
                Salvar (CRIAÇÃO)
              </button>
            </>
          ) : (
            <>
              <button className='btn btn-outline-success me-3' type='submit'>
                <i className='fas fa-plus me-2'></i>
                Salvar (EDIÇÃO)
              </button>
            </>
          )}
          <button className='btn btn-outline-warning' onClick={handleCancelar}>
            <i className='fas fa-xmark me-2'></i>
            Cancelar
          </button>
        </div>

        </form>

        <footer/>
      </>
  )
}
