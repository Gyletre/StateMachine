using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Classes{
    public class PlayerClassManager{
        public int totalLevel { get; private set; }
        List<ClassList> prerequisitesMet = new List<ClassList>();
        List<Class> classes = new List<Class>();
        

        public void AddClass(ClassList newClass){
            foreach (Class c in classes){
                if(!c.maxed){
                    GD.Print("You need to max out current classes before adding a new one.");
                    return;
                }
            }
            if (prerequisitesMet.Contains(newClass))
            {
                GD.Print("Cannot add " + GetEnumName(newClass) + " class, because player already has it.");
                return;
            }
            Class classToAdd = GetClassFromTag(newClass);
            
            if(classToAdd is PlayerAdvancedClass pac){
                foreach(ClassList prerequisite in pac.prerequisites){
                    if(!prerequisitesMet.Contains(prerequisite)){
                        GD.Print("Class prerequisites not met.");
                        return;
                    }
                }
            }
            classes.Add(classToAdd);
            prerequisitesMet.Add(classToAdd.identity);
            totalLevel++;
        }

        private static string GetEnumName(ClassList newClass)
        {
            return Enum.GetName(newClass.GetType(), newClass);
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
        private Class GetClassFromTag(ClassList tag){
		switch(tag){
			case ClassList.Mage:
				return new Mage();
			case ClassList.Warrior:
				return new Warrior();
			case ClassList.Cleric:
				return new Cleric();
			case ClassList.Archer:
				return new Archer();
			case ClassList.Sniper:
				return new Sniper();
			case ClassList.Paladin:
				return new Paladin();
			case ClassList.BattleMage:
				return new BattleMage();
			case ClassList.Mystic:
				return new Mystic();

		
		}
		return new Class();
		
	}
    }
}