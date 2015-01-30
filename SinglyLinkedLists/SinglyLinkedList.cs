using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinglyLinkedLists
{
    public class SinglyLinkedList
    {
        private SinglyLinkedListNode firstNode;
        public SinglyLinkedList()
        {
            // NOTE: This constructor isn't necessary, once you've implemented the constructor below.
        }

        // READ: http://msdn.microsoft.com/en-us/library/aa691335(v=vs.71).aspx
        public SinglyLinkedList(params object[] values)
        {
            foreach (object i in values)
            {
                string iString = i.ToString();
                this.AddLast(iString);
            }
        }

        // READ: http://msdn.microsoft.com/en-us/library/6x16t2tx.aspx
        public string this[int i]
        {
            get { return ElementAt(i); }
            set 
            {
                SinglyLinkedListNode nodei = this.firstNode;
                for (int j = 0; j < i - 1; j++)
                {
                    nodei = nodei.Next;
                }
                SinglyLinkedListNode thePriorNext = nodei.Next.Next;
                SinglyLinkedListNode intermediate = new SinglyLinkedListNode(value);
                nodei.Next = intermediate;
                intermediate.Next = thePriorNext;
            }
        }

        public void AddAfter(string existingValue, string value)
        {
            SinglyLinkedListNode node = this.firstNode;
            while (node.Value != existingValue)
            {
                node = node.Next;
                if (node == null)
                {
                    throw new ArgumentException();
                }
            }
            var theNewNode = new SinglyLinkedListNode(value);
            theNewNode.Next = node.Next;
            node.Next = theNewNode;
        }

        public void AddFirst(string value)
        {
            SinglyLinkedListNode oldFirst = this.firstNode;
            SinglyLinkedListNode newFirst = new SinglyLinkedListNode(value);
            this.firstNode = newFirst;
            newFirst.Next = oldFirst;
        }

        public void AddLast(string value)
        {
            if (firstNode == null)
            {
                firstNode = new SinglyLinkedListNode(value);
            }
            else
            {
                SinglyLinkedListNode nodei = this.firstNode;
                while(!nodei.IsLast())
                {
                    nodei = nodei.Next;
                }
                nodei.Next = new SinglyLinkedListNode(value);
            }
        }

        // NOTE: There is more than one way to accomplish this.  One is O(n).  The other is O(1).
        public int Count()
        {
            SinglyLinkedListNode nodei = this.firstNode;
            int i = 0;
            while (nodei != null)
            {
                i++;
                nodei = nodei.Next;
            }
            return i;
        }

        public string ElementAt(int index)
        {
            SinglyLinkedListNode nodei = this.firstNode;
            for (int i = 0; i < index; i++)
            {
                nodei = nodei.Next;
                if (nodei == null)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            if (nodei == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            return nodei.Value;
        }

        public string First()
        {
            if (firstNode == null)
            {
                return null;
            }
            else 
            {
                return firstNode.Value;
            }
        }

        public int IndexOf(string value)
        {
            SinglyLinkedListNode nodei = this.firstNode;

            int i = 0;
            if (firstNode == null)
            {
                return -1;
            }
            while (nodei.Value != value)
            {
                nodei = nodei.Next;
                i++;
                if (nodei == null)
                {
                    return -1;
                }
            }
            return i;
        }

        public bool IsSorted()
        {
            SinglyLinkedListNode nodei = this.firstNode;
            if (this.firstNode == null || this.firstNode.Next == null || nodei.Value == nodei.Next.Value)
            {
                return true;
            }
            else
            {
                while(nodei.Next != null)
                {
                    if (nodei > nodei.Next)
                    {
                        return false;
                    }
                    nodei = nodei.Next;
                }
                return true;
            }
        }

        // HINT 1: You can extract this functionality (finding the last item in the list) from a method you've already written!
        // HINT 2: I suggest writing a private helper method LastNode()
        // HINT 3: If you highlight code and right click, you can use the refactor menu to extract a method for you...
        public string Last()
        {
            SinglyLinkedListNode nodei = this.firstNode;
            if (nodei == null)
            {
                return null;
            }
            else 
            {
                while (!nodei.IsLast())
                {
                    nodei = nodei.Next;
                }
                return nodei.Value;
            }
        }

        public void Remove(string value)
        {
            SinglyLinkedListNode nodei = this.firstNode;
            if (nodei.Value == value)
            {
                firstNode = nodei.Next;
            }

            int indexLocation = this.IndexOf(value);
            if (indexLocation == -1)
            {
                return;
            }
            for (int i = 0; i < indexLocation -1; i++)
            {
                nodei = nodei.Next;
            }
            nodei.Next = nodei.Next.Next;
        }

        private void Swap(SinglyLinkedListNode prevPrev, SinglyLinkedListNode prev, SinglyLinkedListNode curr)
        {
            var temp = prev;
            prev.Next = curr.Next;
            curr.Next = temp;
            if (firstNode == temp)
            {
                firstNode = curr;
            }
            else
            {
                prevPrev.Next = curr;
            }
        }

        private SinglyLinkedListNode NodeAt(int index)
        {
            var result = firstNode;
            for (int i = 0; i < index; i++)
            {
                result = result.Next;
            }
            return result;
        }

        public void Sort()
        {
            if (firstNode == null || firstNode.Next == null)
            {
                return;
            }
            for (int i = 0; i < this.Count(); i++)
            {
                SinglyLinkedListNode lowest = NodeAt(i);
                for (int j = i+1; j < this.Count(); j++)
                {
                    if (lowest > NodeAt(j))
                    {
                        lowest = NodeAt(j);
                    }
                }
                if (lowest != NodeAt(i))
                {
                    Swap(NodeAt(i - 1), NodeAt(i), lowest);
                }
            }
        }

        public string[] ToArray()
        {
            string[] badThings = new string[] { ",", " ", "{", "}", "\"" };
            string list = ToString();
            string[] words = list.Split(badThings, StringSplitOptions.RemoveEmptyEntries);

            return words;

        }

        public override string ToString()
        {
            SinglyLinkedListNode nodei = this.firstNode;
            StringBuilder builderOfStrings = new StringBuilder();
            builderOfStrings.Append("{ ");
            if (nodei == null)
            {
                builderOfStrings.Append("}");
                return builderOfStrings.ToString();
            }
            while (!nodei.IsLast())
            {
                builderOfStrings.Append("\"" + nodei.Value + "\", ");
                nodei = nodei.Next;
            }
            builderOfStrings.Append("\"" + nodei.Value + "\" }");
            return builderOfStrings.ToString();
        }
    }
}
