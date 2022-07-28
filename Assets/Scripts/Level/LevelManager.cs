using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelSequence levelSequence;
    public LevelSettings levelSettings;

    public static int currentLevelIndex = -1;

    //public Level currentLevel { get { return levelSequence.levels[currentLevelIndex < 0? 0 : currentLevelIndex]; } }
    public Level currentLevel;
    private ParticleSystem snowSystem;
    
    void Start()
    {
        currentLevelIndex = levelSettings.levelNumber - 1;
        currentLevel = levelSequence.levels[currentLevelIndex];

        GameObject locationExitSelection =  GameObject.FindGameObjectWithTag("SafehouseLocationExit");
        snowSystem = GameObject.FindGameObjectWithTag("SnowSystem").GetComponent<ParticleSystem>();
        if (currentLevel.snow && !locationExitSelection)
            snowSystem.Play();
        else
            snowSystem.Stop();

        if (locationExitSelection != null)
        {
            currentLevelIndex++;
           locationExitSelection.GetComponent<Location>().SetNextLocation(currentLevel.location);
        }

        Print();
    }

    public void Print()
    {
        Debug.Log($"Current level index {currentLevelIndex}");
        Debug.Log($"Level {currentLevel.level} | Level name : {currentLevel.levelName} | Number of enemies: {currentLevel.numberOfEnemies} | Time of day : {currentLevel.timeSetting} | Snow : {currentLevel.snow} ");
    }


    public Level GetCurrentLevel()
    {
        return currentLevel;
    }

    public void SetCurrentLevel(int level)
    {
        currentLevelIndex = level;
    }

    public Level[] GetLevels()
    {
        return levelSequence.levels;
    }
    
}
