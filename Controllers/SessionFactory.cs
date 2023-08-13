using NHibernate.Tool.hbm2ddl;
using NHibernate;
//using ISession = NHibernate.ISession;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;

namespace Todo_List_App.Controllers
{
    public class SessionFactory
    {
        public SessionFactory()
        {
            if (_sessionFactory is null)
            {
                _sessionFactory = BuildSessionFactory();
            }
        }

        public bool Commit(NHibernate.ISession session)
        {
            using var transaction = session.BeginTransaction();
            try
            {
                if (transaction.IsActive)
                {
                    transaction.Commit();
                }
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }
        }

        public NHibernate.ISession Session => _sessionFactory.OpenSession();

        public ISessionFactory _sessionFactory;
        private ISessionFactory BuildSessionFactory()
        {
            var configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(_connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<SessionFactory>())
                .ExposeConfiguration(BuildSchema);

            var sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory;
        }

        private void BuildSchema(Configuration config)
        {
            new SchemaExport(config).Create(false, false);
        }


        //private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\murphy.ochuba\source\repos\CCSARestaurantAPI\CCSARestaurant.DB\CCSARestaurantDB.mdf;Integrated Security=True;Connect Timeout=30";
        private readonly string _connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PROCUREMENT\Documents\TodoList.mdf;Integrated Security = True; Connect Timeout = 30";
    }
}