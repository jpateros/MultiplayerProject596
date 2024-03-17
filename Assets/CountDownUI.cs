using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDownUI : MonoBehaviour
{
    public Text countDownText;
    public UserInputHandler userInputHandler;

    private void Awake()
    {
        countDownText.text = "";
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(0.3f);

        int counter = 3;
        while (true)
        {
            if (counter != 0)
            {
                countDownText.text = counter.ToString();
            }
            else
            {
                countDownText.text = "GO!";
                break;
            }

            counter--;
            yield return new WaitForSeconds(1.0f);
        }

        yield return new WaitForSeconds(0.4f);
        userInputHandler.EnableInput(); // Enable user input after countdown
        gameObject.SetActive(false);
    }


    // Start is called before the first frame update
    public void callCoRountine()
    {
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
