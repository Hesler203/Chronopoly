using UnityEngine;
using UnityEngine.Audio;

public enum eMixers { Music, SFX }
public enum eMusicTrack { Swing, Smooth, Lounge, All, UserMusic}

/// <summary>
/// Audio: Manager: Singleton that handles game audio via Audio Sources & clips
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [NamedArray(typeof(eMixers))] public AudioMixerGroup[] mixers;
    [NamedArray(typeof(eMixers))] public float[] Volume = { 1f, 1f };
    [NamedArray(typeof(eMixers))] private string[] strMixers = { "MusicVol", "SFXVol" };

    [Header("3D AudioSources")]
    public AudioSource Music;
    public AudioSource SFX;

    [Header("Music Tracks")]
    [NamedArray(typeof(eMusicTrack))] public AudioResource[] MusicTracks;
    public eMusicTrack CurrentTrack;
    private string[] MusicTrackNames = { "Swing", "Smooth", "Lounge", "All", "User Music", "Off" };

    [Header("SFX AudioClips")]
    public AudioClip Click;
    public AudioClip Error;
    public AudioClip Money;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Instance already exists, destroying AudioManager!");
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetMixerLevel(eMixers _mixer, float _soundLevel)
    {
        mixers[(int)_mixer].audioMixer.SetFloat(strMixers[(int)_mixer], Mathf.Log10(_soundLevel) * 20f);
        Volume[(int)_mixer] = _soundLevel; // 0 to 1 value
    }

    public void SetMusicTrack(eMusicTrack _musicTrack)
    {
        Music.resource = MusicTracks[(int)_musicTrack];
        CurrentTrack = _musicTrack;
    }

    public string GetTrackName(eMusicTrack _musicTrack)
    {
        return MusicTrackNames[(int)_musicTrack];
    }

    public static void PlayClick()
    {
        Instance.SFX.PlayOneShot(Instance.Click);
    }

    public static void PlayError()
    {
        Instance.SFX.PlayOneShot(Instance.Error);
    }

    public static void PlayMoney()
    {
        Instance.SFX.PlayOneShot(Instance.Money);
    }
}
