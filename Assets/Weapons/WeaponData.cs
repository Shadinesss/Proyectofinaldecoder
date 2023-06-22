using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "WeaponData Data", menuName = "Data/WeaponData")]

public class WeaponData : ScriptableObject
{
    private StoryGameManager m_gameManager;

    [Header("ID")]
    [SerializeField] private string weaponName;
    [SerializeField] private string weaponDescription;
    [SerializeField] private WeaponTypeEnum weaponType;
    [Header("Primary Shoot")]
    [SerializeField] private float primaryGeneralDamage;
    [SerializeField] private float primaryWeakPointDamage;
    [SerializeField] private StoryGameManager.PiercingType primaryPiercingType;
    [SerializeField] private float primaryFiresPerSecond;
    [SerializeField] private float primaryMaxRange;
    [SerializeField] private AudioSource primaryFireSound;
    [Header("Alternative Shoot")]
    [SerializeField] private float secondaryGeneralDamage;
    [SerializeField] private float secondaryWeakPointDamage;
    [SerializeField] private StoryGameManager.PiercingType secondaryPiercingType;
    [SerializeField] private float secondaryFiresPerSecond;
    [SerializeField] private float secondaryMaxRange;
    [SerializeField] private AudioSource secondaryFireSound;

    public enum WeaponTypeEnum
    {
        Melee,
        Secondary,
        Assault,
        HighLevel,
        Support
    }

    //Getters
    public string GetWeaponName()
    {
        return weaponName;
    }
    public string GetWeaponDescription()
    {
        return weaponDescription;
    }
    public WeaponTypeEnum GetWeaponType()
    {
        return weaponType;
    }
    //PrimaryFire
    public float GetPrimaryGeneralDamage()
    {
        return primaryGeneralDamage;
    }
    public float GetPrimaryWeakPointDamage()
    {
        return primaryWeakPointDamage;
    }
    public StoryGameManager.PiercingType GetPrimaryPiercingType()
    {
        return primaryPiercingType;
    }
    public float GetPrimaryFiresPerSecond()
    {
        return primaryFiresPerSecond;
    }
    public float GetPrimaryMaxRange()
    {
        return primaryMaxRange;
    }
    //SecondaryFire
    public float GetSecondaryGeneralDamage()
    {
        return secondaryGeneralDamage;
    }
    public float GetSecondaryWeakPointDamage()
    {
        return secondaryWeakPointDamage;
    }
    public StoryGameManager.PiercingType GetSecondaryPiercingType()
    {
        return secondaryPiercingType;
    }
    public float GetSecondaryFiresPerSecond()
    {
        return secondaryFiresPerSecond;
    }
    public float GetSecondaryMaxRange()
    {
        return secondaryMaxRange;
    }

}
