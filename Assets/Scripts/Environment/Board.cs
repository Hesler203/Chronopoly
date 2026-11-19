using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data (read): Holds the board spots that players can land on
/// </summary
public class Board : MonoBehaviour
{
    public static Board Instance;
    [NamedArray(typeof(ePos))] public Spot[] Spots;

    public const int SALARY = 200;

    void Awake()
    {
        Instance = this;
    }

    public static soSpot GetSpot(ePos _pos)
    {
        return Instance.Spots[(int)_pos].so_Spot;
    }
}