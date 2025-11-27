using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregarCena : MonoBehaviour
{
    public void CarregarCenaPorNome(string nomeDaCena)
    {
        Debug.Log("Carregando cena: " + nomeDaCena);
        SceneManager.LoadScene(nomeDaCena);
    }

    public void CarregarCenaPorIndice(int indiceDaCena)
    {
        Debug.Log("Carregando cena com índice: " + indiceDaCena);
        SceneManager.LoadScene(indiceDaCena);
    }

    public void ReiniciarCena()
    {
        string cenaAtual = SceneManager.GetActiveScene().name;
        Debug.Log("Reiniciando cena: " + cenaAtual);
        SceneManager.LoadScene(cenaAtual);
    }

    public void CarregarProximaCena()
    {
        int cenaAtualIndex = SceneManager.GetActiveScene().buildIndex;
        int proximaCenaIndex = cenaAtualIndex + 1;
        
        if (proximaCenaIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Carregando próxima cena: " + proximaCenaIndex);
            SceneManager.LoadScene(proximaCenaIndex);
        }
        else
        {
            Debug.LogWarning("Não há próxima cena na Build Settings!");
        }
    }

    public void SairDoJogo()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
        
        // Para testar no Editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}