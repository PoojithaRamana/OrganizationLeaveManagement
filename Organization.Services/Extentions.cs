using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizationLeaveManagement;
using Organizations.Models;
using PetaPoco;

namespace Organizations.Services
{
    public static class Extentions 
    {
        public static void AuditFieldsFillingOnCreating<T>(this OrganizationLeaveManagementDB.Record<T> rec) where T : new ()
        {
            dynamic record = (dynamic)rec;
            record.CreateDate = DateTime.Now;
            record.ModifiedDate = DateTime.Now;
            record.CreatedBy = "Poojitha";
            record.ModifiedBy = "Poojitha";
        }
    }
}
