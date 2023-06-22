using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "Collectable Data", menuName = "Data/Collectable")]
public class Collectable : ScriptableObject
{
    [Header("ID")]
    [SerializeField] private string collectableName;
    [SerializeField] private string collectableDescription;
    [SerializeField] private CollectableType Type;
    [Header("Object stats")]
    [SerializeField] private float collectablePointsGiven; //if the collectable is a shield it will give "collectablePointsGiven" shield when picked up.
    [SerializeField] private AudioSource collectablePickUpSound;
    public enum CollectableType
    {
        Weapon,
        Shield,
        Medicine,
        Battery,
        FuturisticPistolAmmo,
        FuturisticRiffleAmmo1,
        FuturisticRiffleAmmo2,
        Trap
    }
    public string GetCollectableName()
    {
        return collectableName;
    }

    public string GetCollectableDescription()
    {
        return collectableDescription;
    }
    public string GetCollectableType()
    {
        return Type.ToString();
    }

    public float CollectablePointsGiven()
    {
        return collectablePointsGiven;
    }


}
