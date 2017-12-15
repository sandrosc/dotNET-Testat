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

        public virtual T Get(int id)
        {
            using (var context = new AutoReservationContext())
            {
                return context.Set<T>().Find(id);
            }
        }

        public virtual void Add(T entity)
        {
            using (var context = new AutoReservationContext())
            {
                context.Entry(entity).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public virtual void Update(T entity)
        {
            using (var context = new AutoReservationContext())
            {
                try
                {
                    context.Entry(entity).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateOptimisticConcurrencyException(context, entity);
                }
            }
        }

        public virtual void Remove(T entity)
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

        public virtual List<T> GetList()
        {
            using (var context = new AutoReservationContext())
            {
                return context.Set<T>().ToList();
            }
        }
    }
}