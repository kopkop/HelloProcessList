using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloProcessList
{
    public class MainFormCmd
    {
        private ProcessTreeBuilder processTreeBuilder;

        public MainFormCmd(MainForm mainForm)
        {
            processTreeBuilder = new ProcessTreeBuilder();
        }

        public void onClose()
        {
            
        }
    
    }
}
