using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public float weight;
    public AudioSource myAudioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Player.instance.isHelping)
        {
            Player.instance.myAnimator.SetBool(Player.instance.helpingBool, false);
            Player.instance.speed *= weight;
            Player.instance.isHelping = false;

            myAudioSource.Play();
        }
    }
}
