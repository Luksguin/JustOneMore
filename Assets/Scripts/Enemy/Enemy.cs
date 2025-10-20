using UnityEngine;

// Responsável por controlar os Inimigos;

public class Enemy : MonoBehaviour
{
    public float speed; // Velocidade de movimento;
    public Rigidbody2D myRb;
    public Transform[] points; // Pontos de patrulha;
    public Transform vision; // Visão do inimigo;

    [Header("Animations")]
    public Animator myAnimator;
    public string boolUpWalk;
    public string boolDownWalk;
    public string boolLeftWalk;
    public string boolRightWalk;

    [HideInInspector] public Transform nextPoint; // Salva para onde o inimigo está caminhando; Deve ser public para acessar em "SoundsTrigger";
    [HideInInspector] public bool patrolling; // Salva se o inimigo está patrulhando ou destraído; Deve se public para acessar em "SoundsTrigger";

    private int _nextIndex; // Salva o index do próximo ponto de patrulha no qual o inimigo está indo;
    private bool _lookAtPlayer; // Proíbe o inimigo de caminhar quando se torna true; Se torna true apenas no game over;

    // Salvem a distância do inimigo em relação ao ponto no qual está indo em X e Y;
    private float _DistanceX;
    private float _DistanceY;

    private void Awake()
    {
        foreach (var p in points) p.parent = null; // Tira todos o pontos de caminhada de dentro do GameObject;

        patrolling = true;

        _nextIndex = 0;
        _lookAtPlayer = false;

        _DistanceX = 0;
        _DistanceY = 0;

        transform.position = points[0].position; // Garante que o inimigos iniciará o jogo na posição correta;
    }

    private void Update()
    {
        if(_lookAtPlayer == true) return; // Proíbe o Player de mudar direção da visão;

        // Faz o inimigo olhar para direção que faz mais sentido;
        if (_DistanceX >= 0 && Mathf.Abs(_DistanceX) > Mathf.Abs(_DistanceY)) LookRigth();
        else if (_DistanceX < 0 && Mathf.Abs(_DistanceX) > Mathf.Abs(_DistanceY)) LookLeft();
        else if (_DistanceY >= 0 && Mathf.Abs(_DistanceY) > Mathf.Abs(_DistanceX)) LookUp();
        else if (_DistanceY < 0 && Mathf.Abs(_DistanceY) > Mathf.Abs(_DistanceX)) LookDown();
    }

    private void FixedUpdate()
    {
        if(!_lookAtPlayer)WalkManager(); // Se não estiver no game over anda um pouco a cada frame;
    }

    #region WALK
    // Gerencia a caminhada do inimigo;
    private void WalkManager()
    {
        if (_nextIndex >= points.Length) _nextIndex = 0; // Caso tenha finalizado todos os pontos, volta para o início;

        if(patrolling) nextPoint = points[_nextIndex]; // Garante que o inimigo vá ao ponto erto quando estiver em patrulha;

        // Enquanto estiver longe do próximo ponto, vá em sua direção;
        if(Vector2.Distance(transform.position, nextPoint.position) > 0)
        {
            WalkToPoint(nextPoint);
        }
        else
        {
            _nextIndex++;
        }
    }

    // Realiza a caminhada de fato;
    private void WalkToPoint(Transform point)
    {
        // Atualiza as distâncias X e Y para o ponto que está caminhando;
        _DistanceX = point.position.x - transform.position.x;
        _DistanceY = point.position.y - transform.position.y;

        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime); // Movimentação
    }
    #endregion

    #region Animations
    // Controla as Animações e rotação;
    private void LookUp()
    {
        myAnimator.SetBool(boolUpWalk, true);

        myAnimator.SetBool(boolDownWalk, false);
        myAnimator.SetBool(boolRightWalk, false);
        myAnimator.SetBool(boolLeftWalk, false);

        vision.rotation = Quaternion.Euler(0, 0, 180);
    }

    private void LookDown()
    {
        myAnimator.SetBool(boolDownWalk, true);

        myAnimator.SetBool(boolUpWalk, false);
        myAnimator.SetBool(boolRightWalk, false);
        myAnimator.SetBool(boolLeftWalk, false);

        vision.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void LookLeft()
    {
        myAnimator.SetBool(boolLeftWalk, true);

        myAnimator.SetBool(boolUpWalk, false);
        myAnimator.SetBool(boolDownWalk, false);
        myAnimator.SetBool(boolRightWalk, false);

        vision.rotation = Quaternion.Euler(0, 0, -90);
    }

    private void LookRigth()
    {
        myAnimator.SetBool(boolRightWalk, true);

        myAnimator.SetBool(boolUpWalk, false);
        myAnimator.SetBool(boolDownWalk, false);
        myAnimator.SetBool(boolLeftWalk, false);

        vision.rotation = Quaternion.Euler(0, 0, 90);
    }

    private void StopAnimations()
    {
        myAnimator.SetBool(boolUpWalk, false);
        myAnimator.SetBool(boolDownWalk, false);
        myAnimator.SetBool(boolRightWalk, false);
        myAnimator.SetBool(boolLeftWalk, false);
    }
    #endregion

    // Aborda o Player quando encontrá-lo;
    // Chamada pelo "EnemyTrigger";
    public void KillPlayer()
    {
        _lookAtPlayer = true; // Usado para travar os movimentos do inimigo;
        Player.instance.myAnimator.SetBool(Player.instance.gameOverBool, true); // Atualiza a animação do Player;
        Player.instance.speed = 0; // Trava a velocidade do Player;

        // Calcula a distância para o Player e olha para o lugar certo;
        _DistanceX = Player.instance.transform.position.x - transform.position.x;
        _DistanceY = Player.instance.transform.position.y - transform.position.y;

        if (_DistanceX >= 0 && Mathf.Abs(_DistanceX) > Mathf.Abs(_DistanceY)) LookRigth();
        else if (_DistanceX < 0 && Mathf.Abs(_DistanceX) > Mathf.Abs(_DistanceY)) LookLeft();
        else if (_DistanceY >= 0 && Mathf.Abs(_DistanceY) > Mathf.Abs(_DistanceX)) LookUp();
        else if (_DistanceY < 0 && Mathf.Abs(_DistanceY) > Mathf.Abs(_DistanceX)) LookDown();

        Invoke("StopAnimations", .25f); // Para as animações do inimigo; Garante que o inimigo teve tempo de rotacionar para posição certa;
        GameManager.instance.GameOver();
    }
}
