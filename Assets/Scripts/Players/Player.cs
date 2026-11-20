using System.Collections;
using UnityEngine;

/// <summary>
/// Data (read/write): Holds player info (which will change)
/// set at frontend & updated ingame
/// </summary>
public class Player : MonoBehaviour
{
    [Header("Setup")]
    public Color PlayerColor;
    public int PlayerIndex;
    public string PlayerName;
    public soPlayerTypes soPlayerType;
    public soPlayerPieces soPlayerPiece;

    [Header("In-Game")]
    //public ePos currentPos;
    public Transform CameraPosition;
    public GameObject PlayerPiece;
    public int PlayerMoney;
    public int RollAmount;
    public bool isInJail;
    public soCard GetOutOfJailFreeCard;

    public w_HUD wHUD;
    public PropManager mgPropManager;
    public MoveCore MvCore;
    public HandleLandingPosition HandleLandingPos;

    public void AdjustPlayerMoney(int _amount)
    {
        PlayerMoney += _amount;
        wHUD?.ShowCash(PlayerMoney);
    }

    public void IncrementPlayerPiece()
    {
        ePlayerPiece piece = soPlayerPiece.PlayerPiece;
        piece++;
        if (piece == ePlayerPiece.terminator)
        {
            piece = 0;
        }
        soPlayerPiece = GameManager.Instance.soRefs.PlayerPieces[(int)piece];
    }

    public void IncrementPlayerType()
    {
        ePlayerType type = soPlayerType.PlayerType;
        type++;
        if (type == ePlayerType.terminator)
        {
            type = 0;
        }
        soPlayerType = GameManager.Instance.soRefs.PlayerTypes[(int)type];
    }

    public string GetPlayerPieceName()
    {
        return soPlayerPiece.PlayerPieceName;
    }
}
