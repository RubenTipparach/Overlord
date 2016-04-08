using NeuronDotNet.Core;
using NeuronDotNet.Core.Backpropagation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnneysEmpire.Learning
{
	public class AiTrainingModule
	{
		private String _aoe2Directory;
		private String _aiScript;

		private List<CoastalRaidersFuedalResourceManager> _rawMgxStats;

		private CoastalRaidersFuedalResourceManager _currentStats;

		public AiTrainingModule(String aoe2Directory, String aiScript)
		{
			_aoe2Directory = aoe2Directory;
			_aiScript = aiScript;

			_rawMgxStats = new List<CoastalRaidersFuedalResourceManager>();
		}

		public void PullStats()
		{

		}

		public void PushNewTrainingSet()
		{

		}
	
	}
}
