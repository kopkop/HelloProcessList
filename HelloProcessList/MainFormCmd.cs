using System;
using System.Linq;
using System.Text;

namespace HelloProcessList
{
    public class MainFormCmd
    {
        private ProcessTreeBuilder processTreeBuilder;
        private ProcessMgr processMgr;
        private MainForm mainForm;

        private static MainFormCmd cmd;

        public static MainFormCmd getInstance()
        {
            if (null == MainFormCmd.cmd)
            {
                cmd = new MainFormCmd();
                
            }
            return cmd;
        }

        public MainFormCmd()
        {
            
            
        }

        public void onClose()
        {
            mainForm.Close();
        }

        public ProcessTreeBuilder TreeBuilder
        {
            get
            {
                if (null == processTreeBuilder)
                {
                    processTreeBuilder = new ProcessTreeBuilder(ProcessManager.ProcessIdList);
                }
                return processTreeBuilder;
            }
        }

        public ProcessMgr ProcessManager
        {
            get
            {
                if (null == processMgr)
                {
                    processMgr = new ProcessMgr();                    
                }
                return processMgr;
            }
        }

        public MainForm MainProcessForm
        {
            get
            {
                return mainForm;
            }
            set
            {
                mainForm = value;
            }
        }
    }
}
