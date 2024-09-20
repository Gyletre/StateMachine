using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Classes{
    public class PlayerClassManager{
        public int totalLevel { get; private set; }
        List<ClassList> prerequisitesMet = new List<ClassList>();
        List<Class> classes = new List<Class>();
        

        public void AddClass(Class newClass){
            foreach (Class c in classes){
                if(!c.maxed){
                    GD.Print("You need to max out current classes before adding a new one.");
                    return;
                }
            }
            if(newClass is PlayerAdvancedClass pac){
                foreach(ClassList prerequisite in pac.prerequisites){
                    if(!prerequisitesMet.Contains(prerequisite)){
                        GD.Print("Class prerequisites not met.");
                        return;
                    }
                }
            }
            classes.Add(newClass);
            prerequisitesMet.Add(newClass.identity);
            totalLevel++;
        }
        public void GainExp(int exp)
        {
            
            Class lastClass = classes.Last();
            GD.Print("Gained " + exp + " exp! Level: "+ lastClass.level +" "+ lastClass.exp+"/"+lastClass.ExpToLvlUp());
            if(lastClass.exp + exp >= lastClass.ExpToLvlUp() && !lastClass.maxed) {
                totalLevel ++;
            }
            lastClass.GainExp(exp);
        }
        public int GetStat(StatType type)
        {
            int value = 0;
            foreach (Class c in classes)
            {
                value += c.GetStat(type);
            }
            return value;
        }
    }
}