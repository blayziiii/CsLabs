using System;
using System.Collections.Generic;
using System.Text;
namespace sem_1_lab_07_directory_tree;

public class TreeBuilder
{
    private static int _lineIndex;
    private static string[] _lines = null!;

    public static TreeNode Build(string[] lines)
    {
        _lines = lines;
        _lineIndex = 1;

        var root = new TreeNode(_lines[0]);

        BuildChildren(root);

        return root;
    }

    public static void BuildChildren(TreeNode parent)
    {
        while (_lineIndex < _lines.Length)
        {
            var line = _lines[_lineIndex].Trim();

            if (line == "")
            {
                _lineIndex++;
                continue;
            }

            if (line == "END")
            {
                _lineIndex++;
                return;
            }

            if (line.StartsWith("BEGIN "))
            {
                var name = line.Substring(6);

                var child = new TreeNode(name);

                parent.Children.Add(child);

                _lineIndex++;

                BuildChildren(child);
            }
        }
    }
}
