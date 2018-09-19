using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Organizations.Models;
using PetaPoco;

namespace Organizations.Services
{  
    public class OrganizationService : IOrganizationService
    {
        private object orgMapper;

        public Database DB { get; set; }
        public OrganizationService(Database db)
        {
            this.DB = db;
        }
        public Organization GetOrganization()
        {     
            var organizationTable = this.DB.SingleOrDefault<OrganizationLeaveManagement.Organization>("");
            Organization organization = AutoMapper.mapper.Map<OrganizationLeaveManagement.Organization, Organization>(organizationTable);
            return (organizationTable == null) ? null : organization;                    
        }

        public void CreateOrganization(Organization organization)
        {                 
            OrganizationLeaveManagement.Organization organizationDB = AutoMapper.mapper.Map<Organization, OrganizationLeaveManagement.Organization>(organization);
            organizationDB.AuditFieldsFillingOnCreating();
            this.DB.Insert(organizationDB);           
        }
    }
}
