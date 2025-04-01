namespace PaperlessAPI.api.Shared.Models
{
    public record Message(string Code, string Description)
    {
        /// <summary>
        /// Código da mensagem
        /// </summary>
        public string Code { get; set; } = Code;

        /// <summary>
        /// Descrição da mensagem
        /// </summary>
        public string Description { get; set; } = Description;
    }
}