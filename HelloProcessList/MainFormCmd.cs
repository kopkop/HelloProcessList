using System;
using System.Linq;
using System.Text;

namespace HelloProcessList
{
    public class MainFormCmd
    {
        private ProcessTreeBuilder processTreeBuilder;
        private ProcessMgr processMgr;

        public MainFormCmd(MainForm mainForm)
        {
            processMgr = new ProcessMgr();
            processTreeBuilder = new ProcessTreeBuilder(processMgr.ProcessIdList);
        }

        public void onClose()
        {
            
        }

        public ProcessTreeBuilder TreeBuilder
        {
            get
            {
                return processTreeBuilder;
            }
        }

        public ProcessMgr ProcessManager
        {
            get
            {
                return processMgr;
            }
        }
    }
}
