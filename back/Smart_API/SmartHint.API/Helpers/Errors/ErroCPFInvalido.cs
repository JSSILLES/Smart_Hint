namespace SmartHint.API.Helpers.Errors
{
    public class ErroCPFInvalido
    {
        public string Mensagem { get; set; }

        public ErroCPFInvalido(string mensagem)
        {
            Mensagem = mensagem;
        }
    }
}
