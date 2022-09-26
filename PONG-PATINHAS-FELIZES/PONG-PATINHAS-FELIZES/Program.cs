using System;
using System.Data.SqlClient;

namespace PONG_PATINHAS_FELIZES
{
    internal class Program
    {

        #region Inserts Tabelas
        static void InsertDadosAdotantes(SqlConnection conexao)
        {
            string nome, cpf, sexo, telefone, rua, numero, bairro, cidade, estado;
            DateTime nascimento = new DateTime();
            char resposta = 'a';
            bool validacao;
            int quantidade = 0;
            SqlCommand cmd;

            Console.Clear();

            Console.WriteLine("PAINEL DE CADASTRO");

            Console.WriteLine("\nPARA CADASTRAR UM NOVO ADOTANTE SERÁ OBRITAGÓRIO AS SEGUINTES INFORMAÇÕES INICIAIS: ");
            Console.WriteLine("\nNOME\nCPF\nSEXO\nENDEREÇO COMPLETO\nTELEFONE\n");
            Console.WriteLine("DESEJA CONTINUAR COM O CADASTRO (s/n)");
            do
            {
                Console.Write("RESPOSTA: ");
                try
                {
                    resposta = char.Parse(Console.ReadLine().ToUpper());
                    validacao = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPARAMETRO DE ENTRADA É INVÁLIDO!\n");
                    validacao = true;
                }
                if (resposta != 'S' && resposta != 'N')
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nOPÇÃO DIGITADA É INVÁLIDA!\n");
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

            do
            {
                Console.Write("INFORME O NOME DO(A) ADOTANTE[OBRIGATÓRIO]: ");
                nome = Console.ReadLine().ToUpper();
                validacao = false;

                if (nome.Length == 0)
                {
                    Console.WriteLine("\nNOME OBRIGATÓRIO!\n");
                    validacao = true;
                }

            } while (validacao);

            do
            {
                Console.Write("INFORME O CPF DO(A) ADOTANTE[OBRIGATÓRIO]: ");
                cpf = Console.ReadLine().ToUpper();
                validacao = false;

                if (cpf.Length < 11 || cpf.Length > 11)
                {
                    Console.WriteLine("\nCPF DEVE CONTER 11 DIGITOS!\n");
                    validacao = true;
                }

            } while (validacao);
            //Abrindo conexão com o banco
            conexao.Open();
            //Instrução de comando em linguagem SQL para verificar se o CPF digitado já possue cadastro
            cmd = new SqlCommand("SELECT * FROM Pessoa WHERE CPF = @CPF", conexao);

            cmd.Parameters.Add(new SqlParameter("@CPF", cpf));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows) //HasRows é uma proprietado do tipo bool, que informa se existe linhas na tabela
                {
                    while (reader.Read())
                    {
                        quantidade++;
                    }
                }
            }

            if (quantidade > 0)
            {
                Console.WriteLine("\nADOTANTE JÁ POSSUE CADASTRO!");
                Console.ReadKey();
                conexao.Close();
                return;
            }

            conexao.Close(); // fechando conexão, pois já foi realizado os testes

