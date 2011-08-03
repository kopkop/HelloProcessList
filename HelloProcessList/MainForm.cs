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
using DevExpress.XtraTreeList.Columns;

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
            initTreeList();
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
                //node.ToolTipText = imageList.Images[imageIndex].
                return node;
            }
            catch (System.Exception)
            {
            	
            }
            return null;
        }


        private void initTreeList()
        {
            Hashtable processTree = cmd.TreeBuilder.ProcessTree;
            treeList.BeginUnboundLoad();
            applyTreetoDexNodes(-1, processTree);
            treeList.EndUnboundLoad();
        }

        private void applyTreetoDexNodes(int rootNodeId, Hashtable tree)
        {
            //if (rootNodeId == -1)
            //{
                ICollection rootNodes = tree.Keys;
                foreach (int pid in rootNodes)
                {
                    object[] obj = cmd.ProcessManager.getObjByPid(pid);
                    if (null != obj)
                    {
                        treeList.AppendNode(obj, null);
                    }
                    
                }
            //}
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
