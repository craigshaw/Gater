using System;

namespace Gater.Framework
{
    public abstract class Game : IStateManager
    {
        private IState _currentState;
        private IState _nextState;

        public bool Running { get; protected set; }

        /// <summary>
        /// Main game loop
        /// </summary>
        public void Run()
        {
            Running = true;

            // One time game initialisation
            _currentState = InitialiseGame();
            _currentState.Initialise();

            // Now into the game loop
            do
            {
                // Do any game specific pre game frame logic
                PreProcessFrame();

                // Process frame ... in this console context, we'll assume this processes input, logic and drawing
                _currentState.ProcessFrame();

                // Update state if we've been asked to
                if (_nextState != null)
                {
                    _currentState = _nextState;
                    _nextState.Initialise();
                    _nextState = null;
                }

                // Do any game specific post frame logic
                PostProcessFrame();
            } while (Running);
        }

        protected abstract IState InitialiseGame();

        protected virtual void PreProcessFrame()
        {
            // Default, do nothing. This is a lifecycle hook
            // for sub classes to override if they need to
        }

        protected virtual void PostProcessFrame()
        {
            // Default, do nothing. This is a lifecycle hook
            // for sub classes to override if they need to
        }

        public void RegisterNextState(IState nextState)
        {
            if (nextState == null)
                throw new ArgumentNullException("nextState");

            this._nextState = nextState;
        }
    }
}
