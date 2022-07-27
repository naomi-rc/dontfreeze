using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "Setting/Level Settings")]
public class LevelSettings : ScriptableObject
{
    // TODO Améliorer (pour afficher/cacher le cadenas)
    public bool level1Complete = false;
    public bool level2Complete = false;
    public bool level3Complete = false;
    public bool level4Complete = false;

    public GameObject prefabBear;
    public GameObject prefabWolf;
    public GameObject prefabWisp;

    public int wispNumber;
    public int animalNumber;
    public int levelNumber;
}
