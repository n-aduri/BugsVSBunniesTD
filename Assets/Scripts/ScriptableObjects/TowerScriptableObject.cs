using System;
using System.Collections.Generic;
using DefaultNamespace.OnDeathEffects;
using UnityEngine;

namespace DefaultNamespace.TowerSystem
{
    [CreateAssetMenu(fileName = "New Tower", menuName = "New Data / Tower Data")]
    public class TowerScriptableObject : ScriptableObject
    {
        [Header("Purchase Settings")] 
        public int purchaseCost;
        public string selectedTitle;
        public Sprite purchaseIcon;
        public GameObject prefab;
        public Mesh previewMesh;
        public Color purchaseIconAdditionalTint = Color.white;

        [Header("Tower Settings")] 
        public BulletConfig BulletConfig;
        public float fireRate;
        public float attackRadius;
        public float burstDelay;
        public TargetType myTargetingType;
        public List<Debuff> debuffs;
        
        [Header("Upgradable To")]
        public TowerScriptableObject[] upgradeOptions;
        
        [Header("Particul Settings")]
        public ParticleSystem particulOnShoot;

        [Header("Audio Settings")] 
        public float audioCoolDown;
        public AudioType audioPlayType = AudioType.Multisource;
        public List<AudioClip> firingSfx;
    }
    
    [Serializable]
    public struct BulletConfig : IPoolable
    {
        public int ID => prefab.GetHashCode();
        public GameObject prefab;
        public float damage;
        public float speed;
        [Range(1,10)]
        public int maxHit;
        public eDeathEffect deathEffect;
        public bool BulletDiesOnTargetDeath;

        public GameObject Prefab => prefab;

        public void Dispose()
        {
        }
    }

    public enum TargetType
    {
        RandomSelect,
        FocusOnFirst,
        FocusOnMiddle,
        FocusOnLast,
    }

    public enum AudioType
    {
        SingleSource,
        Multisource
    }

    public enum eDeathEffect
    {
        None,
        BubbleUp,
        FlyAway,
        Electrocute,
    }
}