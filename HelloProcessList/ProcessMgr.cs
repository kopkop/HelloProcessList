using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Collections;

namespace HelloProcessList
{
    public class ProcessMgr
    {
        private List<int> processIds;
        private Hashtable nameIdMap;
        private Hashtable processMap;
        //private PictureBox picBox;

        //private Form form;

        public ProcessMgr()
        {
            processIds = new List<int>();
            nameIdMap = new Hashtable();
            processMap = new Hashtable();
            Process[] processes = Process.GetProcesses();

            //form = new Form();
            //form.Show();
            // picBox = new PictureBox();
            
            foreach (Process p in processes)
            {
                try
                {
                    if (!p.HasExited)
                    {
                        processIds.Add(p.Id);
                        processMap.Add(p.Id, p);
                        InitImageByProcess(p);
                    }
                }
                catch (Win32Exception)
                {
                    continue;
                }
                
            }
        }

        public object[] getObjByPid(int pid)
        {
            if (processMap.ContainsKey(pid)) 
            { 
                try
                {
                    Process process = processMap[pid] as Process;
                    if (null != process)
                    {
                        return new object[] { process.ProcessName, process.Id, process.MainModule.FileName};
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (System.Exception)
                {
                    return null;
                }
                
            }
            else
            {
                return null;
            }
        }

        private void InitImageByProcess(Process process)
        {
            string defaultPath = "C:\\Windows\\twunk_16.exe";
            string path = defaultPath;
            try
            {
                path = process.MainModule.FileName;
            }
            catch (Win32Exception)
            {
                path = defaultPath;
            }
            Icon icon = Icon.ExtractAssociatedIcon(path);
            ImageList imageList = MainFormCmd.getInstance().MainProcessForm.FormImageList;
            if (!imageList.Images.ContainsKey(path)) 
            {
                imageList.Images.Add(path, icon);
            }
            int imageIndex = imageList.Images.IndexOfKey(path);
            nameIdMap.Add(process.Id, imageIndex);
        }

        public List<int> ProcessIdList
        {
            get
            {
                return processIds;
            }
        }

        public Hashtable ImageIDMap
        {
            get
            {
                return nameIdMap;
            }
        }

        public Hashtable ProcessMap
        {
            get
            {
                return processMap;
            }
        }
    }
}
