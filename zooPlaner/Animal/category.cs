using System;
using System.Collections.Generic;
using System.Text;

namespace zooPlaner.Animal
{

    class category
    {
        public List<category> subCategory { private set; get; }
        public string subCategoryName { set; get; }
        protected string typeName { set; get; }
        public category parent { private set; get; }
        public string _food;
        public string food
        {
            set
            {
                _food = value;
            }
            get
            {
                if (_food == null)
                {
                    return parent.food;
                }
                else
                {

                }
               
            }
        }
        public double ratio { set; get; }
        public string time { set; get; }


        public category(string typeName)
        {
            this.typeName = typeName;
            this.subCategoryName = null;

        }
        public category(string typeName, string subCategoryName)
        {
            this.typeName = typeName;
            this.subCategoryName = subCategoryName;

        }
        public override string ToString()
        {
            return typeName;
        }
        public category newSubElement(string typeName, string subCategoryName)
        {
            category c = new category(typeName, subCategoryName);
            subCategory.Add(c);
            return c;
        }
        public category newSubElement(string typeName)
        {
            return newSubElement(typeName, null);
        }
        public void printCategoryToRoot()
        {
            string tempSubCategoryName = subCategoryName == null ? "" : "\n" + subCategoryName;
            if (parent != null)
            {
                parent.printCategoryToRoot();

                Console.WriteLine(" : " + typeName + tempSubCategoryName);
            }

            Console.WriteLine(tempSubCategoryName);
        }
    }
}
