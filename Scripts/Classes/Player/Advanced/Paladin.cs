using System.Collections.Generic;

namespace Classes
{
    public class Paladin : PlayerAdvancedClass
    {
        public Paladin()
        {
            prerequisites = new List<ClassList> { ClassList.Warrior, ClassList.Cleric };
            identity = ClassList.Paladin;
            CreateClass(new StatSpread(20, 20, 15, 10, 14, 14)); //advanced stat spread must be 20, 20, 15, 14, 14, 10
        }
    }
}