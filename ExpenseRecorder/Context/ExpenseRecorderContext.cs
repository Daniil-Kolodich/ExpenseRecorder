using ExpenseRecorder.Models ;
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

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if ( !optionsBuilder.IsConfigured )
			optionsBuilder
			   .UseSqlServer( "Server=(localdb)\\mssqllocaldb;Database=expense_recorder_v1;Trusted_Connection=True;" ) ;

		optionsBuilder.EnableSensitiveDataLogging() ;
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating( modelBuilder ) ;

		modelBuilder.Entity< Category >().HasKey( u => u.Id ) ;
		modelBuilder.Entity< Category >().Property( c => c.Id ).ValueGeneratedOnAdd() ;
		modelBuilder.Entity< Category >().Property( c => c.Name ).IsRequired() ;
		modelBuilder.Entity< Category >().Property( c => c.UserId ).IsRequired() ;
	}
}
