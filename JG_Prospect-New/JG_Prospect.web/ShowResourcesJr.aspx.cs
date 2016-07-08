using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace JG_Prospect
{
    public partial class ShowResourcesJr : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TreeView1.ShowCheckBoxes = TreeNodeTypes.Leaf;

                ListDirectory(TreeView1, Server.MapPath("~/Resources"));
            }
            TreeView1.ExpandAll();
        }

        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);


            foreach (var directory in directoryInfo.GetDirectories())
            {
                directoryNode.SelectAction = TreeNodeSelectAction.None;
                directoryNode.ShowCheckBox = false;
                directoryNode.NavigateUrl = "#";
                directoryNode.ImageUrl = "~/img/folder.png";
                directoryNode.ChildNodes.Add(CreateDirectoryNode(directory));
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                directoryNode.ChildNodes.Add(new TreeNode(file.Name));
                directoryNode.ShowCheckBox = false;
                directoryNode.NavigateUrl = "#";
                directoryNode.ImageUrl = "~/img/File.png";
                directoryNode.SelectAction = TreeNodeSelectAction.None;

            }
            if (directoryNode.ChildNodes.Count <= 0)
            {
                FileInfo[] txtFiles = directoryInfo.GetFiles();
                if (txtFiles.Length == 0)
                {
                    directoryNode.ImageUrl = "~/img/folder.png";
                    directoryNode.ShowCheckBox = false;
                    directoryNode.NavigateUrl = "#";
                    directoryNode.SelectAction = TreeNodeSelectAction.None;
                    if (directoryInfo.Name == "Resources")
                    {
                        directoryNode.ImageUrl = "~/img/Resource.png";
                    }
                }
            }
            else
            {
                directoryNode.ImageUrl = "~/img/folder.png";
                if (directoryInfo.Name == "Resources")
                {
                    directoryNode.ImageUrl = "~/img/Resource.png";
                }
            }
            return directoryNode;
        }


        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            string type = TreeView1.SelectedNode.GetType().ToString();

            TreeNode node = sender as TreeNode;
            node.NavigateUrl = node.Text;

            if (sender.GetType() == TreeNodeTypes.Leaf.GetType())
            {

            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (TreeView1.CheckedNodes.Count > 0)
            {
                mpeDeleteResource.Show();
                TreeView1.ExpandAll();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select at least one resource.');", true);
            }
        }
        protected void btnX_Click(object sender, EventArgs e)
        {
            mpeDeleteResource.Hide();
        }
        protected void DeleteResource(object sender, EventArgs e)
        {
            try
            {
                List<TreeNode> checkedNodes = TreeView1.CheckedNodes.OfType<TreeNode>().
                                                     ToList();
                foreach (TreeNode node in checkedNodes)
                {
                    string sourceDir = Server.MapPath("~/Resources/") + node.Parent.Text + "/";
                    string fileToDelete = Directory.GetFiles(sourceDir, node.Text)[0].ToString();
                    node.Parent.ChildNodes.Remove(node);

                    File.Delete(fileToDelete);
                }
                ListDirectory(TreeView1, Server.MapPath("~/Resources"));
                TreeView1.ExpandAll();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Node cann't be deleted');", true);
            }

        }



    }
}