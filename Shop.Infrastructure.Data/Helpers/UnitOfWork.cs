using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Shop.Domain.Core;
using Shop.Domain.Interfaces;
using Shop.Infrastructure.Data.MappingOverrides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Data.Helpers
{
  public class UnitOfWork : IUnitOfWork
  {
    private static readonly ISessionFactory _sessionFactory;
    private ITransaction _transaction;

    public ISession Session { get; set; }

    static UnitOfWork()
    {
      _sessionFactory = Fluently.Configure()
          .Database(MsSqlConfiguration.MsSql2008.ConnectionString(x => x.FromConnectionStringWithKey("UnitOfWorkExample")))
          .Mappings(x => x.AutoMappings.Add(
              AutoMap.AssemblyOf<User>(new AutomappingConfiguration()).UseOverridesFromAssemblyOf<UserOverrides>()))
          .ExposeConfiguration(config => new SchemaUpdate(config).Execute(false, true))
          .BuildSessionFactory();
    }

    public UnitOfWork()
    {
      Session = _sessionFactory.OpenSession();
    }

    public void BeginTransaction()
    {
      _transaction = Session.BeginTransaction();
    }

    public void Commit()
    {
      try
      {
        if (_transaction != null && _transaction.IsActive)
          _transaction.Commit();
      }
      catch
      {
        if (_transaction != null && _transaction.IsActive)
          _transaction.Rollback();

        throw;
      }
      finally
      {
        Session.Dispose();
      }
    }

    public void Rollback()
    {
      try
      {
        if (_transaction != null && _transaction.IsActive)
          _transaction.Rollback();
      }
      finally
      {
        Session.Dispose();
      }
    }
  }
}
