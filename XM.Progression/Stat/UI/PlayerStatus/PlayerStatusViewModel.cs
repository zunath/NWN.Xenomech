using Anvil.Services;
using XM.API.Constants;
using XM.Data;
using XM.UI;
using XM.UI.Component;

namespace XM.Progression.Stat.UI.PlayerStatus
{
    //[ServiceBinding(typeof(PlayerStatusViewModel))]
    internal class PlayerStatusViewModel : 
        GuiViewModelBase<PlayerStatusViewModel, GuiPayloadBase>,
        IGuiRefreshable<PlayerStatusRefreshEvent>
    {
        private int _screenHeight;
        private int _screenWidth;
        private int _screenScale;

        private static readonly GuiColor _hpColor = new(139, 0, 0);
        private static readonly GuiColor _epColor = new(3, 87, 152);

        private readonly DBService _db;
        private readonly StatService _stat;

        public PlayerStatusViewModel(
            GuiService gui,
            DBService db,
            StatService stat)
            : base(gui)
        {
            _db = db;
            _stat = stat;
        }

        public GuiColor Bar1Color
        {
            get => Get<GuiColor>();
            set => Set(value);
        }

        public GuiColor Bar3Color
        {
            get => Get<GuiColor>();
            set => Set(value);
        }

        public string Bar1Label
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Bar3Label
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Bar1Value
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Bar3Value
        {
            get => Get<string>();
            set => Set(value);
        }

        public float Bar1Progress
        {
            get => Get<float>();
            set => Set(value);
        }
        public float Bar3Progress
        {
            get => Get<float>();
            set => Set(value);
        }

        public GuiRectangle RelativeValuePosition
        {
            get => Get<GuiRectangle>();
            set => Set(value);
        }

        protected override void Initialize(GuiPayloadBase initialPayload)
        {
            _screenHeight = -1;
            _screenScale = -1;
            _screenWidth = -1;

            UpdateWidget();
            UpdateAllData();
        }

        private void ToggleLabels(bool isCharacter)
        {
            Bar1Label = "HP:";
            Bar3Label = "FP:";
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
                const float WidgetHeight = 105f;
                const float XOffset = 255f;
                const float YOffset = 165f;

                var scale = screenScale / 100f;
                var x = screenWidth - XOffset * scale;
                var y = screenHeight - YOffset * scale;

                Geometry = new GuiRectangle(
                    x,
                    y,
                    WidgetWidth,
                    WidgetHeight);

                x = 60f * scale;
                RelativeValuePosition = new GuiRectangle(x, 2f, 110f, 50f);

                _screenHeight = screenHeight;
                _screenWidth = screenWidth;
                _screenScale = screenScale;
            }
        }

        private void UpdateHP()
        {
            var currentHP = _stat.GetCurrentHP(Player);
            var maxHP = _stat.GetMaxHP(Player);

            Bar1Value = $"{currentHP} / {maxHP}";
            Bar1Progress = maxHP <= 0 ? 0 : currentHP / (float)maxHP > 1.0f ? 1.0f : currentHP / (float)maxHP;
        }

        private void UpdateEP()
        {
            var currentEP = _stat.GetCurrentEP(Player);
            var maxEP = _stat.GetMaxEP(Player);
            Bar3Value = $"{currentEP} / {maxEP}";
            Bar3Progress = maxEP <= 0 ? 0 : currentEP / (float)maxEP > 1.0f ? 1.0f : currentEP / (float)maxEP;
        }

        private void UpdateSingleData(PlayerStatusRefreshEvent.StatType type)
        {
            ToggleLabels(true);
            Bar1Color = _hpColor;
            Bar3Color = _epColor;

            if (type == PlayerStatusRefreshEvent.StatType.HP)
            {
                UpdateHP();
            }
            else if (type == PlayerStatusRefreshEvent.StatType.EP)
            {
                UpdateEP();
            }
        }

        private void UpdateAllData()
        {
            ToggleLabels(true);
            Bar1Color = _hpColor;
            Bar3Color = _epColor;
            UpdateHP();
            UpdateEP();
        }

        public void Refresh(PlayerStatusRefreshEvent payload)
        {
            UpdateWidget();
            UpdateSingleData(payload.Type);
        }

    }
}
