using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Audio;
using System.Collections;

[System.Serializable]
public class WaveMessage
{
    public int waveNumber; // Adjusted wave number for the message
    public string message;  // The message to display
    public float delay;     // Delay before showing the message
    public float duration;  // Duration for how long the message is displayed
    public AudioClip audioClip; // Audio clip to play with the message
}

public class MessageManager : MonoBehaviour
{
    public static MessageManager instance;

    [SerializeField]
    private TextMeshProUGUI messageText; // Reference to the TextMeshPro component

    [SerializeField]
    private AudioSource audioSource; // Reference to the AudioSource component

    [SerializeField]
    private List<WaveMessage> messages = new List<WaveMessage>(); // List of messages

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        messageText.text = ""; // Clear initial text
    }

    private void Start()
    {
        // Check for Wave 0 messages and display them
        foreach (var msg in messages)
        {
            if (msg.waveNumber == 0)
            {
                StartCoroutine(ShowMessage(msg));
            }
        }
    }

    public void DisplayMessageForWave(int waveNumber)
    {
        // Adjust wave number to match internal indexing
        int adjustedWaveNumber = waveNumber + 1;

        foreach (var msg in messages)
        {
            if (msg.waveNumber == adjustedWaveNumber)
            {
                StartCoroutine(ShowMessage(msg));
            }
        }
    }

    private IEnumerator ShowMessage(WaveMessage waveMessage)
    {
        yield return new WaitForSeconds(waveMessage.delay);
        messageText.text = waveMessage.message;
        if (waveMessage.audioClip != null)
        {
            audioSource.PlayOneShot(waveMessage.audioClip);
        }
        yield return new WaitForSeconds(waveMessage.duration);
        messageText.text = ""; // Clear the message after the duration
    }

    // Method to add messages via editor (if needed)
    public void AddMessage(WaveMessage waveMessage)
    {
        messages.Add(waveMessage);
    }

    // Method to remove messages via editor (if needed)
    public void RemoveMessage(WaveMessage waveMessage)
    {
        messages.Remove(waveMessage);
    }
}
