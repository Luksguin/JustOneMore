using NUnit.Framework;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRb;
    public Transform[] points;

    [Header("Animations")]
    public Animator myAnimator;
    public string boolUpWalk;
    public string boolDownWalk;
    public string boolLeftWalk;
    public string boolRightWalk;

    private int _nextPoint;
    private float _xDir;
    private float _yDir;

    private void Start()
    {
        _nextPoint = 0;
        _xDir = 0;
        _yDir = 0;
    }

    private void Update()
    {
        if (_xDir > 0 && Mathf.Abs(_xDir) > Mathf.Abs(_yDir)) // Indo para direita;
        {
            myAnimator.SetBool(boolRightWalk, true);

            myAnimator.SetBool(boolUpWalk, false);
            myAnimator.SetBool(boolDownWalk, false);
            myAnimator.SetBool(boolLeftWalk, false);
        }
        else if (_xDir < 0 && Mathf.Abs(_xDir) > Mathf.Abs(_yDir)) // Indo para esquerda;
        {
            myAnimator.SetBool(boolLeftWalk, true);

            myAnimator.SetBool(boolUpWalk, false);
            myAnimator.SetBool(boolDownWalk, false);
            myAnimator.SetBool(boolRightWalk, false);
        }
        else if (_yDir > 0 && Mathf.Abs(_yDir) > Mathf.Abs(_xDir)) // Indo para cima;
        {
            myAnimator.SetBool(boolUpWalk, true);

            myAnimator.SetBool(boolDownWalk, false);
            myAnimator.SetBool(boolRightWalk, false);
            myAnimator.SetBool(boolLeftWalk, false);
        }
        else if (_yDir < 0 && Mathf.Abs(_yDir) > Mathf.Abs(_xDir)) // Indo para baixo;
        {
            myAnimator.SetBool(boolDownWalk, true);

            myAnimator.SetBool(boolUpWalk, false);
            myAnimator.SetBool(boolRightWalk, false);
            myAnimator.SetBool(boolLeftWalk, false);
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
        WalkManager();
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
}
