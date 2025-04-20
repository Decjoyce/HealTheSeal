using UnityEngine;

[System.Serializable]
public class Seal
{

    public string id;
    public float health;
    //public float mood;
    public float weight;
    public float hunger;
    //Hunger trait
    public int hungerTrait;//1-fussy, 2- normal, 3- foodie
    //Mood trait
    public int moodTrait;//1-lazy, 2-normal, 3- energetic
    //Health trait
    public int healthTrait;//1-Brittle, 2- normal, 3-resilient

    public int injury;//1-Net entanglement,2- Hook in mouth, 3- Desinfect, 4- normal.

    //public SealStats stats; //Prototype

    public string seal_name; //Prototype

    public Vector2 position;

    public Seal()
    {
        id = System.Guid.NewGuid().ToString();

        position = Vector2.zero;
    }
    public void RandomizeAttributes()
    {
        health = Random.Range(5, 25); //TESTING
        //mood = Random.Range(0, 50);
        weight = Random.Range(10, 15); //TESTING
        hunger = Random.Range(0, 25);
        position = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        healthTrait = Random.Range(1, 4);
        moodTrait = Random.Range(1, 4);
        hungerTrait = Random.Range(1, 4);
        //injury= Random.Range(1, 5); // 


        NameDaSeal(); //Prototype - temp
    }

    //Prototype - Moved from SealStats script to here 
    public void IncreaseHealth(float amount)
    {

        if (injury == 1)
        {
            health += amount / 1.5f;//net
        }
        else if (injury == 2)
        {
            health += amount / 1.2f;//hook
        }
        else if (injury == 3)
        {
            health += amount / 2;//wound desinfection needed
        }
        else if (injury == 4)
        {
            health += amount;
        }

        if (health > 100f) health = 100f;
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
        if (hunger > 100f) hunger = 100f;
        weight++;
        if (weight >= 70) weight = 70;
    }

    public void LoseHungerSeal(float amount)
    {
        hunger -= amount;
        if (hunger < 0f) hunger = 0; // Min cap
        weight++;
        if (weight >= 70) weight = 70;
    }
    ////////////////////////////////////////////////////////
    ///Mood///
    //public void IncreaseMood(float amount)
    //{
    //    mood += amount;
    //    if (mood > 100f) mood = 100f; //Max cap
    //}

    //public void ReduceMood(float amount)
    //{
    //    mood -= amount;
    //    if (mood < 0f) mood = 0f; // Min cap
    //}


    //Prototype temp
    void NameDaSeal()
    {
        int ranName = Random.Range(0, 26);
        switch (ranName)
        {
            case 0:
                seal_name = "Alphonso";
                break;
            case 1:
                seal_name = "5341";
                break;
            case 2:
                seal_name = "Aaron";
                break;
            case 3:
                seal_name = "Stiliyan";
                break;
            case 4:
                seal_name = "Matthew";
                break;
            case 5:
                seal_name = "Finnán";
                break;
            case 6:
                seal_name = "Dec";
                break;
            case 7:
                seal_name = "Darnell Simmons";
                break;
            case 8:
                seal_name = "Michael Jackson";
                break;
            case 9:
                seal_name = "Seaweed";
                break;
            case 10:
                seal_name = "Dúllamán";
                break;
            case 11:
                seal_name = "Rhiannon";
                break;
            case 12:
                seal_name = "Saltwater";
                break;
            case 13:
                seal_name = "Cookie Dough";
                break;
            case 14:
                seal_name = "Blob";
                break;
            case 15:
                seal_name = "Spots";
                break;
            case 16:
                seal_name = "Honeysuckle";
                break;
            case 17:
                seal_name = "Mossy";
                break;
            case 18:
                seal_name = "Phil";
                break;
            case 19:
                seal_name = "Oyster";
                break;
            case 20:
                seal_name = "Clam";
                break;
            case 21:
                seal_name = "Shelly";
                break;
            case 22:
                seal_name = "Sandy";
                break;
            case 23:
                seal_name = "Adeola";
                break;
            case 24:
                seal_name = "Prawn";
                break;
            case 25:
                seal_name = "Toad Crab";
                break;
        }
    }
}
