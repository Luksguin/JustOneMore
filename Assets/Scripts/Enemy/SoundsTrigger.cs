using UnityEngine;
using UnityEngine.InputSystem;

// Respons�vel pela mec�nica de distra��o;

public class SoundsTrigger : MonoBehaviour
{
    public Enemy enemy; // Refer�ncia para o inimigo pai;
    public SpriteRenderer enemyRenderer; // Renderer do inimigo pai;

    public float triggerRadius; //Tamanho do trigger;

    public Color colorOutline; // Cor da borda;
    public float sizeOutLine; // Tamanho da borda;

    private Transform _rockTransform; // Posi��o da distra��o;
    private Transform _startPosition; // Posi��o que o inimigo estava quando escutou a pedra;
    private bool _canBack; // Salva se o inimigo j� alcanou a posi��o da pedra e pode voltar para posi��o anterior;
    private bool _outLine; // Salvas e a borda j� foi aplicada;

    void Awake()
    {
        enemyRenderer.material.SetColor("_OutlineColor", colorOutline); // Configura a cor da borda;
        enemyRenderer.material.SetFloat("_OutlineSize", 0f); // Garante que a borda iniciar� desativada;

        _startPosition = new GameObject("Gambiarra").transform; // Cria um objeto para salvar a posi��o antes de ser distra�do pela pedra;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Rock" && enemy.patrolling) // Garante que o inimigo s� vai receber uma distra��o por vez;
        {
            // Atualiza as vari�veis do inimigo pai para que ande at� a pedra;
            enemy.patrolling = false;
            enemy.nextPoint = collision.transform;

            // Atualiza os pontos que o player vai em dire��o;
            _startPosition.position = enemy.transform.position;
            _rockTransform = collision.transform;
        }
    }

    private void Update()
    {
        if (_rockTransform) // Ser� diferente de nulo apenas se escutar alguma pedra;
        {
            // Entra se alcan�ar a posi��o da pedra;
            if (Vector2.Distance(enemy.transform.position, _rockTransform.position) <= 0)
            {
                enemy.nextPoint = _startPosition.transform; // Passa a posi��o anterior para o "WalkManager";
                _canBack = true; // Pronto para retornar para a posi��o anterior;
            }

            // Entra se alcan�ar a posi��o anterior;
            if (Vector2.Distance(enemy.transform.position, _startPosition.position) <= 0 && _canBack)
            {
                enemy.patrolling = true; // Permite que o inimigo volte a patrulha pelos pontos de patrulha;
                _rockTransform = null; // N�o h� pedras por perto;
                _canBack = false; // Reset na vari�vel de controle;
            }
        }

        // Pega a posi�o do mouse na tela;
        Vector2 mousePixel = Mouse.current.position.ReadValue();
        Vector2 mouse = Camera.main.ScreenToWorldPoint(new Vector2(mousePixel.x, mousePixel.y));

        // Aplica borda no inimigo se o mouse entrar no trigger e ainda n�o tiver aplicado;
        if(Vector2.Distance(mouse, enemy.transform.position) <= triggerRadius && !_outLine)
        {
            enemyRenderer.material.SetFloat("_OutlineSize", sizeOutLine);
            _outLine = true;
        }

        // Desativa a borda do inimigo se o mouse sair do trigger e ainda n�o tiver desativado;
        if (Vector2.Distance(mouse, enemy.transform.position) > triggerRadius && _outLine)
        {
            enemyRenderer.material.SetFloat("_OutlineSize", 0f);
            _outLine = false;
        }
    }
}
