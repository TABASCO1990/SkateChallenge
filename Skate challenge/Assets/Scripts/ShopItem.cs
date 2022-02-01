using UnityEngine;
using UnityEngine.UI;

/*
 * Предметы в магазине (покупка)
 */
public class ShopItem : MonoBehaviour
{
    public enum ItemType
    {
        FIRST_SKIN,
        SECOND_SKIN
    }

    public ItemType Type;
    public Button BuyBtn, ActivateBtn;
    public bool IsBought;
    public int Cost;
    
    bool IsActive
    {
        get
        {
            return Type == SM.ActiveSkin;
        }
    }
    private GameManager gm;
    public ShopManager SM;

    public void Init()
    {
        gm = FindObjectOfType<GameManager>();
    }
    
    public void CheckButtons()
    {
        BuyBtn.gameObject.SetActive(!IsBought);
        BuyBtn.interactable = CanByu();
        
        ActivateBtn.gameObject.SetActive(IsBought);
        ActivateBtn.interactable = !IsActive;
    }

    bool CanByu()
    {
        return gm.CoinsCount >= Cost;
    }

    public void BuyItem()
    {
        if(!CanByu())
            return;
        IsBought = true;
        gm.CoinsCount -= Cost;
        CheckButtons();
        AudioManager.Instance.MenuEffect();
        SaveManager.Instance.SaveGame();
        gm.RefreshText();
    }

    public void ActivateItem()
    {
        SM.ActiveSkin = Type;
        SM.CheckItemButtons();
        AudioManager.Instance.MenuEffect();
        switch (Type)
        {
            case ItemType.FIRST_SKIN:
                gm.ActivateSkin(0,true);
                break;
            case ItemType.SECOND_SKIN:
                gm.ActivateSkin(1,true);
                break;
        }
        SaveManager.Instance.SaveGame();
    }
}
