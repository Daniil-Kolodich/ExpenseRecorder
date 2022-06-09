using System.Runtime.CompilerServices ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Models.Configurations ;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore ;
using Microsoft.EntityFrameworkCore ;

namespace ExpenseRecorder.Context ;

public sealed class ExpenseRecorderContext : IdentityDbContext< User >
{
	public ExpenseRecorderContext(DbContextOptions< ExpenseRecorderContext > options)
		: base( options )
	{
//		Database.EnsureDeleted() ;
		Database.EnsureCreated() ;
	}

	public DbSet< Category > Categories { get ; set ; } = default! ;
	public DbSet<Transaction> Transactions { get; set; } = default!;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if ( !optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=expense_recorder_v1;Trusted_Connection=True;") ;
		}
		optionsBuilder.EnableSensitiveDataLogging() ;
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating( modelBuilder ) ;
		modelBuilder.ApplyConfiguration( new CategoryConfiguration() ) ;
		modelBuilder.ApplyConfiguration( new TransactionConfiguration() ) ;
	}
}
