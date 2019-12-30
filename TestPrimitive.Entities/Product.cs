using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MetaShare.Common.Core.Entities;

namespace TestPrimitive.Entities
{
	public class Product : MetaShare.Common.Core.Entities.Common
	{
		[DataMember]
		public bool Sex {get; set;}

		[DataMember]
		public DateTime CreateTime {get; set;}

		[DataMember]
		public double Price {get; set;}

	}
}
