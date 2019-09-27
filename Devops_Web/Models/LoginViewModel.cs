using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Devops_Web.Models
{ 
    public class LoginViewModel
    {
        public Login Login { get; set; }
        public Cadastro Cadastro { get; set; }
    }

    public class Login
    {
        #region Properties 
        private bool loginCheck = false;
        public bool LoginCheck
        {
            get { return loginCheck; }
            set { loginCheck = value; }
        }

        ///<summary>
        ///Get ou set de Email ou Celular.
        ///</summary>
        [Required(ErrorMessage = "Por favor preecha com Email ou Celular!")]
        [StringLength(32, ErrorMessage = "Este campo deve conter entre 8 e 32 digitos!", MinimumLength = 8)]
        [Display(Name = "Email ou Celular")]        
        [RegularExpression(@"^(?:(?:\+|00)?(55)\s?)?([0-9]{2})((9[0-9]{8}))$|^[a-z0-9.]+@[a-z-0-9]+\.[a-z]+\:?.([a-z]+)$", ErrorMessage = "Por favor preecha um Email ou Celular válido!")]
        public string EmailouCelular { get; set; }

        ///<summary>
        ///Get ou set da Senha
        ///</summary>
        [Required(ErrorMessage = "Por favor digite sua senha!")]
        [StringLength(16, ErrorMessage = "Sua senha deve conter entre 8 e 16 digitos!", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        ///<summary>
        ///Get ou set do uso de cookie para autenticação
        ///</summary>
        [Display(Name = "Lembrar-me")]
        public bool LembrarMe { get; set; }
        #endregion
    }

    public class Cadastro
    {
        #region Properties 
        private bool cadastroCheck = false;
        public bool CadastroCheck
        {
            get { return cadastroCheck; }
            set { cadastroCheck = value; }
        }


        [Required(ErrorMessage = "Por favor preecha seu nome!")]
        [Display(Name = "Nome")]
        [RegularExpression(@"^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$", ErrorMessage = "Por favor preecha um nome válido!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor preecha seu sobrenome!")]
        [Display(Name = "Sobrenome")]
        [RegularExpression(@"^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$", ErrorMessage = "Por favor preecha um sobrenome válido!")]
        public string Sobrenome { get; set; }

        public DateTime? dataNascimento = null;
        [Required(ErrorMessage = "Por favor preecha sua data de nascimento!")]
        [Display(Name = "Data de Nascimento")]        
        public DateTime? DataNascimento
        {
            get { return dataNascimento; }
            set { dataNascimento = value; }
        }

        [Required(ErrorMessage = "Por favor preecha com seu Email ou Celular!")]
        [StringLength(32, ErrorMessage = "Este campo deve conter entre 8 e 32 digitos!", MinimumLength = 8)]
        [Display(Name = "Email ou Celular")]
        [RegularExpression(@"^(?:(?:\+|00)?(55)\s?)?([0-9]{2})((9[0-9]{8}))$|^[a-z0-9.]+@[a-z-0-9]+\.[a-z]+\:?.([a-z]+)$", ErrorMessage = "Por favor preecha um Email ou Celular válido!")]
        public string EmailouCelularCadastro { get; set; }

        [Required(ErrorMessage = "Por favor digite uma senha!")]
        [StringLength(16, ErrorMessage = "Sua senha deve conter entre 8 e 16 digitos!", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        #endregion
    }
}
