[System.Serializable]
public class SealData
{
    public float mood;
    public float health;
    public float hunger;

    // Optional constructor
    public SealData(float mood, float health, float hunger)
    {
        this.mood = mood;
        this.health = health;
        this.hunger = hunger;
    }

}
