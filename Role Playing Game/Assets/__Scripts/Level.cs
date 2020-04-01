using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public int level;
    public int statBoostPerLevel;
    public int currentExperience = 0;
    public int levelExperience = 1000;

    public Level(int level = 1)
    {
        this.level = level;
    }

    public void IncreaseExperience(int experience)
    {
        if(currentExperience + experience >= 1000)
        {
            level++;
            currentExperience = (currentExperience + experience) - 1000;
        }
        else
        {
            currentExperience += experience;
        }
    }
}