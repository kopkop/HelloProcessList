using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Globalization;
using System.Collections;


namespace HelloProcessList
{
    public class ProcessTreeBuilder
    {
        private Hashtable processTree;

        public ProcessTreeBuilder(List<int> processList)
        {
            processTree = new Hashtable();
            CreateTree(processList);
        }

        /**
         * 
         */
        private bool CreateTree(List<int> processIds)
        {
            HashSet<int> processedIds = new HashSet<int>();
            foreach (int pid in processIds)
            {
                if (processedIds.Contains(pid)) 
                {
                    continue;
                }
                LinkedList<int> pidChain = new LinkedList<int>();
                int rootProcessId = getProcessIdChain(pid, pidChain);
                // Add the ids to processed
                foreach (int id in pidChain)
                {
                    processedIds.Add(id);
                }
                // Remove the top one since it is useless
                if (pidChain.Count > 1)
                {
                    pidChain.RemoveFirst();
                }
                if (pidChain.Count > 0)
                {
                    applyNewChain(pidChain);
                }
                
            }
            return true;
        }

        /**
         * This function is used to  
         */
        private bool applyNewChain(LinkedList<int> processChain)
        {
            int topProcess = processChain.First.Value;
            processChain.RemoveFirst();
            if (processTree.ContainsKey(topProcess))
            {
                Hashtable tree = processTree[topProcess] as Hashtable;
               
                if (processChain.Count > 0)
                {
                    applyChainToTree(tree, processChain);
                }                
            } 
            else
            {
                Hashtable tree = createNewTree(processChain);
                processTree.Add(topProcess, tree);
            }
            return true;
        }

        private Hashtable createNewTree(LinkedList<int> processChain)
        {
            if (processChain.Count == 0)
            {
                return new Hashtable();
            }
            else
            {
                Hashtable leaf = new Hashtable();
                int firstValue = processChain.First.Value;
                processChain.RemoveFirst();
                leaf.Add(firstValue, createNewTree(processChain));
                return leaf;
            }
        }

        private bool applyChainToTree(Hashtable tree, LinkedList<int> chain)
        {
            if (chain.Count > 0)
            {
                int firstValue = chain.First.Value;
                chain.RemoveFirst();
                if (tree.ContainsKey(firstValue))
                {
                    Hashtable subTree = tree[firstValue] as Hashtable;
                    return applyChainToTree(subTree, chain);
                }
                else
                {
                    Hashtable newTree = createNewTree(chain);
                    tree.Add(firstValue, newTree);
                }
            }
            return true;
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



        public Hashtable ProcessTree
        {
            get
            {
                return processTree;
            }
        }
    }
}
