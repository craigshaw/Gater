using System;
using System.Collections;
using System.Collections.Generic;

namespace Gater.Framework
{
    public abstract class GameState : IState
    {
        private readonly IList<IEnumerator> _activeCoroutines;
        private readonly IStateManager _stateManager;

        protected IStateManager StateManager
        {
            get
            {
                return _stateManager;
            }
        }

        public GameState(IStateManager stateManager)
        {
            if (stateManager == null)
                throw new ArgumentNullException("stateManager");

            _stateManager = stateManager;
            _activeCoroutines = new List<IEnumerator>();
        }

        protected void RegisterNextState(IState nextState)
        {
            if (nextState == null)
                throw new ArgumentNullException("nextState");

            _stateManager.RegisterNextState(nextState);
        }

        protected void StartCoroutine(IEnumerator coroutine)
        {
            if (coroutine == null)
                throw new ArgumentNullException("coroutine");

            _activeCoroutines.Add(coroutine);
        }

        protected void RunCoroutines()
        {
            List<IEnumerator> completedCoroutines = new List<IEnumerator>();

            foreach (var coroutine in _activeCoroutines)
            {
                if (!coroutine.MoveNext())
                    completedCoroutines.Add(coroutine);
            }

            foreach (var coroutine in completedCoroutines)
                _activeCoroutines.Remove(coroutine);
        }

        public abstract void Initialise();

        public virtual void ProcessFrame()
        {
            RunCoroutines();
        }
    }
}
