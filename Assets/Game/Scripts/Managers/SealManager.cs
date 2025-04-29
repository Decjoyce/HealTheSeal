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
        Debug.LogWarning(parsed_name);
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
