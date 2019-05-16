using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Managed.Data
{
	public class ObjectDomain
	{
		private Int32 _objectID;
		private Context _context;
		#region Constructors
		public ObjectDomain(Context context, Int32 objectID, Boolean selectComplete)
		{
			_context = context;
			_objectID = objectID;
			if (selectComplete)
				this.SelectCompleteObject();
			else
				this.Select();
		}
		public ObjectDomain(Context context)
		{
			_context = context;
		}
		#endregion

		#region Properties
		public DTO.Object _object;
		private DTO.ObjectAddress _objectAddress;
		private DTO.ObjectEmail _objectEmail;
		private DTO.ObjectPhone _objectPhone;
		#endregion

		#region Public Methods

		public Int32 CreateObject(Context context)
		{
			var objpackage = _object.Insert();
			_object.ObjectId = Convert.ToInt32(context.ExecuteSqlStatementAsScalar(objpackage.SqlText, objpackage.Params));

			var domainPackage = _objectAddress.Insert() + _objectEmail.Insert() + _objectPhone.Insert();
			var objectIdParam = new SqlParameter("@objectId", System.Data.SqlDbType.Int);
			objectIdParam.Value = _object.ObjectId;
			domainPackage.Params.Add(objectIdParam);

			context.ExecuteParameterizedNonQuerySQLStatement(domainPackage.SqlText, domainPackage.Params);
			return _object.ObjectId;
		}

		public void UpdateObject(Context context)
		{
			var objPackage = _object.Update();
			context.ExecuteParameterizedNonQuerySQLStatement(objPackage.SqlText, objPackage.Params);
		}

		public void DeleteObject(Context context)
		{
			var objPackage = _object.Delete();
			context.ExecuteParameterizedNonQuerySQLStatement(objPackage.SqlText, objPackage.Params);
		}

		public void SelectCompleteObject()
		{
			_objectAddress = new DTO.ObjectAddress(_context, _objectID);
		}

		private void Select()
		{
			_object = new DTO.Object(_context, _objectID);
		}

		#endregion

		#region Private Methods
		#endregion
	}
}
