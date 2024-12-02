using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GUManager : MonoBehaviour
{
    public bool ShopActivated = false;
    public Button buyButton;
    public Button reelButton;

    private PlayerController _controller;
    [SerializeField] private GameObject Keeper; 
    [SerializeField] private Player _player;
    
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject UpgradeShop;
    bool canInteract = false;
    
    
    
    public void OnSelect(string SoundBiteToPlay)
    {
        PlayAudio(SoundBiteToPlay);
    }

    public void PlayInfoText(string info, UpgradeDataContainer upgrade = null)
    {
        switch (info)
        {
            case "Power":
                WindowsVoice.speak("The power upgrade helps you deplete the stamina of fish faster."); 
                break;
            case "Reeling":
                WindowsVoice.speak("The reeling upgrade helps you reel in fish faster.");
                break;
            case "Rarity":
                WindowsVoice.speak("The rarity upgrade helps you find rare fish more often.");
                break;
            case "Price":
                break;

        }

        if (upgrade != null)
        {
            WindowsVoice.speak("The upgrade costs " + upgrade.CostOfUpgrade + ". You have " + _player.upgradePoints);
        }
       

    }

    public void ClickBuyButton()
    {
        if ((!canInteract))
        {
            return; 
        }

        Debug.Log("Click Buy button");
        //Show Upgrade menu
        UpgradeShop.SetActive(true);
        MainMenu.SetActive(false);
        reelButton.Select();
    }
    public void ClickExitButton()
    {
        Debug.Log("Click Exit button");
        if(_controller == null)
        {
            _controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
        PlayerController.GetInstance().DisableMovement(true);

        gameObject.SetActive(false);
        UpgradeShop.SetActive(false);
        MainMenu.SetActive(true);
        
        PlayAudio("GoodBye");

    }
    
    public void ClickSellFishButton()
    {
        Debug.Log("Click Sell  button");
        if (_player.HowManyFishesHasBeenCaught() < 1)
        {
            PlayAudio("NoFish");
            return;
        }
        string toSay = "You sold " + _player.HowManyFishesHasBeenCaught() + ". You gain "; 
        int fishSold = _player.SellFish();
        _player.upgradePoints += fishSold;
        WindowsVoice.speak(toSay + fishSold + " upgrade points. You now have "+  _player.upgradePoints + " upgrade points");
        
        WindowsVoice.speak("You sold ");
        //PlayAudio("SoldFish");
    }
   
    // Start is called before the first frame update
    void Start()
    {
     buyButton.Select();
     


    }

    public void BackButton()
    {
        UpgradeShop.SetActive(false);
        MainMenu.SetActive(true);
        buyButton.Select();
    }
    public void Buy(UpgradeDataContainer upgrade)
    {
        if (upgrade == null)
        {
            Debug.Log("Invalid upgrade data");
            return;
        }
        Debug.Log(_player.upgradePoints);
        if (_player.upgradePoints < upgrade.CostOfUpgrade + SettingsLoader.GetInstance().GetSettings().UpgradeCost)
        {
            PlayAudio("YouCanNotAffordThatUpgrade");
            return;
        }
        Debug.Log(_player.upgradePoints);
        _player.upgradePoints -= (upgrade.CostOfUpgrade + SettingsLoader.GetInstance().GetSettings().UpgradeCost);
        _player.AddUpgrade(upgrade);
        PlayAudio("UpgradeBought");
    }
    
    void OnEnable()
    {
        buyButton.Select();
        Debug.Log("No"); 
        OnSelect("BuyButtonSelected");
        UpgradeShop.SetActive(false);
        MainMenu.SetActive(true);
        StartCoroutine(WaitForInput());
    }

    
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayAudio(string audioToPlay)
    {
        if (AudioManager.instance == null)
        {
            Debug.Log("No Audio Manager");
            return;
        }
        AudioManager.instance.Play(audioToPlay, Keeper);
    }

    IEnumerator WaitForInput()
    {
        canInteract = false; 
        yield return new WaitForSeconds(0.2f);
        canInteract = true; 


    }
}
