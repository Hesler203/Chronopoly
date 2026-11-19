using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Widget: Creates the property purchase panel in the HUD canvas for the property landed on
/// </summary>
public class w_PropPanel : MonoBehaviour
{
	private static readonly int ShowConfirmPanelHash = Animator.StringToHash("ShowConfirmPanel");
	private static readonly int HideConfirmPanelHash = Animator.StringToHash("HideConfirmPanel");

	public soSpotProp Prop;
	public Player currentPlayer;
	public TMP_Text PropPurchaseLabel;
	public Image PropFrontImage;

	public Animator PanelAnimator;

	public GameObject ConfirmPanel;
	public TMP_Text ConfirmPurchaseLabel;

	public void InitWidget(soSpotProp _prop)
	{
		Prop = _prop;
		currentPlayer = PlayerManager.GetCurrentPlayer();
		SetPanels();
	}

	private void SetPanels()
	{
		PropPurchaseLabel.SetText("Purchase \"" + Prop.SpotName + "\" for $" + Prop.Price + "?");
		PropFrontImage.sprite = Prop.SpotArt;

		ConfirmPurchaseLabel.SetText("Are you sure you wish to buy \"" + Prop.SpotName + "\" for $" + Prop.Price + "?");
	}

	public void OnPassClicked()
	{
		Debug.Log("<color=green>OnPassClicked</color>");
		AudioManager.PlayClick();
		//TODO instantiate the auction panel widget
		Debug.Log("<color=green>Property goes to Auction</color>");
		// for now, bring back the bottom UI & destroy this widget
		CanvasManager.Instance.HUD.BottomUI.SetActive(true);
		Destroy(this.gameObject);
	}

	public void OnManageClicked()
	{
		Debug.Log("<color=green>OnManageClicked</color>");
		AudioManager.PlayClick();
		//TODO
	}

	public void OnTradeClicked()
	{
		Debug.Log("<color=green>OnTradeClicked</color>");
		AudioManager.PlayClick();
		//TODO
	}

	public void OnBuyClicked()
	{
		Debug.Log("<color=green>OnBuyClicked</color>");
		AudioManager.PlayClick();
		ConfirmPanel.SetActive(true);
		PanelAnimator.SetTrigger(ShowConfirmPanelHash);
	}

	public void OnConfirmYesClicked()
	{
		Debug.Log("<color=green>OnConfirmClicked</color>");
		if (currentPlayer.mgPropManager.BuyProperty(Board.Instance.Spots[(int)currentPlayer.currentPos].so_Spot.GetSpotProp()))
		{
			CanvasManager.Instance.HUD.BottomUI.SetActive(true);
			Destroy(this.gameObject);
		}
		PanelAnimator.SetTrigger(HideConfirmPanelHash);
	}

	public void OnConfirmNoClicked()
	{
		Debug.Log("<color=green>OnConfirmClicked</color>");
		PanelAnimator.SetTrigger(HideConfirmPanelHash);
	}

	private void ResetTriggers() //AnimEvent: Shrink Anims start
	{
		foreach (AnimatorControllerParameter param in PanelAnimator.parameters)
		{
			PanelAnimator.ResetTrigger(param.name);
		}
	}

	private void HideConfirmPanel() //AnimEvent: Confirm Panel - Shrink Anim End
	{
		ConfirmPanel.SetActive(false);
	}
}
