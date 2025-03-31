using Unity.Android.Types;
using UnityEngine;

[System.Serializable]
public class Seal
{
    public string id;
    public int health;
    public int mood;
    public int hunger;
    //Hunger trait
    public int hungerTrait;//1-fussy, 2- normal, 3- foodie
    //Mood trait
    public int moodTrait;//1-lazy, 2-normal, 3- energetic
    //Health trait
    public int healthTrait;//1-Brittle, 2- normal, 3-resilient


    public Vector2 position;

    public Seal()
    {
        id = System.Guid.NewGuid().ToString();

        position = Vector2.zero;
    }
    public void RandomizeAttributes()
    {
        health = Random.Range(50, 100);
        mood = Random.Range(0, 100);
        hunger = Random.Range(0, 100);
        position = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        hungerTrait = Random.Range(1, 3);
        moodTrait = Random.Range(1, 3);
        healthTrait = Random.Range(1, 3);

    }
}
