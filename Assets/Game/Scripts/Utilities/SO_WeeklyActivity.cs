using UnityEngine;

[CreateAssetMenu(fileName = "New_WeeklyActivity", menuName = "Weekly Activities/New Weekly Activity")]
public class SO_WeeklyActivity : ScriptableObject
{
    public string activity_name;
    public Sprite image;
    [TextArea]
    public string body_text;
}
