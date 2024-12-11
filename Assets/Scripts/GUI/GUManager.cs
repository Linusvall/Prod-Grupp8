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
    private SelectData SelectedButton;
    bool playTuturial = true; 
    
    
    public void OnSelect(string SoundBiteToPlay)
    {
        PlayAudio(SoundBiteToPlay);
        SelectedButton = null;
    }

    public void OnSelect(SelectData Data)
    {
        PlayAudio(Data.AudioToPlay);
        SelectedButton = Data; 
    }

    public void PlayInfoText(string info, UpgradeDataContainer upgrade = null)
    {
        WindowsVoice.clearSpeechQueue(); 
        switch (info)
        {
            case "Power":
                WindowsVoice.addToSpeechQueue("The reeling power upgrade helps you deplete the stamina of fish faster.");
               
                break;
            case "Reeling":
                WindowsVoice.addToSpeechQueue("The reeling upgrade helps you reel in fish faster.");
                break;
            case "Rarity":
                WindowsVoice.addToSpeechQueue("The rarity upgrade helps you find rare fish more often.");
                break;
            case "Price":
                break;

        }

        if (upgrade != null)
        {
            WindowsVoice.addToSpeechQueue("The upgrade costs " + upgrade.CostOfUpgrade + ". You have " + _player.upgradePoints);
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
        WindowsVoice.clearSpeechQueue();
        if (_player.HowManyFishesHasBeenCaught() < 1)
        {
            PlayAudio("NoFish");
            return;
        }
        string toSay = "You sold " + _player.HowManyFishesHasBeenCaught() + ". You gain "; 
        int fishSold = _player.SellFish();
        _player.upgradePoints += fishSold;
        WindowsVoice.addToSpeechQueue(toSay + fishSold + " upgrade points. You now have "+  _player.upgradePoints + " upgrade points");
        
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
            string cantAfford = "You can not afford a " + upgrade.UpgradeName + " for " + upgrade.CostOfUpgrade + SettingsLoader.GetInstance().GetSettings().UpgradeCost + 
                ". You  have " + _player.upgradePoints + " upgrade points. You need " + upgrade.CostOfUpgrade + (SettingsLoader.GetInstance().GetSettings().UpgradeCost - _player.upgradePoints) + " more upgrade points.";
            WindowsVoice.clearSpeechQueue();
            WindowsVoice.addToSpeechQueue(cantAfford);


            return;
        }
        Debug.Log(_player.upgradePoints);
        _player.upgradePoints -= (upgrade.CostOfUpgrade + SettingsLoader.GetInstance().GetSettings().UpgradeCost);
        string ToSay = "You bought the " + upgrade.UpgradeName + " for " + upgrade.CostOfUpgrade + SettingsLoader.GetInstance().GetSettings().UpgradeCost + ". You now have " + _player.upgradePoints + " left.";
        WindowsVoice.clearSpeechQueue();
        WindowsVoice.addToSpeechQueue(ToSay);
        _player.upgradePoints -= (upgrade.CostOfUpgrade + SettingsLoader.GetInstance().GetSettings().UpgradeCost);
        _player.AddUpgrade(upgrade);
        //PlayAudio("UpgradeBought");
    }
    
    void OnEnable()
    {
  
        if (!playTuturial)
        {
            buyButton.Select();
            OnSelect("BuyButtonSelected");
            UpgradeShop.SetActive(false);
            MainMenu.SetActive(true);
            StartCoroutine(WaitForInput());
        }
        else
        {
            WindowsVoice.clearSpeechQueue();
            WindowsVoice.addToSpeechQueue("Welcome to the shop, here you can buy upgrades and sell your fish for upgrade points. You get one upgrade point per fish sold. Navigate by pressing the left joystick left or right. Press y, the upmost face button on the right side, over an upgrade to hear the upgrades details. Try moving the left joystick right now  ");
           
            UpgradeShop.SetActive(false);
            MainMenu.SetActive(false);
            StartCoroutine(WaitForTuturial());
            playTuturial = false;
        }

        


    }

    
    
    // Update is called once per frame
    void Update()
    {

        if (!canInteract)
        {
          
            return; 
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            Debug.Log("Y has been pressed");
            if(SelectedButton == null)
            {
                Debug.Log("No selected button"); 
                return; 
            }

            PlayInfoText(SelectedButton.NameOfTheButton, SelectedButton.UpgradeContainer);

            StartCoroutine(WaitForInput());

        }


        
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

    IEnumerator WaitForTuturial()
    {
        canInteract = false;

        yield return new WaitForSeconds(25);
        canInteract = true;
        UpgradeShop.SetActive(false);
        MainMenu.SetActive(true);
        buyButton.Select();
        OnSelect("BuyButtonSelected");



    }

}

