using UnityEngine;
using UnityEngine.UI;

/*
 * Основное меню игры (методы кнопок)
 */
public class MainMenuController : MonoBehaviour
{
    public GameManager GM;
    public Sprite SoundOn, SoundOff;
    public Image SoundBtnImg;
    public ShopManager SM;
    public void PlayBtn()
    {
        AudioManager.Instance.MenuEffect();
        gameObject.SetActive(false);
        GM.StartGame();
    }
    public void OpenMenu()
    {
        AudioManager.Instance.MenuEffect();
        gameObject.SetActive(true);
    }
    public void QuitBtn()
    {
        AudioManager.Instance.MenuEffect();
        Application.Quit();
    }
    
    public void OpenShop()
    {
        AudioManager.Instance.MenuEffect();
        gameObject.SetActive(false);
        SM.OpenShop();
    }
    
    public void SoundBtn()
    {
        AudioManager.Instance.MenuEffect();
        GM.IsSound = !GM.IsSound;
        SoundBtnImg.sprite = GM.IsSound ? SoundOn : SoundOff;
        AudioManager.Instance.RefreshSoundState();
    }
    
}
