
namespace Classes{
    public class Warrior : PlayerBaseClass{
        public Warrior()
        {
            identity = ClassList.Warrior;
            CreateClass(new StatSpread(10,15,5,10,12,7)); //base class needs stats of 15, 12, 10, 10, 7, 5
        }
        

    }
}