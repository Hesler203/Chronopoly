using TMPro;
using UnityEngine;

/// <summary>
/// Canvas: Input: view or reset game stats & roll history
/// </summary>
public class Stats : MonoBehaviour
{
    private static readonly int hashToFE = Animator.StringToHash("ToFE");
    private static readonly int hashToIdleYes = Animator.StringToHash("ToIdleYes");
    private static readonly int hashToIdleNo = Animator.StringToHash("ToIdleNo");
    private static readonly int hashToReset = Animator.StringToHash("ToReset");
    private static readonly int hashToRollHistory = Animator.StringToHash("ToRollHistory");

    public Animator PanelAnimator;

    public GameObject ResetPanel;
    public TextMeshProUGUI[] StatTexts;

    public void PopulateStatLabels2(bool isReset = false) //can be used in case playerprefs isn't implemented
    {
        int outcomeStatCount = StatTexts.Length - 3;
        for (int i = 0; i <= outcomeStatCount; i++)
        {
            if (i < outcomeStatCount)
            {
                StatTexts[i].text = "0/0";
                continue;
            }
            if (!isReset)
            {
                StatTexts[i++].text = "$2000";
                StatTexts[i++].text = "3";
            }
            else
            {
                StatTexts[i++].text = "$0";
                StatTexts[i++].text = "0";
            }
            StatTexts[i].text = "0";
        }
    }

    public void PopulateStatLabels(bool isDemoReset = false)
    {
        for (int i = 0; i < StatTexts.Length; i++)
        {
            int j = i;
            int k = i + 1;
            if (i < SaveManager.Instance.OutcomeCounts.Length / 2)
            {
                if (i > 0)
                {
                    j = i + i;
                    k = j + 1;
                }
                string stat1 = SaveManager.Instance.OutcomeCounts[j].ToString();
                string stat2 = SaveManager.Instance.OutcomeCounts[k].ToString();
                string statPair = stat1 + "/" + stat2;
                StatTexts[i].text = statPair;
                continue;
            }
            if (!GameManager.Instance.isDemo)
            {
                StatTexts[i++].text = "$" + SaveManager.Instance.MostMoneyEarned;
                StatTexts[i++].text = SaveManager.Instance.HighestHouses.ToString();
            }
            else if (!isDemoReset)
            {
                StatTexts[i++].text = "$2000";
                StatTexts[i++].text = "3";
            }
            else
            {
                StatTexts[i++].text = "$0";
                StatTexts[i++].text = "0";
            }
            StatTexts[i].text = SaveManager.Instance.HighestHotels.ToString();
        }
    }

    public void OnBackClicked()
    {
        Debug.Log("<color=green>OnBackClicked</color>");
        AudioManager.PlayClick();
        PanelAnimator.SetTrigger(hashToFE);
    }

    private void TransitionToCanvasFE() //AnimEvent: ShrinkAnim end
    {
        CanvasManager.Instance.HideCanvasStats();
    }

    public void OnResetStatsClicked()
    {
        Debug.Log("<color=green>OnResetStatsClicked</color>");
        AudioManager.PlayClick();
        ResetPanel.SetActive(true);
        PanelAnimator.SetTrigger(hashToReset);
    }

    public void OnYesResetClicked()
    {
        Debug.Log("<color=green>OnYesClicked</color>");
        AudioManager.PlayClick();
        PanelAnimator.SetTrigger(hashToIdleYes);
    }

    public void OnNoResetClicked()
    {
        Debug.Log("<color=green>OnNoClicked</color>");
        AudioManager.PlayClick();
        PanelAnimator.SetTrigger(hashToIdleNo);
    }

    private void ResetStats()
    {
        SaveManager.Instance.ClearAllStats();
        if (!GameManager.Instance.isDemo)
        {
            PopulateStatLabels();
        }
        else
        {
            bool isDemoReset = true;
            PopulateStatLabels(isDemoReset);
        }
    }

    public void OnDiceStatsClicked()
    {
        Debug.Log("<color=green>OnDiceStatsClicked</color>");
        AudioManager.PlayClick();
        PanelAnimator.SetTrigger(hashToRollHistory);
    }

    private void TransitionToRollHistory() //AnimEvent: ShrinkAnim end
    {
        CanvasManager.Instance.ShowCanvasRollHistory();
        CanvasManager.Instance.HideCanvasStats();
    }

    private void ResetTriggers() //AnimEvent: ShrinkAnim start
    {
        foreach (AnimatorControllerParameter param in PanelAnimator.parameters)
        {
            PanelAnimator.ResetTrigger(param.name);
        }
    }

    private void TransitionToIdle() //AnimEvent: Reset_ShrinkAnim end
    {
        ResetPanel.SetActive(false);
    }
}
