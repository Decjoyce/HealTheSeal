using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealManager : MonoBehaviour
{
    public delegate void Event_OnSealNeedsRescue();
    public static event Event_OnSealNeedsRescue OnSealNeedsRescue;

    public static SealManager Instance;

    public List<Seal> seals = new List<Seal>();
    public Seal selectedSeal;

    public int seal_limit { get; private set; } = 9;

    public bool justRescuedSeal = false;
    public bool isSealAvailableForRescue = false;

    public bool currentSealNeedsRescue = false;
    public int currentSealInjury = 0; // 0 - no injury (healthy), 1 - net, 2 - hook, 3 - antibiotics

    [SerializeField] float rescue_delay_min = 0.1f, rescue_delay_max = 0.5f;

    [Header("Seal Stuff")]
    // Contains all graphics relating to seals
    public SO_SealStuff seal_stuff;

    public System.DateTime next_time_to_rescue_seal;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CheckSealAvailability();
    }

    private void Update()
    {
        CheckSealAvailability();
    }

    public void SetSealAvailable(bool status)
    {
        isSealAvailableForRescue = status;
        if (status)
        {
            DetermineSealCondition(); // randomize condition when timer reaches 0
        }
        OnSealNeedsRescue?.Invoke();
    }

    public void ScheduleSealToBeRescuedTime()
    {
        if (seals.Count < seal_limit)
        {
            float ran_delay = Random.Range(rescue_delay_min, rescue_delay_max);
            next_time_to_rescue_seal = System.DateTime.Now.AddHours(ran_delay);
            GameData.instance.gd_sealdata.next_time_for_rescue = next_time_to_rescue_seal.ToString();
            Debug.LogWarning("Seal Scheduled For Rescue = " + System.DateTime.Now.AddHours(ran_delay).ToString());
        }
        else
        {
            Debug.LogWarning("REACHED LIMIT");
        }
    }

    void DetermineSealCondition()//determines if the seal is healthy and if it need rescuing
    {
        // 30% chance the seal is healthy (no rescue needed)
        currentSealNeedsRescue = Random.value > 0.3f;

        if (currentSealNeedsRescue)
        {
            // assing injury clearly (1-net, 2-hook, 3-antibiotics)
            currentSealInjury = Random.Range(1, 6);
        }
        else
        {
            currentSealInjury = 0; // no injury
        }
    }
    public void SpawnSeal()
    {
        Seal newSeal = new Seal();
        newSeal.RandomizeAttributes();  // Call this after construction
        seals.Add(newSeal);
    }

    void CheckSealAvailability()
    {
        if(seals.Count < seal_limit && System.DateTime.Now >= next_time_to_rescue_seal && !isSealAvailableForRescue)
        {
            SetSealAvailable(true);
            Debug.LogWarning("Seal is Ready to be rescued: " + System.DateTime.Now.ToString());
        }
    }

    public Seal GetSealById(string id)
    {
        return seals.Find(s => s.id == id);
    }

    public Sprite GetSealInjuryGraphics()
    {
        return seal_stuff.GetInjuryGraphics(currentSealInjury);
    }

    public int CheckNameAvailability(string _name)
    {
        int i = 0;
        string parsed_name = _name.Trim('I');
        parsed_name = parsed_name.Trim();

        foreach (Seal s in seals)
        {
            string parsed_seal_name = s.seal_name;
            parsed_seal_name = parsed_seal_name.Trim('I');
            parsed_seal_name = parsed_seal_name.Trim();
            if (parsed_name == parsed_seal_name) i++;
        }
        
        return i;
    }
}
