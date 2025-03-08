using UnityEngine;

public class SealStats : MonoBehaviour
{
    public float health;
    public float mood;
    public float hunger;

    public void InitializeStats(float health, float mood, float hunger)
    {
        this.health = health;
        this.mood = mood;
        this.hunger = hunger;
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
