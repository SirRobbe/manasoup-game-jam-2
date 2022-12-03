using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSelection : MonoBehaviour
{
    public void SelectCard(GameObject card)
    {
        if(SelectedCards.Contains(card))
        {
            SelectedCards.Remove(card);
        }
        else
        {
            SelectedCards.Add(card);
        }
        
        UpdateSelectedCardsText();

        DreamButton.interactable = SelectedCards.Count == CardsToSelect;
    }

    public void StartGame()
    {
        gameObject.SetActive(false);
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
    
    [HideInInspector] public List<GameObject> SelectedCards = new List<GameObject>();
    public int CardsToSelect = 2;
}