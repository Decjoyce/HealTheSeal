using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip main_music, minigame_music, release_music;

    float fade_speed;

    Coroutine coroutine_fade_in, coroutine_fade_out;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        GameManagement.OnLoadScene += ChangeMusic;
    }

    private void OnDisable()
    {
        GameManagement.OnLoadScene -= ChangeMusic;
    }

    private void OnLevelWasLoaded(int level)
    {
        StopCoroutine(coroutine_fade_out);
        FadeMusicIn(0.75f);
    }

    void ChangeMusic(string scene_name, int index = -1)
    {
        if(scene_name == "HabitatScene" || index == 1 || scene_name == "SealDetailScene" || index == 2)
        {
            if (source.clip == main_music)
                return;
            source.clip = main_music;
            source.Play();
        }
        else if(scene_name.Contains("MG_") || index >= 6)
        {
            if (source.clip == minigame_music)
                return;
            source.clip = minigame_music;
            source.Play();
        }
        else if(scene_name == "Release Scene" || index == 5)
        {
            if (source.clip == release_music)
                return;
            source.clip = release_music;
            source.Play();
        }
    }

    public void FadeMusicOut(float delay)
    {
        coroutine_fade_out = StartCoroutine(FadeMusicOutCoroutine(delay));
    }

    private IEnumerator FadeMusicOutCoroutine(float delay)
    {
        float timedelay = delay;
        fade_speed = -0.75f / delay;
        while (timedelay > 0)
        {
            source.volume -= Time.deltaTime;
            timedelay -= Time.deltaTime;
            yield return null;
        }
        Mathf.Clamp(source.volume, 0, 0.75f);
    }

    public void FadeMusicIn(float delay)
    {
        coroutine_fade_in = StartCoroutine(FadeMusicInCoroutine(delay));
    }

    private IEnumerator FadeMusicInCoroutine(float delay)
    {
        float timedelay = delay;
        fade_speed = 0.75f / delay;
        while (timedelay > 0)
        {
            source.volume += Time.deltaTime;
            timedelay -= Time.deltaTime;
            yield return null;
        }
        Mathf.Clamp(source.volume, 0, 0.75f);
    }
}
