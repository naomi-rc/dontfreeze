using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Level level;
    public ParticleSystem snowSystem;

    void Start()
    {
        Print();
        if (level.snow)
            snowSystem.Play();
        else
            snowSystem.Stop();
    }

    public void Print()
    {
        Debug.Log($"Level {level.level} | Level name : {level.levelName} | Number of enemies: {level.numberOfEnemies} | Time of day : {level.timeSetting} | Snow : {level.snow} ");
    }
}
