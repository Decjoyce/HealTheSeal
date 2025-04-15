using System.Collections.Generic;
using UnityEngine;

public class SealManager : MonoBehaviour
{
    public static SealManager Instance;

    public List<Seal> seals = new List<Seal>();
    public Seal selectedSeal;

    public bool justRescuedSeal = false;
    public bool isSealAvailableForRescue = false;

    public bool currentSealNeedsRescue = false;
    public int currentSealInjury = 0; // 0 - no injury (healthy), 1 - net, 2 - hook, 3 - antibiotics

    [Header("Seal Stuff")]
    // Contains all graphics relating to seals
    public SO_SealStuff seal_stuff;

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

    //Makes all seals available to feed
    public void a_SetAllCanFeed(int a_current_schedule, bool y = true)
    {
        Debug.Log(a_current_schedule);
        foreach (Seal s in seals)
        {
            if(s.a_times_fed == a_current_schedule)
            s.can_feed = y;
        }
    }

    //Makes all seals available to heal
    public void a_SetAllCanHeal(int a_current_schedule, bool y = true)
    {
        Debug.Log(a_current_schedule);
        foreach (Seal s in seals)
        {
            if(s.a_times_healed == a_current_schedule)
                s.can_heal = y;
        }
    }

    public void SetSealAvailable(bool status)
    {
        isSealAvailableForRescue = status;
        if (status)
        {
            DetermineSealCondition(); // randomize condition when timer reaches 0
        }
    }
    void DetermineSealCondition()//determines if the seal is healthy and if it need rescuing
    {
        // 30% chance the seal is healthy (no rescue needed)
        currentSealNeedsRescue = Random.value > 0.3f;

        if (currentSealNeedsRescue)
        {
            // assing injury clearly (1-net, 2-hook, 3-antibiotics)
            currentSealInjury = Random.Range(1, 4);
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

    public Seal GetSealById(string id)
    {
        return seals.Find(s => s.id == id);
    }

    public Sprite GetSealInjuryGraphics()
    {
        return seal_stuff.GetInjuryGraphics(currentSealInjury);
    }
}
