namespace CIV.Interfaces
{
    public class Transition
    {
        public string Label { get; set; }
        public IProcess Process { get; set; }
    }
}
