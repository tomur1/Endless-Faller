using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaverLoader
{

    public static void Save(int highscoreToSave)
    {
        Save(Application.persistentDataPath + "/save.game", highscoreToSave);
    }

    public static void Save(string path, int highscoreToSave)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(path, FileMode.Create);

        formatter.Serialize(fs, highscoreToSave);
        fs.Close();
    }

    public static int Load()
    {
        return Load(Application.persistentDataPath + "/save.game");
    }

    public static int Load(string path)
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            int highscore = (int) formatter.Deserialize(fs);
            fs.Close();

            return highscore;
        }
        else
        {
            Debug.LogError("Save file does not exists");
            return 0;
        }
        
        
    }

    public static void SaveStartingValues(Vector3 initialPlayerPosition, float initialPlatformSpeed)
    {
        InitialPreferences initialPreferences = new InitialPreferences(initialPlayerPosition, initialPlatformSpeed);
        string json = JsonUtility.ToJson(initialPreferences);
        File.WriteAllText("InitialValues.json", json);
    }

    public static InitialPreferences LoadStartingValues()
    {
        string json = File.ReadAllText("InitialValues.json");
        InitialPreferences initialPreferences = JsonUtility.FromJson<InitialPreferences>(json);
        return initialPreferences;
    }

    public static bool InitialValuesExist()
    {
        return File.Exists("InitialValues.json");
    }


}
