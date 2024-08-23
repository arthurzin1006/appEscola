namespace appCrudLivros
{
    public class Livros
    {
        public int Id { get; set; }
        public string titulo { get; set; }
        public string autor { get; set; }
        public int anopublic { get; set; }
        public string genero { get; set; }
        public int numpag { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Titulo: {titulo}, Autor: {autor}, Ano Publicacao: {anopublic}, Genero:{genero}, Numero paginas:{numpag} ";
        }

    }
}