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
            cmd = MainFormCmd.getInstance();
            cmd.MainProcessForm = this;
            initProcessTree();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmd.onClose();
        }

        private void initProcessTree()
        {
            Hashtable processTree = cmd.TreeBuilder.ProcessTree;
            processTreeView.ImageList = imageList;
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
                if (null != treeNode)
                {
                    treeNodes.Add(treeNode);
                    Hashtable subTree = tree[pid] as Hashtable;
                    if (subTree.Count != 0)
                    {
                        applyTreetoNodes(treeNode.Nodes, subTree);
                    }
                }                                
            }
        }

        private TreeNode getTreeNodeByPid(int pid)
        {
            try
            {
                Process process = Process.GetProcessById(pid);
                int imageIndex = (int)(cmd.ProcessManager.ImageIDMap[pid]);
                TreeNode node = new TreeNode(process.ProcessName, imageIndex, imageIndex);
                return node;
            }
            catch (System.Exception)
            {
            	
            }
            return null;
        }


        public ImageList FormImageList
        {
            get
            {
                return imageList;
            }
        }
        
    }
}
