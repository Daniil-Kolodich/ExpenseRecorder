using Microsoft.EntityFrameworkCore ;
using Microsoft.EntityFrameworkCore.Metadata.Builders ;

namespace ExpenseRecorder.Models.Configurations ;

public class PaymentAccountConfiguration : IEntityTypeConfiguration< PaymentAccount >
{
	public void Configure(EntityTypeBuilder< PaymentAccount > builder)
	{
		builder.HasKey( u => u.Id ) ;
		builder.Property( c => c.Id ).IsRequired().ValueGeneratedOnAdd() ;
		builder.Property( c => c.UserId ).IsRequired() ;
		
		builder.Property( c => c.Name ).IsRequired() ;
		builder.Property( c => c.Balance ).IsRequired() ;
		builder.Property( c => c.Currency ).IsRequired() ;
	}
}
