using Luksguin.Singleton;
using UnityEngine;
using UnityEngine.InputSystem;

// Gerencia o Player;

public class Player : Singleton<Player>
{
    [Header("Movements")]
    public float speed; // Velocidade de caminhada;
    public Rigidbody2D myRb;

    [Header("Animations")]
    public Animator myAnimator;
    public string friendTag;

    public string gameOverBool;
    public string boolIdle;
    public string helpingBool;

    public string boolUpWalk;
    public string boolDownWalk;
    public string boolLeftWalk;
    public string boolRightWalk;

    [Header("Rock")]
    public int rockAmount; // Quantidade de pedras restantes;
    public GameObject rock; // Prefab da pedra;
    [HideInInspector] public Vector2 lastClick; // Armazena a posição do click; Deve ser public para acessar em "Rock";

    private float _xDir;
    private float _yDir;

    [HideInInspector] public bool isHelping; // Salva se o player está ajudando algum aliado; Deve ser public para acessar em "Friend";

    private void Update()
    {
        // Gerencia as animações;
        #region HORIZONTAL
        if (_xDir > 0)
        {
            myAnimator.SetBool(boolRightWalk, true);
            myAnimator.SetBool(boolIdle, false);
            myAnimator.SetBool(boolLeftWalk, false);
        }

        if (_xDir < 0)
        {
            myAnimator.SetBool(boolLeftWalk, true);
            myAnimator.SetBool(boolIdle, false);
            myAnimator.SetBool(boolRightWalk, false);
        }

        if (_xDir == 0)
        {
            myAnimator.SetBool(boolRightWalk, false);
            myAnimator.SetBool(boolLeftWalk, false);
        }
        #endregion

        #region VERTICAL
        if (_yDir > 0)
        {
            myAnimator.SetBool(boolUpWalk, true);
            myAnimator.SetBool(boolIdle, false);
            myAnimator.SetBool(boolDownWalk, false);
        }

        if (_yDir < 0)
        {
            myAnimator.SetBool(boolDownWalk, true);
            myAnimator.SetBool(boolIdle, false);
            myAnimator.SetBool(boolUpWalk, false);
        }

        if (_yDir == 0)
        {
            myAnimator.SetBool(boolUpWalk, false);
            myAnimator.SetBool(boolDownWalk, false);
        }
        #endregion
    }

    private void FixedUpdate()
    {
        Movement();
    }

    #region MOVEMENT

    //Salva os valores dos inputs;
    private void OnMove(InputValue input)
    {
        _xDir = input.Get<Vector2>().x;
        _yDir = input.Get<Vector2>().y;
    }

    // Realiza o movimento de fato;
    private void Movement()
    {
        myRb.linearVelocityX = _xDir * speed * Time.deltaTime;
        myRb.linearVelocityY = _yDir * speed * Time.deltaTime;
    }
    #endregion

    // Atira as pedras;
    private void OnAttack()
    {
        // Pega a posião do mouse na tela;
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        lastClick = Camera.main.ScreenToWorldPoint(new Vector2(mousePosition.x, mousePosition.y));

        if (rockAmount > 0) SpawnRock();
    }

    // Instancia uma pedra;
    private void SpawnRock()
    {
        Instantiate(rock, null);
        rockAmount--;
    }
}
