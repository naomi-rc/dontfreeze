using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Level level;
    //public SkyboxModifierHandler skyboxModifierHandler;
    void Start()
    {
        Print();
    }

    public void Print()
    {
        Debug.Log($"Level {level.level} | Level name : {level.levelName} | Number of enemies: {level.numberOfEnemies} | Time of day : {level.timeSetting} | Snow : {level.snow} ");
    }
}
