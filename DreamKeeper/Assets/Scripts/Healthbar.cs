using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    
    public void Update()
    {
        Image.fillAmount = (float)Hero.DreamDepth / (float)Hero.MaxDreamDepth;
    }

    public Image Image;
    public Hero Hero;
}
