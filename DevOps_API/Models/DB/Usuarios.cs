using System;
using System.Collections.Generic;

namespace DevOps_API.Models.DB
{
    public partial class Usuarios
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Senha { get; set; }
    }
}
