
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Tadi.Utils.Tree
{
    public class TreeNode<T>
    {
        public T value;
        public List<TreeNode<T>> children = new List<TreeNode<T>>();

        public TreeNode(T value)
        {
            this.value = value;
        }

        public void AddChild(TreeNode<T> child)
        {
            children.Add(child);
        }

        public TreeNode<T> FindNode(T targetValue)
        {
            if (value.Equals(targetValue))
            {
                return this; // Found the root
            }

            foreach (var child in children)
            {
                var result = child.FindNode(targetValue);
                if (result != null)
                {
                    return result; // Node found in child subtree
                }
            }

            return null; // Node not found in this subtree
        }

        public TreeNode<T> FindNodeByIndices(int depthIndex, int breadthIndex)
        {
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(this); // Start BFS from the root
            int depth = 0;

            while (queue.Count > 0)
            {
                int levelSize = queue.Count;

                for (int i = 0; i < levelSize; i++)
                {
                    TreeNode<T> node = queue.Dequeue();

                    // Check if the root's depth and breadth indices match the Target indices
                    if (depth == depthIndex && i == breadthIndex)
                    {
                        return node; // Found the root
                    }

                    // Enqueue children for the next learnLevel
                    foreach (TreeNode<T> child in node.children)
                    {
                        queue.Enqueue(child);
                    }
                }
                depth++; // Move to the next depth learnLevel
            }

            return null; // Node with the specified indices not found
        }

        public void RemoveAllNodes()
        {
            // Traverse the Tree in post-order (children first, then parent)
            RemoveAllNodesRec(this);
        }

        private void RemoveAllNodesRec(TreeNode<T> node)
        {
            foreach (TreeNode<T> child in node.children)
            {
                RemoveAllNodesRec(child);
            }

            node.children.Clear(); // Remove all children
        }
    }

    public class Tree<T>
    {
        private TreeNode<T> root;
        private Queue<TreeNode<T>> queue;

        public Tree(TreeNode<T> root)
        {
            this.root = root;
            queue = new Queue<TreeNode<T>>();
        }

        // In-order traversal
        public List<T> InOrderTraversal()
        {
            List<T> result = new List<T>();
            InOrderTraversalHelper(root, result);
            return result;
        }

        private void InOrderTraversalHelper(TreeNode<T> node, List<T> result)
        {
            if (node == null)
                return;

            // Traverse left child
            foreach (var child in node.children)
            {
                InOrderTraversalHelper(child, result);
            }

            // Visit current root
            result.Add(node.value);

            // Traverse right child
            // (This is not needed for a typical Tree traversal as we are dealing with only one root root in this case)
        }

        public int GetBreadthAtDepth(int depth)
        {
            if (root == null)
                return 0;

            queue.Clear();
            queue.Enqueue(root);

            int currentDepth = 0;
            int nodesAtDepth = 0;

            while (queue.Count > 0)
            {
                int levelSize = queue.Count;

                for (int i = 0; i < levelSize; i++)
                {
                    TreeNode<T> currentNode = queue.Dequeue();

                    if (currentDepth == depth)
                    {
                        nodesAtDepth++;
                        continue;
                    }

                    foreach (var child in currentNode.children)
                    {
                        queue.Enqueue(child);
                    }
                }

                currentDepth++;

                if (currentDepth > depth)
                    break;
            }

            return nodesAtDepth;
        }

        public List<T> GetListAtDepth(int depth)
        {
            if (root == null)
                return null;

            queue.Clear();
            queue.Enqueue(root);

            int currentDepth = 0;
            List<T> nodesAtDepth = new List<T>();

            while (queue.Count > 0)
            {
                int levelSize = queue.Count;

                for (int i = 0; i < levelSize; i++)
                {
                    TreeNode<T> currentNode = queue.Dequeue();

                    if (currentDepth == depth)
                    {
                        nodesAtDepth.Add(currentNode.value);
                        continue;
                    }

                    foreach (var child in currentNode.children)
                    {
                        queue.Enqueue(child);
                    }
                }

                currentDepth++;

                if (currentDepth > depth)
                    break;
            }

            return nodesAtDepth;
        }
    }
}