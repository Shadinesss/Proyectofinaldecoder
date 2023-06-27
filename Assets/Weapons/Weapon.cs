using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    //Generic Methods
    protected virtual void PrimaryFire()
    {
    }
    protected virtual void SecondaryFire() 
    {
    }
    protected virtual void PrimaryFireRecoil()
    {
    }
    protected virtual void SecondaryFireRecoil()
    {
    }
    protected virtual void Reload()
    {
    }
    //protected virtual void Recoil()
    //{
    //}
}
