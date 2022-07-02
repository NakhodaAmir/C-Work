using System;
using System.Collections.Generic;
using System.Diagnostics;

#region Ranked Pairing Heap
public class RankedPairingHeap<T> where T : IRankedPairingHeap<T>
{
	const int AUX_BUCKET_ARRAY_SIZE = 65;//Size of bucket array. Based on maximum rank

	RankedPairingHeapNode minRoot;//last node in root list

	int size;//size of the heap

	RankedPairingHeapNode[] aux;//auxilliary array for consolidation

	RankedPairingHeap<T> other;

	RankedPairingHeapType type;//type of heap. Min or Max

	//Mapping


	public RankedPairingHeap(int maxSize, RankedPairingHeapType type = RankedPairingHeapType.MIN)
	{
		minRoot = null;
		size = 0;
		aux = new RankedPairingHeapNode[AUX_BUCKET_ARRAY_SIZE];
		other = this;

		this.type = type;

	}

	#region Properties
	public bool IsEmpty//returns true if heap size is 0
	{
		get { return size == 0; }
	}

	public int Count//return the size of the heap
	{
		get { return size; }
	}

	public T Root//return min root of heap
	{
		get
		{
			if (size == 0) throw new ArgumentException("Heap is empty");

			return minRoot.Value;
		}
	}
	#endregion

	#region Public Methods
	public void Insert(T value)
	{
		InsertToHeap(value);
	}

	public T ExtractMin()
	{
		T minValue = ExtractMinFromHeap();

		minValue.HeapKey = null;

		return minValue;
	}

	public bool Contains(T value)
	{
		return ContainsKey(value);
	}

	public void Update(T value)
	{
		value.HeapKey.Update(value);
	}

	public void Clear()
	{
		ClearHeap();
	}
	#endregion

	#region Heap Methods
	RankedPairingHeapNode InsertToHeap(T value)//inserts value in heap
	{
		if (other != this) throw new ArgumentException("A heap cannot be used after a meld");

		RankedPairingHeapNode n = new RankedPairingHeapNode(this, value);

		if (minRoot == null)
		{
			n.RightSibling = n;
			minRoot = n;
		}
		else
		{
			n.RightSibling = minRoot.RightSibling;
			minRoot.RightSibling = n;

			if (LessOrMore(n, minRoot))
			{
				minRoot = n;
			}
		}

		size++;

		return n;
	}

	T ExtractMinFromHeap()
	{
		if (size == 0) throw new ArgumentException("Heap is empty");

		//Sever Spine

		RankedPairingHeapNode oldMinRoot = minRoot;
		RankedPairingHeapNode spine = null;

		if (minRoot.LeftChild != null)
		{
			spine = SeverSpine(minRoot.LeftChild);
			minRoot.LeftChild = null;
		}

		//One pass spine

		int maxRank = -1;
		RankedPairingHeapNode output = null;

		void HelperFunction1(ref RankedPairingHeapNode node, out RankedPairingHeapNode cur)
		{
			if (node.RightSibling == node)
			{
				cur = node;
				node = null;
			}
			else
			{
				cur = node.RightSibling;
				node.RightSibling = cur.RightSibling;
			}

			cur.RightSibling = null;
		}

		void HelperFunction2(ref RankedPairingHeapNode cur)
		{
			int rank = cur.Rank;

			RankedPairingHeapNode auxEntry = aux[rank];

			if (auxEntry == null)
			{
				aux[rank] = cur;
				if (rank > maxRank)
				{
					maxRank = rank;
				}
			}
			else
			{
				aux[rank] = null;
				cur = Link(cur, auxEntry);

				HelperFunction3(ref cur);
			}
		}

		void HelperFunction3(ref RankedPairingHeapNode cur)
		{
			if (output == null)
			{
				cur.RightSibling = cur;
				output = cur;
			}
			else
			{
				cur.RightSibling = output.RightSibling;
				output.RightSibling = cur;

				if (LessOrMore(cur, output))
				{
					output = cur;
				}
			}
		}

		while (spine != null)
		{
			HelperFunction1(ref spine, out RankedPairingHeapNode cur);

			HelperFunction2(ref cur);
		}

		//One pass old half-trees, careful to skip old minimum which is still in the root list.

		while (minRoot != null)
		{
			HelperFunction1(ref minRoot, out RankedPairingHeapNode cur);

			if (cur == oldMinRoot)
			{
				continue;
			}

			HelperFunction2(ref cur);
		}

		//Process remaining in buckets

		for (int i = 0; i <= maxRank; i++)
		{
			RankedPairingHeapNode cur = aux[i];

			if (cur != null)
			{
				aux[i] = null;

				HelperFunction3(ref cur);
			}
		}

		minRoot = output;
		size--;
		return oldMinRoot.Value;
	}

	void ClearHeap()
	{
		minRoot = null;
		size = 0;
	}

	#endregion

	#region Ranked Pairing Heap Node
	public class RankedPairingHeapNode
	{
		public RankedPairingHeap<T> Heap { get; set; }//Belonging Heap

		public T Value { get; set; }//Value

		public RankedPairingHeapNode Parent { get; set; }//Parent of this node
		public RankedPairingHeapNode LeftChild { get; set; }//Left most child of this node
		public RankedPairingHeapNode RightSibling { get; set; }//Right sibling of this node

		public int Rank { get; set; }//rank of the node


