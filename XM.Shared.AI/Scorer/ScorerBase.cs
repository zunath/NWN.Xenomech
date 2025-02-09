using System.Collections.Generic;
using System.Linq;
using XM.AI.Actions;

namespace XM.AI.Scorer
{
    internal abstract class ScorerBase: IAIScorer
    {
        protected abstract List<IAIAction> Actions { get; }
        private IAIAction _currentAction;
        private bool _isInitialized;
        protected IAIContext Context { get; }

        protected ScorerBase(IAIContext context)
        {
            Context = context;
        }

        private void InitializeActions()
        {
            if (!_isInitialized)
            {
                foreach (var action in Actions)
                {
                    action.Initialize();
                }

                _isInitialized = true;
            }
        }

        public void Update()
        {
            InitializeActions();

            if (_currentAction != null && !_currentAction.IsComplete)
            {
                return;
            }

            _currentAction = Actions
                .OrderByDescending(o => o.DetermineScore())
                .FirstOrDefault();

            if (_currentAction != null)
            {
                _currentAction.Execute();
            }
        }
    }
}
