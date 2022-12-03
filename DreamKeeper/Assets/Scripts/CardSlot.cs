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

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Activate(CardType cardType)
    {
        gameObject.SetActive(true);
        ACardManager = CardFactory.GetCard(cardType);
        GetComponent<Image>().sprite = CardFactory.GetLowResSprite(cardType);
    }

    public void OnClick()
    {
        if(Mathf.Approximately(ACardManager.GetCooldown(), 0f))
        {
            ACardManager.Invoke();
            Button.interactable = false;
        }
    }

    private void Update()
    {
        Button.interactable = Mathf.Approximately(ACardManager.GetCooldown(), 0f);
    }

    private ACardManager ACardManager;
    private CardFactory CardFactory;
    public Button Button;
}
