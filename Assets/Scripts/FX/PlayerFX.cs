using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFX : EntityFX
{
    [SerializeField] private List<GameObject> swordSlashImage;
    GameObject swordSlash;
    [SerializeField] private Transform swordParent;

    private void Awake()
    {

    }

    private void Update()
    {

    }

    public void SetSwordSlash(int ImageNumber, float _xPosition, float _yPosition,int _Mode2Rotation = 0)
    {
        Vector3 SwordSlashPosition = new Vector3(_xPosition * player.facingDir, _yPosition);
        SwordSlashPosition += player.transform.position;
        Quaternion SwordSlashRotation = Quaternion.Euler(0, player.facingDir == 1 ? 0 : 180, _Mode2Rotation);
        swordSlash = Instantiate(swordSlashImage[ImageNumber], SwordSlashPosition, SwordSlashRotation, swordParent);
        Destroy(swordSlash,0.1f);
    }
    public void DestroySwordSlash()
    {
        if(swordSlash != null)
            Destroy(swordSlash);
    }


}
