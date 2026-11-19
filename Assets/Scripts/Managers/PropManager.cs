using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PropertyOwnership
{
    public string PropName;
    public soSpotProp so_SpotProp;
    public bool isMortgaged;
    public int HouseCount;
    public int HotelCount;

    public PropertyOwnership(soSpotProp _spotProp, bool _isMortgaged, int _houseCount, int _hotelCount)
    {
        so_SpotProp = _spotProp;
        PropName = so_SpotProp.GetSpotProp().SpotName;
        isMortgaged = _isMortgaged;
        HouseCount = _houseCount;
        HotelCount = _hotelCount;
    }
}

public class PropManager : MonoBehaviour
{
    private Player player;
    public List<PropertyOwnership> ListPropertiesOwned;
    public const int MAXHOUSES = 3;
    public int TotalHousesOwned;
    public int TotalHotelsOwned;

    void Awake()
    {
        player = GetComponentInParent<Player>();
        ListPropertiesOwned = new List<PropertyOwnership>();
    }

    public bool BuyProperty(soSpotProp _soSpotProp)
    {
        if (player.PlayerMoney >= _soSpotProp.GetPrice())
        {
            Debug.Log(player.PlayerName + " bought " + _soSpotProp.SpotName + " for $" + _soSpotProp.GetPrice());
            ListPropertiesOwned.Add(new PropertyOwnership(_soSpotProp, false, 0, 0));
            player.AdjustPlayerMoney(-_soSpotProp.GetPrice());
            AudioManager.PlayMoney();
            return true;
        }
        else
        {
            Debug.Log("Cannot buy property. Player does not have enough money.");
            AudioManager.PlayError();
            return false;
        }
    }

    public bool IsPropOwned(soSpotProp _soSpotProp)
    {
        foreach (PropertyOwnership property in ListPropertiesOwned)
        {
            if (property.so_SpotProp == _soSpotProp) return true;
        }
        return false;
    }

    public int GetHouseCount(soSpotProp _soSpotProp)
    {
        foreach (PropertyOwnership property in ListPropertiesOwned)
        {
            if (property.so_SpotProp == _soSpotProp) return _soSpotProp.HouseCount;
        }
        return 0;
    }

    public int GetRailCountOwned()
    {
        int rrOwned = 0;
        foreach (PropertyOwnership property in ListPropertiesOwned)
        {
            if (property.so_SpotProp.SpotType == eSpotType.Railroad) return rrOwned++;
        }
        return rrOwned;
    }

    public int GetUtilCountOwned()
    {
        int utilOwned = 0;
        foreach (PropertyOwnership property in ListPropertiesOwned)
        {
            if (property.so_SpotProp.SpotType == eSpotType.Utilities) return utilOwned++;
        }
        return utilOwned;
    }
}