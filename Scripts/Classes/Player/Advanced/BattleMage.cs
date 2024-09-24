using System.Collections.Generic;

namespace Classes
{
    public class BattleMage : PlayerAdvancedClass
    {
        public BattleMage()
        {
            prerequisites = new List<ClassList> { ClassList.Warrior, ClassList.Mage };
            identity = ClassList.BattleMage;
            CreateClass(new StatSpread(10, 20, 20, 15, 14, 14)); //advanced stat spread must be 20, 20, 15, 14, 14, 10
        }
    }
}