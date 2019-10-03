namespace Data.GOAP
{
    public class State
    {
        private readonly string _parameter;
        private readonly bool _value;

        public State(string parameter, bool value)
        {
            _parameter = parameter;
            _value = value;
        }

        public string Parameter => _parameter;

        public bool Value => _value;
    }
}