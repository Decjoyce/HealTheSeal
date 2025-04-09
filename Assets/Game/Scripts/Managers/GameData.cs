using System.IO;
using UnityEngine;
using TMPro;
using System.Linq;

/// Temp
[System.Serializable]
public struct SealDude
{
    public bool beenInit;

    public string name;

    public float health;
    public float mood;
    public float hunger;

    public void ResetData()
    {
        health = 0;
        mood = 0;
        hunger = 0;
    }
}
/// Temp

[System.Serializable]
public struct GameData_SealData
{
    public bool beenInit;

    public Seal[] player_seals;

    public void save_seal_data(Seal[] seals_to_save)
    {
        player_seals = seals_to_save;
    }

    public void ResetData()
    {
        player_seals = null;
    }
}

[System.Serializable]
public struct GameData_Settings
{
    public bool beenInit;

    public float volume;

    public void ResetData()
    {
        volume = 0f;
    }
}

[System.Serializable]
public struct GameData_Statistics
{
    public bool beenInit;

    public int seals_rescued;
    public int minigame_play_amount;
    public int minigame_feed_amount;
    public int minigame_heal_amount;

    public void ResetData()
    {
        seals_rescued = 0;
        minigame_play_amount = 0;
        minigame_feed_amount = 0;
        minigame_heal_amount = 0;
    }
}

public class GameData : MonoBehaviour
{
    public static GameData instance;

    public delegate void Event_OnLoadGameData_SealData();
    public static event Event_OnLoadGameData_SealData OnLoadGameData_SealData;

    public delegate void Event_OnSaveGameData_SealData();
    public static event Event_OnSaveGameData_SealData OnSaveGameData_SealData;

    public delegate void Event_OnLoadGameData_Settings();
    public static event Event_OnLoadGameData_Settings OnLoadGameData_Settings;

    public delegate void Event_OnSaveGameData_Settings();
    public static event Event_OnSaveGameData_Settings OnSaveGameData_Settings;

    public delegate void Event_OnLoadGameData_Statistics();
    public static event Event_OnLoadGameData_Statistics OnLoadGameData_Statistics;

    public delegate void Event_OnSaveGameData_Statistics();
    public static event Event_OnSaveGameData_Statistics OnSaveGameData_Statistics;

    public GameData_SealData gd_sealdata;
    public GameData_Settings gd_settings;
    public GameData_Statistics gd_statistics;

    string file_path;
    const string FILE_NAME_SEALDATA = "sealdata_save.json";
    const string FILE_NAME_SETTINGS = "settings_save.json";
    const string FILE_NAME_STATISTICS = "statistics_save.json";

    [Header("Debugging")]
    [SerializeField] bool rest_data_on_load;

    public void LoadGameData_SealData()
    {
        if (File.Exists(file_path + "/" + FILE_NAME_SEALDATA))
        {
            string loadedJson = File.ReadAllText(file_path + "/" + FILE_NAME_SEALDATA);
            gd_sealdata = JsonUtility.FromJson<GameData_SealData>(loadedJson);

            if(gd_sealdata.beenInit)
             SealManager.Instance.seals = gd_sealdata.player_seals.ToList();

            Debug.Log("SEAL DATA LOADED SUCCESSFULLY");
        }
        else
        {
            SaveGameData_SealData();

            Debug.LogWarning("ERROR: SEAL DATA not found");
        }
        OnLoadGameData_SealData?.Invoke();
    }

    public void SaveGameData_SealData()
    {
        OnSaveGameData_SealData?.Invoke();

        gd_sealdata.beenInit = true;

        gd_sealdata.save_seal_data(SealManager.Instance.seals.ToArray());

        string gameStatusJson = JsonUtility.ToJson(gd_sealdata);

        File.WriteAllText(file_path + "/" + FILE_NAME_SEALDATA, gameStatusJson);
        Debug.Log("SEAL DATA file created and saved");
    }

    public void ResetGameData_SealData()
    {
        if (File.Exists(file_path + "/" + FILE_NAME_SEALDATA))
        {
            File.Delete(file_path + "/" + FILE_NAME_SEALDATA);
        }
    }

    public void LoadGameData_Settings()
    {
        if (File.Exists(file_path + "/" + FILE_NAME_SETTINGS))
        {
            string loadedJson = File.ReadAllText(file_path + "/" + FILE_NAME_SETTINGS);
            gd_settings = JsonUtility.FromJson<GameData_Settings>(loadedJson);
            Debug.Log("GAME SETTINGS LOADED SUCCESSFULLY");
        }
        else
        {
            SaveGameData_Settings();

            Debug.LogWarning("ERROR: GAME SETTINGS not found");
        }
        OnLoadGameData_Settings?.Invoke();
    }

    public void SaveGameData_Settings()
    {
        OnSaveGameData_Settings?.Invoke();

        gd_settings.beenInit = true;

        string gameStatusJson = JsonUtility.ToJson(gd_settings);

        File.WriteAllText(file_path + "/" + FILE_NAME_SETTINGS, gameStatusJson);
        Debug.Log("GAME SETTINGS file created and saved");
    }

    public void ResetGameData_Settings()
    {
        if (File.Exists(file_path + "/" + FILE_NAME_SETTINGS))
        {
            File.Delete(file_path + "/" + FILE_NAME_SETTINGS);
        }
    }

    public void LoadGameData_Statistics()
    {
        if (File.Exists(file_path + "/" + FILE_NAME_STATISTICS))
        {
            string loadedJson = File.ReadAllText(file_path + "/" + FILE_NAME_STATISTICS);
            gd_statistics = JsonUtility.FromJson<GameData_Statistics>(loadedJson);
            Debug.Log("GAMEPLAY STATISTICS LOADED SUCCESSFULLY");
        }
        else
        {
            SaveGameData_Statistics();

            Debug.LogWarning("ERROR: GAMEPLAY STATISTICS not found");
        }
        OnLoadGameData_Statistics?.Invoke();
    }

    public void SaveGameData_Statistics()
    {
        OnSaveGameData_Statistics?.Invoke();

        gd_statistics.beenInit = true;

        string gameStatusJson = JsonUtility.ToJson(gd_statistics);

        File.WriteAllText(file_path + "/" + FILE_NAME_STATISTICS, gameStatusJson);
        Debug.Log("GAMEPLAY STATISTICS file created and saved");
    }

    public void ResetGameData_Statistics()
    {
        if (File.Exists(file_path + "/" + FILE_NAME_STATISTICS))
        {
            File.Delete(file_path + "/" + FILE_NAME_STATISTICS);
        }
    }

    public void SaveAllData()
    {
        SaveGameData_SealData();
        SaveGameData_Settings();
        SaveGameData_Statistics();
    }

    public void LoadAllData()
    {
        LoadGameData_SealData();
        LoadGameData_Settings();
        LoadGameData_Statistics();
    }

    void ResetAllData()
    {
        Debug.Log("hello its me");
        ResetGameData_SealData();
        ResetGameData_Settings();
        ResetGameData_Statistics();
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("ERROR: More than one instance of GAMEDATA found!");
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        //Initial Loading of Data
        file_path = Application.persistentDataPath;
        gd_sealdata = new GameData_SealData();
        gd_settings = new GameData_Settings();
        gd_statistics = new GameData_Statistics();
        Debug.Log(file_path);

        //Resets data
        if (GameManagement.instance.dev_mode && rest_data_on_load)
            ResetAllData();
        else
            LoadAllData();
    }

    private void OnApplicationQuit()
    {
        SaveAllData();
    }
}
