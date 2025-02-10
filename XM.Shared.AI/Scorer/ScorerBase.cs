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

            if (!Context.IsAIEnabled ||
                Context.Services.Activity.IsBusy(Context.Creature))
            {
                return;
            }

            _currentAction = Actions
                .Select(action => new { Action = action, Score = action.DetermineScore() }) // Store scores before ordering
                .Where(a => a.Score > 0)  // Ignore actions with 0 score
                .OrderByDescending(a => a.Score)
                .Select(a => a.Action) // Select the best action
                .FirstOrDefault();

            if (_currentAction != null)
            {
                _currentAction.Execute();
            }
        }
    }
}
