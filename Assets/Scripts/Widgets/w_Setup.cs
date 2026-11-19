using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Widget: Creates the player buttons for the Setup canvas
/// </summary>
public class w_Setup : MonoBehaviour
{
    public GameObject ButtonAddPlayer;
    public GameObject GroupButtonChoices;

    //tooltip
    public Image[] ButtonsToChangeColor;

    private Player player;

    public Image PieceImage;
    public TMP_Text PieceName;
    public Image TypeImage;
    public TMP_Text TypeName;

    public void InitWidget(Player _player)
    {
        player = _player;
        ShowButtons();
        SetColor();
    }

    private void SetColor()
    {
        foreach (Image i in ButtonsToChangeColor)
        {
            i.color = player.PlayerColor;
        }
    }

    private void ShowButtons()
    {
        PieceImage.sprite = player.soPlayerPiece.PlayerPieceArt;
        PieceName.text = player.soPlayerPiece.PlayerPieceName;

        TypeImage.sprite = player.soPlayerType.PlayerTypeArt;
        TypeName.text = player.soPlayerType.PlayerTypeName;

        ButtonAddPlayer.SetActive(player.soPlayerType.PlayerType == ePlayerType.none);
        GroupButtonChoices.SetActive(player.soPlayerType.PlayerType != ePlayerType.none);
    }

    private void PracticeMethod()
    {
        PieceImage.sprite = GameManager.Instance.soRefs.PlayerPieces[(int)ePlayerPiece.battleship].PlayerPieceArt;
    }

    public void OnPieceClicked()
    {
        Debug.Log("<color=green>OnPieceClicked</color>");
        player.IncrementPlayerPiece();
        ShowButtons();
        if (PlayerManager.Instance.IsPlayerPieceDuplicate(player.PlayerIndex))
        {
            OnPieceClicked();
            return;
        }
        AudioManager.PlayClick();
    }

    public void OnTypeClicked()
    {
        Debug.Log("<color=green>OnTypeClicked</color>");
        player.IncrementPlayerType();
        ShowButtons();

        GetComponentInParent<Setup>().IsPlayerPieceChosen();

        if (PlayerManager.Instance.IsPlayerPieceDuplicate(player.PlayerIndex))
        {
            OnPieceClicked();
            return;
        }
        AudioManager.PlayClick();
    }
}
