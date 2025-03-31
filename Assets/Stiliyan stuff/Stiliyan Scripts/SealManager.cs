using System.Collections.Generic;
using UnityEngine;

public class SealManager : MonoBehaviour
{
    public static SealManager Instance;

    public List<Seal> seals = new List<Seal>();
    public Seal selectedSeal;

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
}
