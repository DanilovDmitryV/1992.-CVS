using System;
using System.Collections.Generic;

namespace _1992.CVS
{
    public class Program
    {
        private static void Main()
        {

        }

        public class CloneVersionSystem
        {
            private List<Clone> cloneList;
            public CloneVersionSystem()
            {
                cloneList = new List<Clone>() { new Clone() };
            }

            public string Execute(string query)
            {
                string[] temp = query.Split(' ');
                var command = temp[0];
                var cloneN = int.Parse(temp[1]);
                int programmN = 0;
                if (temp.Length > 2) programmN = int.Parse(temp[2]);
                Clone clone = cloneList[cloneN - 1];
                switch (command)
                {
                    case "learn":
                        clone.Learn(programmN);
                        return null;
                    case "rollback":
                        clone.Rollback();
                        return null;
                    case "relearn":
                        clone.Relearn();
                        return null;
                    case "clone":
                        cloneList.Add(new Clone(new LinkedStack(clone.CopyHeadL()),
                                                new LinkedStack(clone.CopyHeadR())));
                        return null;
                    case "check":
                        return clone.Check();
                    default:
                        break;
                }
                return null;
            }

            public class Clone
            {
                private LinkedStack learned;
                private LinkedStack rollback;

                public Clone()
                {
                    learned = new LinkedStack();
                    rollback = new LinkedStack();
                }

                public Clone(LinkedStack clonelearn, LinkedStack clonerollback)
                {
                    learned = clonelearn;
                    rollback = clonerollback;
                }

                public void Learn(int programm)
                {
                    learned.Push(programm);
                    rollback = new LinkedStack();
                }

                public void Rollback()
                {
                    if (!learned.IsEmpty)
                        rollback.Push(learned.Pop());
                }

                public void Relearn()
                {
                    if (!rollback.IsEmpty)
                        learned.Push(rollback.Pop());
                }

                public string Check()
                {
                    if (learned.IsEmpty)
                        return "basic";
                    else
                        return learned.Peek().ToString();
                }

                public StackItem CopyHeadL()
                {
                    return learned.CopyHead();
                }

                public StackItem CopyHeadR()
                {
                    return rollback.CopyHead();
                }
            }

            public class StackItem
            {
                public int Value { get; set; }
                public StackItem Next { get; set; }
            }

            public class LinkedStack
            {
                StackItem head;

                public LinkedStack()
                {
                    head = null;
                }

                public LinkedStack(StackItem item)
                {
                    head = item;
                }

                public bool IsEmpty
                {
                    get { return head == null; }
                }
                public void Push(int value)
                {
                    var item = new StackItem { Value = value, Next = head };
                    head = item;
                }

                public int Pop()
                {
                    if (head == null) throw new InvalidOperationException();
                    var item = head;
                    head = head.Next;
                    return item.Value;
                }

                public int Peek()
                {
                    return head.Value;
                }

                public StackItem CopyHead()
                {
                    return head;
                }
            }
        }
    }

}
