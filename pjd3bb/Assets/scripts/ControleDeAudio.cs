using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ControleDeAudio : MonoBehaviour
{
    public AudioMixer audioMixer;
    float masterVolume = 0;

    public TMP_Text texto;
    public Slider slider;
    
    void Start()
    {
        audioMixer.GetFloat("Master", out masterVolume);
        
        slider.value = masterVolume;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            masterVolume += 1f;
            audioMixer.SetFloat("Master", masterVolume);
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            masterVolume -= 1f;
            audioMixer.SetFloat("Master", masterVolume);
        }


        masterVolume = slider.value;
        texto.text = masterVolume.ToString();

        if (masterVolume <= -20)
        {
            audioMixer.SetFloat("Master", -80f); 
        }
        else
        {
            audioMixer.SetFloat("Master", masterVolume);  
        }
        
    }
}