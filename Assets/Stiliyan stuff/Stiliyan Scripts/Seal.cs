using Unity.Android.Types;
using UnityEngine;

[System.Serializable]
public class Seal
{
    public string id;
    public float health;
    public float mood;
    public float hunger;
    //Hunger trait
    public int hungerTrait;//1-fussy, 2- normal, 3- foodie
    //Mood trait
    public int moodTrait;//1-lazy, 2-normal, 3- energetic
    //Health trait
    public int healthTrait;//1-Brittle, 2- normal, 3-resilient

    //public SealStats stats;

    public Vector2 position;

    public Seal()
    {
        id = System.Guid.NewGuid().ToString();

        position = Vector2.zero;
    }
    public void RandomizeAttributes()
    {
        health = Random.Range(0, 50);
        mood = Random.Range(0, 50);
        hunger = Random.Range(0, 25);
        position = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        healthTrait = Random.Range(1, 4);
        moodTrait = Random.Range(1, 4);
        hungerTrait = Random.Range(1, 4);

    }

    public void IncreaseHealth(float amount)
    {
        health += amount;
        if (health > 100f) health = 100f; //Max cap
    }

    public void DecreaseHealth(float amount)
    {
        health -= amount;
        if (health < 0f) health = 0f; //Min cap
    }
    ////////////////////////////////////////////////////////
    //////Food///
    public void FeedSeal(float foodAmount)
    {
        hunger += foodAmount;
        if (hunger > 100f) hunger = 100f; // Max cap
    }

    public void LoseHungerSeal(float amount)
    {
        hunger -= amount;
        if (hunger < 0f) hunger = 0; // Min cap
    }
    ////////////////////////////////////////////////////////
    ///Mood///
    public void IncreaseMood(float amount)
    {
        mood += amount;
        if (mood > 100f) mood = 100f; //Max cap
    }

    public void ReduceMood(float amount)
    {
        mood -= amount;
        if (mood < 0f) mood = 0f; // Min cap
    }
}
