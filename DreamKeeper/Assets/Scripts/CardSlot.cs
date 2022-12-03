using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public void Awake()
    {
        gameObject.SetActive(false);
        CardFactory = FindObjectOfType<CardFactory>();
    }

    public void Activate(CardType cardType)
    {
        gameObject.SetActive(true);
        ACardManager = CardFactory.GetCard(cardType);
    }

    public void OnClick()
    {
        Debug.Log("Clicked");
        ACardManager.Invoke();
    }

    private ACardManager ACardManager;
    private CardFactory CardFactory;
}
