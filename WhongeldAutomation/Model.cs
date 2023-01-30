namespace WhongeldAutomation
{

    public class ActionsItem
    {
        public string action { get; set; }
        public List<string> args { get; set; }
    }

    public class PagesItem
    {
        public string Name { get; set; }
        public List<ActionsItem> Actions { get; set; }
    }

    public class Root
    {
        public List<PagesItem> pages { get; set; }
    }

    public class Operation
    {
        public string Action { get; set; }
        public Tuple<string, string> Args { get; set; }
    }
}
