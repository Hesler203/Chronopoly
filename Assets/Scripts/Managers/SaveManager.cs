using UnityEngine;

public enum eLanguages { EnglishUS, EnglishUK, Terminator };
public enum eAnimationsToggle { Off, On, Terminator };
public enum eDiceColors { White, Black, Blue, Red, Purple, Green, Rainbow };
public enum ePossibleRolls { Roll2, Roll3, Roll4, Roll5, Roll6, Roll7, Roll8, Roll9, Roll10, Roll11, Roll12 };
public enum eDifficulties { Overall, VEasy, Easy, Medium, Hard, Length };
public enum eOutcomes { Won, Played, Length };
public enum eUsageSharingToggle { Off, On };

/// <summary>
/// Manager: Singleton that manages PlayerPrefs save data
/// </summary>
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    [NamedArray(typeof(eLanguages))] public string[] LANGUAGES = { "English - US", "English - UK", "Terminator" };

    [Header("Settings")]
    public eDifficulties Difficulty;
    public eLanguages Language; //array at enum index
    public string PlayerName;
    public eAnimationsToggle isAnimationsOn; //bool (0 false, 1 true)
    public eDiceColors DiceColor; //enum toString
    public eUsageSharingToggle isUsageSharingOn; //bool (0 false, 1 true)

    [Header("Game Stats")]
    public int[] OutcomeCounts = new int[(int)eDifficulties.Length * (int)eOutcomes.Length];
    public int MostMoneyEarned;
    public int HighestHouses;
    public int HighestHotels;

    [Header("Roll History")]
    private const int MAXROLLS = 11;
    public int[] PlayerRollCounts = new int[MAXROLLS];
    public int[] AIRollCounts = new int[MAXROLLS];

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Instance already exists, destroying SaveManager!");
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void ClearAllStats()
    {
        LoadSettings();
        PlayerPrefs.DeleteAll();
        SaveSettings();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("Difficulty", (int)Difficulty);
        PlayerPrefs.SetInt("Language", (int)Language);
        PlayerPrefs.SetFloat("MusicVolume", AudioManager.Instance.Volume[(int)eMixers.Music]);
        PlayerPrefs.SetFloat("SFXVolume", AudioManager.Instance.Volume[(int)eMixers.SFX]);
        PlayerPrefs.SetString("PlayerName", PlayerName);
        PlayerPrefs.SetInt("isAnimationsOn", (int)isAnimationsOn);
        PlayerPrefs.SetInt("DiceColor", (int)DiceColor);
        PlayerPrefs.SetInt("MusicTrack", (int)AudioManager.Instance.CurrentTrack);
        PlayerPrefs.SetInt("isUsageSharingOn", (int)isUsageSharingOn);
    }

    public void LoadSettings()
    {
        Difficulty = (eDifficulties)PlayerPrefs.GetInt("Difficulty", (int)eDifficulties.Easy);
        Language = (eLanguages)PlayerPrefs.GetInt("Language", (int)eLanguages.EnglishUS);
        AudioManager.Instance.Volume[(int)eMixers.Music] = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        AudioManager.Instance.Volume[(int)eMixers.SFX] = PlayerPrefs.GetFloat("SFXVolume", 1f);
        PlayerName = PlayerPrefs.GetString("PlayerName", "Player 01");
        isAnimationsOn = (eAnimationsToggle)PlayerPrefs.GetInt("isAnimationsOn", (int)eAnimationsToggle.On);
        DiceColor = (eDiceColors)PlayerPrefs.GetInt("DiceColor", (int)eDiceColors.White);
        AudioManager.Instance.SetMusicTrack((eMusicTrack)PlayerPrefs.GetInt("MusicTrack", (int)eMusicTrack.Swing));
        isUsageSharingOn = (eUsageSharingToggle)PlayerPrefs.GetInt("isUsageSharingOn", (int)eUsageSharingToggle.On);
    }

    public void SaveGameStats(eOutcomes _outcome)
    {
        int newOverallCount = ++OutcomeCounts[(int)_outcome];
        PlayerPrefs.SetInt(eDifficulties.Overall.ToString() + _outcome.ToString(), newOverallCount);

        int skipOverall = 1;
        int newOutcomeCount = ++OutcomeCounts[(int)Difficulty + (int)_outcome + skipOverall];
        PlayerPrefs.SetInt(Difficulty.ToString() + _outcome.ToString(), newOutcomeCount++);

        PlayerPrefs.SetInt("MostMoneyEarned", MostMoneyEarned);
        PlayerPrefs.SetInt("HighestHouses", HighestHouses);
        PlayerPrefs.SetInt("HighestHotels", HighestHotels);
    }

    public void LoadGameStats()
    {
        int outcomeIndex = 0;
        for (int d = 0; d < (int)eDifficulties.Length; d++)
        {
            for (int o = 0; o < (int)eOutcomes.Length; o++)
            {
                eDifficulties currentDifficulty = (eDifficulties)d;
                eOutcomes currentOutcome = (eOutcomes)o;
                OutcomeCounts[outcomeIndex] = PlayerPrefs.GetInt(currentDifficulty.ToString() + currentOutcome.ToString(), 0);
                outcomeIndex++;
            }
        }
        MostMoneyEarned = PlayerPrefs.GetInt("MostMoneyEarned", 0);
        HighestHouses = PlayerPrefs.GetInt("HighestHouses", 0);
        HighestHotels = PlayerPrefs.GetInt("HighestHotels", 0);
    }

    public void SaveDiceRollHistory()
    {
        for (int i = 0; i < MAXROLLS; i++)
        {
            int rollCount = PlayerRollCounts[i];
            if (rollCount > 0)
            {
                ePossibleRolls currentRoll = (ePossibleRolls)i;
                PlayerPrefs.SetInt(ePlayerType.human.ToString() + currentRoll.ToString(), rollCount);
            }
        }

        for (int i = 0; i < MAXROLLS; i++)
        {
            int rollCount = AIRollCounts[i];
            if (rollCount > 0)
            {
                ePossibleRolls currentRoll = (ePossibleRolls)i;
                PlayerPrefs.SetInt(ePlayerType.ai.ToString() + currentRoll.ToString(), rollCount);
            }
        }
    }

    public void LoadDiceRollHistory()
    {
        for (int i = 0; i < MAXROLLS; i++)
        {
            ePossibleRolls currentRoll = (ePossibleRolls)i;
            int rollCount = PlayerPrefs.GetInt(ePlayerType.human.ToString() + currentRoll.ToString(), 0);
            PlayerRollCounts[i] = rollCount;
        }

        for (int i = 0; i < MAXROLLS; i++)
        {
            ePossibleRolls currentRoll = (ePossibleRolls)i;
            int rollCount = PlayerPrefs.GetInt(ePlayerType.ai.ToString() + currentRoll.ToString(), 0);
            AIRollCounts[i] = rollCount;
        }
    }
}
