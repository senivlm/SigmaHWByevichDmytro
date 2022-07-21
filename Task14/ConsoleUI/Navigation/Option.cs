namespace Task14
{
    internal class Option
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        private Action OptionAction;

        public Option() : this("standartName", () => Console.WriteLine("null Action")) { }
        public Option(string name, Action optionAction)
        {
            Name = name;
            OptionAction = optionAction;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
        public void Run()
        {
            OptionAction.Invoke();
        }
    }
}
