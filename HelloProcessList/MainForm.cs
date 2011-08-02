using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace HelloProcessList
{
    public partial class MainForm : Form
    {
        private MainFormCmd cmd;

        public MainForm()
        {
            InitializeComponent();
            cmd = new MainFormCmd(this);
            initProcessTree();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmd.onClose();
        }

        private void initProcessTree()
        {
            Hashtable processTree = cmd.TreeBuilder.ProcessTree;
            ImageList imageList = prepareImageList();
            processTreeView.BeginUpdate();
            applyTreetoNodes(processTreeView.Nodes, processTree);
            processTreeView.EndUpdate();
        }

        private void applyTreetoNodes(TreeNodeCollection treeNodes, Hashtable tree)
        {
            ICollection rootNodes = tree.Keys;
            foreach (int pid in rootNodes)
            {
                TreeNode treeNode = getTreeNodeByPid(pid);
                treeNodes.Add(treeNode);
                Hashtable subTree = tree[pid] as Hashtable;
                if (subTree.Count != 0)
                {
                    applyTreetoNodes(treeNode.Nodes, subTree);
                }
            }
        }

        private TreeNode getTreeNodeByPid(int pid)
        {
            Process process = Process.GetProcessById(pid);
            string procFile = "n/a";
            try
            {
                procFile = process.Modules[0].FileName;
            }
            catch (Win32Exception)
            {
                procFile = "n/a";
            }
            Icon ico = null;
            if (procFile != "n/a")
            {
                ico = Icon.ExtractAssociatedIcon(@procFile);
            }
            TreeNode node = new TreeNode(process.ProcessName);
            return node;
        }

        private ImageList prepareImageList()
        {
            ImageList imageList = new ImageList();
            //imageList.Images.Add()
            return imageList;
        }
    }
}
