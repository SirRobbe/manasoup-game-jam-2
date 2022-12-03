using System;
using UnityEngine;
using UnityEngine.UI;

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
        ACardManager.Invoke();
        Button.interactable = false;
    }

    private void Update()
    {
        Button.interactable = Mathf.Approximately(ACardManager.GetCooldown(), 0f);
    }

    private ACardManager ACardManager;
    private CardFactory CardFactory;
    public Button Button;
}
