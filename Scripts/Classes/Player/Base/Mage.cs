
namespace Classes{
    public class Mage : PlayerBaseClass{
        public Mage()
        {
            identity = ClassList.Mage;
            CreateClass(new StatSpread(7,5,15,12,10,10)); //base class needs stats of 15, 12, 10, 10, 7, 5
        }
        

    }
}