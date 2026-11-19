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
    public ePos currentPos;
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

    public void MovePlayer(int diceRoll)
    {
        if (diceRoll == -99)
        {
            currentPos = ePos.JustVisiting;
            isInJail = true;
            Debug.Log("Is in Jail");
            PlayerManager.Instance.AdvanceToNextPlayer();
            //TODO needs coroutine to wait
            CanvasManager.Instance.HUD.EvaluateEndTurn();
            return;
        }

        currentPos += diceRoll;

        if (currentPos >= ePos.Terminator)
        {
            int passedGoCashBonus = 200;
            AdjustPlayerMoney(passedGoCashBonus);
            currentPos -= ePos.Terminator;
        }

        StartCoroutine(MoveToSpot());
    }

    IEnumerator MoveToSpot1()
    {
        yield return new WaitForSeconds(1.0f);
        CanvasManager.Instance.HUD.BottomUI.SetActive(true);
        CanvasManager.Instance.HUD.SetRollDiceButton(DiceManager.Instance.DoublesRolled);
        Debug.Log("Current Position: " + currentPos);
        MovePlayerToSpot(currentPos);
        //TODO remove this hack
        //mgPropManager.BuyProperty(Board.Instance.Spots[(int)currentPos].so_Spot.GetSpotProp());
    }

    IEnumerator MoveToSpot()
    {
        CameraManager.SetCurrentCamera(eCameraPosition.center, PlayerPiece.transform);
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Current Position: " + currentPos);
        MovePlayerToSpot(currentPos);
        yield return new WaitForSeconds(1.0f);
    }

    public void MovePlayerToSpot(ePos _pos)
    {
        PlayerManager pm = PlayerManager.Instance;
        Transform spotToMoveTo = Board.Instance.Spots[(int)_pos].transform;
        if (_pos == ePos.JustVisiting)
        {
            //TODO adjust position to account for JustVisiting corner
        }
        //TODO needs to travel along the clockwise path towards the spotToMoveTo.position
        PlayerPiece.transform.SetPositionAndRotation(spotToMoveTo.position, spotToMoveTo.rotation);
        PlayerPiece.transform.Translate(PlayerPiece.transform.right * (pm.Offsets[PlayerIndex] * pm.OffsetMultiplier), Space.World);
        //SetLandingSpot();
        HandleLandingPos.InitHandleLandingPosition(this, MvCore);
        HandleLandingPos.SetLandingSpot();
    }
}
