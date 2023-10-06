using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FruitManager : SingletonBase<FruitManager>
{
    [SerializeField] FruitList _fruitList;
    [SerializeField] GameObject _particle;
    public HashSet<Tuple<Fruit, Fruit>> Fruits { get; set; } = new(new SameTuplesComparer());
    protected override void DoAwake(){}
    void Update()
    {
        if(Fruits.Count > 0)
        {
            foreach (var fruit in Fruits)
            {
                Vector3 instantiatePosition = Vector3.Lerp(fruit.Item1.transform.position, fruit.Item2.transform.position, 0.5f);
                Destroy(fruit.Item1.gameObject);
                Destroy(fruit.Item2.gameObject);
                AudioManager.Instance.PlaySE("プールでダイビング");
                NewFruit(fruit.Item1.Level, instantiatePosition);
            }
            Fruits.Clear();
        }
    }
    void NewFruit(int level, Vector3 position)
    {
        level += 1;
        switch (level)
        {
            case 0:
                Instantiate(_fruitList.Level0, position, Quaternion.identity);
                break;
            case 1:
                GameManager.Instance.AddScore(1);
                Instantiate(_fruitList.Level1, position, Quaternion.identity);
                break;
            case 2:
                GameManager.Instance.AddScore(3);
                Instantiate(_fruitList.Level2, position, Quaternion.identity);
                break;
            case 3:
                GameManager.Instance.AddScore(6);
                Instantiate(_fruitList.Level3, position, Quaternion.identity);
                break;
            case 4:
                GameManager.Instance.AddScore(10);
                Instantiate(_fruitList.Level4, position, Quaternion.identity);
                break;
            case 5:
                GameManager.Instance.AddScore(15);
                Instantiate(_fruitList.Level5, position, Quaternion.identity);
                break;
            case 6:
                GameManager.Instance.AddScore(21);
                Instantiate(_fruitList.Level6, position, Quaternion.identity);
                break;
            case 7:
                GameManager.Instance.AddScore(28);
                Instantiate(_fruitList.Level7, position, Quaternion.identity);
                break;
            case 8:
                GameManager.Instance.AddScore(36);
                Instantiate(_fruitList.Level8, position, Quaternion.identity);
                break;
            case 9:
                GameManager.Instance.AddScore(45);
                Instantiate(_fruitList.Level9, position, Quaternion.identity);
                break;
            case 10:
                GameManager.Instance.AddScore(55);
                Instantiate(_fruitList.Level10, position, Quaternion.identity);
                break;
        }
        Destroy(Instantiate(_particle, position, Quaternion.identity), 1);
    }
}
class SameTuplesComparer : EqualityComparer<Tuple<Fruit, Fruit>>
{
    public override bool Equals(Tuple<Fruit, Fruit> t1, Tuple<Fruit, Fruit> t2)
    {
        return t1.Item1.SelfNumber.Equals(t2.Item1.SelfNumber) || t1.Item2.SelfNumber.Equals(t2.Item2.SelfNumber) || t1.Item1.SelfNumber.Equals(t2.Item2.SelfNumber) || t1.Item2.SelfNumber.Equals(t2.Item1.SelfNumber);
    }
    public override int GetHashCode(Tuple<Fruit, Fruit> t)
    {
        return base.GetHashCode();
    }
}