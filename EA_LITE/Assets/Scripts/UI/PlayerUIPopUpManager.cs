using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIPopUpManager : MonoBehaviour
{
    [Header("YOU DIED POP UP")]
    [SerializeField] GameObject youDiedPopUpGameObject;
    [SerializeField] TextMeshProUGUI youDiedPopUpBackgroundText;
    [SerializeField] TextMeshProUGUI youDiedPopUpText;
    [SerializeField] CanvasGroup youDiedPopUpCanvasGroup; // allows us to set the alpha to fade over time

    [Header("TUTORIAL POP UPS")]
    public int stepsOfTutorial = 4;
    [SerializeField] public GameObject wasdPopUpGameObject;
    [SerializeField] CanvasGroup wasdPopUpCanvasGroup; // allows us to set the alpha to fade over time
    [SerializeField] public GameObject dodgePopUpGameObject;
    [SerializeField] CanvasGroup dodgePopUpCanvasGroup; // allows us to set the alpha to fade over time
    [SerializeField] public GameObject weaponSwapPopUpGameObject;
    [SerializeField] CanvasGroup weaponSwapPopUpCanvasGroup; // allows us to set the alpha to fade over time
    [SerializeField] public GameObject attackPopUpGameObject;
    [SerializeField] CanvasGroup attackPopUpCanvasGroup; // allows us to set the alpha to fade over time

    [Header("INPUT FLAGS")]
    private bool sendNextStep = true;
    private bool checkForWASD = true;
    private bool checkForDodge = true;
    private bool checkForWeaponSwap = true;
    private bool checkForAttack = true;


    void Update()
    {
        if(sendNextStep == true)
        {
            SendTutorialPopUp();
            sendNextStep = false;
        }

        if(checkForWASD == true)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                checkForWASD = false;
                // WAIT, THEN FADE OUT THE POP UP 
                StartCoroutine(WaitThenFadeOutPopUpOverTime(wasdPopUpCanvasGroup, 2, 0));
                StartCoroutine(TimeBeforeNextStep());
            }
        }

        if(checkForDodge == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                checkForDodge = false;
                // WAIT, THEN FADE OUT THE POP UP 
                StartCoroutine(WaitThenFadeOutPopUpOverTime(dodgePopUpCanvasGroup, 2, 0));
                StartCoroutine(TimeBeforeNextStep());
            }
        }

        if(checkForWeaponSwap == true)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                checkForWeaponSwap = false;
                // WAIT, THEN FADE OUT THE POP UP 
                StartCoroutine(WaitThenFadeOutPopUpOverTime(weaponSwapPopUpCanvasGroup, 2, 0));
                StartCoroutine(TimeBeforeNextStep());
            }
        }

        if(checkForAttack == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                checkForAttack = false;
                // WAIT, THEN FADE OUT THE POP UP 
                StartCoroutine(WaitThenFadeOutPopUpOverTime(attackPopUpCanvasGroup, 2, 0));
                StartCoroutine(TimeBeforeNextStep());
            }
        }
    }

    void SendTutorialPopUp()
    {
        switch (stepsOfTutorial)
        {
        case 4:
            wasdPopUpGameObject.SetActive(true);
            stepsOfTutorial -= 1;
            Debug.Log(stepsOfTutorial);
            break;
        case 3:
            dodgePopUpGameObject.SetActive(true);
            stepsOfTutorial -= 1;
            Debug.Log(stepsOfTutorial);
            break;
        case 2:
            weaponSwapPopUpGameObject.SetActive(true);
            stepsOfTutorial -= 1;
            Debug.Log(stepsOfTutorial);
            break;
        case 1:
            attackPopUpGameObject.SetActive(true);
            stepsOfTutorial -= 1;
            Debug.Log(stepsOfTutorial);
            break;
        }
    }


    // ------------------------ YOU DIED POP UP ------------------------------------

    public void SendYouDiedPopUp()
    {
        // ACTIVATE POST PROCESSING EFFECTS

        youDiedPopUpGameObject.SetActive(true);
        youDiedPopUpBackgroundText.characterSpacing = 0;

        // STRETCH OUT THE POP UP 
        StartCoroutine(StretchPopUpTextOverTime(youDiedPopUpBackgroundText, 8, 20));
        // FADE IN THE POP UP
        StartCoroutine(FadeInPopUpOverTime(youDiedPopUpCanvasGroup, 5));
        // WAIT, THEN FADE OUT THE POP UP 
        StartCoroutine(WaitThenFadeOutPopUpOverTime(youDiedPopUpCanvasGroup, 2, 5));
    }

    private IEnumerator StretchPopUpTextOverTime(TextMeshProUGUI text, float duration, float stretchAmount)
    {
        if(duration > 0f)
        {
            text.characterSpacing = 0; // RESETS OUR CHARACTER SPACING
            float timer = 0;

            yield return null;

            while(timer < duration)
            {
                timer = timer + Time.deltaTime;
                text.characterSpacing = Mathf.Lerp(text.characterSpacing, stretchAmount, duration * (Time.deltaTime / 20));
                yield return null;
            }
        }
    }

    private IEnumerator FadeInPopUpOverTime(CanvasGroup canvas, float duration)
    {
        if(duration > 0)
        {
            canvas.alpha = 0;
            float timer = 0;

            yield return null;

            while(timer < duration)
            {
                timer = timer + Time.deltaTime;
                canvas.alpha = Mathf.Lerp(canvas.alpha, 1, duration * Time.deltaTime);
                yield return null;
            }
        }

        canvas.alpha = 1;

        yield return null;
    }

    private IEnumerator WaitThenFadeOutPopUpOverTime(CanvasGroup canvas, float duration, float delay)
    {
        if(duration > 0)
        {
            while(delay > 0)
            {
                delay = delay - Time.deltaTime;
                yield return null;
            }

            canvas.alpha = 1;
            float timer = 0;

            yield return null;

            while(timer < duration)
            {
                timer = timer + Time.deltaTime;
                canvas.alpha = Mathf.Lerp(canvas.alpha, 0, duration * Time.deltaTime);
                yield return null;
            }
        }

        canvas.alpha = 0;

        yield return null;
    }


    // ------------------------ TUTORIAL POP UPS ------------------------------------

    public IEnumerator TimeBeforeNextStep()
    {
        yield return new WaitForSeconds(2f);
        sendNextStep = true;
    }
}
