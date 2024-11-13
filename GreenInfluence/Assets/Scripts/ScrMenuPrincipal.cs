using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrMenuPrincipal : MonoBehaviour
{
    public Button ButtonIniciarJogo;
    public Button ButtonContinuar;
    public Button ButtonSair;

    void Start()
    {
        ButtonIniciarJogo.onClick.AddListener(IniciarJogo);
        ButtonContinuar.onClick.AddListener(ContinuarJogo);
        ButtonSair.onClick.AddListener(SairDoJogo);

        // Verifica se h� um nome salvo para ativar o bot�o Continuar
        if (PlayerPrefs.HasKey("NomeJogador"))
        {
            ButtonContinuar.gameObject.SetActive(true);
        }
        else
        {
            ButtonContinuar.gameObject.SetActive(false);
        }
    }

    void IniciarJogo()
    {
        // Carrega a cena de sele��o de personagem
        SceneManager.LoadScene("CenaIntroducao");
    }

    void ContinuarJogo()
    {
        // Carrega a cena do jogo principal (continuando com os dados salvos)
        SceneManager.LoadScene("NomeDaCenaJogoPrincipal");
    }

    void SairDoJogo()
    {
        Application.Quit();
    }
}
