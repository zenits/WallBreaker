using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{

#region "Inspector Fields"
    [SerializeField] float explodeTimer = 2f;
    [SerializeField] GameObject explosion;
    [SerializeField] AudioClip explosionFX;
#endregion


    [HideInInspector]
    public Player playerOwner {get;set;}

    
    float elapsedDuration = 0f;
    AudioManager audioManager;

    
    
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        elapsedDuration += Time.deltaTime;

        Explode();
    }

    void Explode()
    {
        if (elapsedDuration >= explodeTimer)
        {
            if (explosion != null)
                GameManager.Instance.CreateExplosion(transform.position, null, true);
            //Instantiate(explosion, transform.position, Quaternion.identity);
            if (explosionFX != null)
                audioManager.PlayExplosionFX();

            playerOwner.IncreaseAvailableBombQuantity();
            
            Destroy(gameObject);
        }
    }
}
