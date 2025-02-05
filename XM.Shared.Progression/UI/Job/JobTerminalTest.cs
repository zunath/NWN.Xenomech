using Anvil.Services;
using XM.UI;

namespace XM.Progression.UI.Job
{
    [ServiceBinding(typeof(JobTerminalTest))]
    internal class JobTerminalTest
    {
        private GuiService _gui;

        public JobTerminalTest(GuiService gui)
        {
            _gui = gui;
        }

        [ScriptHandler("change_job")]
        public void ChangeJobTerminal()
        {
            _gui.ShowWindow<JobView>(GetLastUsedBy());
        }
    }
}
