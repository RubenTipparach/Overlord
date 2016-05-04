using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlord.Models
{
	/// <summary>
	/// This stores the game state from the MySql Server Point of View.
	/// </summary>
	public class GameData
	{
		/// <summary>
		/// The game identifier.
		/// </summary>
		private int _gameId;

		/// <summary>
		/// The is ready flag.
		/// </summary>
		private bool _isReady;

		/// <summary>
		/// The date game was played. (It's actually when the data was entered.)
		/// </summary>
		private DateTime _dateGamePlayed;

		/// <summary>
		/// Initializes a new instance of the <see cref="GameData"/> class.
		/// </summary>
		/// <param name="gameId">The game identifier.</param>
		/// <param name="isReady">if set to <c>true</c> [is ready].</param>
		/// <param name="dateGamePlayed">The date game played.</param>
		public GameData(int gameId, bool isReady, DateTime dateGamePlayed)
		{
			_gameId = gameId;
			_isReady = isReady;
			_dateGamePlayed = dateGamePlayed;
		}

		/// <summary>
		/// Gets the date game played.
		/// </summary>
		/// <value>
		/// The date game played.
		/// </value>
		public DateTime DateGamePlayed
		{
			get { return _dateGamePlayed; }
		}

		/// <summary>
		/// Gets the game identifier.
		/// </summary>
		/// <value>
		/// The game identifier.
		/// </value>
		public int GameId
		{
			get { return _gameId; }
		}
		/// <summary>
		/// Gets a value indicating whether this instance is ready.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is ready; otherwise, <c>false</c>.
		/// </value>
		public bool IsReady
		{
			get { return _isReady; }
		}
	}
}
