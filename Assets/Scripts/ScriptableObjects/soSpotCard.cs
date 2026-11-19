using UnityEngine;

[CreateAssetMenu(fileName = "New Card Spot", menuName = "Scriptable Objects/Create Card Spot")]
public class soSpotCard : soSpot
{
  public soCard[] soCards;
  public int RiggedCard = -99;

  public void ProcessCard(int _focusedCard)
  {
    soCard card = soCards[_focusedCard];
    card.ActivateCardEffect(PlayerManager.GetCurrentPlayer());
  }

  public override soCard GetCard()
  {
    if (RiggedCard != -99)
    {
      return soCards[RiggedCard];
    }
    else
    {
      return soCards[Random.Range(0, soCards.Length)];
    }
  }
}
