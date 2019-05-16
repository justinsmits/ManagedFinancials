using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Managed.Data
{
	public sealed class Config
	{

		public static Int32 NumberOfTriesOnDeadlock
		{
			get { return 3; }
		}

		public static Int32 ConnectTimeout
		{
			get { return 30; }
		}
		public static Int32 CommandTimeout
		{
			get { return 30; }
		}
	}
}
