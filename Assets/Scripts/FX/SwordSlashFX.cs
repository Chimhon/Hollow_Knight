using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlashFX : EntityFX
{
    [SerializeField] public GameObject swordSlashPrefab;
    private ParticleSystem swordSlashEffect;
    public Transform swordParent;


    private void Awake()
    {
        
    }


    public void SetSwordSlash(float _xPosition, float _yPostion, float _xRotation)
    {
        Vector3 SwordSlashPosition = new Vector3(_xPosition * player.facingDir, _yPostion, 0);
        SwordSlashPosition += player.transform.position;
        Quaternion SwordSlashRotation = Quaternion.Euler(_xRotation, -90, 90);

        GameObject swordSlash = Instantiate(swordSlashPrefab, SwordSlashPosition, SwordSlashRotation, swordParent);
        swordSlashEffect = swordSlash.GetComponent<ParticleSystem>();
        swordSlashEffect.Play();
        Destroy(swordSlash, 1f);
    }

    private void Update()
    {

    }
}
