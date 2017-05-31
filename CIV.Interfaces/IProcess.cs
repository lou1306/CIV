using System.Collections.Generic;

namespace CIV.Interfaces
{
    public interface IProcess
    {
        IEnumerable<Transition> GetTransitions();

        string ToString();
    }

    public class ProcessComparer : IEqualityComparer<IProcess>
    {
        public bool Equals(IProcess x, IProcess y)
        {
            return x.ToString() == y.ToString();
        }

        public int GetHashCode(IProcess obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
