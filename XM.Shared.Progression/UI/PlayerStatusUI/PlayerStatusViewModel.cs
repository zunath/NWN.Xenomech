using System;
using Anvil.API;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.Stat.Event;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using XM.UI;

namespace XM.Progression.UI.PlayerStatusUI
{
    [ServiceBinding(typeof(IViewModel))]
    internal class PlayerStatusViewModel: 
        ViewModel,
        IRefreshable<PlayerHPAdjustedEvent>,
        IRefreshable<PlayerEPAdjustedEvent>
    {
        private int _screenHeight;
        private int _screenWidth;
        private int _screenScale;

        private static readonly Color _hpColor = new(139, 0, 0);
        private static readonly Color _epColor = new(3, 87, 152);

        public Color Bar1Color
        {
            get => Get<Color>();
            set => Set(value);
        }

        public Color Bar2Color
        {
            get => Get<Color>();
            set => Set(value);
        }
        public string Bar1Label
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Bar2Label
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Bar1Value
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Bar2Value
        {
            get => Get<string>();
            set => Set(value);
        }
        public float Bar1Progress
        {
            get => Get<float>();
            set => Set(value);
        }
        public float Bar2Progress
        {
            get => Get<float>();
            set => Set(value);
        }

        public NuiRect RelativeValuePosition
        {
            get => Get<NuiRect>();
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
                const float WidgetWidth = 200f;
                const float WidgetHeight = 70f;
                const float XOffset = 255f;
                const float YOffset = 130f;

                var scale = screenScale / 100f;
                var x = screenWidth - XOffset * scale;
                var y = screenHeight - YOffset * scale;

                Geometry = new NuiRect(
                    x,
                    y,
                    WidgetWidth,
                    WidgetHeight);

                x = 60f * scale;
                RelativeValuePosition = new NuiRect(x, 2f, 110f, 50f);

                _screenHeight = screenHeight;
                _screenWidth = screenWidth;
                _screenScale = screenScale;
            }
        }

        private void UpdateAllData()
        {
            Bar1Label = Locale.GetString(LocaleString.HP) + ":";
            Bar1Color = _hpColor;
            UpdateHP();

            Bar2Label = Locale.GetString(LocaleString.EP) + ":";
            Bar2Color = _epColor;
            UpdateEP();
        }

        private void UpdateHP()
        {
            var currentHP = GetCurrentHitPoints(Player);
            var maxHP = GetMaxHitPoints(Player);
            var ratio = (float)currentHP / (float)maxHP;

            Bar1Value = $"{currentHP} / {maxHP}";
            Bar1Progress = maxHP <= 0 
                ? 0 
                : ratio > 1f 
                    ? 1f 
                    : ratio;
        }

        private void UpdateEP()
        {
            var currentEP = Stat.GetCurrentEP(Player);
            var maxEP = Stat.GetMaxEP(Player);
            var ratio = (float)currentEP / (float)maxEP;

            Bar2Value = $"{currentEP} / {maxEP}";
            Bar2Progress = maxEP <= 0
                ? 0
                : ratio > 1f 
                    ? 1f 
                    : ratio;
        }

        [ScriptHandler("bread_test")]
        public void Test()
        {
            var player = GetLastUsedBy();
            ApplyEffectToObject(DurationType.Instant, EffectDamage(1), player);
        }

        public void Refresh(PlayerHPAdjustedEvent @event)
        {
            Console.WriteLine($"refresh HP");
            UpdateHP();
        }

        public void Refresh(PlayerEPAdjustedEvent @event)
        {
            Console.WriteLine($"refresh EP");
            UpdateEP();
        }
    }
}
