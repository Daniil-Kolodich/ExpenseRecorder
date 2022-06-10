using Microsoft.EntityFrameworkCore ;
using Microsoft.EntityFrameworkCore.Metadata.Builders ;

namespace ExpenseRecorder.Models.Configurations ;

public class TransactionConfiguration : IEntityTypeConfiguration< Transaction >
{
	public void Configure(EntityTypeBuilder< Transaction > builder)
	{
		builder.HasKey( t => t.Id ) ;
		builder.Property( t => t.Id ).ValueGeneratedOnAdd() ;
		builder.Property( t => t.Amount ).IsRequired() ;
		builder.Property( t => t.Date ).IsRequired() ;
		builder.Property( t => t.Type ).IsRequired() ;
		builder.Property( t => t.CategoryId ).IsRequired() ;
		builder.Property( t => t.PaymentAccountId ).IsRequired() ;
		builder.Property( t => t.UserId ).IsRequired() ;

		builder.HasOne( t => t.Category ).WithMany( c => c.Transactions ).HasForeignKey( t => t.CategoryId )
			   .OnDelete( DeleteBehavior.Restrict ) ;
		
		builder.HasOne( t => t.PaymentAccount ).WithMany( c => c.Transactions ).HasForeignKey( t => t.PaymentAccountId )
			   .OnDelete( DeleteBehavior.Restrict ) ;
	}
}

/*
		public int Id { get ; set ; }
	public string? Description { get ; set ; }
	public decimal Amount { get ; set ; }
	public int CategoryId { get ; set ; }
	public Category Category { get ; set ; } = null! ;
	public DateTime Date { get ; set ; }
	public string UserId { get ; set ; }


 */
