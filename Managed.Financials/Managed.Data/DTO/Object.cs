using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Managed.Data.DTO
{
    public class Object
    {
			private Context _context;
        #region Constructors
			public Object(Context context, Int32 objectId)
			{
				_context = context;
				_objectId = objectId;
				this.Select();
			}

			public Object(Context context)
			{
				_context = context;
			}
        #endregion

        #region Properties
        private int _objectId = 0;
        public int ObjectId {
            get { return _objectId; }
            set { _objectId = value; }
        }

        private string _firstName = string.Empty;
        public string FirstName {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string _middleName = string.Empty;
        public string MiddleName {
            get { return _middleName; }
            set { _middleName = value; } 
        }

        private string _lastName = string.Empty;
        public string LastName {
            get { return _lastName; }
            set { _lastName = value; } 
        }

        private DateTime _createdOn = DateTime.Today.Date;
        public DateTime CreatedOn {
            get { return _createdOn; }
            set { _createdOn = value; } 
        }

        private int _objectTypeId = 1;
        public int ObjectTypeId {
            get { return _objectTypeId; }
            set { _objectTypeId = value; }
        }

        private DateTime _lastEdited = DateTime.Today.Date;
        public DateTime LastEdited {
            get { return _lastEdited; }
            set { _lastEdited = value; }
        }

        private string _hash = string.Empty;
        public string Hash {
            get { return _hash; }
            set { _hash = value; } 
        }

				private string _salt = string.Empty;
				public string Salt
				{
					get { return _salt; }
					set { _salt = value; }
				}

        public SqlParameter[] CompleteObjectAarray
        {
            get
            {
                return new List<SqlParameter>()
                {
                    new SqlParameter("@firstName", _firstName),
                    new SqlParameter("@middleName", _middleName),
                    new SqlParameter("@lastName", _lastName),
                    new SqlParameter("@createdOn", _createdOn),
                    new SqlParameter("@objectTypeId", _objectTypeId),
                    new SqlParameter("@editedOn", _lastEdited),
                    new SqlParameter("@hash", _hash),
										new SqlParameter("@salt", _salt)
                }.ToArray();    
            }
        }
        #endregion

        #region Public Methods
        public Managed.Data.SqlPackage Insert()
        {
			Managed.Data.SqlPackage package = new SqlPackage();
			package.SqlText = "INSERT INTO DAL.Object VALUES(@firstName,@middleName,@lastName,@createdOn,@objectTypeId,@editedOn,@hash,@salt); SELECT Scope_Identity()";
			package.Params.AddRange(this.CompleteObjectAarray);
			return package;
        }

        internal Managed.Data.SqlPackage Update()
        {
			SqlPackage package = new SqlPackage();
			package.SqlText = "UPDATE DAL.Object SET firstName = @firstName, middleName = @middleName, lastName = @lastName, " +
                                                "createdOn = @createdOn, objectTypeId = @objectTypeId, editedOn = @editedOn " +
                                                "WHERE objectId = @objectId";
			package.Params.AddRange(this.CompleteObjectAarray);
            return package;
        }
        public Managed.Data.SqlPackage Delete()
        {
			var package = new SqlPackage();
			package.SqlText = "DELETE FROM DAL.Object WHERE objectId = @objectId";
			var objectIDParam = new SqlParameter("@objectId", System.Data.SqlDbType.Int);
			objectIDParam.Value = _objectId;
			package.Params.Add(objectIDParam);
			return package;
        }

        public void Select()
        {
            var package = new SqlPackage();
            package.SqlText = "SELECT * FROM DAL.Object";
						SqlParameter objectIDParm = new SqlParameter("@objectID", System.Data.SqlDbType.Int);
						objectIDParm.Value = _objectId;
						package.Params.Add(objectIDParm);
						DataRow dr = _context.ExecuteParameterizedSqlStatementAsDataTable(package.SqlText, package.Params).Rows[0];
						_objectId = (Int32)dr["ObjectID"];	
						_firstName = dr["FirstName"].ToString();
						_middleName = dr["MiddleName"].ToString();
						_lastName = dr["LastName"].ToString();
						_hash = dr["Hash"].ToString();
						_salt = dr["Salt"].ToString();
						_createdOn = (DateTime)dr["CreatedOn"];
						_lastEdited = (DateTime)dr["LastEdited"];
						_objectTypeId = (Int32)dr["ObjectTypeID"];
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
