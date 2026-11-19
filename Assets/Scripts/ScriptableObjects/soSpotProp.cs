using UnityEngine;

[CreateAssetMenu(fileName = "New Property Spot", menuName = "Scriptable Objects/Create Property Spot")]
public class soSpotProp : soSpot
{
	public int Price;
	public int[] Rent;
	public soSpotProp[] OtherPropsInGroup;
	[Tooltip("Cost to build houses")]
	public int BuildingCost;
	public bool canBuild;
	public int HouseCount;
	public int MortgageValue;
	public bool canMortgage;
	public bool isMortgaged;
	public Sprite SpotBackArt;
	public Player PropOwner;

	public override int GetPrice()
	{
		return Price;
	}

	public override int GetRent(int _houseCount)
	{
		return Rent[_houseCount]; //TODO change depending on spotType
	}

	public override soSpotProp GetSpotProp()
	{
		return this;
	}

	public virtual soSpotRail GetSpotRail()
	{
		return null;
	}

	public virtual soSpotUtil GetSpotUtil()
	{
		return null;
	}
}
