using System;
using System.Collections.Generic;
using System.Text;

namespace sem_1_lab_07_directory_tree;

public class TreeNode
{
    public string Name { get; set; }

    public List<TreeNode> Children { get; set; }

    public TreeNode(string name)
    {
        Name = name;
        Children = new List<TreeNode>();
    }
}
