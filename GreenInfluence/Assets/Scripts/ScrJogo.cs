using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScrJogo : MonoBehaviour
{
    [System.Serializable]
    public class Proposta
    {
        public string descricao;
        public string consequenciaAceita;
        public string consequenciaRecusada;
        public int influencia, moral, dinheiro;
        public int bondade;
        public int maldade;
    }

    // Vari�veis da UI para o nome, imagem e status do jogador
    public TextMeshProUGUI TextNomeJogador;
    public Image ImagePersonagem;
    public TextMeshProUGUI TextInfluencia;
    public TextMeshProUGUI TextMoral;
    public TextMeshProUGUI TextDinheiro;
    public Button ButtonAceitar;
    public Button ButtonRecusar;
    public TextMeshProUGUI TextDescricao;
    public TextMeshProUGUI TextConsequencias; // Texto de consequ�ncias

    // Tela de turno
    public GameObject PanelTurno; // Arraste o PanelTurno no Inspector
    public TextMeshProUGUI TextTurno; // Arraste o TextTurno no Inspector

    // Tela de consequ�ncias
    public GameObject PanelConsequencias; // Arraste o PanelConsequencias no Inspector
    public TextMeshProUGUI TextInfluenciaAlterada; // Arraste o TextInfluenciaAlterada no Inspector
    public TextMeshProUGUI TextMoralAlterada; // Arraste o TextMoralAlterada no Inspector
    public TextMeshProUGUI TextDinheiroAlterado; // Arraste o TextDinheiroAlterado no Inspector
    public Button ButtonFecharConsequencias; // Bot�o para fechar o painel de consequ�ncias
    public GameObject PanelMensagemGameOver; // Painel de mensagem de Game Over
    public TextMeshProUGUI TextMensagemGameOver; // Texto de mensagem de Game Over
    public Button ButtonContinuarGameOver; // Arraste o bot�o do painel de mensagem no Inspector


    // Vari�veis de status do jogador
    private string nomeJogador;
    private Sprite skinJogador;
    private int influencia = 15;
    private int moral = 15;
    private int dinheiro = 15;
    private int bondade = 0;
    private int maldade = 0;
    private int turno = 1;

    // Array de skins configurado no Inspector
    public Sprite[] skins;

    // Lista de propostas e proposta atual
    public Proposta[] propostas;
    private Proposta propostaAtual;

    private void Start()
    {
        // Carregar dados do jogador
        nomeJogador = PlayerPrefs.GetString("NomeJogador", "Jogador");
        int skinIndex = PlayerPrefs.GetInt("SkinJogador", 0);

        // Configurar a skin com base no �ndice
        if (skinIndex >= 0 && skinIndex < skins.Length)
        {
            skinJogador = skins[skinIndex];
        }
        else
        {
            Debug.LogError("�ndice de skin inv�lido. Usando skin padr�o.");
            skinJogador = skins[0];
        }

        // Configurar a UI inicial
        TextNomeJogador.text = nomeJogador;
        ImagePersonagem.sprite = skinJogador;

        // Atualizar as estat�sticas e mostrar a primeira proposta
        AtualizarEstatisticas();
        MostrarPropostaAleatoria();

        // Atribuir eventos aos bot�es
        ButtonAceitar.onClick.AddListener(() => EscolherProposta(true));
        ButtonRecusar.onClick.AddListener(() => EscolherProposta(false));
        ButtonFecharConsequencias.onClick.AddListener(FecharConsequencias); // Adiciona o evento para fechar

        // Aqui adicionamos o evento para o bot�o de Game Over
        ButtonContinuarGameOver.onClick.AddListener(ContinuarParaGameOver); // Adiciona evento ao bot�o do PanelMensagemGameOver

        // Exibir o turno inicial
        ExibirTurno();
    }

    private void AtualizarEstatisticas()
    {
        TextInfluencia.text = "Influencia: " + influencia;
        TextMoral.text = "Moral: " + moral;
        TextDinheiro.text = "Dinheiro: " + dinheiro;
    }

    private void MostrarPropostaAleatoria()
    {
        propostaAtual = propostas[Random.Range(0, propostas.Length)];
        TextDescricao.text = propostaAtual.descricao;
    }

    public void EscolherProposta(bool aceitar)
    {
        if (propostaAtual == null)
        {
            Debug.LogWarning("Nenhuma proposta atual selecionada.");
            return;
        }

        if (aceitar)
        {
            // Atualiza os valores de acordo com a aceita��o da proposta
            influencia += propostaAtual.influencia;
            moral += propostaAtual.moral;
            dinheiro += propostaAtual.dinheiro;
            bondade += propostaAtual.bondade;
            maldade += propostaAtual.maldade;

            // Exibe a consequ�ncia aceita
            TextConsequencias.text = propostaAtual.consequenciaAceita;

            // Atualiza as cores das estat�sticas no painel de consequ�ncias
            AtualizarCorEstatisticasConsequencias();

            // Exibe as altera��es das estat�sticas
            TextInfluenciaAlterada.text = propostaAtual.influencia > 0 ? "+" + propostaAtual.influencia : propostaAtual.influencia.ToString();
            TextMoralAlterada.text = propostaAtual.moral > 0 ? "+" + propostaAtual.moral : propostaAtual.moral.ToString();
            TextDinheiroAlterado.text = propostaAtual.dinheiro > 0 ? "+" + propostaAtual.dinheiro : propostaAtual.dinheiro.ToString();
        }
        else
        {
            // Exibe a consequ�ncia recusada
            TextConsequencias.text = propostaAtual.consequenciaRecusada;

            // Limpa as altera��es nas estat�sticas para recusa
            TextInfluenciaAlterada.text = "";
            TextMoralAlterada.text = "";
            TextDinheiroAlterado.text = "";
        }

        // Mostra o painel de consequ�ncias ap�s a escolha
        PanelConsequencias.SetActive(true);

        // Atualiza as estat�sticas gerais na tela
        AtualizarEstatisticas();

        // Checa o Game Over somente se alguma estat�stica chegar a zero ou se o turno final for atingido
        ChecarGameOver();
    }

    private void ChecarGameOver()
    {
        if (influencia <= 0 || moral <= 0 || dinheiro <= 0)
        {
            // Exibe a mensagem de Game Over somente se alguma estat�stica chegou a zero
            TextMensagemGameOver.text = "� uma pena, mas a sua carreira de influenciador chegou ao fim.";
            PanelMensagemGameOver.SetActive(true);
            return; // Evita a transi��o imediata para a tela de Game Over
        }
        else if (turno >= 10)
        {
            // Caso o turno tenha chegado ao limite (10), exibe a mensagem de Game Over
            TextMensagemGameOver.text = "Fim de jogo! Voc� completou todos os turnos.";
            PanelMensagemGameOver.SetActive(true);
        }
    }

    public void ContinuarParaGameOver()
    {
        if (bondade > maldade)
        {
            SceneManager.LoadScene("GameOverBom");
        }
        else
        {
            SceneManager.LoadScene("GameOverRuim");
        }
    }

    private void FecharConsequencias()
    {
        PanelConsequencias.SetActive(false);
        turno++; // Incrementa o turno ao fechar o painel de consequ�ncias
        ExibirTurno(); // Exibe o turno atual ap�s fechar o painel de consequ�ncias
        MostrarPropostaAleatoria(); // Mostra uma nova proposta
    }

    private void AtualizarCorEstatisticasConsequencias()
    {
        TextInfluenciaAlterada.color = propostaAtual.influencia > 0 ? Color.green : (propostaAtual.influencia < 0 ? Color.red : Color.white);
        TextMoralAlterada.color = propostaAtual.moral > 0 ? Color.green : (propostaAtual.moral < 0 ? Color.red : Color.white);
        TextDinheiroAlterado.color = propostaAtual.dinheiro > 0 ? Color.green : (propostaAtual.dinheiro < 0 ? Color.red : Color.white);
    }

    private void ExibirTurno()
    {
        TextTurno.text = "Turno " + turno;
        PanelTurno.SetActive(true);
        Invoke("EsconderTurno", 1.5f);
    }

    private void EsconderTurno()
    {
        PanelTurno.SetActive(false);
    }
}
