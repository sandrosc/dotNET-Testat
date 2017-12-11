using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal;

namespace AutoReservation.BusinessLayer
{
    public abstract class ManagerBase<T>
        where T : class
    {
        protected static OptimisticConcurrencyException<T> CreateOptimisticConcurrencyException<T>(AutoReservationContext context, T entity)
            where T : class
        {
            var dbEntity = (T)context.Entry(entity)
                .GetDatabaseValues()
                .ToObject();

            return new OptimisticConcurrencyException<T>($"Update {typeof(T).Name}: Concurrency-Fehler", dbEntity);
        }

        public T Get(int id)
        {
            using (var context = new AutoReservationContext())
            {
                return context.Set<T>().Find(id);
            }
        }

        public void Add(T entity)
        {
            using (var context = new AutoReservationContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }

        public void Update(T entity)
        {
            using (var context = new AutoReservationContext())
            {
                try
                {
                    context.Set<T>().Attach(entity);
                    var entry = context.Entry(entity);
                    entry.State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateOptimisticConcurrencyException(context, entity);
                }
            }
        }

        public void Remove(T entity)
        {
            using (var context = new AutoReservationContext())
            {
                try
                {
                    context.Set<T>().Remove(entity);
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateOptimisticConcurrencyException(context, entity);
                }
            }
        }

        protected List<T> GetList()
        {
            using (var context = new AutoReservationContext())
            {
                return context.Set<T>().ToList();
            }
        }
    }
}