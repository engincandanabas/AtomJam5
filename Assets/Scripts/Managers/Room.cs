using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    [SerializeField] private int _maxCapacity=4;
    [SerializeField] private int _currentCapacity=0;
    public VerticalLayoutGroup _heroLayoutGroup;
    public VerticalLayoutGroup _enemyLayoutGroup;

    [SerializeField] private int _currnetPassCount;
    public int MaxCapacity { get { return _maxCapacity; }}
    public int CurrentCapacity { get { return _currentCapacity; }set { _currentCapacity = value; } }

    public List<HeroManager> heroManagers = new List<HeroManager>();
    public List<EnemyManager> enemyManagers = new List<EnemyManager>();

}
