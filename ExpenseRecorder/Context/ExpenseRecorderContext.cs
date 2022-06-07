using ExpenseRecorder.Models ;
using Microsoft.EntityFrameworkCore ;

namespace ExpenseRecorder.Context ;

public sealed class ExpenseRecorderContext : DbContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<Category> Categories { get; set; }
	public ExpenseRecorderContext(DbContextOptions< ExpenseRecorderContext > options)
		: base( options )
	{
//		Database.EnsureDeleted() ;
		Database.EnsureCreated() ;
	}
	
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if ( !optionsBuilder.IsConfigured )
		{
			optionsBuilder
			   .UseSqlServer( "Server=(localdb)\\mssqllocaldb;Database=expense_recorder_v1;Trusted_Connection=True;" ) ;
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity< User >()
					.HasKey( u => u.Id ) ;
		modelBuilder.Entity<User>().Property( u => u.Id )
					.ValueGeneratedOnAdd() ;
		modelBuilder.Entity<User>().Property( u => u.Name )
					.IsRequired() ;
		modelBuilder.Entity<User>().Property( u => u.Email )
					.IsRequired() ;
		modelBuilder.Entity<User>().Property( u => u.Password )
					.IsRequired() ;

		modelBuilder.Entity< Category >()
					.HasKey( u => u.Id ) ;
		modelBuilder.Entity< Category >().Property( c => c.Id ).ValueGeneratedOnAdd() ;
		modelBuilder.Entity< Category >().Property( c => c.Name ).IsRequired() ;
		modelBuilder.Entity< Category >().Property( c => c.UserId ).IsRequired() ;

		modelBuilder.Entity<Category>().HasOne( c => c.User ).WithMany( u => u.Categories ).HasForeignKey( c => c.UserId ) ;
	}
}
