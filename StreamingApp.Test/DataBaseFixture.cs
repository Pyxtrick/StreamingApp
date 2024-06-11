using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StreamingApp.DB;

namespace StreamingApp.Tests
{
	public abstract class DataBaseFixture : IDisposable
	{
		private const string ConnectionString = "DataSource=:memory:";

		private SqliteConnection _connection;

		~DataBaseFixture()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected UnitOfWorkContext CreateUnitOfWork()
		{
			if (_connection == null)
			{
				Initialize();
			}

			DbContextOptions<UnitOfWorkContext> options = new DbContextOptionsBuilder<UnitOfWorkContext>().UseSqlite(_connection).Options;
			UnitOfWorkContext context = new(options);
			context.Database.EnsureCreated();

			return context;
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_connection != null)
				{
					_connection.Close();
					_connection.Dispose();
				}
			}
		}

		private void Initialize()
		{
			_connection = new SqliteConnection(ConnectionString);
			_connection.Open();
		}
	}
}
