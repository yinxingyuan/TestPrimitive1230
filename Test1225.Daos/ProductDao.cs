using System;
using System.Data;
using MetaShare.Common.Core.Daos;
using Test1225.Daos.Interfaces;
using Test1225.Entities;

namespace Test1225.Daos
{
	public class ProductDao : CommonObjectDao<Product>, IProductDao
	{
		public class ProductSqlBuilder : ObjectSqlBuilder
		{
			public ProductSqlBuilder(SqlDialect sqlDialect) : base(sqlDialect,"Product")
			{
				this.SqlInsert = "INSERT INTO Product (" + this.SqlBaseFieldInsertFront + ") VALUES (" + this.SqlBaseFieldInsertBack + ")";
				this.SqlUpdate = "UPDATE Product SET " + this.SqlBaseFieldUpdate + " WHERE Id=@Id";
			}
		}

		public class ProductResultHandler : CommonObjectResultHandler<Product>
		{
			public override void GetColumnValues(IDataReader reader, Product item)
			{
				base.GetColumnValues(reader, item);
			}

			public override void AddInsertParameters(IContext context, IDbCommand command, Product item)
			{
				base.AddInsertParameters(context, command, item);
			}
		}

		public ProductDao(SqlDialect sqlDialect) : base(new ProductSqlBuilder(sqlDialect), new ProductResultHandler())
		{
		}

		public ProductDao(SqlDialect sqlDialect, string schemaConnectionString) : base(new ProductSqlBuilder(sqlDialect), new ProductResultHandler(), schemaConnectionString)
		{
		}
	}
}
