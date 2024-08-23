using MySql.Data.MySqlClient;

namespace appCrudLivros
{
    public class Program
    {
        private static string connectionString =
            "Server=localhost;Port=3306;Database=db_aulas_2024;User=daniele;password=1234567;SslMode=none;";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1 - Cadastrar Livro");
                Console.WriteLine("2 - Listar Livro");
                Console.WriteLine("3 - Editar Livro");
                Console.WriteLine("4 - Excluir Livro");
                Console.WriteLine("5 - Sair");
                Console.Write("Escolha uma opção acima: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        CadastrarLivro();
                        break;
                    case "2":
                        ListarLivro();
                        break;
                    case "3":
                        Editar();
                        break;
                    case "4":
                        Excluir();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }
            }
        }
        static void CadastrarLivro()
        {
            Console.Write("Informe o Titulo: ");
            string titulo = Console.ReadLine();

            Console.Write("Informe o Autor do livro: ");
            string autor = Console.ReadLine();

            Console.Write("Informe o ano de publicação: ");
            int anopublic = int.Parse(Console.ReadLine());

            Console.Write("Informe o Gênero: ");
            string genero = Console.ReadLine();

            Console.Write("Informe o numero de páginas: ");
            int numpag = int.Parse(Console.ReadLine());

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO livros (titulo,autor,anopublic,genero,numpag) VALUES (@titulo,@autor,@anopublic, @genero, @numpag)";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@titulo", titulo);
                cmd.Parameters.AddWithValue("@autor", autor);
                cmd.Parameters.AddWithValue("@anopublic", anopublic);
                cmd.Parameters.AddWithValue("@genero", genero);
                cmd.Parameters.AddWithValue("@numpag", numpag);
                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("Livro cadastrado com sucesso");
        }
        static void ListarLivro()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, titulo, autor, anopublic, genero, numpag FROM livros";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Id: {reader["Id"]}, titulo: {reader["titulo"]}, autor: {reader["autor"]},anopublic: {reader["anopublic"]}, genero: {reader["genero"]}, numpag: {reader["numpag"]}");

                        }
                    }
                    else
                    {
                        Console.WriteLine("Não existe Livro cadastrado");
                    }

                }

            }
        }
        static void Excluir()
        {
            Console.Write("Informe o Id do livro que deseja excluir: ");
            int idExclusao = int.Parse(Console.ReadLine());

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM livros WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@Id", idExclusao);

                int linhaAfetada = cmd.ExecuteNonQuery();
                if (linhaAfetada > 0)
                {
                    Console.WriteLine("Livro excluido com sucesso");
                }
                else
                {
                    Console.WriteLine("Livro não encontrado");
                }
            }
        }
        static void Editar()
        {
            Console.Write("Informe o Id do livro que deseja editar: ");
            int idEditar = int.Parse(Console.ReadLine());

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM livros WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", idEditar);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.Write("Informe o novo titulo: (* Deixe o campo em branco, para não alterar): ");
                        string novotitulo = Console.ReadLine();

                        Console.Write("Informe o novo autor: (* Deixe o campo em branco, para não alterar): ");
                        string novoautor = Console.ReadLine();

                        Console.Write("Informe o novo ano de publicação: (* Deixe o campo em branco, para não alterar): ");
                        string novoanopublic = Console.ReadLine();


                        Console.Write("Informe o novo gênero: (* Deixe o campo em branco, para não alterar): ");
                        string novogenero = Console.ReadLine();

                        Console.Write("Informe o novo numeros de páginas: (* Deixe o campo em branco, para não alterar): ");
                        string novonumpag = Console.ReadLine();
                        reader.Close();

                        string queryUpdate = "UPDATE livros SET titulo = @titulo, autor = @autor, anopublic= @anopublic, genero = @genero, numpag = @numpag WHERE Id = @Id";
                        cmd = new MySqlCommand(queryUpdate, connection);
                        cmd.Parameters.AddWithValue("@titulo", string.IsNullOrWhiteSpace(novotitulo) ? reader["titulo"] : novotitulo);
                        cmd.Parameters.AddWithValue("@autor", string.IsNullOrWhiteSpace(novoautor) ? reader["autor"] : novoautor);
                        cmd.Parameters.AddWithValue("@anopublic", string.IsNullOrWhiteSpace(novoanopublic) ? reader["anopublic"] : int.Parse(novoanopublic));
                        cmd.Parameters.AddWithValue("@genero", string.IsNullOrWhiteSpace(novogenero) ? reader["genero"] : novogenero);
                        cmd.Parameters.AddWithValue("@numpag", string.IsNullOrWhiteSpace(novonumpag) ? reader["numpag"] : int.Parse(novonumpag));
                        cmd.Parameters.AddWithValue("@Id", idEditar);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("O livro foi atualizada com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("O Id do livro informado não existe!");
                    }
                }
            }

        }
    }
}