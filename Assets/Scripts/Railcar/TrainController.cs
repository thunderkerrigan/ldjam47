using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.Soundy;
using UnityEngine;

public class TrainController : MonoBehaviour
{

    private Animator _animator;

    private PathFollower_Tilled _follower;
    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponentInChildren<Animator>();
        _follower = gameObject.GetComponent<PathFollower_Tilled>();
        _follower.KillMePleaseHandler += Kill;
    }

    private IEnumerator DeathSwitch()
    {
        _animator.SetTrigger("Death");
        SoundyManager.Play("Game", "boom", transform.position);
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }
    
    private void Kill(GameObject myself)
    {
        StartCoroutine(DeathSwitch());
        _follower.KillMePleaseHandler -= Kill;

    }
}
