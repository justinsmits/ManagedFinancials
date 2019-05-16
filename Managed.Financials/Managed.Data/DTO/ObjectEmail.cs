using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Managed.Data.DTO
{
    public class ObjectEmail
    {
        #region Constructors
        #endregion

        #region Properties
        private string _email = string.Empty;
        public string Email {
            get { return _email; }
            set { _email = value; } 
        }

        private string _emailType = string.Empty;
        public string EmailType {
            get { return _emailType; }
            set { _emailType = value; } 
        }

        private bool _primaryEmail = false;
        public bool PrimaryEmail {
            get { return _primaryEmail; }
            set { _primaryEmail = value; } 
        }

        public SqlParameter[] CompleteObjectEmailAarray
        {
            get
            {
                return new List<SqlParameter>()
                {
                    new SqlParameter("@email", this._email),
                    new SqlParameter("@emailType", this._emailType),
                    new SqlParameter("@primaryEmail", this._primaryEmail)
                }.ToArray();
            }
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        public SqlPackage Update()
        {
            var package = new SqlPackage();
            package.SqlText = "UPDATE DAL.ObjectEmail SET email = @email,emailType = @emailType,primaryEmail = @primaryEmail " +
                                    "WHERE objectId = @objectId";
            package.Params.AddRange(this.CompleteObjectEmailAarray);
            return package;
        }

        public SqlPackage Insert()
        {
            var package = new SqlPackage();
            package.SqlText = "INSERT INTO DAL.ObjectEmail VALUES(@objectId,@email,@emailType,@primaryEmail)";
            package.Params.AddRange(this.CompleteObjectEmailAarray);
            return package;
        }

        public SqlPackage Delete()
        {
            var package = new SqlPackage();
            package.SqlText = "DELETE FROM DAL.ObjectEmail WHERE objectId = @objectId AND emailType = @emailType";
            package.Params.Add(new SqlParameter("@emailType", this._emailType));
            return package;
        }
    }
}
