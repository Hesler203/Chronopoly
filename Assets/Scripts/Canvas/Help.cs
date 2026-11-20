using UnityEngine;

/// <summary>
/// Canvas: Input: view about info & agreements
/// </summary>
public class Help : MonoBehaviour
{
    private static readonly string UrlEULA = "https://www.ea.com/legal/user-agreement";
    private static readonly string UrlPrivacy = "https://www.ea.com/legal/privacy-and-cookie-policy";
    private static readonly int hashToHelpIdle = Animator.StringToHash("ToHelpIdle");
    private static readonly int hashToFE = Animator.StringToHash("ToFE");
    private static readonly int hashToAbout = Animator.StringToHash("ToAbout");
    private static readonly int hashToEULA = Animator.StringToHash("ToEULA");
    private static readonly int hashToPrivacy = Animator.StringToHash("ToPrivacy");
    private static readonly int hashToUsage = Animator.StringToHash("ToUsage");
    private static readonly int hashToOverview = Animator.StringToHash("ToOverview");

    public Animator PanelAnimator;

    [Header("Panels")]
    public GameObject HelpPanel;
    public GameObject AboutPanel;
    public GameObject EULAPanel;
    public GameObject PrivacyPanel;
    public GameObject UsagePanel;
    public GameObject OverviewPanel;

    public void OnAboutClicked()
    {
        Debug.Log("<color=green>About Clicked - Show About Panel</color>");
        AudioManager.PlayClick();
        AboutPanel.SetActive(true);
        PanelAnimator.SetTrigger(hashToAbout);
    }
    public void OnEULAClicked()
    {
        Debug.Log("<color=green>EULA Clicked - Show EULA Panel</color>");
        AudioManager.PlayClick();
        EULAPanel.SetActive(true);
        PanelAnimator.SetTrigger(hashToEULA);
    }
    public void OnPrivacyClicked()
    {
        Debug.Log("<color=green>Privacy Clicked - Show Privacy Panel</color>");
        AudioManager.PlayClick();
        PrivacyPanel.SetActive(true);
        PanelAnimator.SetTrigger(hashToPrivacy);
    }
    public void OnUsageClicked()
    {
        Debug.Log("<color=green>Usage Clicked - Show Usage Panel</color>");
        AudioManager.PlayClick();
        UsagePanel.SetActive(true);
        PanelAnimator.SetTrigger(hashToUsage);
    }
    public void OnOverviewClicked()
    {
        Debug.Log("<color=green>Overview Clicked - Show Overview Panel</color>");
        AudioManager.PlayClick();
        OverviewPanel.SetActive(true);
        PanelAnimator.SetTrigger(hashToOverview);
    }

    public void OnHelpBackClicked(bool isPaused = false)
    {
        Debug.Log("<color=green>Back Clicked - Hide Help Canvas</color>");
        AudioManager.PlayClick();
        if (!isPaused)
        {
            PanelAnimator.SetTrigger(hashToFE);
        }
        else
        {
            //TODO hide this help canvas & bring up pause canvas
            // replace with animation trigger instead
            CanvasManager.Instance.HideCanvasHelp(); // shrinkAnimToPause triggered
            CanvasManager.Instance.ShowCanvasPause(); // called at shrinkAnimToPause end
        }
    }

    public void OnAboutBackClicked()
    {
        Debug.Log("<color=green>Back Clicked - Hide About Panel</color>");
        AudioManager.PlayClick();
        PanelAnimator.SetTrigger(hashToHelpIdle);
    }

    public void OnOverviewBackClicked()
    {
        Debug.Log("<color=green>Back Clicked - Hide Overview Panel</color>");
        AudioManager.PlayClick();
        PanelAnimator.SetTrigger(hashToHelpIdle);
    }

    public void OnEULAYesClicked()
    {
        Debug.Log("<color=green>Yes Clicked - Open UrlEULA & Exit Game</color>");
        AudioManager.PlayClick();
        Application.OpenURL(UrlEULA);
        Debug.Break(); //TODO should be Application.Quit() for build
    }

    public void OnEULANoClicked()
    {
        Debug.Log("<color=green>No Clicked - Hide EULA Panel</color>");
        AudioManager.PlayClick();
        PanelAnimator.SetTrigger(hashToHelpIdle);
    }

    public void OnPrivacyYesClicked()
    {
        Debug.Log("<color=green>Yes Clicked - Open UrlPrivacy & Exit Game</color>");
        AudioManager.PlayClick();
        Application.OpenURL(UrlPrivacy);
        Debug.Break(); //TODO should be Application.Quit() for build
    }

    public void OnPrivacyNoClicked()
    {
        Debug.Log("<color=green>No Clicked - Hide Privacy Panel</color>");
        AudioManager.PlayClick();
        PanelAnimator.SetTrigger(hashToHelpIdle);
    }

    public void OnDisableUsageClicked(bool disableUsageSharing = true)
    {
        if (disableUsageSharing)
        {
            Debug.Log("<color=green>Yes Clicked - Disable Usage Sharing</color>");
        }
        else
        {
            Debug.Log("<color=green>No Clicked - Enable Usage Sharing</color>");
        }
        AudioManager.PlayClick();
        SaveManager.Instance.isUsageSharingOn = !disableUsageSharing ? eUsageSharingToggle.On : eUsageSharingToggle.Off;
        SaveManager.Instance.SaveSettings();
        PanelAnimator.SetTrigger(hashToHelpIdle);
    }

    private void ResetTriggers() //AnimEvent: Shrink Anims start
    {
        foreach (AnimatorControllerParameter param in PanelAnimator.parameters)
        {
            PanelAnimator.ResetTrigger(param.name);
        }
    }

    private void TransitionToCanvasFE() //AnimEvent: Help_Shrink-ToFE Anim end
    {
        CanvasManager.Instance.HideCanvasHelp();
    }

    private void TransitionToAbout() //AnimEvent: Help_Shrink-ToAbout Anim end
    {
        AboutPanel.SetActive(true);
        HelpPanel.SetActive(false);
    }

    private void TransitionToOverview() //AnimEvent: Help_Shrink-ToOverview Anim end
    {
        OverviewPanel.SetActive(true);
        HelpPanel.SetActive(false);
    }

    private void TransitionToHelpIdle() //AnimEvent: Shrink Anims end
    {
        HelpPanel.SetActive(true);
        AboutPanel.SetActive(false);
        EULAPanel.SetActive(false);
        PrivacyPanel.SetActive(false);
        UsagePanel.SetActive(false);
    }
}
