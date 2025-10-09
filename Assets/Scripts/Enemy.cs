using NUnit.Framework;
using System.Collections;
using System.Drawing;
using UnityEditor.Timeline.Actions;
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
        if(_patrolling == false) return;

        if (_xDir > 0 && Mathf.Abs(_xDir) > Mathf.Abs(_yDir)) // Olhar para direita;
        {
            LookRigth();
        }
        else if (_xDir < 0 && Mathf.Abs(_xDir) > Mathf.Abs(_yDir)) // Olhar para esquerda;
        {
            LookLeft();
        }
        else if (_yDir > 0 && Mathf.Abs(_yDir) > Mathf.Abs(_xDir)) // Olhar para cima;
        {
            LookUp();
        }
        else if (_yDir < 0 && Mathf.Abs(_yDir) > Mathf.Abs(_xDir)) // Olhar para baixo;
        {
            LookDown();
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

    #region Animations
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
        Player.instance.speed = 0;

        _xDir = Player.instance.transform.position.x - transform.position.x;
        _yDir = Player.instance.transform.position.y - transform.position.y;

        if (_xDir > 0 && Mathf.Abs(_xDir) > Mathf.Abs(_yDir)) // Olhar para direita;
        {
            LookRigth();
        }
        else if (_xDir < 0 && Mathf.Abs(_xDir) > Mathf.Abs(_yDir)) // Olhar para esquerda;
        {
            LookLeft();
        }
        else if (_yDir > 0 && Mathf.Abs(_yDir) > Mathf.Abs(_xDir)) // Olhar para cima;
        {
            LookUp();
        }
        else if (_yDir < 0 && Mathf.Abs(_yDir) > Mathf.Abs(_xDir)) // Olhar para baixo;
        {
            LookDown();
        }

        Invoke("StopAnimations", .25f);
    }
}
