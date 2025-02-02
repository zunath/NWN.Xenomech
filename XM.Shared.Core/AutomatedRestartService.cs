using Anvil.Services;
using NLog;
using System;
using XM.Shared.API.NWNX.AdminPlugin;
using XM.Shared.Core.Data;
using XM.Shared.Core.Entity;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;

namespace XM.Shared.Core
{
    [ServiceBinding(typeof(AutomatedRestartService))]
    [ServiceBinding(typeof(IInitializable))]
    internal class AutomatedRestartService: IInitializable
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        // This determines what time the server will restart.
        // Restarts happen within a range of 30 seconds of this specified time. 
        // All times are in UTC.
        private TimeSpan RestartTime => new TimeSpan(0, 10, 0, 0); // 0 = Restarts happen at 6 AM eastern time
        private DateTime _nextNotification;

        private readonly XMEventService _event;
        private readonly DBService _db;
        private readonly TimeService _time;
        private readonly SchedulerService _scheduler;

        public AutomatedRestartService(
            XMEventService @event,
            DBService db,
            TimeService time,
            SchedulerService scheduler)
        {
            _event = @event;
            _db = db;
            _time = time;
            _scheduler = scheduler;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<XMEvent.OnServerHeartbeat>(ProcessAutoRestart);
        }
        public void Init()
        {
            ScheduleRestartReminder();
        }

        private void ProcessAutoRestart(uint obj)
        {
            var now = DateTime.UtcNow.TimeOfDay;
            var restartRange = RestartTime.Add(new TimeSpan(0, 0, 0, 30));

            // Current time is within 30 seconds of the specified restart time.
            if ((now > RestartTime) && (now < restartRange))
            {
                for (var player = GetFirstPC(); GetIsObjectValid(player); player = GetNextPC())
                {
                    ExportSingleCharacter(player);
                    var message = LocaleString.TheServerIsAutomaticallyRestarting.ToLocalizedString();
                    BootPC(player, message);
                }

                var log = LocaleString.ServerShuttingDownForAutomatedRestart.ToLocalizedString();
                _logger.Info(log);

                DelayCommand(0.1f, AdminPlugin.ShutdownServer);
            }
        }

        private void ScheduleRestartReminder()
        {
            var bootNow = DateTime.UtcNow;
            _nextNotification = new DateTime(bootNow.Year, bootNow.Month, bootNow.Day, bootNow.Hour, 0, 0)
                .AddMinutes(1);

            _scheduler.ScheduleRepeating(() =>
            {
                var now = DateTime.UtcNow;
                var restartDate = new DateTime(now.Year, now.Month, now.Day, RestartTime.Hours, RestartTime.Minutes, RestartTime.Seconds);

                if (RestartTime < now.TimeOfDay)
                {
                    restartDate = restartDate.AddDays(1);
                }

                if (now >= _nextNotification)
                {
                    var delta = restartDate - now;
                    var rebootString = _time.GetTimeLongIntervals(delta, false);
                    var message = LocaleString.ServerWillAutomaticallyRebootInApproximatelyX.ToLocalizedString(rebootString);
                    _logger.Info(message);

                    for (var player = GetFirstPC(); GetIsObjectValid(player); player = GetNextPC())
                    {
                        var playerId = GetObjectUUID(player);
                        var dbPlayerSettings = _db.Get<PlayerSettings>(playerId);

                        if (GetIsDM(player) || GetIsDMPossessed(player) || (dbPlayerSettings != null && dbPlayerSettings.DisplayServerResetReminders))
                            SendMessageToPC(player, message);
                    }

                    _nextNotification = delta.TotalMinutes <= 15
                        ? now.AddMinutes(1)
                        : now.AddHours(1);
                }
            }, TimeSpan.FromMinutes(1));
        }

    }
}
