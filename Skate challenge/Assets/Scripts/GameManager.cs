using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * Игровой менеджер 
 */
public class GameManager : MonoBehaviour
{
    public PlayerMovement PM;
    public RoadSpawner RS;
    
    public GameObject LoseScreen;
    public GameObject WinScreen;
    public GameObject InstructionScreen1; // экран инструкций 1
    public GameObject InstructionScreen2; // экран инструкций 2
    
    public Animator zoomButtonW;
    public Animator zoomButtonS;
    
    public TextMeshProUGUI CoinsTxt;
    public TextMeshProUGUI LevelTxt;
    public TextMeshProUGUI BulletTxt;

    public int BulletCount;
    public int CoinsCount = 0;
    public int LevelCount = 1;
    public float MoveSpeed;
    
    public bool CanPlay = true;
    public bool IsSound = true;
    
    public List<Skin> Skins; // массив скинов персонажей (доступных в магазине за "Coins")
    
    //Метод рестарта и запуска игры
    public void StartGame()
    {
        AudioManager.Instance.MenuEffect();
        RS.StartGame();
        CanPlay = true;
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
        PM.SkinAnimator.SetTrigger("respawn");
        StartCoroutine(FixTrigger());
    }
    
    IEnumerator FixTrigger()
    {
        yield return null;
        PM.SkinAnimator.ResetTrigger("respawn");
    }

    private void Update()
    {
        BulletTxt.text = BulletCount.ToString();
    }

    public void ShowResult()
    {
        LoseScreen.SetActive(true);
        SaveManager.Instance.SaveGame();
    }

    public void AddCoins(int number)
    {
        CoinsCount += number;
        RefreshText();
    }

    public void RefreshText()
    {
        CoinsTxt.text = CoinsCount.ToString();
        LevelTxt.text = "Level: " + LevelCount;
    }
    
    public void AddLevel()
    {
        LevelCount++;
        RefreshText();
    }
    public void ShowFinish()
    {
        WinScreen.SetActive(true);
        SaveManager.Instance.SaveGame();
    }
    
    public void ActivateSkin(int skinIndex, bool setTrigger = false)
    {
        foreach (var skin in Skins)
            skin.HideSkin();
        Skins[skinIndex].ShowSkin();
        PM.SkinAnimator = Skins[skinIndex].AC;
        if(setTrigger)
            PM.SkinAnimator.SetTrigger("death");
    }
}
