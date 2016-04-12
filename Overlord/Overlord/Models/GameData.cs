using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlord.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class GameData
	{
		private int _gameId;
		
		private bool _isReady;

		private DateTime _dateGamePlayed;

		public GameData(int gameId, bool isReady, DateTime dateGamePlayed)
		{
			_gameId = gameId;
			_isReady = isReady;
			_dateGamePlayed = dateGamePlayed;
		}

		public DateTime DateGamePlayed
		{
			get { return _dateGamePlayed; }
		}

		public int GameId
		{
			get { return _gameId; }
		}
		public bool IsReady
		{
			get { return _isReady; }
		}
	}
}
