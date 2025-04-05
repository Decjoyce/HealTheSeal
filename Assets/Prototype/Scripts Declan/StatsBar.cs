using UnityEngine;
using UnityEngine.UI;

public class StatsBar : MonoBehaviour
{
    public bool DEBUGMODE = false;
    private Image statbar;
    [SerializeField] Sprite[] statsbar_states;
    private int value;

    private void Start()
    {
        statbar = GetComponent<Image>();
        //SetValue(Random.Range(0f, 100f));
    }

    private void Update()
    {
        if (DEBUGMODE && Input.GetKeyDown(KeyCode.Space))
        {
            SetValue(Random.Range(0f, 100f));
        }
    }

    public void SetValue(float new_value)
    {
        value = Mathf.FloorToInt((new_value * 7f) / 100f);
        statbar.sprite = statsbar_states[value];
        Debug.Log(value);
    }

}
