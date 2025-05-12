using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class MinigameTreatment : MonoBehaviour
{
    Seal current_seal;
    public int current_injury = 4;

    [SerializeField] Tools current_tool = Tools.hand;

    [SerializeField] TreatmentRecipes[] recipes;
    [SerializeField] int current_recipe_step;


    [Header("UI")]
    [SerializeField] GameObject seal_namer_ui;
    [SerializeField] GameObject treatment_ui;
    [SerializeField] Image seal_sprite;
    [SerializeField] Image cursor_ui;
    [SerializeField] SO_SealStuff seal_graphics;
    [SerializeField] GameObject injuryobject_net, injuryobject_flipper, injuryobject_hook;

    //Spray Minigame Stuff
    [Header("Spray")]
    [SerializeField] Color seal_spray_color;
    [SerializeField] SprayCan spray_can;
    int sprayed_area;
    int max_sprayed_area = 10;

    [Header("Scissors")]
    [SerializeField] GameObject target_net;
    [SerializeField] GameObject target_flipper;
    [SerializeField] GameObject target_hook;
    int net_targets_cut;

    [Header("Tissue")]
    [SerializeField] RectTransform snot_hitbox;
    [SerializeField] float min_tissuemove_required;
    [SerializeField] float tissue_hitbox_radius;
    Vector2 prev_mousepos;
    int snot_health = 3;

    [SerializeField] TutorialTreatment tut;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current_seal = SealManager.Instance.selectedSeal;
        current_injury = SealManager.Instance.currentSealInjury;
        //current_injury = Random.Range(1, 6); //TEMP
        InitInjury();
    }

    private void Update()
    {
        if (current_tool == Tools.spray)
            Spray();
        else if (current_tool == Tools.scissors && GetCurrentRecipeStepTool() == Tools.scissors)
        {
            if (current_injury == 1)
                target_net.SetActive(true);
            else
                target_hook.SetActive(true);
        }
        else if (current_tool == Tools.bandaid && GetCurrentRecipeStepTool() == Tools.bandaid)
            target_flipper.SetActive(true);
        else if (current_tool == Tools.tissue)
            Tissue();
    }

    private void OnMouseDown()
    {
        Debug.Log("Hey");
        if (current_tool == Tools.hand && GetCurrentRecipeStepTool() == Tools.hand)
        {
            //Over Stuff
            treatment_ui.SetActive(false);
            seal_namer_ui.SetActive(true);
            if (GameManagement.instance.tutorial)
                tut.CagedSeal();
        }

    }

    #region Recipes

    void FinishedRecipeStep()
    {
        current_recipe_step++;
        current_tool = GetCurrentRecipeStepTool();
        if (current_recipe_step == GetCurrentRecipe().process.Length - 1)
            seal_sprite.sprite = seal_graphics.g_small_seal_normal;

    }

    TreatmentRecipes GetCurrentRecipe()
    {
        return recipes[current_injury];
    }

    Tools GetCurrentRecipeStepTool()
    {
        return recipes[current_injury].process[current_recipe_step];
    }

    void InitInjury()
    {

        switch (current_injury)
        {
            case 1: // Net
                injuryobject_net.SetActive(true);
                break;
            case 2: // Flipper
                injuryobject_flipper.SetActive(true);
                break;
            case 3: // Hook
                injuryobject_hook.SetActive(true);
                break;
            case 4: // Cold
                seal_sprite.sprite = seal_graphics.g_cold_injury;
                break;
            case 5: // Orphaned

                break;
            default:
                return;
        }
    }

    void InitCurrentStep()
    {

        switch (current_injury)
        {
            case 1: // Net
                target_net.SetActive(true);
                break;
            case 2: // Flipper
                target_flipper.SetActive(true);
                break;
            case 3: // Hook
                target_hook.SetActive(true);
                break;
            case 4: // Cold
                
                break;
            case 5: // Orphaned
                
                break;
            default:
                return;
        }
    }

    #endregion

    public void ChangeToolbyInt(int new_tool)
    {
        target_flipper.SetActive(false);
        target_net.SetActive(false);
        target_hook.SetActive(false);
        spray_can.enabled = false;
        switch (new_tool)
        {
            case 1:
                current_tool = Tools.spray;
                spray_can.enabled = true;
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
        if(current_tool == Tools.spray)
            spray_can.enabled = true;
    }

    void Spray()
    {
        if (GetCurrentRecipeStepTool() == Tools.spray)
        {
            Debug.Log("Sup");
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 1f);
            sprayed_area = cols.Length;
            if (sprayed_area >= max_sprayed_area)
            {
                Debug.Log("YO");
                seal_sprite.color = seal_spray_color;
                FinishedRecipeStep();
                if (GameManagement.instance.tutorial)
                    tut.SprayedSeal();
            }
        }


    }

    public void Scissors(bool net)
    {
        if (net)
        {
            net_targets_cut++;
            if (net_targets_cut >= 3)
            {
                FinishedRecipeStep();
                injuryobject_net.SetActive(false);
            }
                
        }
        else
        {
            FinishedRecipeStep();
        }

    }

    void Tissue()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos.x = mousePos.x - Screen.width / 2;
        mousePos.y = mousePos.y - Screen.height / 2;
        Vector2 mousePos2 = mousePos / GetComponentInParent<Canvas>().scaleFactor;

        if (Vector2.Distance(mousePos2, snot_hitbox.anchoredPosition) <= tissue_hitbox_radius)
        {
            Debug.Log("InsideMainframe " + Vector2.Distance(prev_mousepos, mousePos2));
            if (Vector2.Distance(prev_mousepos, mousePos2) >= min_tissuemove_required)
            {
                Debug.Log("We gottem");
                snot_health--;
                if(snot_health <= 0)
                {
                    FinishedRecipeStep();
                }
            }
        }
     
        prev_mousepos = mousePos2;
    }

    public void Bandaid()
    {
        FinishedRecipeStep();
        if (GameManagement.instance.tutorial)
            tut.BandagedSeal();
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