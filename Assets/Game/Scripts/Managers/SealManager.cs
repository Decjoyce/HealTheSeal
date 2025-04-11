using System.Collections.Generic;
using UnityEngine;

public class SealManager : MonoBehaviour
{
    public static SealManager Instance;

    public List<Seal> seals = new List<Seal>();
    public Seal selectedSeal;

    public bool justRescuedSeal = false;

    public bool isSealAvailableForRescue = false;

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
    public void SetSealAvailable(bool status)
    {
        isSealAvailableForRescue = status;
    }
}
