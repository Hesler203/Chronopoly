using UnityEngine;

/// <summary>
/// Data (read): Holds info for all player pieces
/// </summary>
public enum ePlayerPiece { battleship, car, cat, dog, hat, shoe, thimble, wheelbarrow, terminator }

[CreateAssetMenu(fileName = "New Player Piece", menuName = "Scriptable Objects/Create Player Piece")]
public class soPlayerPieces : ScriptableObject
{
  public ePlayerPiece PlayerPiece;
  public string PlayerPieceName;
  public Sprite PlayerPieceArt;
  public GameObject PlayerPieceModel;
  public AudioClip AudioMove;
}
