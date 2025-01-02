using System;
using Anvil.Services;

namespace XM.UI.TestUI
{
    [ServiceBinding(typeof(IViewModel))]
    internal class TestViewModel : ViewModel
    {
        private int _counter;

        public string TestProp1
        {
            get => Get<string>();
            set => Set(value);
        }

        [Inject]
        public GuiService Gui { get; set; }

        public TestViewModel()
        {
            
        }

        public override void OnOpen()
        {
            TestProp1 = "my new test value from OnOpen";
        }

        public override void OnClose()
        {
            
        }

        public Action TestMethodToRun => () =>
        {
            _counter++;
            TestProp1 = $"val: {_counter}";
        };

        public Action TestMethodToRun2 => () =>
        {
            Console.WriteLine($"test method 2 running");
        };
    }
}
