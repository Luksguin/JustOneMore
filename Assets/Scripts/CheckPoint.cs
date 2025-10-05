using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public string playerTag;
    public float weight;
    public AudioSource myAudioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.tag == playerTag)
        {
            Player.instance.myAnimator.SetBool(Player.instance.helpingBool, false);
            Player.instance.speed *= weight;

            myAudioSource.Play();
        }
    }
}
