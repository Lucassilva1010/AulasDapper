
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;

#region "Apresentação com o Dapper"
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
#endregion

#region "CRUD com o Dapper"
//Dapper Contrib
//using (var conn = new SqlConnection($@"Server=(localdb)\MSSQLLocalDB;Database=Aula;"))// Criamos aqui uma porta para conecxão do SQL
//{
//    var categorias = conn.GetAll<Categoria>();// Listar
//    var categoria = conn.Get<Categoria>(1);// Selecionar


//    conn.Insert<Categoria>(new Categoria() { Nome = "Aparelhos" }); //Inserir Dados
//    conn.Insert<Categoria>(new Categoria() { Nome = "Peças" }); //Inserir Dados

//    conn.Update<Categoria>(new Categoria() { Id = 1, Nome = "Jogos Digitais" });//Atualizando 

//    //conn.Delete<Categoria>(new Categoria() {Id = 5 });//Deletando pro Refeencia
//}
//[Table("[Aula].[dbo].[Categoria]")]//Mapeamento de Rotas para o Dapper entender a aplicação e o apontamento
//public class Categoria
//{
//   // [Dapper.Contrib.Extensions.Key]// O Dapper na nova versão precisa desse Tipo antes do seus Anoteitions
//    public int Id { get; set; }
//    public string Nome { get; set; }
//}

#endregion

#region "Muliplas execuções"
//using (var conn = new SqlConnection($@"Server=(localdb)\MSSQLLocalDB;Database=AulaDapper;"))// Criamos aqui uma porta para conecxão do SQL
//{
//    string query = @"insert into AulaDapper.dbo.Categoria (Nome) values (@nome)";// Isso vai Inserir dados em uma nova tabela
//                                                                                 //DynamicParameters parameters = new DynamicParameters();//Isso server para ler e gravar os dados do Usuario
//parameters.Add("nome", "Antonella");// Adicionando na tabela


//Essa é uma forma de executar os comando anteriores e tirar a responsabilidade do Dapper de
//manipular os dados de forma indireta, como no ultimo comando: System.Data.Commandtype.text
//int linhasAfetadas = conn.Execute(query, parameters, commandType: System.Data.CommandType.Text);

//if (linhasAfetadas == 0)
//{
//    Console.WriteLine("Falhou!");
//}
//else
//{
//    Console.WriteLine($"Quantidade de Linhas Afetadas: {linhasAfetadas}");
//}



//    int linhasAfetadas2 = conn.Execute(query,
//        new[]
//        {
//            new { nome = "Anastacia"},
//            new { nome = "Arlina"},
//            new { nome = "Marina"},
//            new { nome = "Deus é fiel"},

//        });

//    if (linhasAfetadas2 == 0)
//    {
//        Console.WriteLine("Falhou!");
//    }
//    else
//    {
//        Console.WriteLine($"Quantidade de Linhas Afetadas: {linhasAfetadas2}");
//    }

//}

#endregion

#region "Bucando por Objeto e apresentando na tela"
//using (var conn = new SqlConnection($@"Server=(localdb)\MSSQLLocalDB;Database=AulaDapper;"))// Criamos aqui uma porta para conecxão do SQL
//{
//    string query = @"SELECT * FROM [AulaDapper].[dbo].[Categoria]";//Buscando as informações nessa tabela 

//    //var info = conn.Query(query);//aqui ele passa todos os dados da tabela, porém com o nome DapperRow
//    var info = conn.Query<Categoria>(query);//Aqui ele se transforma em um tipo/Objeto categoria
//    Console.WriteLine("Resultados vindo do banco!");
//    foreach (var item in info)
//    {
//        //Console.WriteLine(item);//Dessa forma ele apresenta o nome DapperRoww
//        Console.WriteLine(item.Id + " - " + item.Nome);//Dessa forma ele apresenta o nome DapperRoww
//    }
//    Console.WriteLine();
//    var info2 = conn.QueryFirst<Categoria>(query);//Aqui ele se transforma em um tipo/Objeto categoria
//    Console.WriteLine("Apresentando o Primeiro valor");
//        Console.WriteLine("+ "+info2.Id + " - " +info2.Nome+" +");//Dessa forma ele apresenta O primeiro valor da tabela
   
    
//    Console.WriteLine();

//    string query2 = @"SELECT * FROM [AulaDapper].[dbo].[Categoria] where id = 3";//Buscando as informações a partid o ID 
//    Console.WriteLine("Listagem Personalizada");
//    var info3 = conn.QueryFirst<Categoria>(query2);//Aqui ele se transforma em um tipo/Objeto categoria, e carrega em uma variavel
//    Console.WriteLine("# "+info3.Id + " - " +info3.Nome+" #");//Apresentando o valor do Id da busca


//}

//class Categoria
//{
//    public int Id { get; set; }
//    public string Nome { get; set; }
//}

#endregion

#region "Multiplas buscas por Objeto e apresentando na tela"
using (var conn = new SqlConnection($@"Server=(localdb)\MSSQLLocalDB;Database=AulaDapper;"))// Criamos aqui uma porta para conecxão do SQL
{
    string query2 = @"
                SELECT * FROM [AulaDapper].[dbo].[Categoria];
                SELECT * FROM [AulaDapper].[dbo].[Categoria] where id = 3
                ";//Buscando as informações a partid o ID 


    using (var multi = conn.QueryMultiple(query2, commandType: System.Data.CommandType.Text)) {
        var resultado1 = multi.Read<Categoria>();
        var resultado2 = multi.ReadFirst<Categoria>();
        
        foreach (var item in resultado1)
        {
            Console.WriteLine($"{item.Id} - {item.Nome}");
        }
        Console.WriteLine();
        Console.WriteLine("Resultado Unico");
        Console.WriteLine($"{resultado2.Id} - {resultado2.Nome}");
    } 

}



class Categoria
{
    public int Id { get; set; }
    public string Nome { get; set; }
}

#endregion