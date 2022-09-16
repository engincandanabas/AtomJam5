using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    [SerializeField] private int _maxCapacity=4;
    [SerializeField] private int _currentCapacity=0;
    [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;

    public int MaxCapacity { get { return _maxCapacity; }}
    public int CurrentCapacity { get { return _currentCapacity; }set { _currentCapacity = value; } }

    public List<HeroManager> heroManagers = new List<HeroManager>();
    public List<EnemyManager> enemyManagers = new List<EnemyManager>();

}
