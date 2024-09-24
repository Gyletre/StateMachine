using System.Collections.Generic;

namespace Classes
{
    public class Mystic : PlayerAdvancedClass
    {
        public Mystic()
        {
            identity = ClassList.Mystic;
            prerequisites = new List<ClassList> { ClassList.Mage, ClassList.Cleric };
            CreateClass(new StatSpread(14, 10, 20, 20, 14, 15)); //advanced stat spread must be 20, 20, 15, 14, 14, 10
        }
    }
}