using UnityEngine;

/// <summary>
/// Canvas: Input: view roll history graph
/// </summary>
public class RollHistory : MonoBehaviour
{
    private static readonly int hashToStats = Animator.StringToHash("ToStats");
    private static readonly int hashToInfo = Animator.StringToHash("ToInfo");
    private static readonly int hashToDiceRolls = Animator.StringToHash("ToDiceRolls");

    public Animator PanelAnimator;

    public GameObject InfoPanel;

    public void PopulateRollsGraph()
    {
        //TODO
    }

    public void OnInfoClicked()
    {
        Debug.Log("<color=green>Info Clicked - Show Info Panel</color>");
        AudioManager.PlayClick();
        InfoPanel.SetActive(true);
        PanelAnimator.SetTrigger(hashToInfo);
    }

    public void OnOKClicked(bool isInfo = false)
    {
        if (!isInfo)
        {
            Debug.Log("<color=green>OK Clicked - Hide Roll History Canvas</color>");
            PanelAnimator.SetTrigger(hashToStats);
        }
        else
        {
            Debug.Log("<color=green>OK Clicked - Hide Info Panel</color>");
            PanelAnimator.SetTrigger(hashToDiceRolls);
        }
        AudioManager.PlayClick();
    }

    private void ResetTriggers() //AnimEvent: ShrinkAnim start
    {
        foreach (AnimatorControllerParameter param in PanelAnimator.parameters)
        {
            PanelAnimator.ResetTrigger(param.name);
        }
    }

    private void TransitionToCanvasStats() //AnimEvent: DiceRolls_ShrinkAnim end
    {
        CanvasManager.Instance.ShowCanvasStats();
        CanvasManager.Instance.HideCanvasRollHistory();
    }

    private void TransitionToIdle()  //AnimEvent: Info_ShrinkAnim end
    {
        InfoPanel.SetActive(false);
    }
}
