using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour, IDamageable
{

    #region "Inspector Fields"
    [SerializeField] float explodeTimer = 2f;
    [SerializeField] GameObject explosion;
    [SerializeField] AudioClip explosionFX;
    #endregion


    [HideInInspector]
    public Player playerOwner { get; set; }

    int explosionRange = 1;
    float elapsedDuration = 0f;
    AudioManager audioManager;


    public event Action<BombController> OnExplode;

    public static BombController Create(GameObject bomPrefab, Vector3 position, int explosionRange)
    {
        GameObject newObject = Instantiate(bomPrefab, position, Quaternion.identity) as GameObject;
        BombController result = newObject.GetComponent<BombController>();
        result.explosionRange = explosionRange;
        //do additional initialization steps here
        return result;
    }

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {

    }

    void Update()
    {
        elapsedDuration += Time.deltaTime;

        if (elapsedDuration >= explodeTimer)
            Explode();
    }

    void Explode()
    {
        if (explosion != null)
            Explosion.Create(explosion, transform.position, explosionRange, true);

        audioManager.PlayExplosionFX();

        if (OnExplode != null)
            OnExplode(this);

        Destroy(gameObject);
    }

    public void GetDamage(int damageValue)
    {
        Explode();
    }
}
