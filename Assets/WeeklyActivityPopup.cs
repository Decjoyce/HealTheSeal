using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeeklyActivityPopup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI activity_name_text;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI body_text;

    GameManagement gm;

    private void Start()
    {
        gm = GameManagement.instance;
    }

    private void OnEnable()
    {
        activity_name_text.text = gm.current_weekly_activity.activity_name;
        image.sprite = gm.current_weekly_activity.image;
        body_text.text = gm.current_weekly_activity.body_text;
    }
}
