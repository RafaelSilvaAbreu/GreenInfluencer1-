using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrSelecaoPersonagem : MonoBehaviour
{
    public TMP_InputField inputNome; // Arraste o InputFieldNomeJogador no Inspector
    public Image imgSkin; // Arraste a ImageSkinPersonagem no Inspector
    public Sprite[] skins; // Arraste as sprites das skins no Inspector
    private int skinIndex = 0; // Índice da skin selecionada

    private void Start()
    {
        CarregarDados(); // Carregar dados ao iniciar
    }

    public void EscolherSkin(int index)
    {
        skinIndex = index;
        imgSkin.sprite = skins[skinIndex]; // Atualiza a imagem da skin
    }

    public void ConfirmarSelecao()
    {
        string nome = inputNome.text;

        // Salvar dados do jogador
        SalvarDados(nome, skinIndex);

        // Carregar a próxima cena
        UnityEngine.SceneManagement.SceneManager.LoadScene("Jogo");
    }

    private void SalvarDados(string nome, int skinIndex)
    {
        PlayerPrefs.SetString("NomeJogador", nome);
        PlayerPrefs.SetInt("SkinJogador", skinIndex);
        PlayerPrefs.Save(); // Salva as alterações
    }

    private void CarregarDados()
    {
        if (PlayerPrefs.HasKey("NomeJogador"))
        {
            inputNome.text = PlayerPrefs.GetString("NomeJogador");
            skinIndex = PlayerPrefs.GetInt("SkinJogador");
            imgSkin.sprite = skins[skinIndex];
        }
    }

    public void RolarEsquerda()
    {
        skinIndex--;
        if (skinIndex < 0) skinIndex = skins.Length - 1; // Volta para a última skin
        imgSkin.sprite = skins[skinIndex]; // Atualiza a imagem da skin
    }

    public void RolarDireita()
    {
        skinIndex++;
        if (skinIndex >= skins.Length) skinIndex = 0; // Volta para a primeira skin
        imgSkin.sprite = skins[skinIndex]; // Atualiza a imagem da skin
    }
}
