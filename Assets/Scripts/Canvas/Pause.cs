using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Canvas: Input: manages Pause Menu buttons' inputs & panels
/// </summary>
public class Pause : MonoBehaviour
{
    public Animator PanelAnimator;

    [Header("Confirm Widget Settings")]
    public GameObject ConfirmWidget;
    public TMP_Text ConfirmTitle;
    public TMP_Text ConfirmTitleShadow;
    public TMP_Text ConfirmPrompt;
    public Button ConfirmSaveYesButton;
    public Button ConfirmAbandonYesButton;

    public void OnOptionsClicked()
    {
        Debug.Log("<color=green>OnOptionsClicked</color>");
        AudioManager.PlayClick();
        //TODO replace with animation trigger instead
        CanvasManager.Instance.HideCanvasPause(); // pauseCanvas shrinkAnimToOptions triggered
        CanvasManager.Instance.ShowCanvasOptions(); // follow with optionsCanvas growAnim
    }

    public void TransitionToCanvasOptions() //AnimEvent: shrinkAnimToOptions end
    {
        CanvasManager.Instance.ShowCanvasOptions();
        CanvasManager.Instance.HideCanvasPause();
    }

    public void OnHelpAboutClicked()
    {
        Debug.Log("<color=green>OnHelp&AboutClicked</color>");
        AudioManager.PlayClick();
        //TODO replace with animation trigger instead
        CanvasManager.Instance.HideCanvasPause(); // pauseCanvas shrinkAnimToHelp triggered
        CanvasManager.Instance.ShowCanvasHelp(); // follow with helpCanvas growAnim
    }

    public void TransitionToCanvasHelp() //AnimEvent: shrinkAnimToHelp end
    {
        CanvasManager.Instance.ShowCanvasHelp();
        CanvasManager.Instance.HideCanvasPause();
    }

    public void OnHouseRulesClicked()
    {
        Debug.Log("<color=green>OnHouseRulesClicked</color>");
        AudioManager.PlayClick();
        //TODO
    }

    public void OnSaveExitClicked()
    {
        Debug.Log("<color=green>OnSave&ExitClicked</color>");
        AudioManager.PlayClick();
        string title = "SAVE AND EXIT?";
        string prompt = "Are you sure you want to save and exit the game?";
        ShowConfirmWidget(title, prompt, false);
    }

    public void ShowConfirmWidget(string _title, string _prompt, bool isAbandon)
    {
        ConfirmWidget.SetActive(true);
        ConfirmTitle.text = ConfirmTitleShadow.text = _title;
        ConfirmPrompt.text = _prompt;
        ConfirmSaveYesButton.gameObject.SetActive(!isAbandon);
        ConfirmAbandonYesButton.gameObject.SetActive(isAbandon);
        //TODO confirm widget growAnim triggered + followed by glassFill fadeInAnim
    }

    public void OnConfirmNoClicked()
    {
        Debug.Log("<color=green>OnNoClicked</color>");
        AudioManager.PlayClick();
        //TODO replace with animation trigger instead
        ConfirmWidget.SetActive(false); // confirm widget shrinkAnim triggered
    }

    public void OnConfirmYesClicked(bool shouldSave)
    {
        Debug.Log("<color=green>OnYesClicked</color>");
        AudioManager.PlayClick();
        ExitGame(shouldSave);
    }

    private void ExitGame(bool shouldSave)
    {
        if (shouldSave)
        {
            SaveManager.Instance.SaveSettings();
            //TODO save players & their data
        }
        //TODO replace with animation trigger instead
        // confirm widget shrinkAnim triggered
        ConfirmWidget.SetActive(false); // called at end of shrinkAnim via anim event
        SceneMgr.Instance.LoadScene(eScene.FrontEnd); // called at end of shrinkAnim via anim event
    }

    public void OnAbandonGameClicked()
    {
        Debug.Log("<color=green>OnAbandonGameClicked</color>");
        AudioManager.PlayClick();
        string title = "ABANDON?";
        string prompt = "Are you sure you want to abandon the game?\nProgress will not be saved.";
        ShowConfirmWidget(title, prompt, true);
    }

    public void OnResumeClicked()
    {
        Debug.Log("<color=green>OnResumeClicked</color>");
        AudioManager.PlayClick();
        // TODO replace with animation trigger
        CanvasManager.Instance.HideCanvasPause(); // canvasPause shrinkAnim triggered
        CanvasManager.Instance.ShowCanvasHUD();
    }

}
