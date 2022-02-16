using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestLine : MonoBehaviour
{

    public InputActionReference linereference = null;

    private LineRenderer lineRenderer;
    private int positionCount;

    [SerializeField] Transform HandAnchor;

    private Vector3 linevec;
    private Vector3 currentvec;

    private List<Vector3> kado = new List<Vector3>();

    private int cornercount = 1;//角の個数

    [Tooltip("近距離におけるベクトルの除外")]
    public float removevec = 0.001f;
    [Tooltip("始点から終点の許容距離")]
    public float OKStartAndEndDis = 0.01f;
    [Tooltip("必要な直線の距離")]
    public float needvec = 0.1f;
    [Tooltip("土魔法の魔法陣エフェクト")]
    public GameObject ClayMagicCircle;//土魔法
    [Tooltip("雷魔法の魔法陣エフェクト")]
    public GameObject ThunderMagicCircle;
    [Tooltip("星魔法の魔法陣エフェクト")]
    public GameObject StarMagicCircle;
    [Tooltip("火魔法の魔法陣エフェクト")]
    public GameObject FlameMagicCircle;
    [Tooltip("水魔法の魔法陣エフェクト")]
    public GameObject WaterMagicCircle;

    [SerializeField] private float ArrowCircleRadius = 0.12f;

    private Transform Pointer
    {
        get
        {
            return HandAnchor;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        positionCount = 0;
       
    }

    // Update is called once per frame
    void Update()
    {
        float value = linereference.action.ReadValue<float>();
        var pointer = Pointer;

        if (value >= 1 )
        {

            // マウススクリーン座標をワールド座標に直す
           

            // 得られたローカル座標をラインレンダラーに追加する
            positionCount++;
            lineRenderer.positionCount = positionCount;
            lineRenderer.SetPosition(positionCount - 1, pointer.position);
        }
        

        if(positionCount > 3)
            {
            var beforePositon = lineRenderer.GetPosition(positionCount - 2);//一個前のベクトル
            var currentPosition = lineRenderer.GetPosition(positionCount - 1);//現在のベクトル

            currentvec = currentPosition - beforePositon;//一個前から現在のベクトル

            //currentvecがあまりにも小さい場合無視する
            if (currentvec.sqrMagnitude >= removevec)
            {
                linevec += currentvec; //ベクトル総和
                //ベクトルの総和とcurrentvecの内積
                var dot = Vector3.Dot(linevec.normalized, currentvec.normalized);

                //内積がcornerを認識する
                if (dot <= 0.8f)
                {  //ベクトルの総和が小さい場合、角と認識しない
                    if (linevec.sqrMagnitude > needvec)
                    {
                        linevec = currentvec;
                        Debug.Log("Corner");

                        kado.Add(currentPosition);
                        cornercount += 1;

                    }
                }

            }

        }

        //図形認識（魔法認識）
        if (value <= 0)
        {
            //描いた魔法が四角形かどうか
            if (SquareJudge())
            {
                Debug.Log("Square!!");
                Reset();
            }
            else if (Thunder())//
            {
                Debug.Log("Thunder");
                Reset();
            }
            else if (Star())
            {
                Debug.Log("Star");
                Reset();
            }
            else if (Flame())
            {
                Debug.Log("Flame");
                Reset();
            }
            else if (Water())
            {
                Debug.Log("Water");
                Reset();
            }
            else Reset();

        }







    }

    //Reset
    void Reset()
    {
        cornercount = 1;
        kado.Clear();
        positionCount = 0;
        linevec = currentvec;
    }

    //四角形判定（土魔法）
    bool SquareJudge()
    {　　//角の個数
        if (cornercount == 4)
        {
            kado.Add(lineRenderer.GetPosition(0));
            var c1 = kado[3]; //startpoint
            var c2 = kado[0]; //firstcorner
            var c3 = kado[1]; //secondcorner
            var c4 = kado[2]; //thirdcorner
            /*
            Debug.Log(c1);
            Debug.Log(c2);
            Debug.Log(c3);
            Debug.Log(c4);*/

            var end = lineRenderer.GetPosition(positionCount - 1);

            //始点と終点の位置判定
            if (Vector3.Distance(c1, end) < OKStartAndEndDis)
            {　　　//許容する角度
                const float ErrorAngle = 20;
                var ang0 = Vector3.Angle(c1 - c2, c1 - c4);
                var ang1 = Vector3.Angle(c2 - c1, c2 - c3);
                var ang2 = Vector3.Angle(c3 - c2, c3 - c4);
                var ang3 = Vector3.Angle(c4 - c1, c4 - c3);

                  Debug.Log(ang0);
                  Debug.Log(ang1);
                  Debug.Log(ang2);
                  Debug.Log(ang3);

                //cornerの角度を算出
                if (Mathf.Abs(ang0 - 90) < ErrorAngle &&
                    Mathf.Abs(ang1 - 90) < ErrorAngle &&
                    Mathf.Abs(ang2 - 90) < ErrorAngle &&
                    Mathf.Abs(ang3 - 90) < ErrorAngle)
                {
                    //図形の中心点を割り出す
                    var center = (c1 + c2 + c3 + c4) * 0.25f;

                    Debug.Log(center);

                    Instantiate(ClayMagicCircle, center, Quaternion.identity);

                    return true;
                }
                else
                {
                    Debug.Log("Square:角度不足");
                    return false;
                }
            }
            else
            {
                Debug.Log("Square:始点と終点が離れすぎている");
                return false;
            }
        }
        else return false;
    }

    bool Thunder()
    {    //角の個数
        if (cornercount == 3)
        {
            kado.Add(lineRenderer.GetPosition(0));
            var c1 = kado[2];
            var c2 = kado[0];
            var c3 = kado[1];

            var end = lineRenderer.GetPosition(positionCount - 1);

            if (Vector3.Distance(c1, end) > 0.2f)
            {
                const float ErrorAngle = 20;
                var ang1 = Vector3.Angle(c2 - c1, c2 - c3);
                var ang2 = Vector3.Angle(c3 - c2, c3 - end);



                if (Mathf.Abs(ang1 - 60) < ErrorAngle &&
                    Mathf.Abs(ang2 - 60) < ErrorAngle)
                {
                    var center = (c2 + c3) * 0.5f;
                    Instantiate(ThunderMagicCircle, center, Quaternion.identity);
                    return true;
                }
                else
                {
                    Debug.Log("Thunder: 角度不足");
                    return false;
                }
            }
            else
            {
                Debug.Log("Thunder:始点と終点が近すぎる");
                return false;
            }
        }
        else return false;

    }

    //星魔法認識
    bool Star()
    {    //角の個数
        if (cornercount == 5)
        {
            kado.Add(lineRenderer.GetPosition(0));

            var c1 = kado[4];
            var c2 = kado[0];
            var c3 = kado[1];
            var c4 = kado[2];
            var c5 = kado[3];

            var end = lineRenderer.GetPosition(positionCount - 1);

            if (Vector3.Distance(c1, end) < OKStartAndEndDis)
            {
                const float ErrorAngle = 20;
                var ang0 = Vector3.Angle(c1 - c2, c1 - c5);
                var ang1 = Vector3.Angle(c2 - c1, c2 - c3);
                var ang2 = Vector3.Angle(c3 - c2, c3 - c4);
                var ang3 = Vector3.Angle(c4 - c3, c4 - c5);
                var ang4 = Vector3.Angle(c5 - c1, c5 - c4);

                if (Mathf.Abs(ang0 - 36) < ErrorAngle &&
                   Mathf.Abs(ang1 - 36) < ErrorAngle &&
                   Mathf.Abs(ang2 - 36) < ErrorAngle &&
                   Mathf.Abs(ang3 - 36) < ErrorAngle &&
                   Mathf.Abs(ang4 - 36) < ErrorAngle)
                {
                    var center = (c1 + c2 + c3 + c4 + c5) * 0.2f;
                    Instantiate(StarMagicCircle, center, Quaternion.identity);
                    return true;
                }
                else
                {
                    Debug.Log("Star:角度不足");
                    return false;
                }
            }
            else
            {
                Debug.Log("Star:始点と終点が離れすぎている");
                return false;
            }
        }
        else return false;
    }

    //炎魔法
    bool Flame()
    {
        if (cornercount > 6)
        {
            var start = lineRenderer.GetPosition(0);
            var end = lineRenderer.GetPosition(positionCount - 1);

            if (Vector3.Distance(start, end) < OKStartAndEndDis)
            {
                Instantiate(FlameMagicCircle, start, Quaternion.identity);
                return true;
            }
            else
            {
                Debug.Log("Flame:始点と終点が離れすぎている");
                return false;
            }

        }
        else return false;
    }

    //円認識（水魔法）
    bool Water()
    {
        if (positionCount > 30)
        {
            var Sum = Vector3.zero;
            var start = lineRenderer.GetPosition(0);
            var end = lineRenderer.GetPosition(positionCount - 1);
            var AveDistance = 0f;
            var nextAveDistnce = 0f;

            if (Vector3.Distance(start, end) < OKStartAndEndDis && cornercount < 2)
            {
                //適当な間隔の点を足し、中心点を割り出す。
                for (int i = 0; i < positionCount - 5; i += 5)
                {
                    Sum += lineRenderer.GetPosition(i);
                }

                var Center = Sum / (positionCount / 5);

                //各点の中心点からの距離を比較し、許容範囲内か判定する
                for (int j = 0; j < positionCount - 5; j += 5)
                {
                    AveDistance = Vector3.Distance(Center, lineRenderer.GetPosition(j));
                    if (j < 1)
                    {
                        nextAveDistnce = AveDistance;
                        continue;
                    }

                    if (Mathf.Abs(AveDistance - nextAveDistnce) < 0.6 && AveDistance > ArrowCircleRadius)
                    {
                        nextAveDistnce = AveDistance;
                    }
                    else
                    {
                        Debug.Log("Circle:長さ違い");
                        return false;
                    }



                }

                Instantiate(WaterMagicCircle, Center, Quaternion.identity);
                return true;
            }

            Debug.Log("Circle:始点と終点が離れすぎているもしくは角が存在している");
            return false;
        }

        return false;
    }




}
