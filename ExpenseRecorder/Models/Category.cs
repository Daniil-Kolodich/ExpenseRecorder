﻿using ExpenseRecorder.Models.Interfaces ;

namespace ExpenseRecorder.Models ;

public class Category : IEntity< Category >
{
	public string Name                      { get ; set ; } = string.Empty ;
	public string UserId                    { get ; set ; } = string.Empty ;
	public int    Id                        { get ; set ; } = 0 ;
	public void   CopyFrom(Category entity) { Name = entity.Name ; }
}
