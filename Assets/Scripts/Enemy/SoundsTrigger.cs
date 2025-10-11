using UnityEngine;
using UnityEngine.InputSystem;

public class SoundsTrigger : MonoBehaviour
{
    public Enemy enemy;
    public SpriteRenderer enemyRenderer;

    public float minDistance;
    public float triggerRadius;

    public Color colorOutline;
    public float sizeOutLine;

    private Transform _rockTransform;
    private Transform _startPosition;
    private bool _canBack;
    private bool _outLine;

    void Awake()
    {
        enemyRenderer.material.SetColor("_OutlineColor", colorOutline);
        enemyRenderer.material.SetFloat("_OutlineSize", 0f);

        _startPosition = new GameObject("Gambiarra").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Rock" && enemy.patrolling)
        {
            enemy.patrolling = false;
            enemy.nextPoint = collision.transform;

            _startPosition.position = enemy.transform.position;
            _rockTransform = collision.transform;
            Destroy(collision);
        }
    }

    private void Update()
    {
        if (_rockTransform)
        {
            print(_startPosition.position);

            if (Vector2.Distance(enemy.transform.position, _rockTransform.position) <= minDistance)
            {
                enemy.nextPoint = _startPosition.transform;
                _canBack = true;
            }

            if (Vector2.Distance(enemy.transform.position, _startPosition.position) <= 0 && _canBack)
            {
                enemy.patrolling = true;
                _rockTransform = null;
                _canBack = false;
            }
        }

        Vector2 mousePixel = Mouse.current.position.ReadValue();
        Vector2 mouse = Camera.main.ScreenToWorldPoint(new Vector2(mousePixel.x, mousePixel.y));

        if(Vector2.Distance(mouse, enemy.transform.position) <= triggerRadius && !_outLine)
        {
            enemyRenderer.material.SetFloat("_OutlineSize", sizeOutLine);
            _outLine = true;
        }

        if (Vector2.Distance(mouse, enemy.transform.position) > triggerRadius && _outLine)
        {
            enemyRenderer.material.SetFloat("_OutlineSize", 0f);
            _outLine = false;
        }
    }
}
