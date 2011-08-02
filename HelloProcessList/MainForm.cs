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
            processTreeView.BeginUpdate();
            applyTreetoNodes(processTreeView.Nodes, processTree);
            processTreeView.EndUpdate();
        }

        private void applyTreetoNodes(TreeNodeCollection treeNodes, Hashtable tree)
        {
            ICollection rootNodes = tree.Keys;
            foreach (int pid in rootNodes)
            {
                Process process = Process.GetProcessById(pid);
                TreeNode treeNode = new TreeNode(process.ProcessName + pid);
                treeNodes.Add(treeNode);
                Hashtable subTree = tree[pid] as Hashtable;
                if (subTree.Count != 0)
                {
                    applyTreetoNodes(treeNode.Nodes, subTree);
                }
            }
        }
    }
}
