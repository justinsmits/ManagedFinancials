using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed.Data
{
	public class SqlPackage
	{
		public string SqlText { get; set; }
		public List<System.Data.SqlClient.SqlParameter> Params { get; set; }

		public SqlPackage()
		{
			this.Params = new List<System.Data.SqlClient.SqlParameter>();
		}
		public static SqlPackage operator + (SqlPackage x, SqlPackage y)
		{
			SqlPackage s = new SqlPackage();
			s.Params.AddRange(x.Params);
			s.Params.AddRange(y.Params);
			s.SqlText = x.SqlText + System.Environment.NewLine + y.SqlText;
			return s;
		}
	}
}
