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
using DevExpress.XtraTreeList.Nodes;

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
            initTreeList();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmd.onClose();
        }

        private void initTreeList()
        {
            //treeList.Columns.Add();
            Hashtable processTree = cmd.TreeBuilder.ProcessTree;
            CreateColumns();
            treeList.BeginUnboundLoad();
            applyTreetoDexNodes(null, processTree);
            treeList.EndUnboundLoad();
            treeList.Refresh();
        }

        private void CreateColumns()
        {
            // Create three columns.
            treeList.BeginUpdate();
            treeList.Columns.Add();
            treeList.Columns[0].Caption = "Process Name";
            treeList.Columns[0].VisibleIndex = 0;
            treeList.Columns.Add();
            treeList.Columns[1].Caption = "Process Id";
            treeList.Columns[1].VisibleIndex = 1;
            treeList.Columns.Add();
            treeList.Columns[2].Caption = "File Path";
            treeList.Columns[2].VisibleIndex = 2;
            treeList.EndUpdate();
        }


        private void applyTreetoDexNodes(TreeListNode rootNode, Hashtable tree)
        {

            ICollection rootNodes = tree.Keys;
            foreach (int pid in rootNodes)
            {
                object[] obj = cmd.ProcessManager.getObjByPid(pid);
                if (null != obj)
                {
                    TreeListNode node = treeList.AppendNode(obj, rootNode);
                    node.StateImageIndex = (int)(cmd.ProcessManager.ImageIDMap[pid]);
                    //node.ImageIndex = (int)(cmd.ProcessManager.ImageIDMap[pid]);
                    Hashtable subTree = tree[pid] as Hashtable;
                    if (0 != subTree.Count)
                    {
                        applyTreetoDexNodes(node, subTree);
                    }
                }

            }
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
