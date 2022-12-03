using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public void Awake()
    {
        Slider = GetComponent<Slider>();
    }
    
    public void Update()
    {
        Slider.value = Hero.DreamDepth / 100f;
        Slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, Slider.value);
    }

    [HideInInspector] public Slider Slider;
    public Hero Hero;
}
