using System;
//
using System.Data;
using MySql.Data.MySqlClient;

namespace Treinando_MVC_e_Sessao.Data_Access_Layer
{
    public class DAL
    {
        private static String Server = "localhost"; // Servidor MySQL
        private static String Database = "CrudClientes"; // Banco de Dados no MySQL
        private static String User = "paulo"; // Usuário MySQL
        private static String Password = "abc123"; // Senha MySQL

        // String de Conexão MySQL
        private static String Connectionstring= $"Server={Server};Database={Database};Uid={User};Pwd={Password};Sslmode=none;Charset=utf8;";

        public MySqlConnection Conexao ()
        {
            return new MySqlConnection(Connectionstring);
        }

        public void FecharConexao()
        {
            MySqlConnection conn = Conexao();
            conn.Close();
        }

        private MySqlParameterCollection Colecao = new MySqlCommand().Parameters;

        public void LimparParametro()
        {
            Colecao.Clear();
        }

        public void AddParametros(String nome, Object valor)
        {
            Colecao.Add(new MySqlParameter(nome, valor));
        }

        public Object ExecutarManipulacao(CommandType commandType, String Sp_Ou_Texto)
        {
            try
            {
                MySqlConnection conn = Conexao();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = commandType;
                cmd.CommandText = Sp_Ou_Texto;
                cmd.CommandTimeout = 3600;

                foreach (MySqlParameter param in Colecao)
                {
                    cmd.Parameters.Add(new MySqlParameter(param.ParameterName, param.Value));
                }
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ExecutarConsulta(CommandType commandType, String Sp_Ou_Texto)
        {
            try
            {
                MySqlConnection conn = Conexao();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = commandType;
                cmd.CommandText = Sp_Ou_Texto;
                cmd.CommandTimeout = 3600;

                foreach (MySqlParameter param in Colecao)
                {
                    cmd.Parameters.Add(new MySqlParameter(param.ParameterName, param.Value));
                }
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable RetDatatable (String sql)
        {
            try
            {
                DataTable dt = new DataTable();
                MySqlCommand cmd = new MySqlCommand(sql, Conexao());
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Polimorfismo para evitar ataque de injecão de SQL...
        public DataTable RetDatatable(MySqlCommand cmd)
        {
            try
            {
                DataTable dt = new DataTable();
                cmd.Connection = Conexao();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}