using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Canvas: Input: decide how many players will play, whether human or ai, and what piece each will use
/// </summary>
public class Setup : MonoBehaviour
{
    public RectTransform GroupPlayerButtons;
    public Button ButtonPlay;

    public void InitCanvas()
    {
        for (int i = 0; i < PlayerManager.MAXPLAYERS; i++)
        {
            w_Setup wSetup = Instantiate(Resources.Load<GameObject>("Widgets/w_PlayerButtons"), GroupPlayerButtons).GetComponent<w_Setup>();
            wSetup.InitWidget(PlayerManager.Instance.Players[i]);
        }
    }

    public void IsPlayerPieceChosen()
    {
        int humanPlayers = 0;
        int activePlayers = 0;

        foreach (Player player in PlayerManager.Instance.Players)
        {
            if (player.soPlayerType.PlayerType == ePlayerType.human)
            {
                humanPlayers++;
            }
            if (player.soPlayerType.PlayerType != ePlayerType.none)
            {
                activePlayers++;
            }

            SetButtonPlay(humanPlayers > 0 && activePlayers > 1);
        }
    }

    private void SetButtonPlay(bool _active)
    {
        ButtonPlay.interactable = _active;
    }

    public void OnPlayClicked()
    {
        Debug.Log("<color=green>OnPlayClicked</color>");
        AudioManager.PlayClick();
        SceneMgr.Instance.LoadScene(eScene.InGame);
    }

    public void OnCancelClicked()
    {
        Debug.Log("<color=green>OnCancelClicked</color>");
        AudioManager.PlayClick();
        CanvasManager.Instance.HideCanvasSetup();
    }
}
