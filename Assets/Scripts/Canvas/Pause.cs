using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Canvas: Input: manages Pause Menu buttons' inputs & panels
/// </summary>
public class Pause : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject OptionsPanel;

    public void OnOptionsClicked()
    {
        Debug.Log("<color=green>OnOptionsClicked</color>");
        AudioManager.PlayClick();
        //TODO replace with animation trigger instead
        PausePanel.SetActive(false); //
        OptionsPanel.SetActive(true); //
    }

    public void OnHelpAboutClicked()
    {
        Debug.Log("<color=green>OnHelp&AboutClicked</color>");
        AudioManager.PlayClick();
        //TODO
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
        SaveManager.Instance.SaveSettings();
        SceneMgr.Instance.LoadScene(eScene.FrontEnd);
    }

    public void OnAbandonGameClicked()
    {
        Debug.Log("<color=green>OnAbandonGameClicked</color>");
        AudioManager.PlayClick();
        SceneMgr.Instance.LoadScene(eScene.FrontEnd); //TODO may change to full application quit
    }

    public void OnResumeClicked()
    {
        Debug.Log("<color=green>OnResumeClicked</color>");
        AudioManager.PlayClick();
        // TODO replace with animation trigger
        CanvasManager.Instance.ShowCanvasHUD(); //
        CanvasManager.Instance.HideCanvasPause(); //
    }

}
