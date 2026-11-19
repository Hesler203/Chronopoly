using UnityEngine;

/// <summary>
/// Data (read): Holds info for all player types (none, human, AI)
/// </summary>

public enum ePlayerType { none, human, ai, terminator }

[CreateAssetMenu(fileName = "New Player Type", menuName = "Scriptable Objects/Create Player Type")]
public class soPlayerTypes : ScriptableObject
{
  public ePlayerType PlayerType;
  public string PlayerTypeName;
  public Sprite PlayerTypeArt;
}
