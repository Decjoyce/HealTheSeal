using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{

    public static GameManagement instance;

    [SerializeField] AndroidService am;
    [SerializeField] SealManager sm;
    [SerializeField] GameData gd;

    public bool dev_mode; //{ get; private set; }

    public List<SealScheduleItem> seal_feed_schedules = new List<SealScheduleItem>();
    public List<SealScheduleItem> seal_heal_schedules = new List<SealScheduleItem>();

    public float feed_delay = 4, heal_delay = 6;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of GAMEMANAGEMENT found");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(System.DateTime.Now + " / " + System.DateTime.Today);
        //SetupSchedule();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTimeSchedule();
    }

    #region Time Stuff

    public void SetupSchedule()
    {
        foreach (Seal seal in gd.gd_sealdata.player_seals)
        {
            Debug.Log("T - inside foreach");
            if (!seal.can_feed)
            {
                AddItemToSchedule_Feed(seal.next_time_feed, seal);
                Debug.Log("T - inside feed");
            }
            if (!seal.can_heal)
            {
                AddItemToSchedule_Heal(seal.next_time_heal, seal);
                Debug.Log("T - inside heal");
            }

        }
       //CheckTimeSchedule();
    }

    void CheckTimeSchedule()
    {
        if (seal_feed_schedules.Count > 0 && System.DateTime.Now >= seal_feed_schedules[0].date)
        {
            seal_feed_schedules[0].seal.can_feed = true;
            seal_feed_schedules.RemoveAt(0);
        }

        if (seal_heal_schedules.Count > 0 && System.DateTime.Now >= seal_heal_schedules[0].date)
        {
            seal_heal_schedules[0].seal.can_heal = true;
            seal_heal_schedules.RemoveAt(0);
        }
    }

    public void AddItemToSchedule_Feed(string date, Seal seal)
    {
        seal_feed_schedules.Add(new SealScheduleItem(date, seal));

        seal_feed_schedules.Sort((x, y) => x.date.CompareTo(y.date));
    }

    public void AddItemToSchedule_Heal(string date, Seal seal)
    {
        seal_heal_schedules.Add(new SealScheduleItem(date, seal));

        seal_heal_schedules.Sort((x, y) => x.date.CompareTo(y.date));
    }

    #endregion

    #region SceneStuff
    public void LoadScene(string scene_name = "", int index = -1)
    {
        if (scene_name != "")
            SceneManager.LoadScene(scene_name);
        else if(index != -1)
            SceneManager.LoadScene(index);
        else
        {
            Debug.LogWarning("Scene Name: " + scene_name + " | Scene Index: " + index + " does not exist");
            return;
        }
        GameData.instance.SaveAllData();
    }

    public void LoadHabitatScene()
    {
        LoadScene("HabitatScene");
    }

    public void EnterDebugMode()
    {
        dev_mode = true;
    }
    #endregion
}

[System.Serializable]
public class SealScheduleItem
{
    public System.DateTime date;
    public Seal seal;
    public string string_date;

    public SealScheduleItem(string _date, Seal _seal)
    {

        date = System.DateTime.Parse(_date);
        seal = _seal;
        //string_date = date.ToString();
    }
}