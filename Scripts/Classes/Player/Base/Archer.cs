

namespace Classes
{
    public class Archer : PlayerBaseClass
    {
        public Archer()
        {
            identity = ClassList.Archer;
            CreateClass(new StatSpread(10, 15, 5, 12, 7, 10)); //base class needs stats of 15, 12, 10, 10, 7, 5
        }


    }
}