﻿using AnneysEmpire.Learning;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AnneysEmpire.Models;

namespace AnneysEmpire
{
    /// <summary>
    /// The purpose of this method is to poll the game's recorded save files,
    /// to see if a game is finished. OR if we are just starting our first game.
    /// The engine detects that it has to regenerate the AI script based of the
    /// neural network's predictions. The engine is designed to manage interactions
    /// between the parts of this program to either train an AI and make a prediction,
    /// grab or write data to the db, or generate a script with the new values to make
    /// an attempt at winning.
    /// </summary>
    public class AILearningEngine
    {
        /// <summary>
        /// 
        /// </summary>
        private bool _isRunning;

        /// <summary>
        /// 
        /// </summary>
        private bool _networkProcessed;

        /// <summary>
        /// This tells us what state the entire game is in.
        /// </summary>
        private GameState _currentGameSate;

		/// <summary>
		/// This variable tells us what the most recent game was played.
		/// We'll update the is processed flag after the program regenerates itself.
		/// The newest and freshest data should have a record of this GameData row.
		/// </summary>
		private GameData _recentGamePlayed;

        /// <summary>
        /// 
        /// </summary>
        private AiTrainingModule _trainingModule;

        /// <summary>
        /// Referenced instance of the main static logger. This is basically just some
        /// simple logging mechanism.
        /// </summary>
        private Logger _logger;

        /// <summary>
        /// 
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
        }
        
        /// <summary>
        /// Construct some stuff.
        /// </summary>
        public AILearningEngine(string scriptDir, string scriptName)
        {
            _isRunning = true;
            _networkProcessed = false;
            _currentGameSate = GameState.GameNotStarted;
            _logger = Program.Logger;

            // TODO: try other combinations of networks and neurons later.
            _trainingModule = new AiTrainingModule(scriptDir, scriptName);
        }

        /// <summary>
        /// Keep running in a loop.
        /// </summary>
        public void Run()
        {
            Thread.CurrentThread.Name = "Main";
            Task task = Task.Factory.StartNew(() => CheckGameState());
            
            // waits for the task to complete its thing.
            task.Wait();
        }


        /// <summary>
        /// Checks to see what state the game is in by checking the new recording.
        /// Other ways of doing this maybe to employ cheat engine, but too much hex hacking
        /// means to much time wasted on deadends. Gotta stick to what works/
        /// </summary>
        private void CheckGameState()
        {
            while(_isRunning)
            {
                Learn();
            }

        }

        /// <summary>
        /// This method will either learn the game, or poll the database
		/// to check if an update to the Game table was made.
        /// </summary>
		/// <remarks>
		/// Games shall be entered upon completion of
		/// entering the data of the enconomics score.
		/// </remarks>
        private void Learn()
        {
            // holds the network that runs the show.
            if (_currentGameSate == GameState.GameEnd
                && _isRunning
                && !_networkProcessed)
            {
                _trainingModule.PushNewTrainingSet();
                _networkProcessed = false;
            }
			else
			{
				while(_isRunning)
				{
					GameData newGame = StreamUtilities.GetLatestGame();
					
					if(newGame != null)
					{
						// update game flag to processed after processing with the current game.
					}

					Thread.Sleep(500);
				}
			}
        }

        public enum GameState
        {
            GameNotStarted,
            GameRunning,
            GameEnd
        }
    }
}
