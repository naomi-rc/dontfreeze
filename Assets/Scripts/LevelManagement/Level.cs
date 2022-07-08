using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // Script pour définir les paramètres d'un niveau
    private int levelNumber; // Equal world number
    private bool isComplete;
    private string difficulty;
    private int minEnnemyNumber;
    private int maxEnnemyNumber;

    private string skybox;
    private string backgroundMusic;

    private const int minEasy = 10;
    private const int minNormal = 15;
    private const int minHard = 20;

    public void setDifficulty(string difficulty)
    {
        this.difficulty = difficulty;
        switch (difficulty) 
        {
            case "Easy":
                setMinEnnemyNumber(minEasy);
                break;
            case "Normal":
                setMinEnnemyNumber(minNormal);
                break;
            case "Hard":
                setMinEnnemyNumber(minHard);
                break;
        }
    }

    private void setLevelNumber(int number)
    {
        levelNumber = number;
    }

    public void setMinEnnemyNumber(int min)
    {
        minEnnemyNumber = min;
    }

    public void setMaxEnnemyNumber(int max)
    {
        maxEnnemyNumber = max;
    }

    public void setSkybox(string skybox)
    {
        this.skybox = skybox;
    }

    public void setBackgroundMusic(string music)
    {
        backgroundMusic = music;
    }

    public void setCompletion(bool value)
    {
        isComplete = value;
    }

    public void createLevel(int levelNumber, string difficulty, int maxEnnemyNumber, string skybox, string backgroundMusic)
    {
        setLevelNumber(levelNumber);
        setDifficulty(difficulty);
        setMaxEnnemyNumber(maxEnnemyNumber);
        setSkybox(skybox);
        setBackgroundMusic(backgroundMusic);
    }
}
