using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(FireballAudioManager))]
public class FireBall : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject fireballEffect;
    private MeshRenderer mesh;
    private FireballAudioManager audioManager;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        audioManager = GetComponent<FireballAudioManager>();
    }

    private void FixedUpdate()
    {
        if (transform.position.y <= 0 && mesh.enabled == true)
            Boom();
    }

    private void Boom()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        mesh.enabled = false;
        fireballEffect.SetActive(false);
        explosionEffect.SetActive(true);
        audioManager.PlayBoom();

        //увеличиваем коллайдер во время взрыва и отключаем его через 0,5сек
        GetComponent<SphereCollider>().radius = 1;
        Invoke("InactiveTrigger", 0.5f);

        Invoke("DestroyGO", 2);

    }

    private void InactiveTrigger()
    {
        GetComponent<SphereCollider>().enabled = false;
    }

    private void DestroyGO()
    {
        Destroy(gameObject);
    }
}
