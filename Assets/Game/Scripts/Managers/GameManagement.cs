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

    public bool universal_time_schedule;
    public a_ScheduleKey[] a_feed_schedule = new a_ScheduleKey[4];
    public a_ScheduleKey[] a_heal_schedule = new a_ScheduleKey[2];

    public List<b_SealScheduleItem> b_seal_feed_schedules = new List<b_SealScheduleItem>();
    public List<b_SealScheduleItem> b_seal_heal_schedules = new List<b_SealScheduleItem>();

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
        //a_SetupSchedule();
        //b_SetupSchedule();
    }

    // Update is called once per frame
    void Update()
    {
        //if (universal_time_schedule)
        //a_CheckTimeSchedule();
        //else
        //{
        b_CheckTimeSchedule();
        //}
    }

    #region Time Stuff (Universal - A)

    public void a_SetupSchedule()
    {
        //Feed Times
        a_feed_schedule[0] = new a_ScheduleKey(false, System.DateTime.Today.AddHours(8f));
        a_feed_schedule[1] = new a_ScheduleKey(false, System.DateTime.Today.AddHours(10f));
        a_feed_schedule[2] = new a_ScheduleKey(false, System.DateTime.Today.AddHours(10f));
        a_feed_schedule[3] = new a_ScheduleKey(false, System.DateTime.Today.AddHours(20f));

        //Heal Times
        a_heal_schedule[0] = new a_ScheduleKey(false, System.DateTime.Today.AddHours(10f));
        a_heal_schedule[1] = new a_ScheduleKey(false, System.DateTime.Today.AddHours(14f));
    }

    public void a_CheckTimeSchedule()
    {
        if (!a_feed_schedule[0].is_completed && System.DateTime.Now >= a_feed_schedule[0].date)
        {
            a_feed_schedule[0].is_completed = true;

            gd.gd_sealdata.a_current_feed_schedule = 0;

            sm.a_SetAllCanFeed(0);

            Debug.Log("Feed Schedule 1: done");
        }

        if (!a_feed_schedule[1].is_completed && System.DateTime.Now >= a_feed_schedule[1].date)
        {
            a_feed_schedule[1].is_completed = true;

            gd.gd_sealdata.a_current_feed_schedule = 1;

            sm.a_SetAllCanFeed(1);

            Debug.Log("Feed Schedule 2: done");
        }

        if (!a_feed_schedule[2].is_completed && System.DateTime.Now >= a_feed_schedule[2].date)
        {
            a_feed_schedule[2].is_completed = true;

            gd.gd_sealdata.a_current_feed_schedule = 2;

            sm.a_SetAllCanFeed(2);

            Debug.Log("Feed Schedule 3: done");
        }

        if (!a_feed_schedule[3].is_completed && System.DateTime.Now >= a_feed_schedule[3].date)
        {
            a_feed_schedule[3].is_completed = true;

            gd.gd_sealdata.a_current_feed_schedule = 3;

            sm.a_SetAllCanFeed(3);

            Debug.Log("Feed Schedule 4: done");
        }

        if (!a_heal_schedule[0].is_completed && System.DateTime.Now >= a_heal_schedule[0].date)
        {
            a_heal_schedule[0].is_completed = true;

            gd.gd_sealdata.a_current_heal_schedule = 0;

            sm.a_SetAllCanHeal(0);

            Debug.Log("Heal Schedule 1: done");
        }

        if (!a_heal_schedule[1].is_completed && System.DateTime.Now >= a_heal_schedule[1].date)
        {
            a_heal_schedule[1].is_completed = true;

            gd.gd_sealdata.a_current_heal_schedule = 1;

            sm.a_SetAllCanHeal(1);

            Debug.Log("Heal Schedule 1: done");
        }

    }

    #endregion

    #region Time Stuff (Non-Unviersal - B)

    public void b_SetupSchedule()
    {
        foreach (Seal seal in gd.gd_sealdata.player_seals)
        {
            Debug.Log("T - inside foreach");
            if (!seal.can_feed)
            {
                b_AddItemToSchedule_Feed(seal.next_time_feed, seal);
                Debug.Log("T - inside feed");
            }
            if (!seal.can_heal)
            {
                b_AddItemToSchedule_Heal(seal.next_time_heal, seal);
                Debug.Log("T - inside heal");
            }

        }
       // b_CheckTimeSchedule();
    }

    void b_CheckTimeSchedule()
    {
        if (b_seal_feed_schedules.Count > 0 && System.DateTime.Now >= b_seal_feed_schedules[0].date)
        {
            b_seal_feed_schedules[0].seal.can_feed = true;
            b_seal_feed_schedules.RemoveAt(0);
        }

        if (b_seal_heal_schedules.Count > 0 && System.DateTime.Now >= b_seal_heal_schedules[0].date)
        {
            b_seal_heal_schedules[0].seal.can_heal = true;
            b_seal_heal_schedules.RemoveAt(0);
        }
    }

    public void b_AddItemToSchedule_Feed(string date, Seal seal)
    {
        b_seal_feed_schedules.Add(new b_SealScheduleItem(date, seal));

        b_seal_feed_schedules.Sort((x, y) => x.date.CompareTo(y.date));
    }

    public void b_AddItemToSchedule_Heal(string date, Seal seal)
    {
        b_seal_heal_schedules.Add(new b_SealScheduleItem(date, seal));

        b_seal_heal_schedules.Sort((x, y) => x.date.CompareTo(y.date));
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

public class a_ScheduleKey
{
    public System.DateTime date;
    public bool is_completed;

    public a_ScheduleKey(bool _is_complete, System.DateTime _date)
    {
        is_completed = _is_complete;
        date = _date;
    }
}

[System.Serializable]
public class b_SealScheduleItem
{
    public System.DateTime date;
    public Seal seal;
    public string string_date;

    public b_SealScheduleItem(string _date, Seal _seal)
    {

        date = System.DateTime.Parse(_date);
        seal = _seal;
        //string_date = date.ToString();
    }
}