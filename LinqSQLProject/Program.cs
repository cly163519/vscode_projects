// using System.Data.SQLite;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

// See https://aka.ms/new-console-template for more information

using (SqliteConnection conn = new("Data Source = netball.sqlite")) {
	Console.WriteLine("Hello, World!");
	conn.Open();
	// var x = conn.CreateCommand();
	// x.CommandText = "CREATE TABLE testing (id INTEGER PRIMARY KEY, name TEXT)";
	// x.ExecuteNonQuery();
	
	var x = conn.CreateCommand();
	// x.CommandText = "INSERT INTO testing (name) VALUES ('hello world')";
	// x.ExecuteNonQuery();

	x.CommandText = "SELECT * FROM teams";
	var result = x.ExecuteReader();
	// Optional:
	var schema = result.GetColumnSchema();
	List<string> fields = new();
	// Console.WriteLine("Finding SQL result shape and types with SqliteConnection");
	foreach (var s in schema)
    {
    	// Console.Write(s.ColumnName);
		// Console.Write("\t");
		// Console.WriteLine(s.DataTypeName);
		fields.Add(s.ColumnName);
    }
	Console.WriteLine("Listing teams with SqliteConnection");
	Dictionary<string, object> rowMap = new();
	while (result.Read())
    {
        // Console.Write(result.GetInt64(0));     // Indexed columns in the order they appear
		// Console.Write("\t");                   // in the table, or in the list given by the
		// Console.WriteLine(result.GetString(1));// code above.
		// Can read it all into a Dictionary (HashMap) as strings:
		for (int i = 0; i < fields.Count; i++)
			rowMap[fields[i]] = result.GetString(i);
		Console.WriteLine(string.Join(", ", from entry in rowMap select entry.Key + " = " + entry.Value));
	}
}


using var db = new NetballContext();
Console.WriteLine("\n\nNow using LINQ instead:");
Console.WriteLine("\nListing teams, coaches, captains, and their positions with LINQ");

foreach (var row in from team in db.teams
                    where team.Coach.Name.CompareTo("R") > 0
                    select new { team.Name, Coach = team.Coach, Captain = team.Captain })
{
    Console.WriteLine($"Team {row.Name} is coached by {row.Coach.Name} and the captain is {row.Captain.Name} who plays {string.Join(", ", row.Captain.Positions)}");
}


















Console.WriteLine("\nBucketing players by height and displaying bucket sizes with LINQ");
foreach (var row in from x in db.players
					let tens = x.Height / 10
                    group x by tens into heightGroup
					where heightGroup.Key < 21
					orderby heightGroup.Key
                    select new
                    {
						Bucket = heightGroup.Key * 10 + "-" + (heightGroup.Key * 10 + 9),
                        Count = heightGroup.Count(),
						Average = heightGroup.Select(g => g.Height).Sum() / heightGroup.Count()
                    })
{
    Console.WriteLine(row);
}




















Console.WriteLine("\nFinding average heights by position with LINQ:");
foreach (var row in from playerpos in db.player_positions
				group playerpos.Player.Height by playerpos.Position into hg
				orderby hg.Average()
				select new {
					Position = hg.Key,
					Average = hg.Sum() / hg.Count(),
				}
				)
{
    Console.WriteLine(row);
}


public record Coach
{
    public int Id {get; set;}

	public required string Name {get; set;}
}

public record Player
{
    public int Id {get; set;}

	public required string Name {get; set;}

	public int Height {get; set;}

	public required string Hometown {get; set;}

	[Column("team")]
	public int TeamId {get; set;}

	public required virtual Team Team { get; set; }

	public virtual required ICollection<PlayerPosition> Positions {get; set;}
}


public class PlayerPosition
{
    [Column("player_id")]
    public int PlayerId { get; set; }

    [Column("position")]
    public string Position { get; set; } = string.Empty;

    public virtual Player Player { get; set; } = null!;

    public override string ToString()
    {
        return Position;
    }
}

public record Team
{
	public int Id {get; set;}

	public required string Name {get; set;}

	[Column("coach")]
	public int CoachId {get; set;}

	public required virtual Coach Coach {get; set;}

	[Column("captain")]
	public int CaptainId {get; set;}

	public required virtual Player Captain {get; set;}

	public virtual ICollection<Player> Players {get; set;} = new List<Player>();

    public override string ToString()
    {
		var captainName = Captain?.Name ?? "None";
    	var coachName = Coach?.Name ?? "None";
	    return $"Team( Name = {Name}, Captain = {captainName}, Coach = {coachName}, {Players?.Count ?? 0} players)";
    }
}

class NetballContext : DbContext
{
    public DbSet<Team> teams { get; set; }

	public DbSet<Coach> coaches {get; set;}

	public DbSet<Player> players {get; set;}

	public DbSet<PlayerPosition> player_positions {get; set;}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=netball.sqlite")
			.UseLazyLoadingProxies();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>()
            .HasOne(t => t.Coach)
            .WithMany()
            .HasForeignKey(t => t.CoachId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Player>()
            .HasOne(p => p.Team)
            .WithMany()
            .HasForeignKey(p => p.TeamId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Team>()
            .HasOne(t => t.Captain)
            .WithOne()
            .HasForeignKey<Team>(t => t.CaptainId)
            .OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<Team>()
			.HasMany(t => t.Players)
			.WithOne(p => p.Team)
			.HasForeignKey(p => p.TeamId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<PlayerPosition>()
            .HasKey(pp => new { pp.PlayerId, pp.Position });

        modelBuilder.Entity<Player>()
            .HasMany(p => p.Positions)
            .WithOne(pp => pp.Player)
            .HasForeignKey(pp => pp.PlayerId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}