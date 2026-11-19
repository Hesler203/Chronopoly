using TMPro;
using UnityEngine;

public class w_SpotProp : w_Spot
{
    public TMP_Text PurchaseMessage;

    public override void InitWidget(soSpot _soSpot)
    {
        base.InitWidget(_soSpot);
        ActionMessage.text = "FOR SALE";
        ActionMessageShadow.text = "FOR SALE";
        PurchaseMessage.text = "Purchase \"" + so_Spot.SpotName + "\" for $" + so_Spot.GetPrice() + "?";
    }

    public void OnBuyClicked()
    {
        Debug.Log("<color=green>OnBuyClicked</color>");
        if (player.mgPropManager.BuyProperty(so_Spot.GetSpotProp()))
        {
            DestroyUI();
            return;
        }
        CanvasManager.Instance.HUD.BottomUI.SetActive(false);
    }
}
