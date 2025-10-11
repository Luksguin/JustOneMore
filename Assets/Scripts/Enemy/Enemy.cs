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

    [HideInInspector] public Transform nextPoint;
    [HideInInspector] public bool patrolling;

    private int _nextIndex;
    private bool _lookAtPlayer;

    private float _xDir;
    private float _yDir;

    private void Awake()
    {
        foreach (var p in points) p.parent = null;

        patrolling = true;

        _nextIndex = 0;
        _lookAtPlayer = false;

        _xDir = 0;
        _yDir = 0;
    }

    private void Update()
    {
        if(_lookAtPlayer == true) return;

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
        if(!_lookAtPlayer)WalkManager();
    }

    private void WalkManager()
    {
        if (_nextIndex >= points.Length) _nextIndex = 0;

        if(patrolling) nextPoint = points[_nextIndex];

        if(Vector2.Distance(transform.position, nextPoint.position) > 0)
        {
            WalkToPoint(nextPoint);
        }
        else
        {
            _nextIndex++;
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

    public void KillPlayer()
    {
        _lookAtPlayer = true;
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
