using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlord
{
	public static class Configurations
	{
		public static readonly string ConnectionString = ConfigurationManager.AppSettings["MySqlConnString"];

		public static readonly string TargetAoe2Path = ConfigurationManager.AppSettings["TargetAoe2Path"];

		public static readonly string TargetAoe2Script = ConfigurationManager.AppSettings["TargetAoe2Path"];
	}
}
