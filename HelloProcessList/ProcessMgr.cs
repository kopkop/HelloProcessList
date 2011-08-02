using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace HelloProcessList
{
    public class ProcessMgr
    {
        private List<int> processIds;
        public ProcessMgr()
        {
            processIds = new List<int>();
            Process[] processes = Process.GetProcesses();
      
            foreach (Process p in processes)
            {
                processIds.Add(p.Id);
            }
        }

        public List<int> ProcessIdList
        {
            get
            {
                return processIds;
            }
        }
    
    }
}
