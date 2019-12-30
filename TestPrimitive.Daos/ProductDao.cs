using System;
using System.Data;
using MetaShare.Common.Core.Daos;
using TestPrimitive.Daos.Interfaces;
using TestPrimitive.Entities;

namespace TestPrimitive.Daos
{
	public class ProductDao : CommonObjectDao<Product>, IProductDao
	{
		public class ProductSqlBuilder : ObjectSqlBuilder
		{
			public ProductSqlBuilder(SqlDialect sqlDialect) : base(sqlDialect,"Product")
			{
				this.SqlInsert = "INSERT INTO Product (Price,CreateTime,Sex," + this.SqlBaseFieldInsertFront + ") VALUES (@Price,@CreateTime,@Sex," + this.SqlBaseFieldInsertBack + ")";
				this.SqlUpdate = "UPDATE Product SET Price=@Price,CreateTime=@CreateTime,Sex=@Sex," + this.SqlBaseFieldUpdate + " WHERE Id=@Id";
			}
		}

		public class ProductResultHandler : CommonObjectResultHandler<Product>
		{
			public override void GetColumnValues(IDataReader reader, Product item)
			{
				base.GetColumnValues(reader, item);
				int ordinalSex = reader.GetOrdinal("Sex");
				item.Sex = !reader.IsDBNull(ordinalSex) && reader.GetBoolean(ordinalSex);
				int ordinalCreateTime = reader.GetOrdinal("CreateTime");
				item.CreateTime = reader.IsDBNull(ordinalCreateTime) ? DateTime.MinValue : reader.GetDateTime(ordinalCreateTime);
				int ordinalPrice = reader.GetOrdinal("Price");
				item.Price =  reader.IsDBNull(ordinalPrice) ? 0 : reader.GetDouble(ordinalPrice);
			}

			public override void AddInsertParameters(IContext context, IDbCommand command, Product item)
			{
				base.AddInsertParameters(context, command, item);
				context.AddParameter(command, "Sex", item.Sex);
				context.AddParameter(command, "CreateTime", item.CreateTime == DateTime.MinValue ? (object)DBNull.Value : item.CreateTime);
				context.AddParameter(command, "Price", item.Price);
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
