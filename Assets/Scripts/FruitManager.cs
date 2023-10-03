using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [SerializeField] FruitList _fruitList;
    public static FruitManager instance;
    public HashSet<Tuple<Fruit, Fruit>> _fruits { get; set; } = new(new SameTuplesComparer());
    private void Awake()
    {
        if(instance == null )
        {
            instance = this;
            DontDestroyOnLoad( gameObject );
        }
        else
        {
            Destroy( gameObject );
        }
    }
    void Update()
    {
        if(_fruits.Count > 0)
        {
            foreach (var fruit in _fruits)
            {
                Vector3 instantiatePosition = Vector3.Lerp(fruit.Item1.transform.position, fruit.Item2.transform.position, 0.5f);
                Destroy(fruit.Item1.gameObject);
                Destroy(fruit.Item2.gameObject);
                AudioManager.instance.PlaySE("kotsudumi");
                NewFruit(fruit.Item1._level, instantiatePosition);
            }
            _fruits.Clear();
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
                GameManager.instance.Score += 1;
                Instantiate(_fruitList.Level1, position, Quaternion.identity);
                break;
            case 2:
                GameManager.instance.Score += 3;
                Instantiate(_fruitList.Level2, position, Quaternion.identity);
                break;
            case 3:
                GameManager.instance.Score += 6;
                Instantiate(_fruitList.Level3, position, Quaternion.identity);
                break;
            case 4:
                GameManager.instance.Score += 10;
                Instantiate(_fruitList.Level4, position, Quaternion.identity);
                break;
            case 5:
                GameManager.instance.Score += 15;
                Instantiate(_fruitList.Level5, position, Quaternion.identity);
                break;
            case 6:
                GameManager.instance.Score += 21;
                Instantiate(_fruitList.Level6, position, Quaternion.identity);
                break;
            case 7:
                GameManager.instance.Score += 28;
                Instantiate(_fruitList.Level7, position, Quaternion.identity);
                break;
            case 8:
                GameManager.instance.Score += 36;
                Instantiate(_fruitList.Level8, position, Quaternion.identity);
                break;
            case 9:
                GameManager.instance.Score += 45;
                Instantiate(_fruitList.Level9, position, Quaternion.identity);
                break;
            case 10:
                GameManager.instance.Score += 55;
                Instantiate(_fruitList.Level10, position, Quaternion.identity);
                break;
        }
    }
}
class SameTuplesComparer : EqualityComparer<Tuple<Fruit, Fruit>>
{
    public override bool Equals(Tuple<Fruit, Fruit> t1, Tuple<Fruit, Fruit> t2)
    {
        return t1.Item1._selfNumber.Equals(t2.Item1._selfNumber) || t1.Item2._selfNumber.Equals(t2.Item2._selfNumber) || t1.Item1._selfNumber.Equals(t2.Item2._selfNumber) || t1.Item2._selfNumber.Equals(t2.Item1._selfNumber);
    }


    public override int GetHashCode(Tuple<Fruit, Fruit> t)
    {
        return base.GetHashCode();
    }
}