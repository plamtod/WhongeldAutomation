// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using WhongeldAutomation;

//string modelString = File.ReadAllText("mizueran.json");
string modelString = File.ReadAllText("sns.json");
var model =
                JsonConvert.DeserializeObject<Root>(modelString);
var x = model.pages.Where(p => p.Enabled).SelectMany(x => x.Actions)

    .Select(x => new Operation { Action = x.action, Args = Tuple.Create<string, string>(x.args[0], x.args[1]) }).ToArray();

var parser = new Tools();
parser.Execute(x);
Console.Read();

