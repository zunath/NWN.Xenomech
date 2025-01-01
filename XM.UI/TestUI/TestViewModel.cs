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
    }
}
