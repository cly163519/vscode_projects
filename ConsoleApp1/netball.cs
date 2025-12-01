using Microsoft.Data.Sqlite;
using (SqliteConnection conn = new("Data Source = netball.sqlite")) {
   conn.Open();
   var cmd = conn.CreateCommand();
   cmd.CommandText = "SELECT * FROM teams";
   var result = x.ExecuteReader();
   while (result.Read())
   {
           Console.Write(result.GetInt64(0));      // Indexed columns (id)
      Console.Write("\t");
      Console.WriteLine(result.GetString(1)); // Indexed columns (name)
   }
}