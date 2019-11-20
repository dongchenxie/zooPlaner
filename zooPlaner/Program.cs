using System;
using Newtonsoft.Json.Linq;

namespace zooPlaner
{
    class Program
    {

        static category root;
        static void Main(string[] args)
        {
            root = setup();
            runPlan(root);
        }
        static void runPlan(category c)
        {
            if (c.subCategoryName == null)
            {
                Console.WriteLine("Please input the weight in KG:");
                try
                {
                    double input = double.Parse(Console.ReadLine());
                    printMealRecommendation(c, input);
                    Console.WriteLine("Do you want to try another animal? input q to quit, any other key to continue.");
                    if (Console.ReadLine() == "q")
                    {
                        System.Environment.Exit(1);
                    }
                    else
                    {
                        runPlan(root);
                    }

                }catch(System.FormatException e)

                {
                    Console.WriteLine(e.Message);
                    runPlan(c);
                }
              
            }
            else
            {
                c.printTitle();
                c.printSubElementList();
                Console.WriteLine("selection:");
                try
                {
                    int input = int.Parse(Console.ReadLine());
                    category nextNode = c.getSubElement(input);
                    if (nextNode == null) {
                        throw new System.FormatException("Input: "+input+" is out of boundary.");
                    }
                    else
                    {
                        runPlan(nextNode);
                    }
                   
                }
                catch (System.FormatException e)

                {
                    Console.WriteLine(e.Message);
                    runPlan(c);
                }
            }



        }
        static category setup()
        {

            JObject bearInfo = JObject.Parse(@"{'time':'9AM and 3PM'}");
            JObject blackBearInfo = JObject.Parse(@"{ 'ratio':0.014,'food':'berries, green vegetation, flowers, fruits, fish'}");
            JObject polarBearInfo = JObject.Parse(@"{ 'ratio':0.016,'food':'berries, fish'}");
            JObject monkeyInfo = JObject.Parse(@"{'food':'fresh fruit, vegetables, nuts, insects, berries','time':'9AM, 12PM and 5PM.'}");
            JObject squirrelMonkeyInfo = JObject.Parse(@"{ 'ratio':0.006}");
            JObject howlerMonkeyInfo = JObject.Parse(@"{ 'ratio':0.007}");
            JObject colobusMonkeyInfo = JObject.Parse(@"{ 'ratio':0.008}");
            JObject rootInfo = JObject.Parse(@"{'notes':'Keep area secure at all times.'}");
            category root = new category("root", "Mammal", rootInfo);
            category bear = root.newSubElement("Bear", "species", bearInfo);
            category monkey = root.newSubElement("Monkey", "species", monkeyInfo);
            bear.newSubElement("Blackbear", null, blackBearInfo);
            bear.newSubElement("Polarbear", null, polarBearInfo);
            monkey.newSubElement("Squirrel", null, squirrelMonkeyInfo);
            monkey.newSubElement("howler", null, howlerMonkeyInfo);
            category c = monkey.newSubElement("colobus", null, colobusMonkeyInfo);
            printMealRecommendation(c, 10.1);

            //Console.WriteLine((double)j.GetValue("ratio")*100);

            return root;
        }
        static void printMealRecommendation(category c, double weight)
        {
            string output = "";
            output += c.printCategoryToRoot();
            output += $"\nWeight: {Math.Round(weight, 3)} KG\n";
            output += c.getServing(weight);
            output += c.getInstructions();


            Console.WriteLine(output);

        }
    }
}
