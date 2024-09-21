using System.Collections.Generic;

namespace Classes{
    public class Sniper : PlayerAdvancedClass
    {
        public Sniper()
        {
            prerequisites = new List<ClassList>{ClassList.Warrior, ClassList.Archer};
            identity = ClassList.Sniper;
            CreateClass(new StatSpread(15,20,10,20,14,14)); //advanced stat spread must be 20, 20, 15, 14, 14, 10
        }
    }
}