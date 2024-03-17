using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System.Collections;

public class CountDownUI : NetworkBehaviour
{
    public Text countDownText;
    public AudioSource audioSource;

    [ServerRpc(RequireOwnership = false)]
    public void StartCountdownServerRpc()
    {
        // Start the countdown on all clients
        StartCountdownClientRpc();
    }

    [ClientRpc]
    public void StartCountdownClientRpc()
    {
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        // Play the audio clip
        audioSource.Play();
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
            yield return new WaitForSeconds(0.9f);
        }

        yield return new WaitForSeconds(0.4f);
        countDownText.text = "";
    }

    // Method to call when the countdown button is clicked
    public void OnCountdownButtonClick()
    {
        // Call the StartCountdownServerRpc on the host
     
            StartCountdownServerRpc();
        
    }
}
