using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Managed.Data;
using System.Data;

namespace Managed.Service
{
    public class ObjectManager
    {
        public void CreateUser()
        {
            var context = new Context();
						var objDomain = new ObjectDomain(context);
            using (context.GetConnection(false))
            {
                context.BeginTransaction();
                try
                {
                    objDomain.CreateObject(context);
                    context.CommitTransaction();
                }
                catch (Exception)
                {
                    context.RollbackTransaction();
                }
            }
        }

        public void UpdateUser()
        {
            var context = new Context();
						var objDomain = new ObjectDomain(context);

            using (context.GetConnection(false))
            {
                context.BeginTransaction();
                try
                {
                    objDomain.UpdateObject(context);
                    context.CommitTransaction();
                }
                catch (Exception)
                {
                    context.RollbackTransaction();
                    throw;
                }
            }
        }

        public void DeleteUser()
        {
            var context = new Context();
						var objDomain = new ObjectDomain(context);

            using (context.GetConnection(false))
            {
                context.BeginTransaction();
                try
                {
                    objDomain.DeleteObject(context);
                    context.CommitTransaction();
                }
                catch (Exception)
                {
                    context.RollbackTransaction();
                }
            }
        }

        public Data.ObjectDomain SelectUserInformation(Int32 objectID)
        {
            var context = new Context();
						var objDomain = new ObjectDomain(context, objectID, true);
						return objDomain;
        }
    }
}
