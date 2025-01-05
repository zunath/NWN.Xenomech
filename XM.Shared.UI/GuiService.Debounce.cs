using System;
using System.Collections.Generic;

namespace XM.UI
{
    public partial class GuiService
    {
        private readonly Queue<KeyValuePair<IViewModel, Action>> _eventQueue = new();
        private readonly Dictionary<IViewModel, DateTime> _lastEventTimestamps = new();
        private readonly TimeSpan _debounceTime = TimeSpan.FromMilliseconds(350);

        private void EnqueueUIAction(IViewModel viewModel, Action action)
        {
            if (_lastEventTimestamps.TryGetValue(viewModel, out var lastTimestamp))
            {
                if (DateTime.UtcNow - lastTimestamp < _debounceTime)
                    return;
            }

            _lastEventTimestamps[viewModel] = DateTime.UtcNow;
            _eventQueue.Enqueue(new KeyValuePair<IViewModel, Action>(viewModel, action));
        }

        private void ProcessUIEvents()
        {
            while (_eventQueue.TryDequeue(out var @event))
            {
                if (_viewModelsProcessedThisFrame.Contains(@event.Key))
                    continue;

                try
                {
                    @event.Value();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }

                _viewModelsProcessedThisFrame.Add(@event.Key);
            }

            _viewModelsProcessedThisFrame.Clear();
        }

        private readonly HashSet<IViewModel> _viewModelsProcessedThisFrame = new();
        public void Update()
        {
            ProcessUIEvents();
        }
    }
}
