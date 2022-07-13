namespace MirJan
{
    namespace PriorityQueues
    {

        #region D-Ary Heap
        public class DaryHeap<T> where T : IDaryHeap<T>
        {
            #region Variables
            T[] heap;
            int d;
            int currentHeapCount;

            readonly DaryHeapType type;
            #endregion

            #region Properties
            public int Count
            {
                get { return currentHeapCount; }
            }

            public bool IsEmpty
            {
                get { return currentHeapCount == 0; }
            }

            public T Peek
            {
                get { return heap[0]; }
            }
            #endregion

            #region Constructor
            public DaryHeap(int maxSize, int d = 2, DaryHeapType type = DaryHeapType.MIN)
            {
                this.d = (d >= 2) ? d : throw new ArgumentException("Degree cannot be less than 2");
                this.type = type;

                currentHeapCount = 0;

                heap = new T[maxSize];
            }
            #endregion

            #region Public Methods
            public void Insert(T item)
            {
                item.HeapIndex = currentHeapCount;
                heap[currentHeapCount] = item;
                SortUp(item);
                currentHeapCount++;
            }

            public T ExtractMin()
            {
                T root = heap[0];
                currentHeapCount--;
                heap[0] = heap[currentHeapCount];
                heap[0].HeapIndex = 0;
                SortDown(heap[0]);
                return root;
            }

            public void Update(T item)
            {
                if (type == DaryHeapType.MIN)
                {
                    SortUp(item);
                }
                else
                {
                    SortDown(item);
                }
            }

            public bool Contains(T item)
            {
                return Equals(heap[item.HeapIndex], item);
            }
            #endregion

            #region Private Methods
            void SortDown(T item)
            {
                while (true)
                {
                    int smallestChildIndex = GetSmallestChildren(item.HeapIndex);

                    if (smallestChildIndex == -1 || Compare(heap[item.HeapIndex], heap[smallestChildIndex]) < 0) return;

                    Swap(heap[smallestChildIndex], item);
                }
            }

            void SortUp(T item)
            {
                int parentIndex = GetParent(item.HeapIndex);

                while (item.HeapIndex > 0 && Compare(heap[item.HeapIndex], heap[parentIndex]) < 0)
                {
                    Swap(heap[parentIndex], item);

                    parentIndex = GetParent(item.HeapIndex);
                }
            }

            void Swap(T itemA, T itemB)
            {
                heap[itemA.HeapIndex] = itemB;
                heap[itemB.HeapIndex] = itemA;
                int itemAIndex = itemA.HeapIndex;
                itemA.HeapIndex = itemB.HeapIndex;
                itemB.HeapIndex = itemAIndex;
            }

            int GetParent(int index)
            {
                return (index - 1) / d;
            }

            int GetSmallestChildren(int index)
            {
                int smallestChildIndex = index * d + 1;

                if (smallestChildIndex >= currentHeapCount) return -1;

                for (int i = 2; i <= d; i++)
                {
                    int siblingIndex = index * d + i;

                    if (siblingIndex < currentHeapCount && Compare(heap[siblingIndex], heap[smallestChildIndex]) < 0)
                    {
                        smallestChildIndex = siblingIndex;
                    }
                }

                return smallestChildIndex;
            }


            int Compare(T itemA, T itemB)
            {
                int c = itemA.CompareTo(itemB);

                if (type == DaryHeapType.MIN)
                {
                    return c;
                }
                else
                {
                    return -c;
                }
            }
            #endregion
        }
        #endregion

        #region D-Ary Heap Interface
        public interface IDaryHeap<T> : IComparable<T>
        {
            int HeapIndex { get; set; }
        }
        #endregion

        #region Enum
        public enum DaryHeapType
        {
            MIN,
            MAX
        }
        #endregion

    }
}
