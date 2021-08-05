using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Treinando_MVC_e_Sessao.Data_Access_Layer;

namespace Treinando_MVC_e_Sessao.Models
{
    public class ModelCadastro
    {
        public String Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório !")]
        [Display(Name = "Nome")]
        [MinLength(3, ErrorMessage = ("Campo entre 03 e 50 caracteres"))]
        public String Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório !")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "O Email não possui um formato válido !")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Campo obbigatório !")]
        [MinLength(4, ErrorMessage = "Mínimo 04 caracteres !")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public String Senha { get; set; }

        [Required(ErrorMessage = "Campo obbigatório !")]
        [MinLength(4, ErrorMessage = "Mínimo 04 caracteres !")]
        [Display(Name = "Confirme à Senha")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "A Senha não confere !")]
        public String ConfSenha { get; set; }

        public void InserirUsuario()
        {
            try
            {
               if (Id == null)
                {
                    DAL objDAL = new DAL();
                    objDAL.LimparParametro();
                    objDAL.AddParametros("Nome", Nome);
                    objDAL.AddParametros("Email", Email);
                    objDAL.AddParametros("Senha", Senha);
                    String IdLogado = objDAL.ExecutarManipulacao(CommandType.Text, "Insert Into Login (Nome, Email, Senha) Values (@Nome, @Email, @Senha)").ToString();
                    objDAL.FecharConexao();
                }
            }
            catch
            {
                //
            }
        }
    }
}
