using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public delegate void Event_OnLoadScene(string scene_name, int index = -1);
    public static event Event_OnLoadScene OnLoadScene;

    public static GameManagement instance;


    public bool dev_mode; //{ get; private set; }
    public bool is_pc;

    public bool first_time = true;

    public bool tutorial;

    public int tutorial_index = 1;

    [SerializeField] AndroidService am;
    [SerializeField] SealManager sm;
    [SerializeField] MusicManager mm;
    [SerializeField] GameData gd;

    [Header("Schedules")]
    public List<SealScheduleItem> seal_feed_schedules = new List<SealScheduleItem>();
    public List<SealScheduleItem> seal_heal_schedules = new List<SealScheduleItem>();

    public float feed_delay = 4, heal_delay = 6;

    [Header("Weekly Activities")]
    [SerializeField] SO_WeeklyActivity[] all_weekly_activities;
    public SO_WeeklyActivity current_weekly_activity { get; private set; }

    [Header("Scene Transitions")]
    public bool is_transitioning { get; private set; }
    [SerializeField] Animator[] transition_animators;
    Coroutine current_loadscene_coroutine;
    public int current_trans = 0;

    public GameObject current_info;

    public void OpenCurrentInfoBox()
    {
        current_info.SetActive(true);
        for(int i = 0; i < current_info.transform.parent.childCount; i++)
        {
            if (i != 1)
                current_info.transform.parent.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void BeginTutorial()
    {
        tutorial = true;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of GAMEMANAGEMENT found");
            Destroy(gameObject);
            return;
        }
        instance = this;
        is_pc = !Application.isMobilePlatform;
        if (gd.gd_sealdata.done_tutorial)
        {
            tutorial = false;
            first_time = false;
        }
    }

    public void EnterDebugMode()
    {
        dev_mode = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PickWeeklyActivity();

    }

    // Update is called once per frame
    void Update()
    {
        CheckTimeSchedule();
    }

    #region Weekly Activity Stuff

    void PickWeeklyActivity()
    {
        int ran_activity = Random.Range(0, all_weekly_activities.Length);
        current_weekly_activity = all_weekly_activities[ran_activity];
    }

    #endregion

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

    #region Scene Stuff
    public void LoadScene(string scene_name = "", int index = -1, int transition_index = 0)
    {
        if (is_transitioning)
            return;
        //StopCoroutine(current_loadscene_coroutine);
        current_loadscene_coroutine = StartCoroutine(LoadSceneCoroutine(scene_name, index, transition_index));
    }

    public void LoadHabitatScene(int transition_index = 0)
    {
        if (is_transitioning)
            return;
        //StopCoroutine(current_loadscene_coroutine);
        current_loadscene_coroutine = StartCoroutine(LoadSceneCoroutine("HabitatScene"));
    }

    public IEnumerator LoadSceneCoroutine(string scene_name = "", int index = -1, int transition_index = 0)
    {
        Animator tran_anim = SceneTransitioner.instance.transition_anims[transition_index];
        current_trans = transition_index;
        tran_anim.SetTrigger("START");

        AnimatorClipInfo anim_info = tran_anim.GetCurrentAnimatorClipInfo(0)[0];
        float delay = anim_info.clip.length;

        mm.FadeMusicOut(delay);

        yield return new WaitForSecondsRealtime(delay);

        OnLoadScene?.Invoke(scene_name, index);
        if (scene_name != "")
            SceneManager.LoadScene(scene_name);
        else if (index != -1)
            SceneManager.LoadScene(index);
        else
        {
            Debug.LogWarning("Scene Name: " + scene_name + " | Scene Index: " + index + " does not exist");
            yield break;
        }

        if(!tutorial)
            GameData.instance.SaveAllData();

        yield return null;

        SceneTransitioner.instance.ActivateTran(transition_index);

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