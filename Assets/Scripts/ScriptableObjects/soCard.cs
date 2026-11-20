using UnityEngine;

public enum eCardAction { AdjustCash, GetOutOfJail, GoToJail, GoTo, GoToNearestRR, GoToNearestUtil, GoBackThreeSpaces, HandleRepairs }

[CreateAssetMenu(fileName = "New Card", menuName = "Scriptable Objects/Create Card")]
public class soCard : ScriptableObject
{
	public eCardAction CardAction;
	public Sprite CardArt;


	[Header("Adjust Player Money")]
	public int Amount;
	public bool isPaidToOtherPlayers;

	[Header("Send To Destination")]
	public ePos Destination;

	[Header("Handle Repairs")]
	public int PerHouseAmount;
	public int PerHotelAmount;

	public void AdjustPlayerMoney(Player _player)
	{
		if (isPaidToOtherPlayers)
		{
			foreach (Player otherPlayer in PlayerManager.Instance.Players)
			{
				if (otherPlayer != _player && otherPlayer.soPlayerType.PlayerType != ePlayerType.none)
				{
					_player.AdjustPlayerMoney(-Amount);
					otherPlayer.AdjustPlayerMoney(Amount);
					if (Amount < 0)
					{
						Debug.Log(otherPlayer.PlayerName + " pays " + Amount + " to " + _player.PlayerName);
						continue;
					}
					Debug.Log(_player.PlayerName + " pays " + Amount + " to " + otherPlayer.PlayerName);
				}
			}
			return;
		}
		_player.AdjustPlayerMoney(Amount);
	}

	public void TravelToDestination(Player _player, bool isGoBack = false)
	{
		int travelAmount = Destination - _player.MvCore.currentPos;
		if (isGoBack)
		{
			travelAmount = (int)_player.MvCore.currentPos - 3;
		}
		if (travelAmount < 0)
		{
			travelAmount += (int)ePos.Terminator;
		}
		_player.MvCore.MovePlayer(travelAmount);
	}

	public void ActivateCardEffect(Player _player)
	{
		switch (CardAction)
		{
			case eCardAction.AdjustCash:
				AdjustPlayerMoney(_player);
				break;
			case eCardAction.GetOutOfJail:
				_player.GetOutOfJailFreeCard = this;
				Debug.Log(CardAction.ToString() + " card added to " + _player.PlayerName + "'s inventory");
				break;
			case eCardAction.GoToJail:
				_player.MvCore.MovePlayer(-99);
				break;
			case eCardAction.GoTo:
				TravelToDestination(_player);
				break;
			case eCardAction.GoToNearestRR:
				//TODO
				// Advance token to the nearest Railroad and pay owner Twice the Rental to which
				// he is otherwise entitled. If Railroad is unowned, you may buy it from the Bank.
				break;
			case eCardAction.GoToNearestUtil:
				//TODO
				// ADVANCE TOKEN TO NEAREST UTILITY. IF UNOWNED you may buy it from bank.
				// IF OWNED, throw dice and pay owner a total ten times the amount thrown.
				break;
			case eCardAction.GoBackThreeSpaces:
				bool isGoBack = true;
				TravelToDestination(_player, isGoBack);
				break;
			case eCardAction.HandleRepairs:
				//TODO
				int houseRepairCost = _player.mgPropManager.TotalHousesOwned * PerHouseAmount;
				int hotelRepairCost = _player.mgPropManager.TotalHotelsOwned * PerHotelAmount;
				Amount = -(houseRepairCost + hotelRepairCost);
				AdjustPlayerMoney(_player);
				break;
		}
	}
}
