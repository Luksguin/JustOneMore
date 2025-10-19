using UnityEngine;
using UnityEngine.InputSystem;

// Responsável pela mecânica de distração;

public class SoundsTrigger : MonoBehaviour
{
    public Enemy enemy; // Referência para o inimigo pai;
    public SpriteRenderer enemyRenderer; // Renderer do inimigo pai;

    public float triggerRadius; //Tamanho do trigger;

    public Color colorOutline; // Cor da borda;
    public float sizeOutLine; // Tamanho da borda;

    private Transform _rockTransform; // Posição da distração;
    private Transform _startPosition; // Posição que o inimigo estava quando escutou a pedra;
    private bool _canBack; // Salva se o inimigo já alcanou a posição da pedra e pode voltar para posição anterior;
    private bool _outLine; // Salvas e a borda já foi aplicada;

    void Awake()
    {
        enemyRenderer.material.SetColor("_OutlineColor", colorOutline); // Configura a cor da borda;
        enemyRenderer.material.SetFloat("_OutlineSize", 0f); // Garante que a borda iniciará desativada;

        _startPosition = new GameObject("Gambiarra").transform; // Cria um objeto para salvar a posição antes de ser distraído pela pedra;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Rock" && enemy.patrolling) // Garante que o inimigo só vai receber uma distração por vez;
        {
            // Atualiza as variáveis do inimigo pai para que ande até a pedra;
            enemy.patrolling = false;
            enemy.nextPoint = collision.transform;

            // Atualiza os pontos que o player vai em direção;
            _startPosition.position = enemy.transform.position;
            _rockTransform = collision.transform;
        }
    }

    private void Update()
    {
        if (_rockTransform) // Será diferente de nulo apenas se escutar alguma pedra;
        {
            // Entra se alcançar a posição da pedra;
            if (Vector2.Distance(enemy.transform.position, _rockTransform.position) <= 0)
            {
                enemy.nextPoint = _startPosition.transform; // Passa a posição anterior para o "WalkManager";
                _canBack = true; // Pronto para retornar para a posição anterior;
            }

            // Entra se alcançar a posição anterior;
            if (Vector2.Distance(enemy.transform.position, _startPosition.position) <= 0 && _canBack)
            {
                enemy.patrolling = true; // Permite que o inimigo volte a patrulha pelos pontos de patrulha;
                _rockTransform = null; // Não há pedras por perto;
                _canBack = false; // Reset na variável de controle;
            }
        }

        // Pega a posião do mouse na tela;
        Vector2 mousePixel = Mouse.current.position.ReadValue();
        Vector2 mouse = Camera.main.ScreenToWorldPoint(new Vector2(mousePixel.x, mousePixel.y));

        // Aplica borda no inimigo se o mouse entrar no trigger e ainda não tiver aplicado;
        if(Vector2.Distance(mouse, enemy.transform.position) <= triggerRadius && !_outLine)
        {
            enemyRenderer.material.SetFloat("_OutlineSize", sizeOutLine);
            _outLine = true;
        }

        // Desativa a borda do inimigo se o mouse sair do trigger e ainda não tiver desativado;
        if (Vector2.Distance(mouse, enemy.transform.position) > triggerRadius && _outLine)
        {
            enemyRenderer.material.SetFloat("_OutlineSize", 0f);
            _outLine = false;
        }
    }
}
