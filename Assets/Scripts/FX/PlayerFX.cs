using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFX : EntityFX
{
    [SerializeField] private List<Texture2D> SwordSlashImage;


    private void Awake()
    {

    }


    //public void SetSwordSlash(float _xPosition, float _yPostion, float _xRotation)
    //{
    //    Vector3 SwordSlashPosition = new Vector3(_xPosition * player.facingDir, _yPostion, 0);
    //    SwordSlashPosition += player.transform.position;
    //    Quaternion SwordSlashRotation = Quaternion.Euler(_xRotation, -90, 90);

    //    GameObject swordSlash = Instantiate(swordSlashPrefab, SwordSlashPosition, SwordSlashRotation, swordParent);
    //    swordSlashEffect = swordSlash.GetComponent<ParticleSystem>();
    //    swordSlashEffect.Play();
    //    Destroy(swordSlash, 1f);
    //}

    private void Update()
    {

    }

    public void SetSwordSlash(int ImageNumber)
    {
        GameObject swordSlash = Instantiate(SwordSlashImage[ImageNumber].GameObject(),player.transform.position,Quaternion.identity);
    }


}
