
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;


//$@"Server=(localdb)\MSSQLLocalDB;Database=AulaDapper;"
////Dapper
//using (var conn = new SqlConnection($@"Server=(localdb)\MSSQLLocalDB;Database=Aula;"))// Criamos aqui uma porta para conecxão do SQL
//{
//    //string query = @"SELECT [Id], [Nome], [Cor] FROM [Aula].[dbo].[Produtoss]";
//    string query = @"SELECT * FROM [Aula].[dbo].[Produtoss]";//Isso o Dapper consegue identificar
//    //que queremos usar a tabela Produtoss
//   var listaProdutos =  conn.Query<Produtoss>(query);
//    Console.WriteLine(listaProdutos.ToList());
//}
//public class Produtoss
//{
//    public int Id { get; set; }
//    public string Nome { get; set; }
//    public string Cor { get; set; }
//}


//Dapper Contrib
using (var conn = new SqlConnection($@"Server=(localdb)\MSSQLLocalDB;Database=Aula;"))// Criamos aqui uma porta para conecxão do SQL
{
    var categorias = conn.GetAll<Categoria>();// Listar
    var categoria = conn.Get<Categoria>(1);// Selecionar

   
    conn.Insert<Categoria>(new Categoria() { Nome = "Aparelhos" }); //Inserir Dados
    conn.Insert<Categoria>(new Categoria() { Nome = "Peças" }); //Inserir Dados

    conn.Update<Categoria>(new Categoria() { Id = 1, Nome = "Jogos Digitais" });//Atualizando 

    //conn.Delete<Categoria>(new Categoria() {Id = 5 });//Deletando pro Refeencia
}
[Table("[Aula].[dbo].[Categoria]")]//Mapeamento de Rotas para o Dapper entender a aplicação e o apontamento
public class Categoria
{
   // [Dapper.Contrib.Extensions.Key]// O Dapper na nova versão precisa desse Tipo antes do seus Anoteitions
    public int Id { get; set; }
    public string Nome { get; set; }
}

