using UnityEngine;
using UnityEngine.InputSystem;

public class SoundsTrigger : MonoBehaviour
{
    public Enemy enemy;
    public SpriteRenderer enemyRenderer;

    public float minDistance;
    public float triggerRadius;

    private Transform _rockTransform;
    private bool _flash;

    public Color colorOutline;
    public float sizeOutLine;

    void Awake()
    {
        enemyRenderer.material.SetColor("_OutlineColor", colorOutline);
        enemyRenderer.material.SetFloat("_OutlineSize", 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Rock")
        {
            enemy.patrolling = false;
            enemy.nextPoint = collision.transform;

            _rockTransform = collision.transform;
            Destroy(collision);
        }
    }

    private void Update()
    {
        if (_rockTransform)
        {
            if (Vector2.Distance(enemy.transform.position, _rockTransform.position) <= minDistance)
            {
                enemy.patrolling = true;
                _rockTransform = null;
            }
        }

        Vector2 mousePixel = Mouse.current.position.ReadValue();
        Vector2 mouse = Camera.main.ScreenToWorldPoint(new Vector2(mousePixel.x, mousePixel.y));

        if(Vector2.Distance(mouse, enemy.transform.position) <= triggerRadius && !_flash)
        {
            enemyRenderer.material.SetFloat("_OutlineSize", sizeOutLine);
            _flash = true;
        }

        if (Vector2.Distance(mouse, enemy.transform.position) > triggerRadius && _flash)
        {
            enemyRenderer.material.SetFloat("_OutlineSize", 0f);
            _flash = false;
        }
    }
}