		public RankedPairingHeapNode(RankedPairingHeap<T> heap, T value)
		{
			Heap = heap;
			Value = value;

			Parent = LeftChild = RightSibling = null;

			Rank = 0;

			Value.HeapKey = this;//mapping
		}

		public void Update(T newValue)
		{
			GetOwner().Update(this, newValue);
		}

		RankedPairingHeap<T> GetOwner()
		{
			if (Heap.other != Heap)
			{
				RankedPairingHeap<T> root = Heap;

				while (root != root.other)
				{
					root = root.other;
				}

				RankedPairingHeap<T> cur = Heap;

				while (cur.other != root)
				{
					RankedPairingHeap<T> next = cur.other;
					cur.other = root;
					cur = next;
				}

				Heap = root;
			}
			return Heap;
		}
	}
	#endregion

	#region Heap Helper Methods
	void Update(RankedPairingHeapNode n, T newValue)
	{
		//int c = Compare(newValue, n.Value);

		//if( c > 0) throw new ArgumentException("Value not following " + type);

		n.Value = newValue;

		//if (c == 0) return;

		if (n.Parent == null && n.RightSibling == null) throw new ArgumentException("Invalid Handle");

		if (n.Parent == null)
		{
			if (LessOrMore(n, minRoot))
			{
				minRoot = n;
			}

			return;
		}

		RankedPairingHeapNode u = n.Parent;

		Cut(n);

		if (minRoot == null)
		{
			n.RightSibling = n;

			minRoot = n;
		}
		else
		{
			n.RightSibling = minRoot.RightSibling;
			minRoot.RightSibling = n;

			if (LessOrMore(n, minRoot))
			{
				minRoot = n;
			}
		}

		n.Rank = (n.LeftChild == null) ? 0 : n.LeftChild.Rank + 1;

		RestoreType1Ranks(u);
	}

	void Cut(RankedPairingHeapNode x)
	{
		RankedPairingHeapNode u = x.Parent;

		Debug.Assert(u != null);

		RankedPairingHeapNode y = x.RightSibling;

		if (u.LeftChild == x)
		{
			u.LeftChild = y;
		}
		else
		{
			u.RightSibling = y;
		}

		if (y != null)
		{
			y.Parent = u;
		}

		x.Parent = null;
		x.RightSibling = x;
	}

	void RestoreType1Ranks(RankedPairingHeapNode u)
	{
		while (u != null)
		{
			int leftRank = (u.LeftChild == null) ? -1 : u.LeftChild.Rank;

			if (u.Parent == null)
			{
				u.Rank = leftRank + 1;
				break;
			}

			int rightRank = (u.RightSibling == null) ? -1 : u.RightSibling.Rank;

			int k = (leftRank == rightRank) ? leftRank + 1 : Math.Max(leftRank, rightRank);

			if (k >= u.Rank)
			{
				break;
			}

			u.Rank = k;
			u = u.Parent;
		}
	}

	RankedPairingHeapNode SeverSpine(RankedPairingHeapNode x)
	{
		RankedPairingHeapNode cur = x;

		while (cur.RightSibling != null)
		{
			cur.Parent = null;

			if (cur.LeftChild == null)
			{
				cur.Rank = 0;
			}
			else
			{
				cur.Rank = cur.LeftChild.Rank + 1;
			}

			cur = cur.RightSibling;
		}

		cur.Parent = null;

		if (cur.LeftChild == null)
		{
			cur.Rank = 0;
		}
		else
		{
			cur.Rank = cur.LeftChild.Rank + 1;
		}

		cur.RightSibling = x;

		return x;
	}

	RankedPairingHeapNode Link(RankedPairingHeapNode x, RankedPairingHeapNode y)
	{
		int c = Compare(x, y);

		if (c <= 0)
		{
			y.RightSibling = x.LeftChild;

			if (x.LeftChild != null)
			{
				x.LeftChild.Parent = y;
			}

			x.LeftChild = y;
			y.Parent = x;
			x.Rank += 1;

			return x;
		}
		else
		{
			x.RightSibling = y.LeftChild;

			if (y.LeftChild != null)
			{
				y.LeftChild.Parent = x;
			}

			y.LeftChild = x;
			x.Parent = y;
			y.Rank += 1;

			return y;
		}
	}

	bool LessOrMore(RankedPairingHeapNode x, RankedPairingHeapNode y)
	{
		return Compare(x, y) < 0;
	}

	int Compare(RankedPairingHeapNode x, RankedPairingHeapNode y)
	{
		int c = x.Value.CompareTo(y.Value);

		if (type == RankedPairingHeapType.MIN)
		{
			return c;
		}
		else
		{
			return -c;
		}
	}

	int Compare(T x, T y)
	{
		int c = x.CompareTo(y);

		if (type == RankedPairingHeapType.MIN)
		{
			return c;
		}
		else
		{
			return -c;
		}
	}


	#endregion

	#region Mapping Methods

	bool ContainsKey(T value)
	{
		if (value.HeapKey != null && value.HeapKey.Heap == this)
		{
			return Equals(value.HeapKey.Value, value);
		}
		else
		{
			return false;
		}
	}
	#endregion
}
#endregion

public interface IRankedPairingHeap<T> : IComparable<T> where T : IRankedPairingHeap<T>
{
	public RankedPairingHeap<T>.RankedPairingHeapNode HeapKey { get; set; }
}

public enum RankedPairingHeapType
{
	MIN,
	MAX
}



