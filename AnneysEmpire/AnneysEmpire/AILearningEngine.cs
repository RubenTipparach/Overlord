using AnneysEmpire.Learning;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        /// 
        /// </summary>
        private GameState _currentGameSate;

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
        /// 
        /// </summary>
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
        }

        public enum GameState
        {
            GameNotStarted,
            GameRunning,
            GameEnd
        }
    }
}
