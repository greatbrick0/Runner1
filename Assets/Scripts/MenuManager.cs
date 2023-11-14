using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq.Expressions;

public class MenuManager : MonoBehaviour
{
    [Header("UI Elements For Starting Menu")]
    [SerializeField] TextMeshProUGUI gameTitle;
    [SerializeField] TextMeshProUGUI pressButtonToPlay;
    [SerializeField] float uiLerpTime;

    [Header("UI Elements For GameOver Screen")]
    [SerializeField] Image gameOverScreen;
    [SerializeField] TextMeshProUGUI gameOverTitleText;
    [SerializeField] TextMeshProUGUI coinCount;
    [SerializeField] Button restartButton;
    [SerializeField] float  buttonPosition;
    [SerializeField] Button quitButton;
    [SerializeField] float  quitButtonPosition;

    [Header("Menu Sounds")]
    [SerializeField] AudioSource menuAs;
    [SerializeField] AudioClip hoverSound, clickSound;

    [Header("Other Elements")]
    [SerializeField] MovementScript ms;
    [SerializeField] CoinCollector cc;

    bool start;
    bool isDead;

    bool stateOne = true;
    bool stateTwo;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        MenuStates();
    }

    /// <summary>
    /// This method handles all the states and what each state does
    /// </summary>
    void MenuStates()
    {
        LeanTween.init();
        if (stateOne)
        {
            StateOne();
        }
        if(stateTwo)
        {
            StateTwo();
        }

    }

    void StateOne()
    {
        if (pressButtonToPlay.alpha == 1)
        {
            LeanTween.value(pressButtonToPlay.gameObject, pressButtonToPlay.color.a, 0f, uiLerpTime).setOnUpdate(LerpAlphaValue);
        }
        else if (pressButtonToPlay.alpha == 0)
        {
            LeanTween.value(pressButtonToPlay.gameObject, pressButtonToPlay.color.a, 1f, uiLerpTime).setOnUpdate(LerpAlphaValue);
        }


        if (Input.anyKeyDown)
        {
            menuAs.clip = clickSound;
            menuAs.Play();
            ms.SetInMenu(false);
            LeanTween.value(pressButtonToPlay.gameObject, pressButtonToPlay.color.a, 0f, uiLerpTime).setOnUpdate(LerpAlphaValue);
            LeanTween.value(gameTitle.gameObject, gameTitle.color.a, 0f, 0.5f).setOnUpdate(LerpAlphaValueTitle);

            stateOne = false;
        }
    }

    void StateTwo()
    {
        coinCount.text = "Coins: " + cc.coins;

        //TEXT
        LeanTween.value(gameOverScreen.gameObject, gameOverScreen.color.a, 1f, uiLerpTime).setOnUpdate(LerpGameOverScreen);
        LeanTween.value(coinCount.gameObject, coinCount.color.a, 1f, uiLerpTime).setOnUpdate(LerpAlphaCoinCount);
        LeanTween.value(gameOverTitleText.gameObject, gameOverTitleText.color.a, 1f, uiLerpTime).setOnUpdate(LerpAlphaGameOver);

        //BUTTONS 
        LeanTween.moveY(restartButton.gameObject, buttonPosition, 2).setEaseInOutCubic();
        LeanTween.moveY(quitButton.gameObject, quitButtonPosition, 2).setEaseInOutCubic();
        
        stateTwo = false;


    }


    
    void LerpAlphaValue(float a)
    {
        var alphaChange = new Vector4(pressButtonToPlay.color.r, pressButtonToPlay.color.g, pressButtonToPlay.color.b, a);

        pressButtonToPlay.color = alphaChange;
    }
    void LerpAlphaValueTitle(float a)
    {
        var alphaChange = new Vector4(gameTitle.color.r, gameTitle.color.g, gameTitle.color.b, a);

        gameTitle.color = alphaChange;
    }

    void LerpGameOverScreen(float a)
    {
        var alphaChange = new Vector4(gameOverScreen.color.r, gameOverScreen.color.g, gameOverScreen.color.b, a);

        gameOverScreen.color = alphaChange;
    }
    void LerpAlphaCoinCount(float a)
    {
        var alphaChange = new Vector4(coinCount.color.r, coinCount.color.g, coinCount.color.b, a);

        coinCount.color = alphaChange;
    }

    void LerpAlphaGameOver(float a)
    {
        var alphaChange = new Vector4(gameOverTitleText.color.r, gameOverTitleText.color.g, gameOverTitleText.color.b, a);

        gameOverTitleText.color = alphaChange;
    }
    //------------LERP METHODS ENDS------------

    //------------BUTTON METHODS------------
    public void RestartButton()
    {


        menuAs.clip = clickSound;
        menuAs.Play();
        Invoke("DelaySceneChange", 0.5f);
    }

    public void HoverSound()
    {
        menuAs.clip = hoverSound;
        menuAs.Play();
    }

    public void QuitButton()
    {
        menuAs.clip = clickSound;
        menuAs.Play();
        Invoke("DelaySoundChangeQuit", 0.5f);
    }
    
    void DelaySceneChange()
    {
        SceneManager.LoadScene(0);
    }

    void DelaySoundChangeQuit()
    {
        Application.Quit();
    }

    //------------GETTER AND SETTER METHODS------------
    public void SetStateTwo(bool isDead)
    {
        stateTwo = isDead;
    }
}
