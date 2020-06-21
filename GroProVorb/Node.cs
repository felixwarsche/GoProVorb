using System;
using System.Collections.Generic;
using System.Text;

namespace GroProVorb
{
    class Node
    {
        Node left;
        Node right;
        int value;

        public Node() { }

        public Node(Node left, Node right, int val)
        {
            this.left = left;
            this.right = right;
            this.value = val;
        }
    }
}
