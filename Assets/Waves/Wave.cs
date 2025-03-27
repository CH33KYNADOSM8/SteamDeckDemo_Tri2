using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave")]
public class Wave : ScriptableObject
{
    [SerializeField] int enemyCount;

    public int EnemyCount { get { return enemyCount; } }


}
