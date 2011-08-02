using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Management;
using System.Globalization;

namespace HelloProcessList
{
    public class ProcessTreeBuilder
    {
        public ProcessTreeBuilder()
        {
            Process[] processes = Process.GetProcesses();
            HashSet<int> pidSet = new HashSet<int>();
            foreach (Process p in processes)
            {
                pidSet.Add(p.Id);
            }
            foreach (int pid in pidSet)
            {
            }

           //int pid = 6032;
            //LinkedList<int> procList = new LinkedList<int>();
            //int rootPid = getProcessIdChain(pid, procList);
           // int rootPid = getRootProcessId(pid);
        }

        /**
         * 
         */
        private bool CreateTree(HashSet<int> processIds)
        {

            return false;
        }


        private int getProcessIdChain(int pid, LinkedList<int> pidChain)
        {
            //int rootPid = 0;
            // Prevent the 
            if (0 == pid || -1 == pid)
            {
                return -1;
            }
            pidChain.AddFirst(pid);
            int parentId = getParentProcessId(pid);
            if (0 == parentId || -1 == parentId)
            {
                return pid;
            }
            else
            {
                return getProcessIdChain(parentId, pidChain);
            }
        }


        private int getParentProcessId(int pid)
        {
            int parentPid = 0;
            using (ManagementObject mo = new ManagementObject("win32_process.handle='" + pid.ToString(CultureInfo.InvariantCulture) + "'"))
            {
                try
                {
                    mo.Get();
                }
                catch (ManagementException)
                {
                    return -1;
                }
                parentPid = Convert.ToInt32(mo["ParentProcessId"], CultureInfo.InvariantCulture);
            }
            return parentPid;
        }
    }
}
