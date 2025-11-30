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
    private bool _canBack; // Salva se o inimigo já alcançou a posição da pedra e pode voltar para posição anterior;

    void Awake()
    {
        enemyRenderer.material.SetColor("_OutlineColor", colorOutline); // Configura a cor da borda;
        enemyRenderer.material.SetFloat("_OutlineSize", 0f); // Garante que a borda iniciará desativada;

        _startPosition = new GameObject("Gambiarra").transform; // Cria um objeto para salvar a posição antes de ser distraído pela pedra;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Rock" && enemy.patrolling && !GameManager.instance.inTrap) // Garante que o inimigo só vai receber uma distração por vez;
        {
            // Atira um raycast em direção a pedra, se tiver alguma coisa no meio: return;
            int mask = LayerMask.GetMask("Default", "Arame");
            RaycastHit2D ray = Physics2D.Raycast(transform.position, (collision.transform.position - transform.position).normalized, triggerRadius, mask);

            if (ray.collider.tag != "Rock") return;

            // Atualiza as variáveis do inimigo pai para que ande até a pedra;
            enemy.patrolling = false;
            enemy.nextPoint = collision.transform;
            _canBack = false;

            // Atualiza os pontos que o player vai em direção;
            _startPosition.position = enemy.transform.position;
            _rockTransform = collision.transform;
        }
    }

    private void Update()
    {
        if (_rockTransform) // True quando escutar alguma pedra;
        {
            // Entra se alcançar a posição da pedra;
            if (Vector2.Distance(enemy.transform.position, _rockTransform.position) <= 0)
            {
                // Para o inimigo por um tempo;
                enemy.stopWalk = true;
                enemy.StopAnimations();

                Invoke("CanBack", GameManager.instance.rockTime);
            }

            // Entra se alcançar a posição anterior;
            if (Vector2.Distance(enemy.transform.position, _startPosition.position) <= 0 && _canBack)
            {
                enemy.patrolling = true; // Permite que o inimigo volte a patrulhar pelos pontos de patrulha;
                _rockTransform = null; // Não há pedras por perto;
                _canBack = false; // Reset na variável de controle;
            }
        }

        // Pega a posião do mouse na tela;
        Vector2 mousePixel = Mouse.current.position.ReadValue();
        Vector2 mouse = Camera.main.ScreenToWorldPoint(new Vector2(mousePixel.x, mousePixel.y));

        int mask = LayerMask.GetMask("Default", "Arame");
        float raySize = Vector2.Distance(mouse, transform.position); // Tamanho do raycast;

        RaycastHit2D ray = Physics2D.Raycast(transform.position, (new Vector3(mouse.x, mouse.y, 0f) - transform.position).normalized, raySize, mask);

        // Aplica borda no inimigo se o mouse estiver perto o suficiente, sem nada entre o ele e o inimigo, não estiver em alerta e tiver pedras restantes;
        if (Vector2.Distance(mouse, enemy.transform.position) < triggerRadius && !ray.collider && !GameManager.instance.inTrap && GameManager.instance.rockAmount > 0) enemyRenderer.material.SetFloat("_OutlineSize", sizeOutLine);

        // Desativa a borda do inimigo quando o mouse se afastar ou estiver destraído;
        if (Vector2.Distance(mouse, enemy.transform.position) > triggerRadius || ray.collider || _rockTransform || GameManager.instance.inTrap) enemyRenderer.material.SetFloat("_OutlineSize", 0f);
    }

    private void CanBack()
    {
        enemy.nextPoint = _startPosition.transform; // Passa a posição anterior para o "WalkManager";
        enemy.stopWalk = false; // Inimigo volta a andar;
        _canBack = true; // Pode voltar para o ponto inicial;
    }
}
