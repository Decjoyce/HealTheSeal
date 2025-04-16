using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SealNamer : MonoBehaviour
{
    [SerializeField]
    string[] set_names;

    [SerializeField] TextMeshProUGUI placeholder_text;
    [SerializeField] TMP_InputField input_field;

    SealManager sm;

    private void Start()
    {
        sm = SealManager.Instance;
    }

    public void NameSeal(string new_name)
    {
        if (new_name != "")
            sm.selectedSeal.seal_name = new_name;


    }

    public void BoxDeselected(string new_name)
    {
        if (new_name == "")
        {
            int ranName = Random.Range(0, set_names.Length);

            sm.selectedSeal.seal_name = set_names[ranName];
            input_field.text = set_names[ranName];
        }

        int i = sm.CheckNameAvailability(sm.selectedSeal.seal_name);
        if (i > 1)
        {
            string name_variant = sm.selectedSeal.seal_name.Trim('I');
            name_variant = name_variant.Trim();
            name_variant = name_variant + " ";
            for (int j = 0; j < i; j++)
            {
                name_variant += "I";
                Debug.LogError(name_variant);
            }
            //Debug.LogError(name_variant);
            sm.selectedSeal.seal_name = name_variant;
            input_field.text = name_variant;
        }
    }
}


/*if (new_name != "")
    seal_name = new_name;
else
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
}*/