using System.Collections.Generic;
using UnityEngine;

/*
 * Управление в окне магазина
 */
public class ShopManager : MonoBehaviour
{
    public List<ShopItem> Items;
    public ShopItem.ItemType ActiveSkin;
    public GameManager GM;
    public MainMenuController MC;
    

    public void OpenShop()
    {
        AudioManager.Instance.MenuEffect();
        GM.WinScreen.SetActive(false);
        CheckItemButtons();
        gameObject.SetActive(true);
    }
    
    public void CloseShop()
    {
        
        gameObject.SetActive(false);
    }
    
    public void CloseShopToMenu()
    {
        gameObject.SetActive(false);
        MC.OpenMenu();
        
    }

    public void PlayBtn()
    {
        gameObject.SetActive(false);
        GM.StartGame();
    }

    public void CheckItemButtons()
    {
        foreach (ShopItem item in Items)
        {
            item.SM = this;
            item.Init();
            item.CheckButtons();
        }
    }
}
