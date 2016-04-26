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
        /// <summary>
        /// The connection string.
        /// </summary>
        public static readonly string ConnectionString = ConfigurationManager.AppSettings["MySqlConnString"];

        /// <summary>
        /// The target aoe2 path.
        /// </summary>
        public static readonly string TargetAoe2Path = ConfigurationManager.AppSettings["TargetAoe2Path"];

        /// <summary>
        /// The target aoe2 script.
        /// </summary>
        public static readonly string TargetAoe2Script = ConfigurationManager.AppSettings["TargetAoe2Path"];
	}
}
