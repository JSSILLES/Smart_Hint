import React, { useState } from 'react';
import ClienteLista from './../../../src/pages/Clientes/ClienteLista';

const App = () => {

  debugger

  const [query, setQuery] = useState('');
  const [searchResults, setSearchResults] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [selectedResult, setSelectedResult] = useState(null); // Estado para armazenar o resultado selecionado

  const handleInputChange = (event) => {
    setQuery(event.target.value);
  };

  const handleSearch = async () => {
    try {
      setLoading(true);
      const response = await fetch(`http://localhost:5145/api/Cliente/pesquisa/${query}`);
      
      if (!response.ok) {
        throw new Error('Erro ao buscar dados');
      }

      const data = await response.json();
      
      if (!Array.isArray(data) && data.length === 0) {
        throw new Error('Nenhum resultado encontrado');
      }

      setSearchResults(data);
      setSelectedResult(null); // Limpar o resultado selecionado quando uma nova pesquisa é realizada
      setError(null);
    } catch (error) {
      console.error('Erro ao buscar dados:', error);
      setError('Nenhum resultado encontrado. Tente novamente.');
    } finally {
      setLoading(false);
    }
  };

  const handleResultClick = (result) => {
    setSelectedResult(result);
  };

  return (
    <div className='col-lg-12' style={{ flex: 1,marginLeft: '10%', padding: '10px', marginBottom: 20, width: '100%' }}>
      <div className='col-lg-12'>
        <input
          type="text"
          placeholder="Pesquisar..."
          value={query}
          onChange={handleInputChange}
          style={{ flex: 1,marginLeft: '10%', padding: '10px',width: '54%' }}
        />
        <button onClick={handleSearch}
              style={{ flex: 1, padding: '10px' }}
        >   Buscar
        </button>
      </div>
      {loading ? (
        <p>Carregando...</p>
      ) : (
        <>
          {error ? (
            <p>{error}</p>
          ) : (
            <>
              {searchResults.length > 0 ? (
                <ul>
                  {searchResults.map((result, index) => (
                    <li key={index} onClick={() => handleResultClick(result)}>
                      {result.nome} - {result.documento} {/* Exemplo de como exibir alguns detalhes do resultado */}
                    </li>
                  ))}
                </ul>
              ) : (
                <div className='col-lg-12'>
                    <p style={{ flex: 1, padding: '10px' }}>Nenhum resultado encontrado</p>
                </div>
              )}
            </>
          )}
        </>
      )}
      {selectedResult && (
        <div>
          <ClienteLista/>
        </div>
      )}
    </div>
  );
};

export default App;
