using System;
using System.Data;
using System.Data.SqlClient;

namespace PONG_PATINHAS_FELIZES
{
    public class BancoOng
    {
        public string Conexao { get; set; }

        public BancoOng()
        {            
        }
        public string CaminhoDeConexao()
        {
            SqlConnection conn = new();

            Console.Clear();

            /*Console.Write("Informe a instancia do servidor para conexão: ");
            string ip = Console.ReadLine();

            Console.Write("Informe o nome da database que será utilizada: ");
            string database = Console.ReadLine();

            Console.Write("Informe o loguin de usuário: ");
            string loguin = Console.ReadLine();

            Console.Write("Informe a senha: ");
            string senha = Console.ReadLine();*/

            //return Conexao = $"Data Source={ip}; Initial Catalog={database}; User Id={loguin}; Password ={senha}";
            return Conexao = @"Data Source = localhost; Initial Catalog = ONG; User Id = sa; Password = 227126993";

        }
       
    }
}
