using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System.Collections;

/// <summary>
/// This class uses the script for handling and synchronzing the 3,2,1 countdown between host and clients.
/// </summary>
public class CountDownUI : NetworkBehaviour
{
    public Text countDownText;
    public AudioSource audioSource;

    // RPC method called on the server to initiate the countdown on all clients for synronization
    [ServerRpc(RequireOwnership = false)]
    public void StartCountdownServerRpc()
    {
        StartCountdownClientRpc();
    }

    // RPC method called on the client to start the countdown.
    [ClientRpc]
    public void StartCountdownClientRpc()
    {
        StartCoroutine(CountDown());
    }

    //The countdown Coroutine will run when the countdown button is clicked 
    //Note: coroutine is used to control the timing of each step of the countdown without blocking the main thread
    IEnumerator CountDown()
    {
        //Audio clip of 3, 2, 1 matches the text display intervals 
        audioSource.Play();
        yield return new WaitForSeconds(0.3f);

        //Loop will pause and display text to the on the convas for UI countdown
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
            yield return new WaitForSeconds(0.9f);
        }

        yield return new WaitForSeconds(0.4f);
        countDownText.text = "";
    }

    public void OnCountdownButtonClick()
    {
            StartCountdownServerRpc();
        
    }
}
