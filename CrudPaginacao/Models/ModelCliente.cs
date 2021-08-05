using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Treinando_MVC_e_Sessao.Data_Access_Layer;

namespace Treinando_MVC_e_Sessao.Models
{
    public class ModelCliente
    {
        public String Id { get; set; }

        [Display(Name ="Nome")]
        [MinLength(3, ErrorMessage ="Mínimo 03 caracteres")]
        public String Nome { get; set; }
        
        [Display(Name ="CPF / CNPJ")]
        public String CPF { get; set; }

        [Display(Name ="Data de Nascimento")]
        [DataType(DataType.Date)]
         //ABAIXO FORMATAR A DATA SEM A HORA
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Limite de Crédito")]
        [DataType(DataType.Currency)]
        public Decimal? LimiteDeCredito { get; set; }

        public List<ModelCliente> ListarTodosClientes()
        {
            try
            {
                DAL objDAL = new DAL();
                objDAL.LimparParametro();
                List<ModelCliente> lista = new List<ModelCliente>();
                ModelCliente item;
                DataTable dt = objDAL.ExecutarConsulta(CommandType.Text, "Select Id, Nome, CPF, DataNascimento, LimiteDeCredito From Clientes Order By Nome");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    item = new ModelCliente()
                    {
                        Id = dt.Rows[i]["Id"].ToString(),
                        Nome = dt.Rows[i]["Nome"].ToString(),
                        CPF = dt.Rows[i]["CPF"].ToString(),
                        DataNascimento = DateTime.Parse (dt.Rows[i]["DataNascimento"].ToString()),
                        LimiteDeCredito = Convert.ToDecimal (dt.Rows[i]["LimiteDeCredito"])
                    };
                    lista.Add(item);
                    objDAL.FecharConexao();
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ModelCliente> ListarTodosClientesCPF(String cpf)
        {
            try
            {
                DAL objDAL = new DAL();
                objDAL.LimparParametro();
                List<ModelCliente> lista = new List<ModelCliente>();
                ModelCliente item;
                DataTable dt = objDAL.ExecutarConsulta(CommandType.Text, $"Select Id, Nome, CPF, DataNascimento, LimiteDeCredito From Clientes Where CPF = '{cpf}'");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    item = new ModelCliente()
                    {
                        Id = dt.Rows[i]["Id"].ToString(),
                        Nome = dt.Rows[i]["Nome"].ToString(),
                        CPF = dt.Rows[i]["CPF"].ToString(),
                        DataNascimento = DateTime.Parse(dt.Rows[i]["DataNascimento"].ToString()),
                        LimiteDeCredito = Convert.ToDecimal(dt.Rows[i]["LimiteDeCredito"])
                    };
                    lista.Add(item);
                    objDAL.FecharConexao();
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ModelCliente RetornarCliente(int? Id)
        {
            try
            {
                DAL objDAL = new DAL();
                objDAL.LimparParametro();
                ModelCliente item;
                DataTable dt = objDAL.ExecutarConsulta(CommandType.Text, $"Select Id, Nome, CPF, DataNascimento, LimiteDeCredito From Clientes Where Id = '{Id}'");

                item = new ModelCliente()
                {
                    Id = dt.Rows[0]["Id"].ToString(),
                    Nome = dt.Rows[0]["Nome"].ToString(),
                    CPF = dt.Rows[0]["CPF"].ToString(),
                    DataNascimento = DateTime.Parse (dt.Rows[0]["DataNascimento"].ToString()),
                    LimiteDeCredito = Convert.ToDecimal (dt.Rows[0]["LimiteDeCredito"])
                };
                objDAL.FecharConexao();
                return item;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Gravar()
        {
            try
            {
                if(Id != null)
                {
                    DAL objDAL = new DAL();
                    objDAL.LimparParametro();
                    objDAL.AddParametros("Id", Id);
                    objDAL.AddParametros("Nome", Nome);
                    objDAL.AddParametros("CPF", CPF);
                    objDAL.AddParametros("DataNascimento", DataNascimento);
                    objDAL.AddParametros("LimiteDeCredito", LimiteDeCredito);
                    String IdCliente = objDAL.ExecutarManipulacao(CommandType.StoredProcedure, "Alterar").ToString();
                    objDAL.FecharConexao();
                }
                else
                {
                    DAL objDAL = new DAL();
                    objDAL.LimparParametro();
                    objDAL.AddParametros("Nome", Nome);
                    objDAL.AddParametros("CPF", CPF);
                    objDAL.AddParametros("DataNascimento", DataNascimento);
                    objDAL.AddParametros("LimiteDeCredito", LimiteDeCredito);
                    String IdCliente = objDAL.ExecutarManipulacao(CommandType.Text, "Insert Into Clientes (Nome, CPF, DataNascimento, LimiteDeCredito) Values (@Nome, @CPF, @DataNascimento, @LimiteDeCredito)").ToString();
                    objDAL.FecharConexao();
                }
            }
            catch
            {
                //
            }
        }

        public void Excluir(int? Id)
        {
            try
            {
                DAL objDAL = new DAL();
                objDAL.LimparParametro();
                objDAL.AddParametros("Id", Id);
                String IdCliente = objDAL.ExecutarManipulacao(CommandType.StoredProcedure, "Excluir").ToString();
                objDAL.FecharConexao();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}