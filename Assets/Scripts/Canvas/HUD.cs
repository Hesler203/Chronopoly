using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Canvas: Input: manages HUD buttons' inputs & stats display
/// </summary>
public class HUD : MonoBehaviour
{
    [Header("TopUI Widget SpawnArea")]
    public RectTransform GroupPlayerTabs;

    public TMP_Text HUDMessage;
    public Image ImageMessageBG;


    [Header("CenterUI Widget SpawnArea")]
    public RectTransform CenterWidgetSpawnPoint;

    [Header("BottomUI ButtonGroup")]
    public GameObject BottomUI;
    public GameObject ButtonRoll;
    public GameObject ButtonEndTurn;

    public void InitCanvas()
    {
        //PlayerManager.Instance.DetermineTurnOrder();
        SetRollDiceButton(true);
        HUDMessage.gameObject.SetActive(false);
        ImageMessageBG.gameObject.SetActive(false);

        for (int i = 0; i < PlayerManager.Instance.Players.Length; i++)
        {
            if (PlayerManager.Instance.Players[i].soPlayerType.PlayerType != ePlayerType.none)
            {
                w_HUD wHUD = Instantiate(Resources.Load<GameObject>("Widgets/w_PlayerTab"), GroupPlayerTabs).GetComponent<w_HUD>();
                wHUD.InitWidget(PlayerManager.Instance.Players[i]);
            }
        }
    }

    public void SetRollDiceButton(bool _active)
    {
        ButtonRoll.SetActive(_active);
        ButtonEndTurn.SetActive(!_active);
    }

    public void ShowHUDMessage(string _message, float _duration)
    {
        HUDMessage.text = _message;
        HUDMessage.gameObject.SetActive(true);
        ImageMessageBG.gameObject.SetActive(true);
        Invoke("HideHUDMessage", _duration);
    }

    public void HideHUDMessage()
    {
        HUDMessage.gameObject.SetActive(false);
        ImageMessageBG.gameObject.SetActive(false);
        CameraManager.SetCurrentCamera(eCameraPosition.main);
        EvaluateEndTurn();
    }

    public void EvaluateEndTurn()
    {
        BottomUI.SetActive(true);
        SetRollDiceButton(DiceManager.Instance.DoublesRolled);
    }

    public void OnEndTurnClicked()
    {
        Debug.Log("<color=green>OnEndTurnClicked</color>");
        AudioManager.PlayClick();
        PlayerManager.Instance.AdvanceToNextPlayer();
    }

    public void OnRollClicked()
    {
        Debug.Log("<color=green>OnRollClicked</color>");
        AudioManager.PlayClick();
        int diceRoll = DiceManager.Instance.RollDice();
        BottomUI.SetActive(false);
        PlayerManager.GetCurrentPlayer().MvCore.MovePlayer(diceRoll);
    }

    public void OnTradeClicked()
    {
        Debug.Log("<color=green>OnTradeClicked</color>");
        AudioManager.PlayClick();
        //TODO
    }

    public void OnManageClicked()
    {
        Debug.Log("<color=green>OnManageClicked</color>");
        AudioManager.PlayClick();
        //TODO
    }

    public void OnLogClicked()
    {
        Debug.Log("<color=green>OnLogClicked</color>");
        AudioManager.PlayClick();
        //TODO
    }

    public void OnPauseClicked()
    {
        Debug.Log("<color=green>OnPauseClicked</color>");
        AudioManager.PlayClick();
        CanvasManager.Instance.HideCanvasHUD();
        //TODO replace with animation trigger
        CanvasManager.Instance.ShowCanvasPause(); // canvasPause growAnim triggered
    }

    public void OnExitClicked()
    {
        Debug.Log("<color=green>OnExitClicked</color>");
        AudioManager.PlayClick();
        Destroy(PlayerManager.Instance.gameObject);
        SceneMgr.Instance.LoadScene(eScene.FrontEnd);
    }
}