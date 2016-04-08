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
	/// <summary>
	/// This training module serves as an automated program to collect, analyze data,
	/// and improve the AI knowledge base. Players should find a statistical edge when
	/// testing this AI, as the AI will learn from its mistakes and generate a new script
	/// after every game. Getting slightly better...
	/// 
	/// Should also include a randomizer mode so we can autonomously gather data after every
	/// round. This is still a todo kinda thing...
	/// </summary>
	public class AiTrainingModule
	{
		private string _aoe2Directory;
		private string _aiScript;

		private List<CoastalRaidersFuedalResourceManager> _rawMgxStats;

		private CoastalRaidersFuedalResourceManager _currentStats;

		public AiTrainingModule(string aoe2Directory, string aiScript)
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
