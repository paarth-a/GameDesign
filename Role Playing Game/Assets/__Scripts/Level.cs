using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public int level;
    public int statBoostPerLevel;
    public int currentExperience = 0;
    public int levelExperience = 1000;
    public float[] baseValues;

    public Level(int level = 1, params float[] baseValues)
    {
        this.level = level;
        this.baseValues = baseValues;
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

        Player.S.speed = (level - 1) * statBoostPerLevel + baseValues[0];
        Player.S.health = (level - 1) * statBoostPerLevel + baseValues[1];
        Player.S.defence = (level - 1) * statBoostPerLevel + baseValues[2];
        Player.S.attack = (level - 1) * statBoostPerLevel + baseValues[3];
    }
}