using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI gameTitle;
    [SerializeField] TextMeshProUGUI pressButtonToPlay;
    [SerializeField] float uiLerpTime;
    [Header("Other Elements")]
    [SerializeField] MovementScript ms;
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
            ms.SetInMenu(false);
            LeanTween.value(pressButtonToPlay.gameObject, pressButtonToPlay.color.a, 0f, uiLerpTime).setOnUpdate(LerpAlphaValue);
            LeanTween.value(gameTitle.gameObject, gameTitle.color.a, 0f, 0.5f).setOnUpdate(LerpAlphaValueTitle);

            stateOne = false;
            stateTwo = true;
        }
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
}
