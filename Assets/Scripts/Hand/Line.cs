using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private int positionCount;
    private Camera mainCamera;

    private Vector3 linevec;
    private Vector3 currentvec;

    [Tooltip("始点から終点の許容距離")]
    public float OKStartAndEndDis = 2.0f;

    private List<Vector3> kado = new List<Vector3>();

    private int cornercount = 1;//角の個数

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


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // ラインの座標指定を、このラインオブジェクトのローカル座標系を基準にするよう設定を変更
        // この状態でラインオブジェクトを移動・回転させると、描かれたラインもワールド空間に取り残されることなく、一緒に移動・回転
        lineRenderer.useWorldSpace = false;
        positionCount = 0;
        mainCamera = Camera.main;
    }

    void Update()
    {
        // このラインオブジェクトを、位置はカメラ前方10m、回転はカメラと同じになるようキープさせる
        transform.position = mainCamera.transform.position + mainCamera.transform.forward * 10;
        transform.rotation = mainCamera.transform.rotation;

        if (Input.GetMouseButton(0))
        {
            // 座標指定の設定をローカル座標系にしたため、与える座標にも手を加える
            Vector3 pos = Input.mousePosition;
            pos.z = 10.0f;

            // マウススクリーン座標をワールド座標に直す
            pos = mainCamera.ScreenToWorldPoint(pos);

            // さらにそれをローカル座標に直す。
            pos = transform.InverseTransformPoint(pos);

            // 得られたローカル座標をラインレンダラーに追加する
            positionCount++;
            lineRenderer.positionCount = positionCount;
            lineRenderer.SetPosition(positionCount - 1, pos);



            //角認識
            if (positionCount > 3)
            {
                var beforePositon = lineRenderer.GetPosition(positionCount - 2);//一個前のベクトル
                var currentPosition = lineRenderer.GetPosition(positionCount - 1);//現在のベクトル

                currentvec = currentPosition - beforePositon;//一個前から現在のベクトル

                //currentvecがあまりにも小さい場合無視する
                if (currentvec.sqrMagnitude >= 0.1f)
                {
                    linevec += currentvec; //ベクトル総和

                    //ベクトルの総和とcurrentvecの内積
                    var dot = Vector3.Dot(linevec.normalized, currentvec.normalized);

                    //内積がcornerを認識する
                    if (dot < 0.8f)
                    {　　//ベクトルの総和が小さい場合、角と認識しない
                        if (linevec.sqrMagnitude > 1f)
                        {
                            linevec = currentvec;
                            Debug.Log("Corner");
                            kado.Add(currentPosition);
                            cornercount += 1;

                        }
                    }

                }

            }


        }






        //図形認識（魔法認識）
        if (!(Input.GetMouseButton(0)))
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

                /*  Debug.Log(ang0);
                  Debug.Log(ang1);
                  Debug.Log(ang2);
                  Debug.Log(ang3);*/

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

            if (Vector3.Distance(c1, end) > 3)
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
        if (positionCount > 10)
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

                    if (Mathf.Abs(AveDistance - nextAveDistnce) < 0.6)
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