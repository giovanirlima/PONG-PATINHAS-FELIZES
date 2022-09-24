using System;
using System.Data.SqlClient;

namespace PONG_PATINHAS_FELIZES
{
    internal class Program
    {
        #region Inserts Tabelas
        static void InsertDadosAdotantes(SqlConnection conexao)
        {
            char resposta = 'a';
            bool validacao = true;
            SqlCommand cmd;

            Console.Clear();

            Console.WriteLine("PAINEL DE CADASTRO");


            Console.WriteLine("\nPara cadastrar um novo adotante será obrigatório as seguintes informações iniciais: ");
            Console.WriteLine("\nNome\nCPF\nSexo\nEndereço Completo\nTelefone\n");
            Console.WriteLine("Deseja continuar com o cadastro: (s/n)");
            do
            {
                Console.Write("Resposta: ");
                try
                {
                    resposta = char.Parse(Console.ReadLine().ToUpper());
                    validacao = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nParamentro de entrada inválido!\n");
                    validacao = true;
                }
                if (resposta != 'S' && resposta != 'N')
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nOpção digitada é inválida!\n");
                        validacao = true;
                    }
                }
            } while (validacao);

            if (resposta == 'N')
            {
                return;
            }
            Console.Clear();

            Console.WriteLine("CADASTRO DE ADOTANTE\n\n");

            Console.Write("informe o nome do(a) adotante[OBRIGATÓRIO]: ");
            string nome = Console.ReadLine();

            Console.Write("informe o CPF do(a) adotante[OBRIGATÓRIO]: ");
            string cpf = Console.ReadLine();

            Console.Write("informe o sexo do adotante (Masculino | Feminino | Indefinido): ");
            string sexo = Console.ReadLine();

            Console.Write("informe o telefone do adotante: ");
            string telefone = Console.ReadLine();

            Console.WriteLine("\nENDEREÇO: ");
            Console.Write("informe a rua: ");
            string rua = Console.ReadLine();

            Console.Write("informe o número do local: ");
            int numero = int.Parse(Console.ReadLine());

            Console.Write("informe o bairro: ");
            string bairro = Console.ReadLine();

            Console.Write("informe a cidade: ");
            string cidade = Console.ReadLine();

            Console.Write("informe o estado: ");
            string estado = Console.ReadLine();


            conexao.Open();

            cmd = new();

            cmd.CommandText = "INSERT INTO Pessoa VALUES(@CPF, @Nome, @Sexo, @Rua, @Numero, @Bairro, @Cidade, @Estado, @Telefone);";

            cmd.Connection = conexao;
            cmd.Parameters.Add(new SqlParameter("@CPF", cpf));
            cmd.Parameters.Add(new SqlParameter("@Nome", nome));
            cmd.Parameters.Add(new SqlParameter("@Sexo", sexo));
            cmd.Parameters.Add(new SqlParameter("@Rua", rua));
            cmd.Parameters.Add(new SqlParameter("@Numero", numero));
            cmd.Parameters.Add(new SqlParameter("@Bairro", bairro));
            cmd.Parameters.Add(new SqlParameter("@Cidade", cidade));
            cmd.Parameters.Add(new SqlParameter("@Estado", estado));
            cmd.Parameters.Add(new SqlParameter("@Telefone", telefone));

