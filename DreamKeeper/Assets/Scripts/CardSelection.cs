using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum CardType
{
    Tower,
    Death,
    Emperor,
    Lovers,
    HangedMan,
}

public class CardSelection : MonoBehaviour
{
    public void SelectCard(CardSelectionButton button)
    {
        if(SelectedCards.Contains(button.CardType))
        {
            SelectedCards.Remove(button.CardType);
            button.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (SelectedCards.Count < 2)
        {
            SelectedCards.Add(button.CardType);
            button.transform.GetChild(1).gameObject.SetActive(true);
        }
        
        UpdateSelectedCardsText();

        DreamButton.interactable = SelectedCards.Count == CardsToSelect;
    }

    public void StartGame()
    {
        gameObject.SetActive(false);
        GameState.s_SelectedCardTypes = SelectedCards;
        FindObjectOfType<GameLogic>().Begin();
    }
    
    private void Awake()
    {
        DreamButton.interactable = false;
        UpdateSelectedCardsText();
    }

    private void UpdateSelectedCardsText()
    {
        SelectedCardsText.text = $"Selected Cards {SelectedCards.Count}/{CardsToSelect}";
    }

    public TextMeshProUGUI SelectedCardsText;

    public Button DreamButton;
    
    [HideInInspector] public List<CardType> SelectedCards = new List<CardType>();
    public int CardsToSelect = 2;
}