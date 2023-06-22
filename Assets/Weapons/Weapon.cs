using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    //Generic Methods
    public virtual void PrimaryFire()
    {
    }
    public virtual void SecondaryFire() 
    {
    }
    public virtual void PrimaryFireRecoil()
    {
    }
    public virtual void SecondaryFireRecoil()
    {
    }
    public virtual void Reload()
    {
    }
    public virtual void Recoil()
    {
        StartCoroutine(RecoilCoroutine());
    }

    private IEnumerator RecoilCoroutine()
    {
        Vector3 initialPosition = Camera.main.transform.localPosition;
        Vector3 recoilDirection = Vector3.up + Random.insideUnitSphere;
        float recoilDuration = 0.5f;
        float elapsedTime = 0f;
        while (elapsedTime < recoilDuration)
        {
            float t = elapsedTime / recoilDuration;
            float recoilAmount = Mathf.Lerp(0f, 0.1f, t);
            Camera.main.transform.localPosition = initialPosition + recoilDirection * recoilAmount;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.localPosition = initialPosition;
    }
}
