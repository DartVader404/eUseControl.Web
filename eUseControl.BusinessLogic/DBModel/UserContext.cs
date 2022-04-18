using System;
using System.Data.Entity;
using eUseControl.Domain.Entities.User;


namespace eUseControl.BusinessLogic.DBModel
{
    public class UserContext : DbContext
    {
        public UserContext() :
            base("name=eUseControl")
        {

        }

        public virtual DbSet<UDbTable> Users { get; set; }
    }
}
