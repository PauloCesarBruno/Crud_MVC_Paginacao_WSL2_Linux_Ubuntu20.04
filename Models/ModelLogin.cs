using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Treinando_MVC_e_Sessao.Data_Access_Layer;

namespace Treinando_MVC_e_Sessao.Models
{
    public class ModelLogin
    {
        public String Id { get; set; }

        [Display(Name ="Nome")]
        [MinLength(3,ErrorMessage =("Campo entre 03 e 50 caracteres"))]
        public String Nome { get; set; }

        [Required(ErrorMessage ="Campo Obrigatório !")]
        [Display(Name ="Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "O Email não possui um formato válido !")]
        public String Email { get; set; }

        [Required(ErrorMessage ="Campo obbigatório !")]
        [MinLength(4,ErrorMessage ="Mínimo 04 caracteres !")]
        [Display(Name ="Senha")]
        [DataType(DataType.Password)]
        public String Senha { get; set; }

        public Boolean ValidarLogin()
        {
            DAL objDAL = new DAL();
            objDAL.LimparParametro();
            String sql = $"Select Id, Nome From Login Where Email = '{Email}' And Senha = '{Senha}'";
            DataTable dt = objDAL.RetDatatable(sql);

            if(dt.Rows.Count == 1)
            {
                Id = dt.Rows[0]["Id"].ToString();
                Nome = dt.Rows[0]["Nome"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
