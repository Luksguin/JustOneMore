using NUnit.Framework;
using System.Drawing;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRb;
    public Transform[] points;
    public Transform vision;

    [Header("Animations")]
    public Animator myAnimator;
    public string boolUpWalk;
    public string boolDownWalk;
    public string boolLeftWalk;
    public string boolRightWalk;

    private int _nextPoint;
    private float _xDir;
    private float _yDir;

    private bool _patrolling;

    private void Awake()
    {
        _nextPoint = 0;
        _xDir = 0;
        _yDir = 0;
        _patrolling = true;
    }

    private void Update()
    {
        if(_patrolling == false)
        {
            myAnimator.SetBool(boolUpWalk, false);
            myAnimator.SetBool(boolDownWalk, false);
            myAnimator.SetBool(boolRightWalk, false);
            myAnimator.SetBool(boolLeftWalk, false);
            return;
        }

        if (_xDir > 0 && Mathf.Abs(_xDir) > Mathf.Abs(_yDir)) // Indo para direita;
        {
            myAnimator.SetBool(boolRightWalk, true);

            myAnimator.SetBool(boolUpWalk, false);
            myAnimator.SetBool(boolDownWalk, false);
            myAnimator.SetBool(boolLeftWalk, false);

            vision.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (_xDir < 0 && Mathf.Abs(_xDir) > Mathf.Abs(_yDir)) // Indo para esquerda;
        {
            myAnimator.SetBool(boolLeftWalk, true);

            myAnimator.SetBool(boolUpWalk, false);
            myAnimator.SetBool(boolDownWalk, false);
            myAnimator.SetBool(boolRightWalk, false);

            vision.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (_yDir > 0 && Mathf.Abs(_yDir) > Mathf.Abs(_xDir)) // Indo para cima;
        {
            myAnimator.SetBool(boolUpWalk, true);

            myAnimator.SetBool(boolDownWalk, false);
            myAnimator.SetBool(boolRightWalk, false);
            myAnimator.SetBool(boolLeftWalk, false);

            vision.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (_yDir < 0 && Mathf.Abs(_yDir) > Mathf.Abs(_xDir)) // Indo para baixo;
        {
            myAnimator.SetBool(boolDownWalk, true);

            myAnimator.SetBool(boolUpWalk, false);
            myAnimator.SetBool(boolRightWalk, false);
            myAnimator.SetBool(boolLeftWalk, false);

            vision.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (_xDir == 0)
        {
            myAnimator.SetBool(boolRightWalk, false);
            myAnimator.SetBool(boolLeftWalk, false);
        }

        if (_yDir == 0)
        {
            myAnimator.SetBool(boolUpWalk, false);
            myAnimator.SetBool(boolDownWalk, false);
        }
    }

    private void FixedUpdate()
    {
        if(_patrolling)WalkManager();
    }

    private void WalkManager()
    {
        if (_nextPoint >= points.Length) _nextPoint = 0;

        if(Vector2.Distance(transform.position, points[_nextPoint].position) > 0)
        {
            WalkToPoint(points[_nextPoint]);
        }
        else
        {
            _nextPoint++;
        }
    }

    private void WalkToPoint(Transform point)
    {
        _xDir = point.position.x - transform.position.x;
        _yDir = point.position.y - transform.position.y;

        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.tag == "Player")
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        _patrolling = false;
        Player.instance.myAnimator.SetBool(Player.instance.gameOverBool, true);
        Player.instance.canWalk = false;
       //Testa isso aq transform.LookAt(Player.instance.transform);
    }
}
