namespace XM.UI.TestUI
{
    internal class TestView
    {
        private readonly GuiBuilder<TestViewModel> _builder = new();

        public TestView()
        {
            _builder.CreateWindow()
                .AddColumn(col =>
                {

                });
        }
    }
}
