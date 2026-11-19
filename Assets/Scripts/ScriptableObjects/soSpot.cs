using UnityEngine;

public enum eSpotType { DoNothing, Property, Railroad, Utilities, Chance, CommChest, Taxes, GoToJail }

[CreateAssetMenu(fileName = "New Spot", menuName = "Scriptable Objects/Create Spot")]
public class soSpot : ScriptableObject
{
	public string SpotName; //Player-facing name of the spot
	public ePos SpotPosition;
	public eSpotType SpotType;
	public Sprite SpotArt;
	public AudioClip SpotAudio;

	public virtual int GetPrice()
	{
		return -99;
	}

	public virtual int GetTax()
	{
		return -99;
	}

	public virtual int GetRent(int _amount)
	{
		return -99;
	}

	public virtual soSpotProp GetSpotProp()
	{
		return null;
	}

	public virtual soCard GetCard()
	{
		return null;
	}
}
