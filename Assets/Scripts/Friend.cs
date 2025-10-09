using UnityEngine;

public class Friend : MonoBehaviour
{
    public string playerTag;
    public float weight;
    public AudioSource myAudioSource;
    public GameObject myCollider;
    public GameObject myRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.parent.tag == playerTag && !Player.instance.isHelping)
        {
            Player.instance.myAnimator.SetBool(Player.instance.helpingBool, true);
            Player.instance.speed /= weight;
            Player.instance.isHelping = true;

            myAudioSource.Play();

            Destroy(myCollider);
            Destroy(myRenderer);

            Destroy(gameObject, .4f);
        }
    }
}
