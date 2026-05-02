using System;
using System.IO;
using UnityEngine;

public abstract class WaveManager
{
    public static void SaveJSONToFile(string json, string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName, false))
        {
            writer.Write(json);
            writer.Close();
        }
    }

    public static string LoadJSONFromFile(string fileName)
    {
        string json;
        using (StreamReader reader = new StreamReader(fileName))
        {
            json = reader.ReadToEnd();
            reader.Close();
        }
        return json;
    }
}
