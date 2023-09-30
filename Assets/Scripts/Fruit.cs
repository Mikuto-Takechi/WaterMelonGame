using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] FruitList _fruitList;
    // フルーツのレベル
    public int Level = 0;
    // ゲームオーバーラインに触れた回数
    public int BorderTouchCount { get; set; } = 0;
    // フルーツを生成する機能が有効になっているかのフラグ
    public bool Activated { get; set; } = true;
    bool _gameOverJudgment = false;
    void Update()
    {
        if(transform.position.y >= GameManager.Instance.GameOverLine && _gameOverJudgment)
        {
            GameManager.Instance.GameOver();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        _gameOverJudgment = true;
        if (collision.gameObject.TryGetComponent(out Fruit fruit))
        {
            if (fruit.Level == Level && Level < 10)
            {
                Vector3 instantiatePosition = Vector3.Lerp(fruit.transform.position, transform.position, 0.5f);
                fruit.Activated = false;
                Destroy(fruit.gameObject);
                NewFruit(Level, instantiatePosition);
            }
        }
    }
    void NewFruit(int level, Vector3 position)
    {
        if (Activated == false) return;
        level += 1;
        switch (level)
        {
            case 0:
                Instantiate(_fruitList.Level0, position, Quaternion.identity);
                break;
            case 1:
                GameManager.Instance.Score += 1;
                Instantiate(_fruitList.Level1, position, Quaternion.identity);
                break;
            case 2:
                GameManager.Instance.Score += 3;
                Instantiate(_fruitList.Level2, position, Quaternion.identity);
                break;
            case 3:
                GameManager.Instance.Score += 6;
                Instantiate(_fruitList.Level3, position, Quaternion.identity);
                break;
            case 4:
                GameManager.Instance.Score += 10;
                Instantiate(_fruitList.Level4, position, Quaternion.identity);
                break;
            case 5:
                GameManager.Instance.Score += 15;
                Instantiate(_fruitList.Level5, position, Quaternion.identity);
                break;
            case 6:
                GameManager.Instance.Score += 21;
                Instantiate(_fruitList.Level6, position, Quaternion.identity);
                break;
            case 7:
                GameManager.Instance.Score += 28;
                Instantiate(_fruitList.Level7, position, Quaternion.identity);
                break;
            case 8:
                GameManager.Instance.Score += 36;
                Instantiate(_fruitList.Level8, position, Quaternion.identity);
                break;
            case 9:
                GameManager.Instance.Score += 45;
                Instantiate(_fruitList.Level9, position, Quaternion.identity);
                break;
            case 10:
                GameManager.Instance.Score += 55;
                Instantiate(_fruitList.Level10, position, Quaternion.identity);
                break;
        }
    }
}
