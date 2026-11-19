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
    public TMP_Text PlayerNameText;
    public TMP_Text AnimationsToggleText;
    public TMP_Text DiceColorText;
    public TMP_Text GameMusicText;

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
        PlayerNameText.text = mgrSaveManager.PlayerName;
        AnimationsToggleText.text = mgrSaveManager.isAnimationsOn.ToString();
        DiceColorText.text = mgrSaveManager.DiceColor.ToString();
        GameMusicText.text = mgrAudioManager.GetTrackName(mgrAudioManager.CurrentTrack);
    }

    private void ResetTriggers() //AnimEvent: ShrinkAnim start
    {
        foreach (AnimatorControllerParameter param in PanelAnimator.parameters)
        {
            PanelAnimator.ResetTrigger(param.name);
        }
    }
    public void TransitionToCanvasFE() //AnimEvent: ShrinkAnim end
    {
        CanvasManager.Instance.HideCanvasOptions();
    }

    public void OnMusicSliderChanged(float _value)
    {
        AudioManager.Instance.SetMixerLevel(eMixers.Music, _value);
    }

    public void OnSFXSliderChanged(float _value)
    {
        AudioManager.Instance.SetMixerLevel(eMixers.SFX, _value);
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
            //TODO hide this options panel & bring up pause menu panel
            // replace with animation trigger instead
            CanvasManager.Instance.Pause.OptionsPanel.SetActive(false); //
            CanvasManager.Instance.Pause.PausePanel.SetActive(true); //
        }
    }

    public void OnPrefLangClicked()
    {
        Debug.Log("<color=green>OnPrefLangClicked</color>");
        AudioManager.PlayClick();
        mgrSaveManager.Language++; //TODO loop around to first language
        mgrSaveManager.SaveSettings();
        SetTextsToMirrorPrefs();
    }

    //TODO player name input field

    public void OnAnimationsClicked()
    {
        Debug.Log("<color=green>OnAnimationsClicked</color>");
        AudioManager.PlayClick();
        mgrSaveManager.isAnimationsOn++; //TODO loop around toggle choices
        mgrSaveManager.SaveSettings();
        SetTextsToMirrorPrefs();
    }

    //TODO other buttons
}
