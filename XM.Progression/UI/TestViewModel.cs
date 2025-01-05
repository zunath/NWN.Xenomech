using System;

namespace XM.UI.TestUI
{
    //[ServiceBinding(typeof(IViewModel))]
    internal class TestViewModel : ViewModel
    {
        private int _counter;

        public string TestProp1
        {
            get => Get<string>();
            set => Set(value);
        }

        public GuiBindingList<string> ButtonNames
        {
            get => Get<GuiBindingList<string>>();
            set => Set(value);
        }

        public TestViewModel()
        {
            ButtonNames = new GuiBindingList<string>();
        }

        public override void OnOpen()
        {
            TestProp1 = "my new test value from OnOpen";

            var buttonNames = new GuiBindingList<string>();
            buttonNames.Add("item 1");
            buttonNames.Add("item 2");
            buttonNames.Add("item 3");
            buttonNames.Add("item 4");
            buttonNames.Add("item 5");
            buttonNames.Add("item 6");

            ButtonNames = buttonNames;
        }

        public override void OnClose()
        {
        }

        public Action TestMethodToRun => () =>
        {
            _counter++;
            ShowModal("are ya sure?", () =>
            {
                Console.WriteLine($"confirming");
            }, () =>
            {
                Console.WriteLine($"cancelling");
            });
        };

        public Action TestMethodToRun2 => () =>
        {
            Console.WriteLine($"test method 2 running");
        };
    }
}
