using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Manager: Singleton that creates players, and allows access to them & their methods
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public Player[] Players;
    public int CurrentPlayer;
    public const int MAXPLAYERS = 4;
    public const int STARTING_MONEY = 1500;
    public float[] Offsets = { .15f, .05f, -0.15f, -.05f };
    public float OffsetMultiplier = 12f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Instance already exists, destroying PlayerManager!");
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void CreatePlayers()
    {
        Players = new Player[MAXPLAYERS];
        for (int i = 0; i < MAXPLAYERS; i++)
        {
            Players[i] = new GameObject("Player " + (i + 1)).AddComponent<Player>();
            Players[i].transform.SetParent(transform);
            Players[i].PlayerColor = GameManager.Instance.soRefs.PlayerColors[i];
            Players[i].PlayerName = "Player " + (i + 1);
            Players[i].PlayerIndex = i;
            Players[i].soPlayerType = GameManager.Instance.soRefs.PlayerTypes[(int)ePlayerType.none];
            Players[i].soPlayerPiece = GameManager.Instance.soRefs.PlayerPieces[i];
            Players[i].AdjustPlayerMoney(STARTING_MONEY);
            Players[i].RollAmount = 0;
            Players[i].mgPropManager = Players[i].AddComponent<PropManager>();
            Players[i].MvCore = Players[i].AddComponent<MoveCore>();
            Players[i].HandleLandingPos = Players[i].AddComponent<HandleLandingPosition>();
        }
        Players[0].soPlayerType = GameManager.Instance.soRefs.PlayerTypes[(int)ePlayerType.human];
        Players[1].soPlayerType = GameManager.Instance.soRefs.PlayerTypes[(int)ePlayerType.ai];
    }

    public bool IsPlayerPieceDuplicate(int _playerIndex)
    {
        for (int i = 0; i < Players.Length; i++)
        {
            if (i != _playerIndex && Players[i].soPlayerType.PlayerType != ePlayerType.none)
            {
                if (Players[i].soPlayerPiece.PlayerPiece == Players[_playerIndex].soPlayerPiece.PlayerPiece)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void DetermineTurnOrder()
    {
        Player playerWithHighestRoll = new GameObject().AddComponent<Player>();
        Player temp = playerWithHighestRoll;
        playerWithHighestRoll.RollAmount = 0;
        foreach (Player player in Players)
        {
            //if (GetPlayerType(player.PlayerIndex) != ePlayerType.none && player.RandomizeRoll() >= playerWithHighestRoll.RollAmount)
            {
                while (player.RollAmount == playerWithHighestRoll.RollAmount)
                {
                    Debug.Log("Tie. " + player.PlayerName + " needs to re-roll.");
                    //player.RandomizeRoll();
                }
                playerWithHighestRoll = (player.RollAmount > playerWithHighestRoll.RollAmount) ? player : playerWithHighestRoll;
            }
        }
        Debug.Log(playerWithHighestRoll.PlayerName + " had the highest roll & will go first");
        //playerWithHighestRoll.IsCurrentTurn = true;
        Destroy(temp.gameObject);
    }

    public static ePlayerType GetPlayerType(int _playerIndex)
    {
        return Instance.Players[_playerIndex].soPlayerType.PlayerType;
    }

    public static Player GetCurrentPlayer()
    {
        return Instance.Players[Instance.CurrentPlayer];
    }

    public static Player WhoOwnsProperty(soSpotProp _soSpotProp)
    {
        foreach (Player player in Instance.Players)
        {
            if (player.soPlayerType.PlayerType != ePlayerType.none && player.mgPropManager.IsPropOwned(_soSpotProp))
            {
                return player;
            }
        }
        return null;
    }

    public void AdvanceToNextPlayer()
    {
        CurrentPlayer++;
        if (CurrentPlayer >= MAXPLAYERS)
        {
            CurrentPlayer = 0;
        }
        if (GetPlayerType(CurrentPlayer) == ePlayerType.none)
        {
            AdvanceToNextPlayer();
        }

        foreach (Player player in Players)
        {
            player.wHUD?.SetTabBGActive(false);
        }
        GetCurrentPlayer().wHUD?.SetTabBGActive(true);

        DiceManager.Instance.ResetDice();
        CanvasManager.Instance.HUD.SetRollDiceButton(true);
    }

    public void CreatePieces()
    {
        for (int i = 0; i < MAXPLAYERS; i++)
        {
            if (GetPlayerType(i) != ePlayerType.none)
            {
                Transform spawnPoint = Board.Instance.Spots[(int)ePos.Go].transform;
                Vector3 newPos = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z + Offsets[i] * OffsetMultiplier);
                Players[i].PlayerPiece = Instantiate(Players[i].soPlayerPiece.PlayerPieceModel, newPos, spawnPoint.rotation);
                Players[i].CameraPosition = Players[i].PlayerPiece.GetComponent<Piece>().CameraPosition;
            }
        }
    }
}
