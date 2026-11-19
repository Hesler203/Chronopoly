using UnityEditor.VersionControl;
using UnityEngine;

public class HandleLandingPosition : MonoBehaviour
{
    private Player player;
    private MoveCore moveCore; //TODO

    public float MessageTime = 2.5f;

    public void InitHandleLandingPosition(Player _player, MoveCore _moveCore)
    {
        player = _player;
        moveCore = _moveCore;
    }

    public void SetLandingSpot()
    {
        // TODO set current spot & switch between spot types
        soSpot so_Spot = Board.GetSpot(player.currentPos);
        CameraManager.SetCurrentCamera(eCameraPosition.overhead);
        string message = null;

        switch (so_Spot.SpotType)
        {
            case eSpotType.Railroad:
            case eSpotType.Utilities:
            case eSpotType.Property:
                soSpotProp so_SpotProp = so_Spot.GetSpotProp();
                Player owner = PlayerManager.WhoOwnsProperty(so_SpotProp);
                if (owner == null)
                {
                    Debug.Log("No one owns this property - show purchase widget");
                    CanvasManager.Instance.ShowPurchaseWidget(so_Spot);
                }
                else if (owner != player)
                {
                    Debug.Log("This property is owned by another player - pay rent");
                    int rent = so_SpotProp.GetRent(so_SpotProp.HouseCount);
                    if (so_Spot.SpotType == eSpotType.Railroad)
                    {
                        rent = so_SpotProp.GetSpotRail().GetRent(owner.mgPropManager.GetRailCountOwned());
                    }
                    if (so_Spot.SpotType == eSpotType.Utilities)
                    {
                        rent = so_SpotProp.GetSpotUtil().GetRent(owner.mgPropManager.GetUtilCountOwned());
                    }
                    player.AdjustPlayerMoney(-rent);
                    owner.AdjustPlayerMoney(rent);
                    message = player.GetPlayerPieceName() + " paid $" + rent +
                                " in rent to " + owner.GetPlayerPieceName() + ".";
                    CanvasManager.Instance.HUD.ShowHUDMessage(message, MessageTime);
                }
                else
                {
                    Debug.Log("Current Player owns this property - do nothing");
                    message = owner.GetPlayerPieceName() + " owns this property.";
                    CanvasManager.Instance.HUD.ShowHUDMessage(message, MessageTime);
                }
                break;
            case eSpotType.Taxes:
                int tax = so_Spot.GetTax();
                player.AdjustPlayerMoney(-tax);
                message = player.GetPlayerPieceName() + " pays $" + tax + " in taxes.";
                CanvasManager.Instance.HUD.ShowHUDMessage(message, MessageTime);
                break;
            case eSpotType.Chance:
                // TODO
                //instantiate card widget
                //so_Spot.GetCard();
                CanvasManager.Instance.HUD.EvaluateEndTurn();
                break;
            case eSpotType.CommChest:
                // TODO
                //instantiate card widget
                //so_Spot.GetCard();
                CanvasManager.Instance.HUD.EvaluateEndTurn();
                break;
            case eSpotType.GoToJail:
                player.MovePlayer(-99);
                break;
            case eSpotType.DoNothing:
                CanvasManager.Instance.HUD.EvaluateEndTurn();
                break;
        }
    }
}
