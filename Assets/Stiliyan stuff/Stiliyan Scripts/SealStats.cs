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

    public void FeedSeal(float foodAmount)
    {
        hunger += foodAmount;
        if (hunger > 100f) hunger = 100f; // Max cap
    }

    public void ReduceMood(float amount)
    {
        mood -= amount;
        if (mood < 0f) mood = 0f; // Min cap
    }
}