            cmd.ExecuteNonQuery();
            conexao.Close();
            Console.WriteLine("\nCadastrado com sucesso!");
            Console.ReadKey();
        }
        static int InsertDadosAnimais(SqlConnection conexao, int id)
        {
            int opcao = 0;
            char resposta = 'a';
            bool validacao;
            SqlCommand cmd;

            Console.Clear();

            Console.WriteLine("PAINEL DE CADASTRO\n\n");

            Console.WriteLine("Para cadastrar um novo animal será obrigatório as seguintes informações iniciais: ");
            Console.WriteLine("\nFamilia\nSexo\nNome(Opcional)\n");
            Console.WriteLine("Deseja continuar com o cadastro: (s/n)");
            do
            {
                Console.Write("Resposta: ");
                try
                {
                    resposta = char.Parse(Console.ReadLine().ToUpper());
                    validacao = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nParamentro de entrada inválido!\n");
                    validacao = true;
                }
                if (resposta != 'S' && resposta != 'N')
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nOpção digitada é inválida!\n");
                        validacao = true;
                    }
                }
            } while (validacao);

            if (resposta == 'N')
            {
                return 0;
            }

            Console.Clear();

            Console.WriteLine("CADASTRO DE ANIMAL\n\n");

            Console.Write("informe qual a família do animal[OBRIGATÓRIO]: ");
            string especie = Console.ReadLine();

            Console.Write("informe o sexo do animal[OBRIGATÓRIO]: ");
            string sexo = Console.ReadLine();
            do
            {
                Console.Write("\nDeseja informar o nome do animal:  ");
                Console.WriteLine("\n\n1 - Sim\n2 - Não\n");
                Console.Write("Opção: ");
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                    validacao = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nParametro informado é inválido\n");
                    validacao = true;
                }

                if (opcao != 1 && opcao != 2)
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nOpção digitada é inválida!\n");
                        validacao = true;
                    }
                }

            } while (validacao);

            if (opcao == 1)
            {
                Console.Write("\ninforme o nome do animal: ");
                string nome = Console.ReadLine();

                conexao.Open();

                cmd = new();

                cmd.CommandText = "INSERT INTO Animal VALUES(@Raca, @Sexo, @Nome);";

                cmd.Connection = conexao;

                cmd.Parameters.Add(new SqlParameter("@Raca", especie));
                cmd.Parameters.Add(new SqlParameter("@Sexo", sexo));
                cmd.Parameters.Add(new SqlParameter("@Nome", nome));

                cmd.ExecuteNonQuery();

                cmd = new();

                cmd.CommandText = "INSERT INTO Animal_Familia VALUES(@ID, @Familia);";

                cmd.Connection = conexao;

                cmd.Parameters.Add(new SqlParameter("@ID", id));
                cmd.Parameters.Add(new SqlParameter("@Familia", especie));

                cmd.ExecuteNonQuery();

                conexao.Close();

                Console.WriteLine("\nCadastrado com sucesso!");
                Console.ReadKey();

                return 1;
            }

            conexao.Open();

            cmd = new();

            cmd.CommandText = "INSERT INTO Animal VALUES(@Raca, @Sexo, @Nome);";

            cmd.Connection = conexao;

            cmd.Parameters.Add(new SqlParameter("@Raca", especie));
            cmd.Parameters.Add(new SqlParameter("@Sexo", sexo));
            cmd.Parameters.Add(new SqlParameter("@Nome", "Não informado"));

            cmd.ExecuteNonQuery();

            cmd = new();

            cmd.CommandText = "INSERT INTO Animal_Familia VALUES(@ID, @Familia);";

            cmd.Connection = conexao;

            cmd.Parameters.Add(new SqlParameter("@ID", id));
            cmd.Parameters.Add(new SqlParameter("@Familia", especie));

            cmd.ExecuteNonQuery();

            conexao.Close();

            Console.WriteLine("\nCadastrado com sucesso!");
            Console.ReadKey();

            return 1;
        }
        static void InsertAdotarAnimais(SqlConnection conexao)
        {
            char resposta = 'a';
            bool validacao;
            SqlCommand cmd = new();

            Console.Clear();

            Console.WriteLine("PAINEL DE CADASTRO");

            
            Console.WriteLine("\nPara adotar um animal será obrigatório as seguintes informações iniciais: ");
            Console.WriteLine("\nCPF do adotante\nID do animalzinho cadastrado\n");
            Console.WriteLine("Deseja continuar com o cadastro: (s/n)");
            do
            {
                Console.Write("Resposta: ");
                try
                {
                    resposta = char.Parse(Console.ReadLine().ToUpper());
                    validacao = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nParamentro de entrada inválido!\n");
                    validacao = true;
                }
                if (resposta != 'S' && resposta != 'N')
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nOpção digitada é inválida!\n");
                        validacao = true;
                    }
                }
            } while (validacao);

            if (resposta == 'N')
            {
                return;
            }

            Console.Clear();

            Console.WriteLine("PAINEL DE CADASTRO");

            Console.Write("\n\nInforme o CPF do adotante: ");
            string cpf = Console.ReadLine();

            Console.Write("Informe o ID do animalzinha: ");
            int id = int.Parse(Console.ReadLine());

            conexao.Open();

            cmd.CommandText = "INSERT INTO Pessoa_Adota_Animal VALUES(@CPF, @ID);";

            cmd.Connection = conexao;

            cmd.Parameters.Add(new SqlParameter("@CPF", cpf));
            cmd.Parameters.Add(new SqlParameter("@ID", id));
                        
            cmd.ExecuteNonQuery();

            conexao.Close();

            Console.WriteLine("\nAnimalzinho adotado com sucesso!");
            Console.ReadKey();
        }
        #endregion



        static void IniciarSistema()
        {
            BancoOng caminho = new();
            SqlConnection conexaoSql = new SqlConnection(caminho.CaminhoDeConexao());
            int opcao = 10, id = 1;
            bool validacao = true;

            do
            {
                Console.Clear();

                Console.WriteLine("BEM VINDO AO SISTEMA ONG ANIMAIS FELIZES");

                Console.WriteLine("\n\nSelecione uma opção: \n");
                Console.WriteLine("1 - Cadastrar ");
                Console.WriteLine("2 - Deletar");
                Console.WriteLine("3 - Editar");
                Console.WriteLine("4 - Adotar");
                Console.WriteLine("5 - Imprimir");
                Console.WriteLine("\n0 - Sair");
                Console.Write("\nOpção: ");
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                    validacao = false;

                }
                catch (Exception)
                {
                    Console.WriteLine("\nParametro de entrada inválido!");
                    Console.WriteLine("Pressione enter para escolher novamente!");
                    validacao = true;
                    Console.ReadKey();
                }
                if (opcao < 0 || opcao > 5)
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nOpção digitada é inválida!");
                        Console.WriteLine("Pressione enter para escolher novamente!");
                        validacao = true;
                        Console.ReadKey();
                    }
                }

                switch (opcao)
                {
                    case 1:
                        do
                        {
                            Console.Clear();

                            Console.WriteLine("CADASTRO ONG ANIMAIS FELIZES");

                            Console.WriteLine("\n\nSELECIONE UMA OPÇÃO: \n");
                            Console.WriteLine("1 - ADOTANTE ");
                            Console.WriteLine("2 - ANIMALZINHO");
                            Console.WriteLine("\n9 - VOLTAR AO MENU ANTERIOR");
                            Console.Write("\nOpção: ");
                            try
                            {
                                opcao = int.Parse(Console.ReadLine());
                                validacao = false;

                            }
                            catch (Exception)
                            {
                                Console.WriteLine("\nParametro de entrada inválido!");
                                Console.WriteLine("Pressione enter para escolher novamente!");
                                return;
                            }
                            if (opcao < 1 || opcao > 2 && opcao != 9)
                            {
                                if (!validacao)
                                {
                                    Console.WriteLine("\nOpção digitada é inválida!");
                                    Console.WriteLine("Pressione enter para escolher novamente!");
                                    return;
                                }
                            }

                            switch (opcao)
                            {
                                case 1:
                                    InsertDadosAdotantes(conexaoSql);
                                    break;


                                case 2:
                                    id += InsertDadosAnimais(conexaoSql, id);
                                    break;
                            }
                        } while (opcao != 9);
                        break;

                    case 2:
                        break;

                    case 3:
                        break;

                    case 4:
                        InsertAdotarAnimais(conexaoSql);
                        break;


                }


            } while (opcao != 0);


        }

        static void Main(string[] args)
        {

            IniciarSistema();




        }
    }
}
