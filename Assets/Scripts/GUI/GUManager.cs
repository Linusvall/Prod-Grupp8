using System.Collections;
using System.Collections.Generic;
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
    
    
    public void OnSelect(string SoundBiteToPlay)
    {
        PlayAudio(SoundBiteToPlay);
    }

    public void ClickBuyButton()
    {
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
        _controller.inShop = false;
        gameObject.SetActive(false);
        _controller.inShopRange = false;

    }
    
    public void ClickSellFishButton()
    {
        Debug.Log("Click Sell  button");
        if (_player.HowManyFishesHasBeenCaught() < 1)
        {
            PlayAudio("NoFish");
            return;
        }

        _player.upgradePoints = _player.SellFish();
        PlayAudio("SoldFish");
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
        
        if (_player.upgradePoints < upgrade.CostOfUpgrade)
        {
            PlayAudio("YouCanNotAffordThatUpgrade");
            return;
        }
        _player.upgradePoints -= upgrade.CostOfUpgrade;
        _player.AddUpgrade(upgrade);
        PlayAudio("UpgradeBought");
    }
    
    void OnEnable()
    {
        buyButton.Select();
        OnSelect("BuyButtonSelected");
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
}
