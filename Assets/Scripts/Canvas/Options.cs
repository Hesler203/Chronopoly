using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Canvas: Input: Music & SFX volume sliders, controls game options
/// </summary>
public class Options : MonoBehaviour
{
    private static readonly int hashToFE = Animator.StringToHash("ToFE");
    public Animator PanelAnimator;

    [Header("Options Selections Texts")]
    public TMP_Text PrefLangText;
    public Slider MusicSlider;
    public Slider SFXSlider;
    public TMP_InputField PlayerNameInput;
    public TMP_Text AnimationsToggleText;
    public TMP_Dropdown DiceColorDropdown;
    public TMP_Dropdown GameMusicDropdown;

    private SaveManager mgrSaveManager;
    private AudioManager mgrAudioManager;

    void Start()
    {
        mgrSaveManager = SaveManager.Instance;
        SetTextsToMirrorPrefs();
    }

    private void SetTextsToMirrorPrefs()
    {
        PrefLangText.text = mgrSaveManager.LANGUAGES[(int)mgrSaveManager.Language];
        MusicSlider.value = mgrAudioManager.Volume[(int)eMixers.Music];
        SFXSlider.value = mgrAudioManager.Volume[(int)eMixers.SFX];
        PlayerNameInput.text = mgrSaveManager.PlayerName;
        AnimationsToggleText.text = mgrSaveManager.isAnimationsOn.ToString();
        DiceColorDropdown.value = (int)mgrSaveManager.DiceColor; //TODO
        GameMusicDropdown.value = (int)mgrAudioManager.CurrentTrack; //TODO
    }

    private void ResetTriggers() //AnimEvent: ShrinkAnim start
    {
        foreach (AnimatorControllerParameter param in PanelAnimator.parameters)
        {
            PanelAnimator.ResetTrigger(param.name);
        }
    }
    public void TransitionToCanvasFE() //AnimEvent: ShrinkAnimToFE end
    {
        CanvasManager.Instance.HideCanvasOptions();
    }

    public void TransitionToCanvasPause() //AnimEvent: ShrinkAnimToOptions end
    {
        CanvasManager.Instance.ShowCanvasPause();
        CanvasManager.Instance.HideCanvasOptions();
    }

    public void OnPrefLangClicked()
    {
        Debug.Log("<color=green>OnPrefLangClicked</color>");
        AudioManager.PlayClick();
        mgrSaveManager.Language++;
        if (mgrSaveManager.Language == eLanguages.Terminator)
        {
            mgrSaveManager.Language = 0;
        }
        mgrSaveManager.SaveSettings();
        SetTextsToMirrorPrefs();
    }

    public void OnMusicSliderChanged(float _value)
    {
        AudioManager.Instance.SetMixerLevel(eMixers.Music, _value);
    }

    public void OnSFXSliderChanged(float _value)
    {
        AudioManager.Instance.SetMixerLevel(eMixers.SFX, _value);
    }

    public void OnPlayerNameChanged()
    {
        Debug.Log("<color=green>Player name changed to " + PlayerNameInput.text + "</color>");
        mgrSaveManager.PlayerName = PlayerNameInput.text;
        mgrSaveManager.SaveSettings();
    }

    public void OnAnimationsClicked()
    {
        Debug.Log("<color=green>OnAnimationsClicked</color>");
        AudioManager.PlayClick();
        mgrSaveManager.isAnimationsOn++;
        if (mgrSaveManager.isAnimationsOn == eAnimationsToggle.Terminator)
        {
            mgrSaveManager.isAnimationsOn = 0;
        }
        mgrSaveManager.SaveSettings();
        SetTextsToMirrorPrefs();
    }

    public void OnDiceColorChanged()
    {
        eDiceColors newColor = (eDiceColors)DiceColorDropdown.value;
        Debug.Log("<color=green>Dice color changed to " + newColor.ToString() + "</color>");
        mgrSaveManager.DiceColor = newColor;
        mgrSaveManager.SaveSettings();
    }

    public void OnMusicTrackChanged()
    {
        eMusicTrack newTrack = (eMusicTrack)GameMusicDropdown.value;
        Debug.Log("<color=green>Game music changed to " + newTrack.ToString() + "</color>");
        mgrAudioManager.SetMusicTrack(newTrack);
        mgrSaveManager.SaveSettings();
    }

    public void OnBackClicked(bool isPaused = false)
    {
        Debug.Log("<color=green>OnBackClicked</color>");
        AudioManager.PlayClick();
        if (!isPaused)
        {
            PanelAnimator.SetTrigger(hashToFE);
        }
        else
        {
            //TODO hide this options canvas & bring up pause canvas
            // replace with animation trigger instead
            CanvasManager.Instance.HideCanvasOptions(); // shrinkAnimToPause triggered
            CanvasManager.Instance.ShowCanvasPause(); // called at shrinkAnimToPause end
        }
    }
}
