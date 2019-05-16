using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Managed.Data.DTO
{
    public class ObjectPhone
    {
        #region Constructors
        #endregion

        #region Properties
        private string _phone = string.Empty;
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        private string _phoneType = string.Empty;
        public string PhoneType
        {
            get { return _phoneType; }
            set { _phoneType = value; }
        }

        private bool _primaryPhone = false;
        public bool PrimaryPhone
        {
            get { return _primaryPhone; }
            set { _primaryPhone = value; }
        }

        public SqlParameter[] CompleteObjectPhoneAarray
        {
            get
            {
                return new List<SqlParameter>()
                {
                    new SqlParameter("@phone", this._phone),
                    new SqlParameter("@phoneType", this._phoneType),
                    new SqlParameter("@phonePrimary", this._primaryPhone)
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
            package.SqlText = "UPDATE DAL.ObjectPhone SET phone = @phone, phoneType = @phoneType, phonePrimary = @phonePrimary " +
                                    "WHERE objectId = @objectId";
            package.Params.AddRange(this.CompleteObjectPhoneAarray);
            return package;
        }

        public SqlPackage Delete()
        {
            var package = new SqlPackage();
            package.SqlText = "DELETE FROM DAL.ObjectPhone WHERE objectId = @objectId AND phoneType = @phoneType";
            package.Params.Add(new SqlParameter("@phoneType", this._phoneType));
            return package;
        }

        public SqlPackage Insert()
        {
			var package = new SqlPackage();
            package.SqlText = "INSERT INTO DAL.ObjectPhone VALUES(@objectId,@phone,@phoneType,@phonePrimary)";
            package.Params.AddRange(this.CompleteObjectPhoneAarray);
            return package;
        }
    }
}
