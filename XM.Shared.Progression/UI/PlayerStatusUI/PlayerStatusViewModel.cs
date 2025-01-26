using System;
using Anvil.API;
using Anvil.Services;
using XM.Progression.Event;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.UI;

namespace XM.Progression.UI.PlayerStatusUI
{
    [ServiceBinding(typeof(IViewModel))]
    internal class PlayerStatusViewModel: 
        ViewModel<PlayerStatusViewModel>,
        IRefreshable<StatEvent.PlayerEPAdjustedEvent>
    {
        private int _screenHeight;
        private int _screenWidth;
        private int _screenScale;

        private static readonly Color _epColor = new(3, 87, 152);

        public Color EPBarColor
        {
            get => Get<Color>();
            set => Set(value);
        }
        public string EPValue
        {
            get => Get<string>();
            set => Set(value);
        }
        public float EPProgress
        {
            get => Get<float>();
            set => Set(value);
        }


        [Inject]
        private StatService Stat { get; set; }

        public override void OnOpen()
        {
            _screenHeight = -1;
            _screenScale = -1;
            _screenWidth = -1;

            UpdateWidget();
            UpdateAllData();
        }

        public override void OnClose()
        {
            
        }


        private void UpdateWidget()
        {
            var screenHeight = GetPlayerDeviceProperty(Player, PlayerDevicePropertyType.GuiHeight);
            var screenWidth = GetPlayerDeviceProperty(Player, PlayerDevicePropertyType.GuiWidth);
            var screenScale = GetPlayerDeviceProperty(Player, PlayerDevicePropertyType.GuiScale);

            if (_screenHeight != screenHeight ||
                _screenWidth != screenWidth ||
                _screenScale != screenScale)
            {
                const float WidgetWidth = 76f;
                const float WidgetHeight = 70f;
                const float XOffset = 0f;
                const float YOffset = 48f;

                var scale = screenScale / 100f;
                var x = (screenWidth - XOffset) * scale;
                var y = YOffset * scale;

                Geometry = new NuiRect(
                    x,
                    y,
                    WidgetWidth,
                    WidgetHeight);

                _screenHeight = screenHeight;
                _screenWidth = screenWidth;
                _screenScale = screenScale;
            }
        }

        private void UpdateAllData()
        {
            EPBarColor = _epColor;
            UpdateEP();
        }

        private void UpdateEP()
        {
            var currentEP = Stat.GetCurrentEP(Player);
            var maxEP = Stat.GetMaxEP(Player);
            var ratio = maxEP > 0f
                ? (float)currentEP / (float)maxEP
                : 0f;

            EPValue = $"{currentEP}";
            EPProgress = Math.Clamp(ratio, 0f, 1f);
        }

        public void Refresh(StatEvent.PlayerEPAdjustedEvent @event)
        {
            UpdateEP();
        }
    }
}
