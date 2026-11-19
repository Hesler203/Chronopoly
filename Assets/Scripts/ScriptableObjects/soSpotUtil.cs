using System.Reflection.Metadata.Ecma335;
using UnityEngine;

[CreateAssetMenu(fileName = "New Utility Spot", menuName = "Scriptable Objects/Create Utility Spot")]
public class soSpotUtil : soSpotProp
{
	public soSpotUtil OtherUtil;

	public override int GetRent(int _utilCountOwned)
	{
		return Rent[_utilCountOwned];
	}

	public override soSpotUtil GetSpotUtil()
	{
		return this;
	}
}
