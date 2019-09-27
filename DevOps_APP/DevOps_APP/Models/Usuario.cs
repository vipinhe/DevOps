using System;

namespace DevOps_APP.Models
{
    public class Usuario
    {
        public int? Id { get; set; }
        public string Hash { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Senha { get; set; }
    }
}