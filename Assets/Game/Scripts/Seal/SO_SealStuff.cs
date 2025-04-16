using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "new seal graphics", menuName = "Seal/Seal Graphics")]
public class SO_SealStuff : ScriptableObject
{
    [Header("Graphics")]
    public Sprite g_error;

    public Sprite g_small_seal_normal;
    public Sprite g_small_seal_sad;

    public Sprite g_medium_seal_normal;
    public Sprite g_medium_seal_sad;

    public Sprite g_big_seal_normal;
    public Sprite g_big_seal_sad;

    public Sprite g_net_injury;
    public Sprite g_flipper_injury;
    public Sprite g_fishhook_injury;
    public Sprite g_cold_injury;

    public Sprite GetInjuryGraphics(int injury_index) //0-No injury, 1-Net entanglement, 2-flipper, 3-Fishhook, 4-Cold, 5-Orphaned
    {
        switch (injury_index)
        {
            case 0:
                return g_small_seal_normal;
            case 1:
                return g_net_injury;
            case 2:
                return g_flipper_injury;
            case 3:
                return g_fishhook_injury;
            case 4:
                return g_cold_injury;
            case 5:
                return g_small_seal_sad;
            default:
                return g_error;
        }
    }
}