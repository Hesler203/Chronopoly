using UnityEngine;

/// <summary>
/// Manager: Singleton handling the gameObject that travels from scene to scene
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public soRef soRefs;
    public bool isDemo = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Instance already exists, destroying GameManager!");
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        Debug.Log("<color=yellow>Starting Game</color>");
        SaveManager.Instance.LoadSettings();
        SaveManager.Instance.LoadGameStats();
        SaveManager.Instance.LoadDiceRollHistory();

        //PracticeCode();
    }

    public void OnGameEnd()
    {
        //if player is winner TODO
        SaveManager.Instance.SaveGameStats(eOutcomes.Won);
        //else TODO
        SaveManager.Instance.SaveGameStats(eOutcomes.Played);
    }

    private void PracticeCode()
    {
        Debug.Log(soRefs.PlayerPieces[(int)ePlayerPiece.battleship].PlayerPieceName);

        Debug.Log(AudioManager.Instance.Volume[(int)eMixers.Music]);

        Debug.Log(PlayerManager.MAXPLAYERS);

        Debug.Log(soRefs.PlayerTypes[(int)ePlayerType.human].PlayerTypeName);
    }
}
