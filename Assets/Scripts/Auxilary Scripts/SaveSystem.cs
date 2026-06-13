using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame(SaveGameScript savedGame)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/HoldThePlushies.savefile";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, savedGame);
        stream.Close();
    }

    public static SaveGameScript LoadGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/HoldThePlushies.savefile";
        FileStream stream = new FileStream(path, FileMode.Open);

        SaveGameScript savedGame = (SaveGameScript) formatter.Deserialize(stream);
        stream.Close();
        return savedGame;
    }
}
