using Luksguin.Singleton;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Singleton<Player>
{
    [Header("Movements")]
    public float speed;
    public Rigidbody2D myRb;

    [Header("Animations")]
    public Animator myAnimator;
    public string friendTag;

    public string boolIdle;
    public string helpingBool;

    public string boolUpWalk;
    public string boolDownWalk;
    public string boolLeftWalk;
    public string boolRightWalk;

    private float _xDir;
    private float _yDir;

    private void Update()
    {
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
    private void OnMove(InputValue input)
    {
        _xDir = input.Get<Vector2>().x;
        _yDir = input.Get<Vector2>().y;
    }

    private void Movement()
    {
        myRb.linearVelocityX = _xDir * speed * Time.deltaTime;
        myRb.linearVelocityY = _yDir * speed * Time.deltaTime;

    }
    #endregion

}
