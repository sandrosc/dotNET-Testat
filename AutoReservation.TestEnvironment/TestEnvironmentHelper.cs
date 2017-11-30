using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AutoReservation.TestEnvironment
{
    public static class TestEnvironmentHelper
    {
        public static void InitializeTestData()
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                var luxusklasseAutoTableName = context.GetTableName<LuxusklasseAuto>();
                var mittelklasseAutoTableName = context.GetTableName<MittelklasseAuto>();
                var standardAutoTableName = context.GetTableName<StandardAuto>();
                var autoTableName = context.GetTableName<Auto>();
                var kundeTableName = context.GetTableName<Kunde>();
                var reservationTableName = context.GetTableName<Reservation>();

                try
                {
                    // Delete all records from tables
                    //      > Cleanup for specific subtypes necessary when not using table per hierarchy (TPH)
                    //        since entities will be stored in different tables.
                    if (luxusklasseAutoTableName != autoTableName)
                    { 
                        context.DeleteAllRecords(luxusklasseAutoTableName); 
                    }
                    if (mittelklasseAutoTableName != autoTableName)
                    { 
                        context.DeleteAllRecords(mittelklasseAutoTableName); 
                    }
                    if (standardAutoTableName != autoTableName)
                    { 
                        context.DeleteAllRecords(standardAutoTableName); 
                    }
                    context.DeleteAllRecords(reservationTableName);
                    context.DeleteAllRecords(autoTableName);
                    context.DeleteAllRecords(kundeTableName);

                    // Reset the identity seed (Id's will start again from 1)
                    context.ResetEntitySeed(luxusklasseAutoTableName);
                    context.ResetEntitySeed(mittelklasseAutoTableName);
                    context.ResetEntitySeed(standardAutoTableName);
                    context.ResetEntitySeed(autoTableName);
                    context.ResetEntitySeed(kundeTableName);
                    context.ResetEntitySeed(reservationTableName);

                    // Temporarily allow insertion of identity columns (Id)
                    context.SetAutoIncrementOnTable(luxusklasseAutoTableName, true);
                    context.SetAutoIncrementOnTable(mittelklasseAutoTableName, true);
                    context.SetAutoIncrementOnTable(standardAutoTableName, true);
                    context.SetAutoIncrementOnTable(autoTableName, true);
                    context.SetAutoIncrementOnTable(kundeTableName, true);
                    context.SetAutoIncrementOnTable(reservationTableName, true);

                    // Insert test data
                    context.Autos.AddRange(Autos);
                    context.Kunden.AddRange(Kunden);
                    context.Reservationen.AddRange(Reservationen);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Error while re-initializing database entries.", ex);
                }
                finally
                {
                    // Disable insertion of identity columns (Id)
                    context.SetAutoIncrementOnTable(luxusklasseAutoTableName, false);
                    context.SetAutoIncrementOnTable(mittelklasseAutoTableName, false);
                    context.SetAutoIncrementOnTable(standardAutoTableName, false);
                    context.SetAutoIncrementOnTable(autoTableName, false);
                    context.SetAutoIncrementOnTable(kundeTableName, false);
                    context.SetAutoIncrementOnTable(reservationTableName, false);
                }
            }
        }

        private static List<Kunde> Kunden =>
            new List<Kunde>
            {
                new Kunde {Id = 1, Nachname = "Nass", Vorname = "Anna", Geburtsdatum = new DateTime(1981, 05, 05)},
                new Kunde {Id = 2, Nachname = "Beil", Vorname = "Timo", Geburtsdatum = new DateTime(1980, 09, 09)},
                new Kunde {Id = 3, Nachname = "Pfahl", Vorname = "Martha", Geburtsdatum = new DateTime(1990, 07, 03)},
                new Kunde {Id = 4, Nachname = "Zufall", Vorname = "Rainer", Geburtsdatum = new DateTime(1954, 11, 11)},
            };

        private static List<Auto> Autos =>
            new List<Auto>
            {
                new StandardAuto {Id = 1, Marke = "Fiat Punto", Tagestarif = 50},
                new MittelklasseAuto {Id = 2, Marke = "VW Golf", Tagestarif = 120},
                new LuxusklasseAuto {Id = 3, Marke = "Audi S6", Tagestarif = 180, Basistarif = 50},
            };

        private static List<Reservation> Reservationen =>
            new List<Reservation>
            {
                new Reservation { ReservationsNr = 1, AutoId = 1, KundeId = 1, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20)},
                new Reservation { ReservationsNr = 2, AutoId = 2, KundeId = 2, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20)},
                new Reservation { ReservationsNr = 3, AutoId = 3, KundeId = 3, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20)},
            };

        private static string GetTableName<T>(this DbContext context)
        {
            // See: https://lowrymedia.com/2014/06/10/ef6-1-mapping-between-types-tables-including-derived-types/
            var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;
            var objectItemCollection = (ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace);
            var entityTypeClr = metadata.GetItems<EntityType>(DataSpace.OSpace).Single(e => objectItemCollection.GetClrType(e) == typeof(T));
            var entityTypeCSpace = metadata.GetItems(DataSpace.CSpace).Where(x => x.BuiltInTypeKind == BuiltInTypeKind.EntityType).Cast<EntityType>().Single(x => x.Name == entityTypeClr.Name);
            var mappingsCSSpace = metadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace).Single().EntitySetMappings.ToList();

            EntitySet entitySet;
            var mapping = mappingsCSSpace.SingleOrDefault(x => x.EntitySet.Name == entityTypeCSpace.Name);
            if (mapping != null)
            {
                entitySet = mapping.EntityTypeMappings.Single().Fragments.Single().StoreEntitySet;
            }
            else
            {
                mapping = mappingsCSSpace.SingleOrDefault(x => x.EntityTypeMappings.Where(y => y.EntityType != null).Any(y => y.EntityType.Name == entityTypeCSpace.Name));
                if (mapping != null)
                {
                    entitySet = mapping.EntityTypeMappings.Where(x => x.EntityType != null).Single(x => x.EntityType.Name == entityTypeClr.Name).Fragments.Single().StoreEntitySet;
                }
                else
                {
                    var entitySetMapping = mappingsCSSpace.Single(x => x.EntityTypeMappings.Any(y => y.IsOfEntityTypes.Any(z => z.Name == entityTypeCSpace.Name)));
                    entitySet = entitySetMapping.EntityTypeMappings.First(x => x.IsOfEntityTypes.Any(y => y.Name == entityTypeCSpace.Name)).Fragments.Single().StoreEntitySet;
                }
            }

            string schema = (string)entitySet.MetadataProperties["Schema"].Value ?? entitySet.Schema;
            string table = (string)entitySet.MetadataProperties["Table"].Value ?? entitySet.Name;

            return $"[{schema}].[{table}]";
        }

        private static void DeleteAllRecords(this DbContext context, string table)
        {
            context.Database.ExecuteSqlCommand($"DELETE FROM {table}");
        }

        private static void ResetEntitySeed(this DbContext context, string table)
        {
            if (context.TableHasIdentityColumn(table))
            {
                context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('{table}', RESEED, 0)");
            }
        }

        private static void SetAutoIncrementOnTable(this DbContext context, string table, bool isAutoIncrementOn)
        {
            if (context.TableHasIdentityColumn(table))
            {
                context.Database.ExecuteSqlCommand($"SET IDENTITY_INSERT {table} {(isAutoIncrementOn ? "ON" : "OFF")}");
            }
        }

        private static bool TableHasIdentityColumn(this DbContext context, string table)
        {
            return context.Database.SqlQuery<int>($"SELECT OBJECTPROPERTY(OBJECT_ID('{table}'), 'TableHasIdentity')").Single() == 1;
        }

    }
}