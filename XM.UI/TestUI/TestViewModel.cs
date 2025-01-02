using System;

namespace XM.UI.TestUI
{
    internal class TestViewModel : ViewModel
    {
        public string TestProp1
        {
            get => Get<string>();
            set => Set(value);
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
            Console.WriteLine($"TEstmethodtorun running");
        };

        public Action TestMethodToRun2 => () =>
        {
            Console.WriteLine($"test method 2 running");
        };
    }
}
