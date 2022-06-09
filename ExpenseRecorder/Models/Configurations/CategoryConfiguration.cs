using Microsoft.EntityFrameworkCore ;
using Microsoft.EntityFrameworkCore.Metadata.Builders ;

namespace ExpenseRecorder.Models.Configurations ;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
	private const string CategoryDefaultColor = "#FFFFFF" ;

	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder.HasKey( u => u.Id ) ;
		builder.Property( c => c.Id ).IsRequired(  ).ValueGeneratedOnAdd() ;
		builder.Property( c => c.Name ).IsRequired() ;
		builder.Property( c => c.UserId ).IsRequired() ;
		builder.Property( c => c.Icon ).IsRequired() ;
		builder.Property( c => c.Color ).IsRequired().HasDefaultValue( CategoryDefaultColor ) ;
	}
}
