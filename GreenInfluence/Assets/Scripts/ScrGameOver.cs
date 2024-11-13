using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrGameOver : MonoBehaviour
{
    public Button ButtonMenuPrincipal;
    public Button ButtonSair;

    void Start()
    {
        // Adicionar um listener ao botão para retornar ao menu principal
        ButtonMenuPrincipal.onClick.AddListener(VoltarMenuPrincipal);

        // Adicionar um listener ao botão de sair do jogo
        ButtonSair.onClick.AddListener(SairDoJogo);
    }

    void VoltarMenuPrincipal()
    {
        // Carregar a cena do menu principal (substitua "NomeDaCenaDoMenu" pelo nome da cena do menu principal)
        SceneManager.LoadScene("MenuPrincipal");
    }

    void SairDoJogo()
    {
        // Fechar o jogo
        Application.Quit();
    }
}
