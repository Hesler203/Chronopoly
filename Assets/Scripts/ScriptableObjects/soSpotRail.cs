using UnityEngine;

[CreateAssetMenu(fileName = "New Railroad Spot", menuName = "Scriptable Objects/Create Railroad Spot")]
public class soSpotRail : soSpotProp
{
	public soSpotRail[] OtherRailsInGroup;

	public override int GetRent(int _railCountOwned)
	{
		return Rent[_railCountOwned];
	}

  public override soSpotRail GetSpotRail()
  {
    return this;
  }
}