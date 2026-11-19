using UnityEngine;

[CreateAssetMenu(fileName = "New Tax Spot", menuName = "Scriptable Objects/Create Tax Spot")]
public class soSpotTax : soSpot
{
  public int Tax;

  public override int GetTax()
  {
    Debug.Log(PlayerManager.GetCurrentPlayer().PlayerName + " pays $" + Tax + " in taxes");
    return Tax;
  }

  public int GetTaxCalculation() //TODO may not be needed
  {
    float incomeTax = .1f;
    return Mathf.RoundToInt((float)PlayerManager.GetCurrentPlayer().PlayerMoney * incomeTax);
  }
}