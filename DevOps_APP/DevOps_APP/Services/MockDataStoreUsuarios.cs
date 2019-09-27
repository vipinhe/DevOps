using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps_APP.Models;

namespace DevOps_APP.Services
{
    public class MockDataStoreUsuarios
    {
        List<Usuario> usuarios;

        public MockDataStoreUsuarios()
        {
            usuarios = new List<Usuario>();
            var mockItems = new List<Usuario>
            {                
                new Usuario { Id = 1, Hash = "c4ca4238a0b923820dcc509a6f75849b", Nome = "Vitor",   SobreNome = "Rodrigo",   DataNascimento = new DateTime(1994, 1, 20), Email = "vitor@gmail.com", Celular = "111", Senha = "698d51a19d8a121ce581499d7b701668"}, 
                new Usuario { Id = 2, Hash = "c81e728d9d4c2f636f067f89cc14862c", Nome = "Jonatha", SobreNome = "Peris",     DataNascimento = new DateTime(2017, 1, 20), Email = "jonatha@gmail.com", Celular = "222", Senha = "bcbe3365e6ac95ea2c0343a2395834dd"},
                new Usuario { Id = 3, Hash = "eccbc87e4b5ce2fe28308fd9f2a7baf3", Nome = "Alex",    SobreNome = "Vitoriano", DataNascimento = new DateTime(2017, 1, 20), Email = "alex@gmail.com", Celular = "333", Senha = "310dcbbf4cce62f762a2aaa148d556bd"},
                new Usuario { Id = 4, Hash = "a87ff679a2f3e71d9181a67b7542122c", Nome = "Caio",    SobreNome = "Mendes",    DataNascimento = new DateTime(2017, 1, 20), Email = "caio@gmail.com", Celular = "444", Senha = "550a141f12de6341fba65b0ad0433500"},
                new Usuario { Id = 5, Hash = "e4da3b7fbbce2345d7772b0674a318d5", Nome = "José",    SobreNome = "Neves",     DataNascimento = new DateTime(2017, 1, 20), Email = "jose@gmail.com", Celular = "555", Senha = "15de21c670ae7c3f6f3f1f37029303c9"},
                new Usuario { Id = 6, Hash = "1679091c5a880faf6fb5e6087eb1b2dc", Nome = "Flavio",  SobreNome = "Romeiro",   DataNascimento = new DateTime(2017, 1, 20), Email = "flavio@gmail.com", Celular = "666", Senha = "fae0b27c451c728867a567e8c1bb4e53"},
            };

            foreach (var usuario in mockItems)
            {
                usuarios.Add(usuario);
            }
        }

        public async Task<string> AutenticarUsuarioAsync(LoginUsuario LoginUsuario)
        {
            bool Login;
            Login = usuarios.Exists(x => (x.Email == LoginUsuario.EmailCelularAutenticacao || x.Celular == LoginUsuario.EmailCelularAutenticacao) && x.Senha == LoginUsuario.SenhaAutenticacao);

            if (!Login)
            {
                return string.Empty;
            }

            var DadosUsuario = usuarios.FirstOrDefault(x => (x.Email == LoginUsuario.EmailCelularAutenticacao || x.Celular == LoginUsuario.EmailCelularAutenticacao) && x.Senha == LoginUsuario.SenhaAutenticacao);

            return await Task.FromResult(DadosUsuario.Hash);
        }

        public async Task<Usuario> BuscarDadosUsuarioAsync(string Hash)
        {
            bool Login;
            Login = usuarios.Exists(x => x.Hash == Hash);

            if (!Login)
            {
                return null;
            }

            var DadosUsuario = usuarios.FirstOrDefault(x => x.Hash == Hash);

            return await Task.FromResult(DadosUsuario);
        }

        public async Task<bool> AdicionarUsuario(Usuario usuario)
        {
            usuarios.Add(usuario);

            var NovoUsuarioId = usuarios.Max(x => x.Id) + 1;
            usuarios.Find(x => x.Email == usuario.Email || x.Celular == usuario.Celular).Id = NovoUsuarioId;


            var NovoUsuarioHash = md5Hash.GetMd5Hash(NovoUsuarioId.ToString());
            usuarios.Find(x => x.Email == usuario.Email || x.Celular == usuario.Celular).Hash = NovoUsuarioHash;

            return await Task.FromResult(true);
        }
    }
}