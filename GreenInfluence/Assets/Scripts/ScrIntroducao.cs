using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScrIntroducao : MonoBehaviour
{
    public GameObject[] imagens; // Array para armazenar as imagens
    public TextMeshProUGUI[] textos; // Array para armazenar os textos
    private int indiceAtual = 0; // Índice do slide atual
    public float tempoAntesDeTrocar = 5f; // Tempo em segundos para mudar

    private void Start()
    {
        AtualizarIntroducao();
        InvokeRepeating("AvancarIntroducao", tempoAntesDeTrocar, tempoAntesDeTrocar); // Troca de slide em intervalos
    }

    private void Update()
    {
        // Troca de cena se o jogador pressionar qualquer tecla
        if (Input.anyKeyDown)
        {
            TrocarCena();
        }
    }

    private void AvancarIntroducao()
    {
        indiceAtual++;
        if (indiceAtual >= imagens.Length)
        {
            TrocarCena(); // Se todos os slides foram exibidos, troca a cena
        }
        else
        {
            AtualizarIntroducao(); // Atualiza para o próximo slide
        }
    }

    private void AtualizarIntroducao()
    {
        for (int i = 0; i < imagens.Length; i++)
        {
            imagens[i].SetActive(i == indiceAtual); // Ativa apenas a imagem atual
        }

        for (int i = 0; i < textos.Length; i++)
        {
            textos[i].gameObject.SetActive(i == indiceAtual); // Ativa apenas o texto atual
        }
    }

    private void TrocarCena()
    {
        SceneManager.LoadScene("CenaSelecaoPersonagem"); // Substitua pelo nome da sua cena principal
    }
}
