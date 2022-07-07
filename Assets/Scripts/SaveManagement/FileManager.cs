using System;
using System.IO;
using UnityEngine;

public static class FileManager
{
    public static void Write(string json)
    {
        string path = Path.Combine(Application.persistentDataPath, "gamesave.frz");

        try
        {
            File.WriteAllText(path, json);
            Debug.LogFormat("Writing to {0}", path);
        }
        catch
        {
            Debug.LogError("Error writing to file");
        }
    }

    public static void Read(out string json)
    {
        string path = Path.Combine(Application.persistentDataPath, "gamesave.frz");

        json = "";

        try
        {
            json = File.ReadAllText(path);
        }
        catch
        {
            Debug.LogError("Error reading file");
        }
    }
}
