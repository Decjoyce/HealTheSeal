using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class MinigameTreatment : MonoBehaviour
{
    Seal current_seal;
    int current_injury = 1;

    [SerializeField] Tools current_tool = Tools.hand;

    [SerializeField] TreatmentRecipes[] recipes;
    [SerializeField] int current_recipe_step;


    [Header("UI")]
    [SerializeField] Image seal_sprite;
    [SerializeField] Image cursor_ui;

    //Spray Minigame Stuff
    [Header("Spray")]
    [SerializeField] Color seal_spray_color;
    [SerializeField] SprayCan spray_can;
    int sprayed_area;
    int max_sprayed_area = 10;

    [Header("Scissors")]
    int net_targets_cut;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current_seal = SealManager.Instance.selectedSeal;
        //current_injury = current_seal.injury;

    }

    private void Update()
    {
        if (current_tool == Tools.spray)
            Spray();
        else if (current_tool == Tools.tissue)
            Tissue();
    }

    private void OnMouseDown()
    {
        if (current_tool == Tools.hand && current_recipe_step == GetCurrentRecipe().process.Length)
        {
            //Over Stuff
            GameManagement.instance.LoadHabitatScene();
        }

    }

    #region Recipes

    void FinishedRecipeStep()
    {
        current_recipe_step++;
        current_tool = GetCurrentRecipeStepTool();
    }

    TreatmentRecipes GetCurrentRecipe()
    {
        return recipes[current_injury];
    }

    Tools GetCurrentRecipeStepTool()
    {
        return recipes[current_injury].process[current_recipe_step];
    }

    void InitCurrentStep()
    {

        switch (current_injury)
        {
            case 1: // Net
                current_tool = Tools.spray;
                break;
            case 2:
                current_tool = Tools.scissors;
                break;
            case 3:
                current_tool = Tools.tissue;
                break;
            case 4:
                current_tool = Tools.bandaid;
                break;
            case 5:
                current_tool = Tools.hand;
                break;
            default:
                return;
        }
    }

    #endregion

    public void ChangeToolbyInt(int new_tool)
    {
        switch (new_tool)
        {
            case 1:
                current_tool = Tools.spray;
                break;
            case 2:
                current_tool = Tools.scissors;
                break;
            case 3:
                current_tool = Tools.tissue;
                break;
            case 4:
                current_tool = Tools.bandaid;
                break;
            case 5:
                current_tool = Tools.hand;
                break;
            default:
                return;
        }
    }

    public void ChangeTool(Tools new_tool)
    {
        spray_can.enabled = false;
        current_tool = new_tool;
    }

    void Spray()
    {
        if (GetCurrentRecipeStepTool() == Tools.spray)
        {
            Debug.Log("Sup");

            spray_can.enabled = true;
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 1f);
            sprayed_area = cols.Length;
            if (sprayed_area >= max_sprayed_area)
            {
                Debug.Log("YO");
                seal_sprite.color = seal_spray_color;
                FinishedRecipeStep();
            }
        }


    }

    public void Scissors(bool net)
    {
        if (net)
        {
            net_targets_cut++;
            if (net_targets_cut <= 3)
                FinishedRecipeStep();
        }
        else
        {
            FinishedRecipeStep();
        }

    }

    void Tissue()
    {

    }

    public void Bandaid()
    {
        FinishedRecipeStep();
    }

}

public enum Tools
{
    spray,
    scissors,
    tissue,
    bandaid,
    hand
}

[System.Serializable]
class TreatmentRecipes
{
    public string treatment_name;
    public Tools[] process;
}