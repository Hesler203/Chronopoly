using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Widget: Creates the player tabs for the HUD canvas
/// </summary>
public class w_HUD : MonoBehaviour
{
    private Player player;
    public GameObject PlayerTab;

    public GameObject TabBG;
    public Image PlatformImage;
    public Image PieceImage;
    public TMP_Text PlayerMoney;
    public TMP_Text Shadow;

    public void InitWidget(Player _player)
    {
        player = _player;
        player.wHUD = this;
        ShowTab();
    }

    private void SetColor()
    {
        PlatformImage.color = player.PlayerColor;
    }

    private void ShowTab()
    {
        PieceImage.sprite = player.soPlayerPiece.PlayerPieceArt;
        SetColor();
        ShowCash(player.PlayerMoney);
        PlayerTab.SetActive(PlayerManager.GetPlayerType(player.PlayerIndex) != ePlayerType.none);
        SetTabBGActive(player.PlayerIndex == PlayerManager.Instance.CurrentPlayer);
    }

    public void SetTabBGActive(bool _active)
    {
        TabBG.SetActive(_active);
    }

    public void ShowCash(int _playerMoney)
    {
        PlayerMoney.text = "$" + _playerMoney;
        Shadow.text = "$" + _playerMoney;
    }
}
