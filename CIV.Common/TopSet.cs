using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
namespace CIV.Common
{
	/// <summary>
	/// Top set for type T.
	/// </summary>
	/// <remarks>
	/// The Top set contains all elements of type T and as such it is the 
    /// superset of any other IEnumerable<T>, never a subset.
	/// </remarks>
	public class TopSet<T> : ISet<T>
    {
        public int Count => Int32.MaxValue;

        public bool IsReadOnly => true;

        public bool Add(T item) => true;

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item) => true;

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSubsetOf(IEnumerable<T> other) => false;

        public bool IsProperSupersetOf(IEnumerable<T> other) => !(other is TopSet<T>);

        public bool IsSubsetOf(IEnumerable<T> other) => other is TopSet<T>;

        public bool IsSupersetOf(IEnumerable<T> other) => true;

        public bool Overlaps(IEnumerable<T> other)
        {
            return other.Any() ;
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public bool SetEquals(IEnumerable<T> other) => other is TopSet<T>;

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void UnionWith(IEnumerable<T> other)
        {
        }

        void ICollection<T>.Add(T item)
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
