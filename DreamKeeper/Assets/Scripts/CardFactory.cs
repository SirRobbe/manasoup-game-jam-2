using UnityEngine;

public class CardFactory : MonoBehaviour
{
    public ACardManager GetCard(CardType cardType)
    {
        switch (cardType)
        {
            case CardType.Emperor:
            {
                return Instantiate(BarrierManagerPrefab);
            }
        }
        
        Debug.LogError($"No Manager for CardType {cardType} found");
        return null;
    }

    public BarrierManager BarrierManagerPrefab;
}
