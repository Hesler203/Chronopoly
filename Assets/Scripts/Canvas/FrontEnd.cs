using UnityEngine;

/// <summary>
/// Canvas: Input: Main Menu Buttons & their functionality
/// </summary>
public class FrontEnd : MonoBehaviour
{
    private void OnTransition()
    {
        CanvasManager.Instance.ShowCanvasFE();
        CanvasManager.Instance.HideCanvasSplash();
    }

    public void OnPlayNowClicked()
    {
        Debug.Log("<color=green>OnPlayNowClicked</color>");
        AudioManager.PlayClick();
        CanvasManager.Instance.ShowCanvasSetup();
    }

    public void OnTeacherModeClicked()
    {
        Debug.Log("<color=green>OnTeacherModeClicked</color>");
        AudioManager.PlayClick();
    }

    public void OnTabletopModeClicked()
    {
        Debug.Log("<color=green>OnTabletopModeClicked</color>");
        AudioManager.PlayClick();
    }

    public void OnLocalNetworkPlayClicked()
    {
        Debug.Log("<color=green>OnLocalNetworkPlayClicked</color>");
        AudioManager.PlayClick();
    }

    public void OnTellAFriendClicked()
    {
        Debug.Log("<color=green>OnTellAFriendClicked</color>");
        AudioManager.PlayClick();
    }

    public void OnStatsClicked()
    {
        Debug.Log("<color=green>OnStatsClicked</color>");
        AudioManager.PlayClick();
        CanvasManager.Instance.ShowCanvasStats();
    }

    public void OnOptionsClicked()
    {
        Debug.Log("<color=green>OnOptionsClicked</color>");
        AudioManager.PlayClick();
        CanvasManager.Instance.ShowCanvasOptions();
    }

    public void OnMusicClicked()
    {
        Debug.Log("<color=green>OnMusicClicked</color>");
        AudioManager.PlayClick();
    }

    public void OnHelpAboutClicked()
    {
        Debug.Log("<color=green>OnHelpAboutClicked</color>");
        AudioManager.PlayClick();
        CanvasManager.Instance.ShowCanvasHelp();
    }
}
