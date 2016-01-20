using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FFSolution.Models
{
    public partial class FFAdminDBEntities : DbContext
    {
        //Zaatar
        public async override Task<int> SaveChangesAsync()
        {
            try
            {
                foreach (var ent in this.ChangeTracker.Entries())
                {
                    if (ent.State == EntityState.Added || ent.State == EntityState.Modified)
                    {
                        Commons.Helpers.UpdateUserAndDate(ent.Entity);
                    }
                }
                return await base.SaveChangesAsync();

            }
            catch (DbEntityValidationException e)
            {
                StringBuilder sb = new StringBuilder();
                
                foreach (var eve in e.EntityValidationErrors)
                {
                   sb.AppendLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:" +
                        eve.Entry.Entity.GetType().Name + eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        sb.AppendLine("- Property: \"{0}\", Error: \"{1}\"" +
                            ve.PropertyName +  ve.ErrorMessage);
                    }
                }
                File.WriteAllText("C:\\error.txt",sb.ToString());
                throw;
            }

        }

        public override int SaveChanges()
        {
            try
            {
                foreach (var ent in this.ChangeTracker.Entries())
                {

                    if (ent.State == EntityState.Added || ent.State == EntityState.Modified)
                    {
                        Commons.Helpers.UpdateUserAndDate(ent.Entity);

                    }
                }
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var eve in e.EntityValidationErrors)
                {
                    sb.AppendLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:" +
                         eve.Entry.Entity.GetType().Name + eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        sb.AppendLine("- Property: \"{0}\", Error: \"{1}\"" +
                            ve.PropertyName + ve.ErrorMessage);
                    }
                }
                File.WriteAllText("C:\\error.txt", sb.ToString());
                throw;
            }
        }
        //End Zaatar
    }
}