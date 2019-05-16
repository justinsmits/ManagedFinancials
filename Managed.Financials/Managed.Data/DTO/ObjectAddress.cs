using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Managed.Data.DTO
{
    public class ObjectAddress
    {

			private Int32 _objectID;
			private Context _context;

        #region Constructors
			public ObjectAddress(Context context, Int32 objectID)
			{
				_objectID = objectID;
				_context = context;
				this.Select();
			}

			public ObjectAddress(Context context)
			{
				_context = context;
			}
        #endregion

        #region Properties
        private string _addressLineOne = string.Empty;
        public string AddressLineOne {
            get { return _addressLineOne; }
            set { _addressLineOne = value; }
        }

        private string _addressLineTwo = string.Empty;
        public string AddressLineTwo {
            get { return _addressLineTwo; }
            set { _addressLineTwo = value; } 
        }

        private string _city = string.Empty;
        public string City {
            get { return _city; }
            set { _city = value; } 
        }

        private string _state = string.Empty;
        public string State {
            get { return _state; }
            set { _state = value; } 
        }

        private string _zip = string.Empty;
        public string Zip {
            get { return _zip; }
            set { _zip = value; } 
        }

        private string _country = string.Empty;
        public string Country {
            get { return _country; }
            set { _country = value; } 
        }

			//Why is this a string? Should this be an Enum and Int db value?
        private string _addressType = string.Empty;
        public string AddressType {
            get { return _addressType; }
            set { _addressType = value; }
        }

        public SqlParameter[] CompleteObjectAddressAarray
        {
            get
            {
                return new List<SqlParameter>()
                {
                    new SqlParameter("@addressLine1", this._addressLineOne),
                    new SqlParameter("@addressLine2", this._addressLineTwo),
                    new SqlParameter("@city", this._city),
                    new SqlParameter("@state", this._state),
                    new SqlParameter("@zip", this._zip),
                    new SqlParameter("@country", this._country),
                    new SqlParameter("@addressType", this._addressType)
                }.ToArray();
            }
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        public Managed.Data.SqlPackage Insert()
        {
            var package = new SqlPackage();
            package.SqlText = "INSERT INTO DAL.ObjectAddress VALUES(@objectId,@addressLine1," + 
                                    "@addressLine2,@city,@state,@zip,@country,@addressType)";
            package.Params.AddRange(this.CompleteObjectAddressAarray);
            return package;
        }

        public Managed.Data.SqlPackage Update()
        {
            var package = new SqlPackage();
            package.SqlText = "UPDATE DAL.ObjectAddress SET addressLine1 = @addressLine1, " +
                                    "addressLine2 = @addressLine2, city = @city, state = @state, zip = @zip, " +
                                    "country = @country, addressType = @addressType WHERE objectId = @objectId";
            package.Params.AddRange(this.CompleteObjectAddressAarray);
            return package;
        }

        public Managed.Data.SqlPackage Delete()
        {
            var package = new SqlPackage();
            package.SqlText = "DELETE FROM DAL.ObjectAddress WHERE objectId = @objectId AND addressType = @addressType";
            package.Params.Add(new SqlParameter("@addressType", this._addressType));
            return package;
        }

				private void Select()
				{
					var package = new SqlPackage();
					package.SqlText = "SELECT * FROM DAL.OjbectAddress";
					SqlParameter objectIDParam = new SqlParameter("@ojbectID", System.Data.SqlDbType.Int);
					objectIDParam.Value = _objectID;
					package.Params.Add(objectIDParam);
					System.Data.DataRow dr = _context.ExecuteParameterizedSqlStatementAsDataTable(package.SqlText, package.Params).Rows[0];
					_addressLineOne = dr["AddressLine1"].ToString();
					_addressLineTwo = dr["AddressLine2"].ToString();
					_city = dr["City"].ToString();
					_state = dr["State"].ToString();
					_zip = dr["Zip"].ToString();
					_country = dr["Country"].ToString();
					_addressType = dr["AddressType"].ToString();
				}
    }
}
