using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    bool bIsBackGround = true;

    float allVolum = 1.0f;
    float effectVolum = 1.0f;
    float bgmVolum = 1.0f;

    AudioSource bgm;
    AudioClip[] backgounrd;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null) instance = new SoundManager();
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void playBGM(AudioClip sound)
    {
        if(sound != null)
        {
            bgm.clip = sound;
            bgm.loop = true;
            bgm.volume = allVolum*bgmVolum;
            bgm.Play(); 
        }
    }

    public void playEffect(AudioClip sound)
    {
        if (sound != null)
        {
            AudioSource effetSound = new AudioSource();
            effetSound.clip = sound;
            effetSound.loop = false;
            effetSound.volume = allVolum * effectVolum;
            effetSound.Play();
        }
    }

    public void SetAllVolum(int volum)
    {
        allVolum = Mathf.Clamp(volum, 0.0f, 1.0f);
    }

    public void SetEffectVolum(int volum)
    {
        effectVolum = Mathf.Clamp(volum, 0.0f, 1.0f);
    }

    public void SetBGMVolum(int volum)
    {
        bgmVolum = Mathf.Clamp(volum, 0.0f, 1.0f);
    }
}
