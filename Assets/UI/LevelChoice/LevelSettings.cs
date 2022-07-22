using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "Setting/Level Settings")]
public class LevelSettings : ScriptableObject
{
    // TODO Améliorer (pour afficher/cacher le cadenas)
    public bool world1Complete = false;
    public bool world2Complete = false;
    public bool world3Complete = false;
    public bool world4Complete = false;

}
