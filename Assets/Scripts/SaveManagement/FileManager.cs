using System;
using System.IO;
using UnityEngine;

public static class FileManager
{
    public static void Write(string fileName, string json)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        try
        {
            File.WriteAllText(path, json);
            Debug.LogFormat("Writing to {0}", path);
        }
        catch
        {
            Debug.LogErrorFormat("Could not write to path: \"{0}\"", path);
        }
    }

    public static void Read(string fileName, out string json)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        json = "";

        try
        {
            json = File.ReadAllText(path);
        }
        catch
        {
            Debug.LogErrorFormat("Could not read path: \"{0}\"", path);
        }
    }
}
