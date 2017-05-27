using System;
using System.Collections;
using System.Collections.Generic;
namespace CIV.Helpers
{
    public class RestrictionSet : ISet<String>
    {
        readonly HashSet<String> set = new HashSet<string>();

        public int Count => set.Count;

        public bool IsReadOnly => false;

        public bool Add(string item)
        {
            if (item == Const.tau)
            {
                throw new ArgumentException("Illegal restriction: tau");
            }
            return set.Add(item);
        }

        public void Clear() => set.Clear();

        public bool Contains(string item) => set.Contains(item);

        public void CopyTo(string[] array, int arrayIndex) => set.CopyTo(array, arrayIndex);

        public void ExceptWith(IEnumerable<string> other) => set.ExceptWith(other);

        public IEnumerator<string> GetEnumerator() => set.GetEnumerator();

        public void IntersectWith(IEnumerable<string> other) => set.IntersectWith(other);

        public bool IsProperSubsetOf(IEnumerable<string> other) => set.IsProperSubsetOf(other);

        public bool IsProperSupersetOf(IEnumerable<string> other) => set.IsProperSupersetOf(other);

        public bool IsSubsetOf(IEnumerable<string> other) => set.IsSubsetOf(other);

        public bool IsSupersetOf(IEnumerable<string> other) => set.IsSupersetOf(other);

        public bool Overlaps(IEnumerable<string> other) => set.Overlaps(other);

        public bool Remove(string item) => set.Remove(item);

        public bool SetEquals(IEnumerable<string> other) => set.SetEquals(other);

        public void SymmetricExceptWith(IEnumerable<string> other) => set.SymmetricExceptWith(other);

        public void UnionWith(IEnumerable<string> other) => set.UnionWith(other);

        void ICollection<string>.Add(string item) => set.Add(item);

        IEnumerator IEnumerable.GetEnumerator() => set.GetEnumerator();
    }
}
