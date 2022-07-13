using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    public int level;
    public string levelName;
    public int numberOfEnemies;
    public bool snow;
    [SerializeField, Range(1,10)] int levelDifficulty;
    public int difficulty
    {
        get { return levelDifficulty; }
        set { levelDifficulty = Mathf.Clamp(value, 1, 10); }
    }

    public Texture2D icon;
    public TimeSetting timeSetting;
    public enum TimeSetting
    {
        DEFAULT,
        MORNING,
        NOON,
        EVENING,
        NIGHT,
        TIMEBASED
    }

    
}
