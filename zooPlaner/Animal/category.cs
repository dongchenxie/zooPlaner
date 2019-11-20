using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace zooPlaner
{

    class category
    {
        private List<category> subCategory;
        public string subCategoryName { set; get; }
        protected string typeName { set; get; }
        public category parent { private set; get; }
        public JObject info { private set; get; }


        public category(string typeName)
        {
            this.typeName = typeName;
            this.subCategoryName = null;
            subCategory = new List<category>();

        }
        public category(string typeName, string subCategoryName)
        {
            this.typeName = typeName;
            this.subCategoryName = subCategoryName;
            subCategory = new List<category>();
            
        }
        public category(string typeName, string subCategoryName,JObject info)
        {
            this.typeName = typeName;
            this.subCategoryName = subCategoryName;
            subCategory = new List<category>();
            this.info = info;
        }
        public void printTitle()
        {
            Console.WriteLine("====================\n" + subCategoryName + "\n====================\n");
        }
        public double getFoodWeight(double bodyWeight)
        {
            return Math.Round(bodyWeight * (double)this.getInfo("ratio"), 3);
        }
        public string getServing(double bodyWeight)
        {
            return "Serving: "+getFoodWeight(bodyWeight) + " KG " + this.getInfo("food")+"\n";
        }
        public string getInstructions()
        {
            return this.getInfo("notes") + "\n" + this.getInfo("time");
        }
        public override string ToString()
        {
            return typeName;
        }
        public JToken getInfo(string infoName)

        {
           
            if (info.GetValue(infoName) != null)
            {
                
                return info.GetValue(infoName);
            }
            if (parent != null)
            {
                return parent.getInfo(infoName);
            }
            return null;
        }
        public category getSubElement(int index)
        {
            try
            {
                return subCategory[index - 1];
            }
            catch(System.ArgumentOutOfRangeException e)
            {
                return null;
            }
        }
        public void printSubElementList()
        {
            for(int i = 0; i < subCategory.Count; i++)
            {
                Console.WriteLine((i + 1) +"."+subCategory[i]);
            }
        }
        public category newSubElement(string typeName, string subCategoryName, JObject info)
        {
            category c = new category(typeName, subCategoryName, info);
            subCategory.Add(c);
            c.parent = this;
            return c;
        }
        public category newSubElement(string typeName, string subCategoryName)
        {
            category c = new category(typeName, subCategoryName);
            subCategory.Add(c);
            c.parent = this;
            return c;
        }
        public category newSubElement(string typeName)
        {
            return newSubElement(typeName, null);
        }
        public string printCategoryToRoot()
        {
           
            string tempSubCategoryName = subCategoryName == null ? "" : "\n" + subCategoryName;
            if (parent != null)
            {
                return parent.printCategoryToRoot()+ " : " + typeName + tempSubCategoryName;

                
            }
            else
            {
                return tempSubCategoryName;
            }

           
        }
    }
}