            do
            {
                Console.Write("INFORME A DATA DE NASCIMENTO DO ADOTANTE: ");
                try
                {
                    nascimento = DateTime.Parse(Console.ReadLine());
                    validacao = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nFormato inválido! [dd/mm/yyyy]\n");
                    validacao = true;
                }
                if (nascimento > DateTime.Now)
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nDATA DE NASCIMENTO NÃO PODE SER FUTURA\n");
                        validacao = true;
                    }                    
                }

            } while (validacao);

            Console.Write("INFORME O SEXO DO ADOTANTE (Masculino | Feminino | Indefinido): ");
            sexo = Console.ReadLine().ToUpper();

            Console.Write("INFORME O TELEFONE DO ADOTANTE: ");
            telefone = Console.ReadLine().ToUpper();

            Console.WriteLine("\nENDEREÇO: ");
            Console.Write("INFORME A RUA: ");
            rua = Console.ReadLine().ToUpper();

            Console.Write("INFORME O NÚMERO DO LOCAL: ");
            numero = Console.ReadLine().ToUpper();

            Console.Write("INFORME O BAIRRO: ");
            bairro = Console.ReadLine().ToUpper();

            Console.Write("INFORME A CIDADE: ");
            cidade = Console.ReadLine().ToUpper();

            Console.Write("INFORME O ESTADO: ");
            estado = Console.ReadLine().ToUpper();

            conexao.Open();//Conexão aberta para inserção

            cmd = new();
            //Sequencia de intrução em codigo sql para inserir uma Pessoa na tabela
            cmd.CommandText = "INSERT INTO Pessoa VALUES(@CPF, @NOME, @NASCIMENTO, @SEXO, @RUA, @NUMERO, @BAIRRO, @CIDADE, @ESTADO, @TELEFONE);";

            cmd.Connection = conexao;
            cmd.Parameters.Add(new SqlParameter("@CPF", cpf));
            cmd.Parameters.Add(new SqlParameter("@NOME", nome));
            cmd.Parameters.Add(new SqlParameter("@NASCIMENTO", nascimento));
            cmd.Parameters.Add(new SqlParameter("@SEXO", sexo));
            cmd.Parameters.Add(new SqlParameter("@RUA", rua));
            cmd.Parameters.Add(new SqlParameter("@NUMERO", numero));
            cmd.Parameters.Add(new SqlParameter("@BAIRRO", bairro));
            cmd.Parameters.Add(new SqlParameter("@CIDADE", cidade));
            cmd.Parameters.Add(new SqlParameter("@ESTADO", estado));
            cmd.Parameters.Add(new SqlParameter("@TELEFONE", telefone));

            try
            {
                cmd.ExecuteNonQuery(); //Executando o comando
            }
            catch (Exception)
            {
                Console.WriteLine("\nDADOS INVÁLIDOS!"); // Verificação de possivel erro
                Console.ReadKey();
                conexao.Close(); // Todos os parametros
                return;
            }

            conexao.Close(); // Todos os parametros 
            Console.WriteLine("\nCADASTRADO COM SUCESSO!");
            Console.ReadKey();
        }
        static void InsertDadosAnimais(SqlConnection conexao)
        {
            string familia, raca, sexo, nome;
            int opcao = 0, verConficao, quantidade = 0;
            char resposta = 'a';
            bool validacao;
            SqlCommand cmd;

            Console.Clear();
            //Tela de interação com usuário, com validação de erro na resposta
            Console.WriteLine("PAINEL DE CADASTRO\n\n");

            Console.WriteLine("PARACADASTRAR UM NOVO ANIMAL SERÁ OBRIGATÓRIO AS SEGUINTES INFORMAÇÕES INICIAIS: ");
            Console.WriteLine("\nFAMILIA\nRAÇA\nSEXO\nNOME(Opcional)\n");
            Console.WriteLine("DESEJA CONTINUAR COM O CADASTRO: (s/n)");
            do
            {
                Console.Write("RESPOSTA: ");
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

            Console.WriteLine("CADASTRO DE ANIMAL\n\n");

            //Validação de possivel de erro caso usuario não digite nada
            do
            {
                Console.Write("INFORME DE QUAL FAMILIA É O ANIMAL[OBRIGATÓRIO]: ");
                familia = Console.ReadLine().ToUpper();
                validacao = false;

                if (familia.Length == 0)
                {
                    Console.WriteLine("\nOBRIGATÓRIO INFORMAR A FAMILIA DO ANIMAL EX: (GATO, CACHORRO, CAVALO...)\n");
                    validacao = true;
                }

            } while (validacao);

            //Validação de possivel de erro caso usuario não digite nada
            do
            {
                Console.Write("INFORME DE QUAL RAÇA É O ANIMAL[OBRIGATÓRIO]: ");
                raca = Console.ReadLine().ToUpper();
                validacao = false;

                if (raca.Length == 0)
                {
                    Console.WriteLine("\nOBRIGATÓRIO INFORMAR A RAÇA DO ANIMAL EX: (VIRA-LATA, PERSA, MANGALARGA...)\n");
                    validacao = true;
                }
            } while (validacao);

            //Validação de possivel de erro caso usuario não digite nada
            do
            {
                Console.Write("INFORME DE QUAL O SEXO DO ANIMAL[OBRIGATÓRIO]: ");
                sexo = Console.ReadLine().ToUpper();
                validacao = false;

                if (sexo.Length == 0)
                {
                    Console.WriteLine("\nOBRIGATÓRIO INFORMAR A FAMILIA DO ANIMAL EX: (GATO, CACHORRO, CAVALO...)\n");
                    validacao = true;
                }

            } while (validacao);

            //Validação de possivel de erro caso usuario não digite nada ou digite uma opção inválida
            do
            {
                Console.Write("\nDESEJA INFORMAR UM NOME PARA O ANIMAL:  ");
                Console.WriteLine("\n\n1 - SIM\n2 - NÃO\n");
                Console.Write("OPÇÃO: ");
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                    validacao = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPARAMETRO INFORMADO É INVÁLIDO!\n");
                    validacao = true;
                }

                if (opcao != 1 && opcao != 2)
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nOPÇÃO DIGITADA É INVÁLIDA!\n");
                        validacao = true;
                    }
                }

            } while (validacao);

            if (opcao == 1) // condição caso usuário queira digitar o nome
            {
                Console.Write("\nINFORME O NOME DO ANIMAL: ");
                nome = Console.ReadLine().ToUpper(); // Condição para que todas as letras digitas sejam maiusculas

                conexao.Open(); // Abrindo conexão

                cmd = new("INSERT INTO Animal VALUES(@Familia, @Raca, @Sexo, @Nome);", conexao); // Parametro em codigo sql
                //Primeiro a intrução de excução e depois o caminho de conexão.

                //Adicionado parametros para ser inserido no banco
                cmd.Parameters.Add(new SqlParameter("@Familia", familia));
                cmd.Parameters.Add(new SqlParameter("@Raca", raca));
                cmd.Parameters.Add(new SqlParameter("@Sexo", sexo));
                cmd.Parameters.Add(new SqlParameter("@Nome", nome));

                verConficao = cmd.ExecuteNonQuery(); // Verificação se retornou um valor > 0, caso verdadeiro o processo foi executado com exito!

                if (verConficao > 0)
                {
                    cmd = new SqlCommand("SELECT * FROM Animal", conexao);

                    //cmd.CommandType = System.Data.CommandType.Text;     //Este comando traz o dado para ser manipulado               

                    using (SqlDataReader reader = cmd.ExecuteReader()) // Verificação para ver qual id do animal
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                quantidade++;
                            }
                        }
                    }

                    cmd = new SqlCommand("INSERT INTO Animais_Disponiveis VALUES(@CHIP);", conexao); //Insersão do id na tabela animais disponiveis

                    cmd.Parameters.Add(new SqlParameter("@CHIP", quantidade));

                    cmd.ExecuteNonQuery();

                    conexao.Close();

                    Console.WriteLine("\nANIMAL CADASTRADO COM SUCESSO!");
                    Console.ReadKey();
                    return;
                }

                conexao.Close();

                Console.WriteLine("\nFALHA AO REALIZAR O CADASTRO, TENTE NOVAMENTE!");
                Console.ReadKey();

                return;
            }

            conexao.Open();

            cmd = new("INSERT INTO Animal VALUES(@Familia, @Raca, @Sexo, @Nome);", conexao);

            cmd.Parameters.Add(new SqlParameter("@Familia", familia));
            cmd.Parameters.Add(new SqlParameter("@Raca", raca));
            cmd.Parameters.Add(new SqlParameter("@Sexo", sexo));
            cmd.Parameters.Add(new SqlParameter("@Nome", "NÃO INFORMADO"));

            verConficao = cmd.ExecuteNonQuery();

            if (verConficao > 0)
            {
                cmd = new SqlCommand("SELECT * FROM Animal", conexao);

                //cmd.CommandType = System.Data.CommandType.Text;                    

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        quantidade++;
                    }
                }

                cmd = new SqlCommand("INSERT INTO Animais_Disponiveis VALUES(@CHIP);", conexao);

                cmd.Parameters.Add(new SqlParameter("@CHIP", quantidade));

                cmd.ExecuteNonQuery();

                conexao.Close();

                Console.WriteLine("\nANIMAL CADASTRADO COM SUCESSO!");
                Console.ReadKey();
                return;
            }
            conexao.Close();

            Console.WriteLine("\nFALHA AO REALIZAR O CADASTRO, TENTE NOVAMENTE!");
            Console.ReadKey();

            return;
        }
        static void InsertAdotarAnimais(SqlConnection conexao)
        {
            string cpf;
            int n = 0, quantidade = 0, chip = 0;
            char resposta = 'a';
            bool validacao;
            SqlCommand cmd;

            Console.Clear();

            Console.WriteLine("PAINEL DE ADOÇÃO");

            //Interface com o usuário já com validação de error
            Console.WriteLine("\nPARA ADOTAR UM ANIMAL SERÁ OBRIGATÓRIO AS SEGUINTES INFORMAÇÕES INICIAIS: ");
            Console.WriteLine("\nCPF DO ADOTANTE\nCHIP DO ANIMAL CADASTRADO\n");
            Console.WriteLine("DESEJA CONTINUAR COM O CADASTRO: (s/n)");
            do
            {
                Console.Write("RESPOSTA: ");
                try
                {
                    resposta = char.Parse(Console.ReadLine().ToUpper());
                    validacao = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPARAMETRO DE ENTRADA INVÁLIDO!!\n");
                    validacao = true;
                }
                if (resposta != 'S' && resposta != 'N')
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nOPÇÃO DIGITADA É INVÁLIDA!\n");
                        validacao = true;
                    }
                }
            } while (validacao);

            if (resposta == 'N') // Condição de desistencia do usuario
            {
                return;
            }

            Console.Clear();

            Console.WriteLine("PAINEL DE ADOÇÃO\n");

            //Validacao de possivel valor null
            do
            {
                Console.Write("INFORME O CPF DO(A) ADOTANTE[OBRIGATÓRIO]: ");
                cpf = Console.ReadLine().ToUpper();
                validacao = false;

                if (cpf.Length < 11 || cpf.Length > 11)
                {
                    Console.WriteLine("\nCPF DEVE CONTER 11 DIGITOS!\n");
                    validacao = true;
                }

            } while (validacao);

            conexao.Open(); // Abrindo conexão com banco

            cmd = new SqlCommand("SELECT * FROM Pessoa WHERE CPF = @CPF", conexao); //Comando em codigo sql para percorrer a tabela pessoa
            //e verificar se o cpf digitado, já possue cadastro

            cmd.Parameters.Add(new SqlParameter("@CPF", cpf));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        quantidade++;
                    }
                }
            }

            if (quantidade == 0)
            {
                Console.WriteLine("\nADOTANTE NÃO POSSUE CADASTRO!");
                Console.ReadKey();
                conexao.Close();
                return;
            }

            conexao.Close(); // fechando conexão

            quantidade = 0; // variavel sera re-utilizada, por isso foi atribuido o valor 0

            //Verificação de possivel valor null
            do
            {
                Console.Write("INFORME O CHIP DO ANIMAL[OBRIGATÓRIO]: ");
                try
                {
                    chip = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPARAMETRO DE ENTRADA INVÁLIDO!\n");
                    validacao = true;
                }
                
                validacao = false;

                if (cpf.Length == 0)
                {
                    Console.WriteLine("\nCHIP OBRIGATÓRIO!\n");
                    validacao = true;
                }

            } while (validacao);

            conexao.Open();//Abrindo conexão

            cmd = new SqlCommand("SELECT * FROM Animais_Disponiveis WHERE CHIP = @CHIP", conexao); //Codigo de sql com instruções
            //Para percorrer a lista de animais dispiniveis e verificar se o CHIP digitado existe e está disponivel.

            cmd.Parameters.Add(new SqlParameter("@CHIP", chip)); //Passando o parametro para ser encaminhado

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        quantidade++;
                    }
                }
            }

            if (quantidade == 0)
            {
                Console.WriteLine("\nANIMAL NÃO POSSUE CADASTRO OU JÁ FOI ADOTADO!");
                Console.ReadKey();
                conexao.Close();
                return;
            }

            conexao.Close(); // Fechando conexão

            quantidade = 0; // variavel sera re-utilizada, por isso foi atribuido o valor 0

            conexao.Open();

            #region CARREGAR DADO PARA MANIPULAÇÃO
            cmd = new SqlCommand("SELECT Quantidade FROM Pessoa_Adota_Animal WHERE CPF = @CPF", conexao);

            cmd.CommandType = System.Data.CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("@CPF", cpf));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    quantidade = Convert.ToInt32(reader["Quantidade"]);
                }
            }
            #endregion // Exemplo de Manupulação de dados do banco

            conexao.Close();

            conexao.Open();

            cmd = new("INSERT INTO Pessoa_Adota_Animal VALUES(@CPF, @CHIP, @QUATIDADE);", conexao);

            #region Exemplo de como pode ser feita a conexão com o banco
            //cmd.CommandText = "INSERT INTO Pessoa_Adota_Animal VALUES(@CPF, @CHIP, @QUATIDADE);";

            //cmd.Connection = conexao;
            #endregion

            cmd.Parameters.Add(new SqlParameter("@CPF", cpf));
            cmd.Parameters.Add(new SqlParameter("@CHIP", chip));
            cmd.Parameters.Add(new SqlParameter("@QUATIDADE", quantidade++));
            try
            {
                n = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("\nANIMAL JÁ FOI ADOTADO!");
                Console.ReadKey();
                conexao.Close();
                return;
            }

            if (n > 0)
            {
                cmd = new("DELETE FROM Animais_Disponiveis WHERE CHIP = @CHIP", conexao);

                //cmd.CommandText = "INSERT INTO Pessoa_Adota_Animal VALUES(@CPF, @CHIP, @QUATIDADE);";

                //cmd.Connection = conexao;

                cmd.Parameters.Add(new SqlParameter("@CHIP", chip));

                cmd.ExecuteNonQuery();

                Console.WriteLine("\nANIMAL ADOTADO COM SUCESSO!");
                Console.ReadKey();
                conexao.Close();
                return;
            }


            Console.WriteLine("\nDADOS INVÁLIDOS PARA ADOÇÃO!");
            Console.ReadKey();
            conexao.Close();
        }
        #endregion

        #region Editar Tabelas
        static void EditarDadosAdotantes(SqlConnection conexao)
        {
            string nome, cpf, sexo, telefone, rua, numero, bairro, cidade, estado;
            DateTime nascimento = new DateTime();
            int opcao = 0, quantidade = 0;
            char resposta = 'a';
            bool validacao;
            SqlCommand cmd;

            Console.Clear();

            Console.WriteLine("PAINEL DE EDIÇÃO");//Interface de interação com usuário e validação de erros

            Console.WriteLine("\nPARA EDITAR UM CADASTRO DE ADOTANTE SERÁ OBRIGATÓRIO A SEGUINTE INFORMAÇÃO:");
            Console.WriteLine("\nCPF\n");
            Console.WriteLine("DESEJA CONTINUAR COM O CADASTRO: (s/n)");

            do
            {
                Console.Write("RESPOSTA: ");
                try
                {
                    resposta = char.Parse(Console.ReadLine().ToUpper());
                    validacao = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPARAMETRO DE ENTRADA INVÁLIDO!\n");
                    validacao = true;
                }
                if (resposta != 'S' && resposta != 'N')
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nOPÇÃO DIGITADA É INVÁLIDA!\n");
                        validacao = true;
                    }
                }
            } while (validacao);

            if (resposta == 'N')
            {
                return;
            }

            Console.Clear();

            Console.WriteLine("EDITAR CADASTRO\n\n");

            //Validação de error cpf null
            do
            {
                Console.Write("INFORME O CPF DO(A) ADOTANTE[OBRIGATÓRIO]: ");
                cpf = Console.ReadLine().ToUpper();
                validacao = false;

                if (cpf.Length < 11 || cpf.Length > 11)
                {
                    Console.WriteLine("\nCPF DEVE CONTER 11 DIGITOS!\n");
                    validacao = true;
                }

            } while (validacao);

            conexao.Open(); // Abrindo conexão

            cmd = new SqlCommand("SELECT * FROM Pessoa WHERE CPF = @CPF", conexao); // Passando parametro em condigo sql para o banco, e inserindo o caminho da conexão

            cmd.Parameters.Add(new SqlParameter("@CPF", cpf)); // adicionando parametro para condição do Where

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        quantidade++;
                    }
                }
            }

            if (quantidade == 0)
            {
                Console.WriteLine("\nCLIENTE NÃO POSSUE CADASTRO!");
                Console.ReadKey();
                conexao.Close();
                return;
            }

            conexao.Close(); // fechando conexão
            
            //Validando possivel error de usuario na opção
            Console.WriteLine("\nINFORME QUAL DADO DESEJA ALTERAR: ");
            Console.WriteLine("\n1 - NOME\n2 - DATA DE NASCIMENTO\n3 - SEXO\n4 - ENDEREÇO\n5 - TELEFONE\n");
            do
            {
                Console.Write("OPÇÃO: ");
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                    validacao = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPARAMETRO DE ENTRADA INVÁLIDO!\n");
                    validacao = true;
                }
                if (opcao < 1 || opcao > 5)
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nOPÇÃO DIGITADA É INVÁLIDA!\n");
                        validacao = true;
                    }
                }

            } while (validacao);

            switch (opcao)
            {
                case 1:
                    Console.Clear();
                    //Validação de Nome null
                    do
                    {
                        Console.Write("INFORME O NOME DO(A) ADOTANTE[OBRIGATÓRIO]: ");
                        nome = Console.ReadLine().ToUpper();
                        validacao = false;

                        if (nome.Length == 0)
                        {
                            Console.WriteLine("\nNOME OBRIGATÓRIO!\n");
                            validacao = true;
                        }

                    } while (validacao);

                    conexao.Open(); // Abrindo conexão

                    cmd = new();

                    cmd.CommandText = "UPDATE Pessoa Set Nome = @NOME WHERE CPF = @CPF;"; //Aqui fiz o segundo exemple de montar os paramentros 
                    //linguagem sql para enviar para o banco

                    cmd.Connection = conexao; //Passei o caminho para conexão
                    cmd.Parameters.Add(new SqlParameter("@CPF", cpf));
                    cmd.Parameters.Add(new SqlParameter("@NOME", nome));

                    int verificacao = cmd.ExecuteNonQuery(); // verificação para ver se o codigo realmente foi executado, valor retornar quantidade de linhas afetadas
                    conexao.Close();

                    if (verificacao > 0)
                    {
                        Console.WriteLine("\nEDITADO COM SUCESSO!");
                        Console.ReadKey();
                        break;
                    }
                    break;

                case 2:
                    Console.Clear();
                    //Validação de Nome null
                    do
                    {
                        Console.Write("INFORME A DATA DE NASCIMENTO DO(A) ADOTANTE: ");
                        try
                        {
                            nascimento = DateTime.Parse(Console.ReadLine());
                            validacao = false;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("\nFORMADO INVÁLIDO! [dd/mm/yyyy]\n");
                        }

                        if (nascimento > DateTime.Now)
                        {
                            Console.WriteLine("\nDATA DE NASCIMENTO NÃO PODE SER FUTURA!\n");
                        }
                            

                    } while (validacao);

                    conexao.Open(); // Abrindo conexão

                    cmd = new();

                    cmd.CommandText = "UPDATE Pessoa Set Nascimento = @NASCIMENTO WHERE CPF = @CPF;"; //Aqui fiz o segundo exemple de montar os paramentros 
                    //linguagem sql para enviar para o banco

                    cmd.Connection = conexao; //Passei o caminho para conexão
                    cmd.Parameters.Add(new SqlParameter("@CPF", cpf));
                    cmd.Parameters.Add(new SqlParameter("@NASCIMENTO", nascimento));

                    verificacao = cmd.ExecuteNonQuery(); // verificação para ver se o codigo realmente foi executado, valor retornar quantidade de linhas afetadas
                    conexao.Close();

                    if (verificacao > 0)
                    {
                        Console.WriteLine("\nEDITADO COM SUCESSO!");
                        Console.ReadKey();
                        break;
                    }
                    break;

                case 3:
                    Console.Clear();

                    Console.Write("INFORME O SEXO DO(A) ADOTANTE(Masculino | Feminino | Indefinido): ");
                    sexo = Console.ReadLine().ToUpper(); ;

                    conexao.Open();

                    cmd = new();

                    cmd.CommandText = "UPDATE Pessoa Set Sexo = @SEXO WHERE CPF = @CPF;";

                    cmd.Connection = conexao;
                    cmd.Parameters.Add(new SqlParameter("@CPF", cpf));
                    cmd.Parameters.Add(new SqlParameter("@SEXO", sexo));

                    verificacao = cmd.ExecuteNonQuery();
                    conexao.Close();

                    if (verificacao > 0)
                    {
                        Console.WriteLine("\nEDITADO COM SUCESSO!");
                        Console.ReadKey();
                        break;
                    }
                    break;

                case 4:
                    Console.Clear();

                    Console.Write("INFORME A RUA: ");
                    rua = Console.ReadLine().ToUpper();

                    Console.Write("INFORME O NUMERO DO LOCAL: ");
                    numero = Console.ReadLine().ToUpper();

                    Console.Write("INFORME O BAIRRO: ");
                    bairro = Console.ReadLine().ToUpper();

                    Console.Write("INFORME A CIDADE: ");
                    cidade = Console.ReadLine().ToUpper();

                    Console.Write("INFORME O ESTADO: ");
                    estado = Console.ReadLine().ToUpper();

                    conexao.Open();

                    cmd = new();

                    cmd.CommandText = "UPDATE Pessoa Set Rua = @RUA, Numero = @NUMERO, Bairro = @BAIRRO, Cidade = @CIDADE, Estado = @ESTADO WHERE CPF = @CPF;";

                    cmd.Connection = conexao;
                    cmd.Parameters.Add(new SqlParameter("@CPF", cpf));
                    cmd.Parameters.Add(new SqlParameter("@RUA", rua));
                    cmd.Parameters.Add(new SqlParameter("@NUMERO", numero));
                    cmd.Parameters.Add(new SqlParameter("@BAIRRO", bairro));
                    cmd.Parameters.Add(new SqlParameter("@CIDADE", cidade));
                    cmd.Parameters.Add(new SqlParameter("@ESTADO", estado));

                    verificacao = cmd.ExecuteNonQuery();
                    conexao.Close();

                    if (verificacao > 0)
                    {
                        Console.WriteLine("\nEDITADO COM SUCESSO!");
                        Console.ReadKey();
                        break;
                    }
                    break;


                case 5:
                    Console.Clear();

                    Console.Write("INFORME O TELEFONE DO ADOTANTE: ");
                    telefone = Console.ReadLine().ToUpper();

                    conexao.Open();

                    cmd = new();

                    cmd.CommandText = "UPDATE Pessoa Set Telefone = @TELEFONE WHERE CPF = @CPF;";

                    cmd.Connection = conexao;
                    cmd.Parameters.Add(new SqlParameter("@CPF", cpf));
                    cmd.Parameters.Add(new SqlParameter("@TELEFONE", telefone));

                    verificacao = cmd.ExecuteNonQuery();
                    conexao.Close();

                    if (verificacao > 0)
                    {
                        Console.WriteLine("\nEDITADO COM SUCESSO!");
                        Console.ReadKey();
                        break;
                    }
                    break;
            }
        }
        static void EditarDadosAnimal(SqlConnection conexao)
        {
            string raca, sexo, nome, familia;
            int opcao = 0, quantidade = 0, chip = 0;
            char resposta = 'a';
            bool validacao;
            SqlCommand cmd;

            Console.Clear();

            Console.WriteLine("PAINEL DE EDIÇÃO");
            //interface com usuário, com validação de erros de opçao e estouro
            Console.WriteLine("\nPARA EDITAR UM CADASTRO DE UM ANIMAL SERÁ OBRIGATÓRIO A SEGUINTE INFORMAÇAO: ");
            Console.WriteLine("\nCHIP\n");
            Console.WriteLine("DESEJA CONTINUAR COM O CADASTRO (s/n): ");
            do
            {
                Console.Write("RESPOSTA: ");
                try
                {
                    resposta = char.Parse(Console.ReadLine().ToUpper());
                    validacao = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPARAMETRO DE ENTRADA INVÁLIDO!\n");
                    validacao = true;
                }
                if (resposta != 'S' && resposta != 'N')
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nOPÇÃO DIGITADA É INVÁLIDA!\n");
                        validacao = true;
                    }
                }
            } while (validacao);

            if (resposta == 'N')
            {
                return;
            }

            Console.Clear();

            Console.WriteLine("EDITAR CADASTRO DE ANIMALZINHO\n\n");
            //validação de possivel inserção de dado null e estouro de inf.
            do
            {
                Console.Write("INFORME O  CHIP DO(A) ANIMAL[OBRIGATÓRIO]: ");
                try
                {
                    chip = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPARAMETRO DE ENTRADA INVÁLIDO!\n");
                    validacao = true;
                }
                
                validacao = false;

                if (chip == 0)
                {
                    Console.WriteLine("\nCHIP OBRIGATÓRIO!\n");
                    validacao = true;
                }

            } while (validacao);

            conexao.Open(); //Abrindo Conexão

            cmd = new SqlCommand("SELECT * FROM Animal WHERE CHIP = @CHIP", conexao); // Passando parametro em linguagem sql e o caminho da conexão

            cmd.Parameters.Add(new SqlParameter("@CHIP", chip)); //Passando o paramentro informado no @CHIP

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        quantidade++;
                    }
                }
            }

            if (quantidade == 0)
            {
                Console.WriteLine("\nANIMAL NÃO POSSUE CADASTRO!");
                Console.ReadKey();
                conexao.Close();
                return;
            }

            conexao.Close();

            //Validação de possivel erro de digitação
            Console.WriteLine("\nINFORME QUAL DADO DESEJA ALTERAR: ");
            Console.WriteLine("\n1 - NOME\n2 - RAÇA\n3 - SEXO\n4 - FAMILIA\n\n");
            do
            {
                Console.Write("OPÇÃO: ");
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                    validacao = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPARAMETRO DE ENTRADA É INVÁLIDO!\n");
                    validacao = true;
                }
                if (opcao < 1 || opcao > 4)
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nOPÇÃO DIGITADA É INVÁLIDA!\n");
                        validacao = true;
                    }
                }

            } while (validacao);

            switch (opcao)
            {
                case 1:
                    Console.Clear();

                    Console.Write("INFORME O NOME DO ANIMAL: ");
                    nome = Console.ReadLine().ToUpper();

                    conexao.Open();

                    cmd = new();

                    cmd.CommandText = "UPDATE Animal Set Nome = @NOME WHERE CHIP = @CHIP;";

                    cmd.Connection = conexao;
                    cmd.Parameters.Add(new SqlParameter("@CHIP", chip));
                    cmd.Parameters.Add(new SqlParameter("@NOME", nome));

                    int verificacao = cmd.ExecuteNonQuery();
                    conexao.Close();

                    if (verificacao > 0)
                    {
                        Console.WriteLine("\nEDITADO COM SUCESSO");
                        Console.ReadKey();
                        break;
                    }
                    break;

                case 2:
                    Console.Clear();

                    Console.Write("INFORME A RAÇA DO ANIMAL: ");
                    raca = Console.ReadLine().ToUpper();

                    conexao.Open();

                    cmd = new();

                    cmd.CommandText = "UPDATE Animal Set Raca = @RACA WHERE CHIP = @CHIP;";

                    cmd.Connection = conexao;
                    cmd.Parameters.Add(new SqlParameter("@CHIP", chip));
                    cmd.Parameters.Add(new SqlParameter("@RACA", raca));

                    verificacao = cmd.ExecuteNonQuery();
                    conexao.Close();

                    if (verificacao > 0)
                    {
                        Console.WriteLine("\nEDITADO COM SUCESSO");
                        Console.ReadKey();
                        break;
                    }
                    break;

                case 3:
                    Console.Clear();

                    Console.Write("INFORME O SEXO DO ANIMAL: ");
                    sexo = Console.ReadLine().ToUpper();

                    conexao.Open();

                    cmd = new();

                    cmd.CommandText = "UPDATE Animal Set Sexo = @SEXO WHERE CHIP = @CHIP;";

                    cmd.Connection = conexao;
                    cmd.Parameters.Add(new SqlParameter("@CHIP", chip));
                    cmd.Parameters.Add(new SqlParameter("@SEXO", sexo));

                    verificacao = cmd.ExecuteNonQuery();
                    conexao.Close();

                    if (verificacao > 0)
                    {
                        Console.WriteLine("\nEDITADO COM SUCESSO");
                        Console.ReadKey();
                        break;
                    }
                    break;


                case 4:
                    Console.Clear();

                    Console.Write("INFORME DE QUAL FAMILIA O ANIMAL PERTENCE: ");
                    familia = Console.ReadLine().ToUpper();

                    conexao.Open();

                    cmd = new();

                    cmd.CommandText = "UPDATE Animal SET Familia = @FAMILIA WHERE CHIP = @CHIP;";

                    cmd.Connection = conexao;
                    cmd.Parameters.Add(new SqlParameter("@CHIP", chip));
                    cmd.Parameters.Add(new SqlParameter("@FAMILIA", familia));

                    verificacao = cmd.ExecuteNonQuery();
                    conexao.Close();

                    if (verificacao > 0)
                    {
                        Console.WriteLine("\nEDITADO COM SUCESSO");
                        Console.ReadKey();
                        break;
                    }
                    break;
            }
        }
        #endregion      

        #region Imprimir Tabelas
        static void ImprimirDadosAdotantes(SqlConnection conexao)
        {
            string cpf;
            SqlCommand cmd = new();
            int opcao = 0, quantidade = 0;
            bool validacao;

            Console.Clear();

            Console.WriteLine("IMPRIMIR ADOTANTES\n");

            Console.WriteLine("ESCOLHA A OPÇÃO DESEJA: \n");
            Console.WriteLine("1 - IMPRIMIR TODOS ADOTANTES");
            Console.WriteLine("2 - IMPRIMIR UM ESPECIFICO\n");
            do
            {
                Console.Write("OPCÃO: ");
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                    validacao = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPARAMETROS DE ENTRADA INVÁLIDO!\n");
                    validacao = true;
                }
                if (opcao < 1 || opcao > 2)
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nOPÇÃO DIGITADA É INVÁLIDA!\n");
                        validacao = true;
                    }
                }

            } while (validacao);

            if (opcao == 2)
            {
                Console.Clear();

                do
                {
                    Console.Write("INFORME O CPF DO(A) ADOTANTE[OBRIGATÓRIO]: ");
                    cpf = Console.ReadLine().ToUpper();
                    validacao = false;

                    if (cpf.Length < 11 || cpf.Length > 11)
                    {
                        Console.WriteLine("\nCPF DEVE CONTER 11 DIGITOS!\n");
                        validacao = true;
                    }

                } while (validacao);

                conexao.Open();

                cmd = new SqlCommand("SELECT * FROM Pessoa WHERE CPF = @CPF", conexao);

                cmd.Parameters.Add(new SqlParameter("@CPF", cpf));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            quantidade++;
                        }
                    }
                }

                if (quantidade == 0)
                {
                    Console.WriteLine("\nADOTANTE NÃO POSSUE CADASTRO!");
                    Console.ReadKey();
                    conexao.Close();
                    return;
                }

                conexao.Close();

                conexao.Open();

                #region CARREGAR DADO PARA MANIPULAÇÃO
                //cmd = new SqlCommand("SELECT Quantidade FROM Pessoa_Adota_Animal WHERE CPF = @CPF", conexao);

                //cmd.CommandType = System.Data.CommandType.Text;

                //cmd.Parameters.Add(new SqlParameter("@CPF", cpf));

                //using (SqlDataReader reader = cmd.ExecuteReader())
                //{
                //    while (reader.Read())
                //    {
                //        quantidade = Convert.ToInt32(reader["Quantidade"]);
                //    }
                //}
                #endregion

                cmd = new();

                cmd.CommandText = "SELECT pessoa.Nome, pessoa.CPF, pessoa.Nascimento, pessoa.Telefone, pessoa.Sexo, " +
                              "pessoa.Rua, pessoa.Numero, pessoa.Bairro, pessoa.Cidade, pessoa.Estado FROM Pessoa " +
                              "WHERE CPF = @CPF;";


                cmd.Connection = conexao;

                cmd.Parameters.Add(new SqlParameter("@CPF", cpf));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.Clear();
                    while (reader.Read())
                    {
                        Console.WriteLine("DADOS ADOTANTE\n");
                        Console.WriteLine($"NOME: {reader.GetString(0)}");
                        Console.WriteLine($"CPF: {reader.GetString(1)}");
                        Console.WriteLine($"NASCIMENTO: {reader.GetDateTime(2).ToShortDateString()}");
                        Console.WriteLine($"TELEFONE: {reader.GetString(3)}");
                        Console.WriteLine($"SEXO: {reader.GetString(4)}");
                        Console.WriteLine($"RUA: {reader.GetString(5)} º{reader.GetString(6)}. {reader.GetString(7)}, {reader.GetString(8)} - {reader.GetString(9)}\n");

                    }
                }

                Console.WriteLine("PRESSIONE ENTER PARA VOLTAR AO MENU ANTERIOR!");
                Console.ReadKey();
                conexao.Close();
                return;
            }

            Console.Clear();

            conexao.Open();

            #region OUTRA FORMA DE FAZER A LEITURA DO BANCO
            //cmd.CommandText = "SELECT pessoa.Nome, pessoa.CPF, pessoa.Sexo, pessoa.Telefone, " +
            //                  "pessoa.Rua, pessoa.Numero, pessoa.Bairro, pessoa.Cidade, pessoa.Estado FROM Pessoa;";
            //cmd.Connection = conexao;
            #endregion

            cmd = new SqlCommand("SELECT pessoa.Nome, pessoa.CPF, pessoa.Nascimento, pessoa.Sexo, pessoa.Telefone, " +
                                 "pessoa.Rua, pessoa.Numero, pessoa.Bairro, pessoa.Cidade, pessoa.Estado FROM Pessoa;", conexao);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine("DADOS ADOTANTE\n");
                    Console.WriteLine($"NOME: {reader.GetString(0)}");
                    Console.WriteLine($"CPF: {reader.GetString(1)}");
                    Console.WriteLine($"DATA DE NASCIMENTO: {reader.GetDateTime(2).ToShortDateString()}");
                    Console.WriteLine($"TELEFONE: {reader.GetString(3)}");
                    Console.WriteLine($"SEXO: {reader.GetString(4)}");
                    Console.WriteLine($"RUA: {reader.GetString(5)} º{reader.GetString(6)}. {reader.GetString(7)}, {reader.GetString(8)} - {reader.GetString(9)}\n");
                }
            }

            Console.WriteLine("PRESSIONE ENTER PARA VOLTAR AO MENU ANTERIOR!");
            Console.ReadKey();
            conexao.Close();
        }
        static void ImprimirDadosAnimais(SqlConnection conexao)
        {
            int opcao = 0, quantidade = 0, chip;
            bool validacao = false;
            SqlCommand cmd = new();

            Console.Clear();

            Console.WriteLine("IMPRIMIR ANIMAIS\n");
            //INTERFACE COM USUARIO COM VALIDAÇÃO DE ERROS E OPÇÃO
            Console.WriteLine("ESCOLHA A OPÇÃO DESEJA: \n");
            Console.WriteLine("1 - IMPRIMIR TODOS ANIMAIS");
            Console.WriteLine("2 - IMPRIMIR UM ESPECIFICO\n");
            do
            {
                Console.Write("OPCÃO: ");
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                    validacao = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPARAMETROS DE ENTRADA INVÁLIDO!\n");
                    validacao = true;
                }
                if (opcao < 1 || opcao > 2)
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nOPÇÃO DIGITADA É INVÁLIDA!\n");
                        validacao = true;
                    }
                }

            } while (validacao);

            if (opcao == 2)
            {
                Console.Clear();

                do
                {
                    Console.Write("INFORME O NÚMERO DO CHIP DO ANIMAL[OBRIGATÓRIO]: ");
                    chip = int.Parse(Console.ReadLine());
                    validacao = false;

                    if (chip == 0)
                    {
                        Console.WriteLine("\nCHIP OBRIGATÓRIO!\n");
                        validacao = true;
                    }

                } while (validacao);

                conexao.Open();

                cmd = new SqlCommand("SELECT * FROM Animal WHERE CHIP = @CHIP", conexao); //Mesmo exemplo dado em pontos anteriores

                cmd.Parameters.Add(new SqlParameter("@CHIP", chip));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            quantidade++;
                        }
                    }
                }

                if (quantidade == 0)
                {
                    Console.WriteLine("\nANIMAL NÃO POSSUE CADASTRO!");
                    Console.ReadKey();
                    conexao.Close();
                    return;
                }

                conexao.Close();

                conexao.Open();

                #region CARREGAR DADO PARA MANIPULAÇÃO
                //cmd = new SqlCommand("SELECT Quantidade FROM Pessoa_Adota_Animal WHERE CPF = @CPF", conexao);

                //cmd.CommandType = System.Data.CommandType.Text;

                //cmd.Parameters.Add(new SqlParameter("@CPF", cpf));

                //using (SqlDataReader reader = cmd.ExecuteReader())
                //{
                //    while (reader.Read())
                //    {
                //        quantidade = Convert.ToInt32(reader["Quantidade"]);
                //    }
                //}
                #endregion  //Deixei porque é algo muito importante 

                cmd = new SqlCommand("SELECT animal.CHIP, animal.Familia, animal.Nome, animal.Raca, " +
                                    "animal.Sexo FROM Animal WHERE CHIP = @CHIP", conexao);

                cmd.Parameters.Add(new SqlParameter("@CHIP", chip));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.Clear();
                    while (reader.Read())
                    {
                        Console.WriteLine("DADOS DO ANIMAL\n");
                        Console.WriteLine($"CHIP: {reader.GetInt32(0)}");
                        Console.WriteLine($"FAMILIA: {reader.GetString(1)}");
                        Console.WriteLine($"NOME: {reader.GetString(2)}");
                        Console.WriteLine($"RACA: {reader.GetString(3)}");
                        Console.WriteLine($"SEXO: {reader.GetString(4)}\n");
                    }
                }

                Console.WriteLine("PRESSIONE ENTER PARA VOLTAR AO MENU ANTERIOR!");
                Console.ReadKey();
                conexao.Close();
                return;
            }

            Console.Clear();

            conexao.Open();

            cmd = new SqlCommand("SELECT animal.CHIP, animal.Familia, animal.Nome, animal.Raca, " +
                                 "animal.Sexo FROM Animal", conexao);  

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine("DADOS DO ANIMAL\n");
                    Console.WriteLine($"CHIP: {reader.GetInt32(0)}");
                    Console.WriteLine($"FAMILIA: {reader.GetString(1)}");
                    Console.WriteLine($"NOME: {reader.GetString(2)}");
                    Console.WriteLine($"RACA: {reader.GetString(3)}");
                    Console.WriteLine($"SEXO: {reader.GetString(4)}\n");
                }
            }

            Console.WriteLine("PRESSIONE ENTER PARA VOLTAR AO MENU ANTERIOR!");
            Console.ReadKey();
            conexao.Close();
        }
        static void ImprimirDadosAnimaisDisponiveis(SqlConnection conexao)
        {
            int opcao = 0, quantidade = 0, chip = 0;
            bool validacao;
            SqlCommand cmd;

            Console.Clear();

            Console.WriteLine("IMPRIMIR ANIMAIS DISPINIVEIS\n");

            Console.WriteLine("ESCOLHA A OPÇÃO DESEJA: \n");
            Console.WriteLine("1 - IMPRIMIR TODOS ANIMAIS DISPONIVEIS PARA ADOÇÃO");
            Console.WriteLine("2 - IMPRIMIR UM ESPECIFICO\n");
            do
            {
                Console.Write("OPCÃO: ");
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                    validacao = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPARAMETROS DE ENTRADA INVÁLIDO!\n");
                    validacao = true;
                }
                if (opcao < 1 || opcao > 2)
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nOPÇÃO DIGITADA É INVÁLIDA!\n");
                        validacao = true;
                    }
                }

            } while (validacao);

            if (opcao == 2)
            {
                Console.Clear();
                //TRATATIVA DE ERROR
                do
                {
                    Console.Write("INFORME O NÚMERO DO CHIP DO ANIMAL[OBRIGATÓRIO]: ");
                    try
                    {
                        chip = int.Parse(Console.ReadLine());
                        validacao = false;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\nPARAMETRO DE ENTRADA INVÁLIDO!\n");
                        validacao = true;
                    }                  

                    if (chip == 0)
                    {
                        Console.WriteLine("\nCHIP OBRIGATÓRIO!\n");
                        validacao = true;
                    }

                } while (validacao);

                conexao.Open();

                cmd = new SqlCommand("SELECT * FROM Animais_Disponiveis WHERE CHIP = @CHIP", conexao);

                cmd.Parameters.Add(new SqlParameter("@CHIP", chip));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            quantidade++;
                        }
                    }
                }

                if (quantidade == 0)
                {
                    Console.WriteLine("\nANIMAL NÃO POSSUE CADASTRO OU JÁ FOI ADOTADO!");
                    Console.ReadKey();
                    conexao.Close();
                    return;
                }

                conexao.Close();

                conexao.Open();

                #region CARREGAR DADO PARA MANIPULAÇÃO
                //cmd = new SqlCommand("SELECT Quantidade FROM Pessoa_Adota_Animal WHERE CPF = @CPF", conexao);

                //cmd.CommandType = System.Data.CommandType.Text;

                //cmd.Parameters.Add(new SqlParameter("@CPF", cpf));

                //using (SqlDataReader reader = cmd.ExecuteReader())
                //{
                //    while (reader.Read())
                //    {
                //        quantidade = Convert.ToInt32(reader["Quantidade"]);
                //    }
                //}
                #endregion

                cmd = new SqlCommand("SELECT animal.CHIP, animal.Familia, animal.Nome, animal.Raca, " +
                                    "animal.Sexo FROM Animal WHERE CHIP = @CHIP", conexao);

                cmd.Parameters.Add(new SqlParameter("@CHIP", chip));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.Clear();
                    while (reader.Read())
                    {
                        Console.WriteLine("DADOS DO ANIMAL\n");
                        Console.WriteLine($"CHIP: {reader.GetInt32(0)}");
                        Console.WriteLine($"FAMILIA: {reader.GetString(1)}");
                        Console.WriteLine($"NOME: {reader.GetString(2)}");
                        Console.WriteLine($"RACA: {reader.GetString(3)}");
                        Console.WriteLine($"SEXO: {reader.GetString(4)}\n");
                    }
                }

                Console.WriteLine("PRESSIONE ENTER PARA VOLTAR AO MENU ANTERIOR!");
                Console.ReadKey();
                conexao.Close();
                return;
            }

            Console.Clear();

            conexao.Open();

            cmd = new SqlCommand("SELECT animal.CHIP, animal.Familia, animal.Nome, animal.Raca, " +
                                 "animal.Sexo FROM Animal JOIN Animais_Disponiveis ON " +
                                 "animais_disponiveis.CHIP = animal.CHIP ", conexao);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    Console.WriteLine("NÃO EXISTE ANIMAIS PARA ADOÇÃO NO MOMENTO!");
                    Console.WriteLine("PRESSIONE ENTER PARA VOLTAR AO MENU ANTERIOR!");
                    Console.ReadKey();
                    conexao.Close();
                    return;
                }

                while (reader.Read())
                {
                    Console.WriteLine("DADOS DO ANIMAL\n");
                    Console.WriteLine($"CHIP: {reader.GetInt32(0)}");
                    Console.WriteLine($"FAMILIA: {reader.GetString(1)}");
                    Console.WriteLine($"NOME: {reader.GetString(2)}");
                    Console.WriteLine($"RACA: {reader.GetString(3)}");
                    Console.WriteLine($"SEXO: {reader.GetString(4)}\n");
                }
            }

            Console.WriteLine("PRESSIONE ENTER PARA VOLTAR AO MENU ANTERIOR!");
            Console.ReadKey();
            conexao.Close();
        }
        static void ImprimirDadosAdocao(SqlConnection conexao)
        {
            Console.Clear();
            conexao.Open();

            SqlCommand cmd = new();

            cmd.CommandText = "SELECT pessoa.Nome, pessoa.CPF, animal.CHIP, animal.Nome, animal.Familia, animal.Raca, animal.Sexo FROM Pessoa_Adota_Animal " +
                              "JOIN Pessoa ON " +
                              "pessoa.CPF = Pessoa_Adota_Animal.CPF " +
                              "JOIN Animal ON " +
                              "animal.CHIP = Pessoa_Adota_Animal.CHIP ";


            cmd.Connection = conexao;


            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine("DADOS DA ADOÇÃO\n");
                    Console.WriteLine($"Nome: {reader.GetString(0)}");
                    Console.WriteLine($"CPF: {reader.GetString(1)}");
                    Console.WriteLine($"CHIP: {reader.GetInt32(2)}");
                    Console.WriteLine($"Nome: {reader.GetString(3)}");
                    Console.WriteLine($"Familia: {reader.GetString(4)}");
                    Console.WriteLine($"Raca: {reader.GetString(5)}");
                    Console.WriteLine($"Sexo: {reader.GetString(6)}\n");

                }
            }

            Console.WriteLine("Pressione enter para voltar ao menu anterior!");
            Console.ReadKey();
            conexao.Close();
        }
        #endregion

        static void IniciarSistema()
        {
            BancoOng caminho = new();
            SqlConnection conexaoSql = new SqlConnection(caminho.CaminhoDeConexao());
            int opcao = 10;
            bool validacao; ;

            do
            {
                Console.Clear();

                Console.WriteLine("BEM VINDO AO SISTEMA ONG ANIMAIS FELIZES");

                Console.WriteLine("\n\nSELECIONE UMA OPÇÃO: \n");
                Console.WriteLine("1 - CADASTRAR ");
                Console.WriteLine("2 - EDITAR");
                Console.WriteLine("3 - ADOTAR");
                Console.WriteLine("4 - IMPRIMIR");
                Console.WriteLine("\n0 - SAIR");
                Console.Write("\nOPÇÃO: ");
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                    validacao = false;

                }
                catch (Exception)
                {
                    Console.WriteLine("\nPARAMETRO DE ENTRADA INVÁLIDO!");
                    Console.WriteLine("PRESSIONE ENTER PARA ESCOLHER NOVAMENTE!");
                    validacao = true;
                    Console.ReadKey();
                }
                if (opcao < 0 || opcao > 5)
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nOPÇÃO DIGITADA É INVÁLIDA!");
                        Console.WriteLine("PRESSIONE ENTER PARA ESCOLHER NOVAMENTE!");
                        validacao = true;
                        Console.ReadKey();
                    }
                }

                switch (opcao)
                {
                    case 1:
                        do
                        {
                            opcao = 10;
                            Console.Clear();

                            Console.WriteLine("CADASTRO ONG ANIMAIS FELIZES");

                            Console.WriteLine("\nSELECIONE UMA OPÇÃO: \n");
                            Console.WriteLine("1 - ADOTANTE ");
                            Console.WriteLine("2 - ANIMAL");
                            Console.WriteLine("\n9 - VOLTAR AO MENU ANTERIOR");
                            Console.Write("\nOpção: ");
                            try
                            {
                                opcao = int.Parse(Console.ReadLine());
                                validacao = false;

                            }
                            catch (Exception)
                            {
                                Console.WriteLine("\nPARAMETRO DE ENTRADA INVÁLIDO!");
                                Console.WriteLine("PRESSIONE ENTER PARA ESCOLHER NOVAMENTE!");
                                validacao = true;
                                Console.ReadKey();
                            }
                            if (opcao < 1 || opcao > 2 && opcao != 9)
                            {
                                if (!validacao)
                                {
                                    Console.WriteLine("\nOPÇÃO DIGITADA É INVÁLIDA!");
                                    Console.WriteLine("PRESSIONE ENTER PARA ESCOLHER NOVAMENTE!");
                                    validacao = true;
                                    Console.ReadKey();
                                }
                            }

                            switch (opcao)
                            {
                                case 1:
                                    InsertDadosAdotantes(conexaoSql);
                                    break;


                                case 2:
                                    InsertDadosAnimais(conexaoSql);
                                    break;
                            }
                        } while (opcao != 9);
                        break;

                    case 2:
                        do
                        {
                            opcao = 10;
                            Console.Clear();

                            Console.WriteLine("EDIÇÃO ONG ANIMAIS FELIZES");

                            Console.WriteLine("\n\nSELECIONE UMA OPÇÃO: \n");
                            Console.WriteLine("1 - ADOTANTE ");
                            Console.WriteLine("2 - ANIMAL");
                            Console.WriteLine("\n9 - VOLTAR AO MENU ANTERIOR");
                            Console.Write("\nOpção: ");
                            try
                            {
                                opcao = int.Parse(Console.ReadLine());
                                validacao = false;

                            }
                            catch (Exception)
                            {
                                Console.WriteLine("\nPARAMETRO DE ENTRADA INVÁLIDO!");
                                Console.WriteLine("PRESSIONE ENTER PARA ESCOLHER NOVAMENTE!");
                                validacao = true;
                                Console.ReadKey();
                            }
                            if (opcao < 1 || opcao > 2 && opcao != 9)
                            {
                                if (!validacao)
                                {
                                    Console.WriteLine("\nOPÇÃO DIGITADA É INVÁLIDA!");
                                    Console.WriteLine("PRESSIONE ENTER PARA ESCOLHER NOVAMENTE!");
                                    validacao = true;
                                    Console.ReadKey();
                                }
                            }

                            switch (opcao)
                            {
                                case 1:
                                    EditarDadosAdotantes(conexaoSql);
                                    break;


                                case 2:
                                    EditarDadosAnimal(conexaoSql);
                                    break;
                            }
                        } while (opcao != 9);
                        break;

                    case 3:
                        InsertAdotarAnimais(conexaoSql);
                        break;                    

                    case 4:
                        do
                        {
                            opcao = 10;
                            Console.Clear();


                            Console.WriteLine("IMPRESSAO ONG ANIMAIS FELIZES");

                            Console.WriteLine("\n\nSELECIONE UMA OPÇÃO: \n");
                            Console.WriteLine("1 - ADOTANTES ");
                            Console.WriteLine("2 - ANIMAIS");
                            Console.WriteLine("3 - ANIMAIS DISPONIVEIS PARA ADOÇÃO");
                            Console.WriteLine("4 - ANIMAIS ADOTADOS");

                            Console.WriteLine("\n9 - VOLTAR AO MENU ANTERIOR");
                            Console.Write("\nOpção: ");
                            try
                            {
                                opcao = int.Parse(Console.ReadLine());
                                validacao = false;

                            }
                            catch (Exception)
                            {
                                Console.WriteLine("\nPARAMETRO DE ENTRADA INVÁLIDO!");
                                Console.WriteLine("PRESSIONE ENTER PARA ESCOLHER NOVAMENTE!");
                                validacao = true;
                                Console.ReadKey();                                
                            }
                            if (opcao < 1 || opcao > 4 && opcao != 9)
                            {
                                if (!validacao)
                                {
                                    Console.WriteLine("\nOPÇÃO DIGITADA É INVÁLIDA!");
                                    Console.WriteLine("PRESSIONE ENTER PARA ESCOLHER NOVAMENTE!");
                                    validacao = true;
                                    Console.ReadKey();
                                }
                            }

                            switch (opcao)
                            {
                                case 1:
                                    ImprimirDadosAdotantes(conexaoSql);
                                    break;


                                case 2:
                                    ImprimirDadosAnimais(conexaoSql);
                                    break;
                                                  
                                case 3:
                                    ImprimirDadosAnimaisDisponiveis(conexaoSql);
                                    break;

                                case 4:
                                    ImprimirDadosAdocao(conexaoSql);
                                    break;
                            }
                        } while (opcao != 9);
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
