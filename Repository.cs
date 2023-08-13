using Todo_List_App.Controllers;
using NHibernate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo_List_App
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        public Repository(SessionFactory sessionFactory) => _session = (NHibernate.ISession?)sessionFactory.Session;
        public void Add(T entity)
        {
            _session?.Save(entity);
            Commit();
        }

        public void Delete(T entity)
        {
            _session?.Delete(entity);
            Commit();
        }

        public void DeleteById(int id)
        {
            var entity = GetById(id);
            _session.Delete(entity);
            Commit();
        }

        public List<T> GetAll()
        {
            var entities = _session?.Query<T>().ToList();
            return entities;
        }

        public T GetById(int id)
        {
            var entity = _session?.Query<T>().FirstOrDefault(x => x.Id == id);
            return entity;
        }

        public void Update(T entity)
        {
            _session?.Update(entity);
            Commit();
        }

        public bool Commit()
        {
            using var transaction = _session.BeginTransaction();
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

        protected readonly NHibernate.ISession _session;
    }
}
