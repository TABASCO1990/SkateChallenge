using UnityEngine;
/*
 * Выбор скина персонажа из магазина
 */
public class Skin : MonoBehaviour
{
    public Animator AC;

    public void ShowSkin()
    {
        gameObject.SetActive(true);
    }
    
    public void HideSkin()
    {
        gameObject.SetActive(false);
    }
}
