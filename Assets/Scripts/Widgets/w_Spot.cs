using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class w_Spot : MonoBehaviour
{
    public Image SpotImage;
    public TMP_Text ActionMessage;
    public TMP_Text ActionMessageShadow;

    protected soSpot so_Spot;
    protected Player player;

    public virtual void InitWidget(soSpot _soSpot)
    {
        so_Spot = _soSpot;
        player = PlayerManager.GetCurrentPlayer();
        ActionMessage.text = "YOU LANDED ON" + so_Spot.SpotName.ToUpper();
        SpotImage.sprite = so_Spot.SpotArt;
    }

    public void OnPassClicked()
    {
        Debug.Log("<color=green>OnPassClicked</color>");
        AudioManager.PlayClick();
        //TODO instantiate the auction panel widget
        Debug.Log("<color=green>Property goes to Auction</color>");
        // for now, bring back the bottom UI & destroy this widget
        DestroyUI();
    }

    public void DestroyUI()
    {
        CameraManager.SetCurrentCamera(eCameraPosition.main);
        CanvasManager.Instance.HUD.EvaluateEndTurn();
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
}
