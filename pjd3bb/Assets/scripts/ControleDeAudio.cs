using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ControleDeAudio : MonoBehaviour
{
    [Header("Configurações de Áudio")]
    public int volume;
    public int volumesFX;
    public bool musica;

    [Header("Componentes UI")]
    public Slider volumeSlider;
    public Slider volumeSFXSlider;
    public Toggle toggleMusica;

    [Header("Audio Mixer (Opcional)")]
    public AudioMixer audioMixer;

    void Start()
    {
        CarregarConfiguracoes();
        ConfigurarEventosUI();
        AplicarConfiguracoesAudio();
    }

    void CarregarConfiguracoes()
    {
        // Carregar configurações salvas ou usar valores padrão
        volume = PlayerPrefs.GetInt("VolumeGeral", 100);
        volumesFX = PlayerPrefs.GetInt("VolumeSFX", 100);
        musica = PlayerPrefs.GetInt("MusicaAtiva", 1) == 1;

        // Aplicar aos componentes UI
        if (volumeSlider != null)
            volumeSlider.value = volume;
        
        if (volumeSFXSlider != null)
            volumeSFXSlider.value = volumesFX;
            
        if (toggleMusica != null)
            toggleMusica.isOn = musica;
    }

    void ConfigurarEventosUI()
    {
        // Configurar eventos dos componentes UI
        if (volumeSlider != null)
            volumeSlider.onValueChanged.AddListener(AlterarVolumeGeral);
        
        if (volumeSFXSlider != null)
            volumeSFXSlider.onValueChanged.AddListener(AlterarVolumeSFX);
            
        if (toggleMusica != null)
            toggleMusica.onValueChanged.AddListener(AlternarMusica);
    }

    public void AlterarVolumeGeral(float novoVolume)
    {
        volume = (int)novoVolume;
        SalvarConfiguracoes();
        AplicarConfiguracoesAudio();
    }

    public void AlterarVolumeSFX(float novoVolumeSFX)
    {
        volumesFX = (int)novoVolumeSFX;
        SalvarConfiguracoes();
        AplicarConfiguracoesAudio();
    }

    public void AlternarMusica(bool musicaAtiva)
    {
        musica = musicaAtiva;
        SalvarConfiguracoes();
        AplicarConfiguracoesAudio();
    }

    void AplicarConfiguracoesAudio()
    {
        // Aplicar volume geral
        AudioListener.volume = volume / 100f;

        // Se estiver usando AudioMixer para SFX
        if (audioMixer != null)
        {
            // Converter volume linear para decibéis (opcional)
            float volumeSFXdB = volumesFX > 0 ? Mathf.Log10(volumesFX / 100f) * 20 : -80f;
            audioMixer.SetFloat("SFXVolume", volumeSFXdB);
        }

        // Controlar música
        if (musica)
        {
            // Ativar música
            AudioListener.pause = false;
        }
        else
        {
            // Desativar música
            AudioListener.pause = true;
        }

        Debug.Log($"Volume: {volume}%, SFX: {volumesFX}%, Música: {(musica ? "Ligada" : "Desligada")}");
    }

    void SalvarConfiguracoes()
    {
        PlayerPrefs.SetInt("VolumeGeral", volume);
        PlayerPrefs.SetInt("VolumeSFX", volumesFX);
        PlayerPrefs.SetInt("MusicaAtiva", musica ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Métodos para resetar configurações
    public void ResetarParaPadrao()
    {
        volume = 100;
        volumesFX = 100;
        musica = true;

        if (volumeSlider != null)
            volumeSlider.value = volume;
        
        if (volumeSFXSlider != null)
            volumeSFXSlider.value = volumesFX;
            
        if (toggleMusica != null)
            toggleMusica.isOn = musica;

        SalvarConfiguracoes();
        AplicarConfiguracoesAudio();
    }

    // Métodos públicos para acesso externo
    public float GetVolumeNormalizado() => volume / 100f;
    public float GetVolumeSFXNormalizado() => volumesFX / 100f;
    public bool IsMusicaAtiva() => musica;
}