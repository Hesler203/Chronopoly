using UnityEngine;

/// <summary>
/// Manager: controls dice rolling
/// </summary>
public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance;

    [Header("Cheats")]
    [SerializeField] private bool isRiggedDice;
    [SerializeField] private int riggedDice1;
    [SerializeField] private int riggedDice2;

    [Header("Stats")]
    public bool DoublesRolled;
    public int DoublesCount;

    void Awake()
    {
        Instance = this;
    }

    public void ResetDice()
    {
        DoublesCount = 0;
        DoublesRolled = false;
    }

    public int RollDice()
    {
        int minRoll = 1;
        int maxRoll = 6;
        int dice1 = Random.Range(minRoll, ++maxRoll);
        int dice2 = Random.Range(minRoll, ++maxRoll);

        if (isRiggedDice)
        {
            dice1 = riggedDice1;
            dice2 = riggedDice2;
        }

        Debug.Log("Dices rolled: " + dice1 + " & " + dice2);
        int totalRoll = dice1 + dice2;

        if (dice1 == dice2)
        {
            DoublesRolled = true;
            DoublesCount++;
        }
        else
        {
            DoublesRolled = false;
        }

        if (DoublesCount > 2)
        {
            Debug.Log("Go To Jail");
            return -99;
        }

        return totalRoll;
    }
}
