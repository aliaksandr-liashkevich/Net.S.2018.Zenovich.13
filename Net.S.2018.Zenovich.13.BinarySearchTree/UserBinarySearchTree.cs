using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.S._2018.Zenovich._13.BinarySearchTree
{
    public class UserBinarySearchTree<T>
    {
        private readonly IComparer<T> comparer;
        private Node<T> root;

        public UserBinarySearchTree(T element, IComparer<T> comparer = null)
        {
            this.comparer = ReferenceEquals(comparer,null) 
                ? Comparer<T>.Default 
                : comparer;

            if (ReferenceEquals(this.comparer, null))
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            root = new Node<T>();
            root.Value = element;   
        }

        public Node<T> Root
        {
            get { return root; }
            set { root = value; }
        }

        public void AddNode(T elem)
        {
            var currentElem = Root;
            while (currentElem != null)
            {
                if (comparer.Compare(elem, currentElem.Value) < 0)
                {
                    if (currentElem.Left == null)
                    {
                        currentElem.Left = new Node<T>()
                        {
                            Value = elem
                        };

                        return;
                    }

                    currentElem = currentElem.Left;
                }
                else
                {
                    if (currentElem.Right == null)
                    {
                        currentElem.Right = new Node<T>()
                        {
                            Value = elem
                        };

                        return;
                    }

                    currentElem = currentElem.Right;
                }
            }
        }

        public IEnumerable<T> PreOrder()
        {
            if (Root == null)
            {
                yield break;
            }

            var stack = new Stack<Node<T>>();
            stack.Push(Root);
            while (stack.Count != 0)
            {
                var node = stack.Pop();
                yield return node.Value;

                if (node.Right != null)
                {
                    stack.Push(node.Right);
                }

                if (node.Left != null)
                {
                    stack.Push(node.Left);
                }
            }
        }

        public IEnumerable<T> Postorder()
        {
            if (Root == null)
            {
                yield break;
            }

            var stack = new Stack<Node<T>>();
            var node = Root;

            while (stack.Count > 0 || node != null)
            {
                if (node == null)
                {
                    node = stack.Pop();
                    if (stack.Count > 0 && node.Right == stack.Peek())
                    {
                        stack.Pop();
                        stack.Push(node);
                        node = node.Right;
                    }
                    else
                    {
                        yield return node.Value;
                        node = null;
                    }
                }
                else
                {
                    if (node.Right != null)
                    {
                        stack.Push(node.Right);
                    }

                    stack.Push(node);
                    node = node.Left;
                }
            }
        }

        public IEnumerable<T> Inorder()
        {
            if (Root == null)
            {
                yield break;
            }

            var stack = new Stack<Node<T>>();
            var node = Root;

            while (stack.Count > 0 || node != null)
            {
                if (node == null)
                {
                    node = stack.Pop();
                    yield return node.Value;

                    if (node.Right != null)
                    {
                        node = stack.Pop();
                    }
                    else
                    {
                        node = null;
                    }
                }
                else
                {
                    if (node.Right != null)
                    {
                        stack.Push(node.Right);
                    }

                    stack.Push(node);
                    node = node.Left;
                }
            }
        }
    }
}
