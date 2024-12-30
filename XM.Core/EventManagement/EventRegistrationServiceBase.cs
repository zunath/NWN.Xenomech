using NLog;
using System;
using System.Collections.Generic;

namespace XM.Core.EventManagement
{
    internal abstract class EventRegistrationServiceBase
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        protected void HandleEvent<T>(IList<T> subscriptions, Action<T> action)
        {
            foreach (var subscription in subscriptions)
            {
                try
                {
                    action(subscription);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }
    }
}
