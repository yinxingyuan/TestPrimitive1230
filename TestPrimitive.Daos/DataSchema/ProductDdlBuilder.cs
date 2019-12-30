using MetaShare.Common.Core.DataSchema;

namespace TestPrimitive.Daos.DataSchema
{
	public class ProductDdlBuilder : DdlBuilder
	{
		public override string GetSqlCreateTable()
		{
			return @"CREATE TABLE Product(Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,Sex bit,CreateTime datetime2,Price decimal(18,2),Description nvarchar(255),Owner_Id int,Entity_Status int)";
		}

		public override string GetSqlDropTable()
		{
			return @"DROP TABLE Product";
		}

		public override string GetSqlExistTable()
		{
			return @"SELECT COUNT(*) FROM Information_Schema.COLUMNS WHERE TABLE_NAME = 'Product'";
		}
	}
}
