using UnityEngine;

public class CardFactory : MonoBehaviour
{
    public ACardManager GetCard(CardType cardType)
    {
        switch (cardType)
        {

            case CardType.Death:
            {
                return Instantiate(DeathPrefab);
            }

            case CardType.Lovers:
            {
                return Instantiate(LoversPrefab);
            }

            case CardType.HangedMan:
            {
                return Instantiate(HangedManPrefab);
            }
            
            case CardType.Emperor:
            {
                return Instantiate(BarrierManagerPrefab);
            }

            case CardType.Tower:
            {
                return Instantiate(TurretSpawnerPrefab);
            }
        }
        
        Debug.LogError($"No Manager for CardType {cardType} found");
        return null;
    }

    public Sprite GetLowResSprite(CardType cardType)
    {
        switch(cardType)
        {

            case CardType.Death:
            {
                return DeathSprite;
            }

            case CardType.Lovers:
            {
                return LoverSprite;
            }

            case CardType.HangedMan:
            {
                return HangedManSprite;
            }

            case CardType.Emperor:
            {
                return EmperorSprite;
            }

            case CardType.Tower:
            {
                return TowerSprite;
            }
        }
        
        Debug.LogError($"No Manager for CardType {cardType} found");
        return null;
    }

    public BarrierManager BarrierManagerPrefab;
    public TurretSpawner TurretSpawnerPrefab;
    public Nuke DeathPrefab;
    public Lovers LoversPrefab;
    public HangedMan HangedManPrefab;

    public Sprite DeathSprite;
    public Sprite LoverSprite;
    public Sprite EmperorSprite;
    public Sprite TowerSprite;
    public Sprite HangedManSprite;
}
