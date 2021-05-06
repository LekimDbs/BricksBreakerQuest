using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BallSpawner : MonoBehaviour
{
    private RaycastHit2D ray;
    private float angle;
    public GameObject Ball;
    public static int BallCount=52;
    public TextMeshPro tmp;
    public GameObject SpeedUpParent;
    public float Force;
    public int layerMask;
    public GameObject TargetParent;
    //public bool moving;
    public GameObject BallSprite;
    public List<GameObject> list = new List<GameObject>();
    private bool stop;
    public GameObject FirstBall;
    public GameObject Reset;
    public Slider slider;
    private bool PointerDown;
    public float angleMin;
    public float angleMax;

    private void Start()
    {
        tmp.text = BallCount + "x";
        layerMask = (LayerMask.GetMask("Wall"));
    }

    private void FixedUpdate()
    {
        
        if (Wall.Shooting == false)
        {
            if (PointerDown == true)
            {
                if (BallSprite.activeSelf == false)
                {
                    BallSprite.SetActive(true);
                }
                ray = Physics2D.Raycast(gameObject.transform.position, transform.right, 12f, layerMask);
                Debug.DrawRay(gameObject.transform.position, transform.right * ray.distance, Color.red);
                BallSprite.transform.position = ray.point;
                Vector2 poss = Vector2.Reflect(new Vector3(ray.point.x, ray.point.y) - this.transform.position, ray.normal);
                DottedLine.DottedLine.Instance.DrawDottedLine(gameObject.transform.position, ray.point);
                DottedLine.DottedLine.Instance.DrawDottedLine(ray.point, ray.point + poss.normalized * 2);

                Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
                Vector3 dir = Input.mousePosition - pos;
                angle = 180f-slider.value;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    
                    ray = Physics2D.Raycast(gameObject.transform.position, transform.right, 12f, layerMask);
                    Debug.DrawRay(gameObject.transform.position, transform.right * ray.distance, Color.red);
                    
                    Vector2 poss = Vector2.Reflect(new Vector3(ray.point.x, ray.point.y) - this.transform.position, ray.normal);

                    BallSprite.transform.position = ray.point;
                    Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
                    Vector3 dir = Input.mousePosition - pos;
                    angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    if (angle >= angleMin && angle <= angleMax)
                    {
                        DottedLine.DottedLine.Instance.DrawDottedLine(gameObject.transform.position, ray.point);
                        DottedLine.DottedLine.Instance.DrawDottedLine(ray.point, ray.point + poss.normalized * 2);
                        if (BallSprite.activeSelf == false)
                        {
                            BallSprite.SetActive(true);
                        }
                    }
                    else
                    {
                        if (BallSprite.activeSelf == true)
                        {
                            BallSprite.SetActive(false);
                        }
                    }
                    
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
        }
    }

    public void PointerD()
    {
        PointerDown = true;
    }
    public void PointerUp()
    {
        PointerDown = false;
    }

    void Update()
    {
        
        if (BallMoveto.firstHit == true)
        {
            if (FirstBall.activeSelf == false)
            {
                FirstBall.SetActive(true);

            }
        }
       

        if (Wall.Shooting == false)
        {
            
          if (Input.GetMouseButtonUp(0))
            {
                BallSprite.SetActive(false);
                if (angle >= angleMin && angle <= angleMax)
                {
                    if (Wall.firstHit == true)
                    {
                        Wall.firstHit = false;
                    }
                    
                    StartCoroutine(ShootBall());
                    
                }
                else
                {
                    transform.rotation = Quaternion.identity;
                    
                }
            }
        }
        /*if (moving == true)
        {
            GetDown();
        }*/
    }

    

    IEnumerator ShootBall()
    {
        slider.value = 90;
        slider.transform.parent.gameObject.SetActive(false);
        Reset.SetActive(true);
        TargetParent.GetComponent<GetDown>().newPos = new Vector2(TargetParent.transform.position.x,TargetParent.transform.position.y-0.75f);
        StartCoroutine(SpeedUp());
        Wall.Shooting = true;
        tmp.text = "";
        TriggerScript.onRight = false;
        stop = false;
        list.Clear();
        FirstBall.SetActive(false);
        for (int i = 0; i < BallCount; i++)
        {
            
            yield return new WaitForSeconds(0.08f);
            if (stop == true)
            {
                break;
            }
            GameObject myinst = Instantiate(Ball, gameObject.transform.position, Quaternion.identity,gameObject.transform);
            list.Add(myinst);
            myinst.GetComponent<Rigidbody2D>().AddForce(transform.right * Force );
            
        }
        }

    IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(4.6f);

        if (Wall.Shooting == true)
        {
            SpeedUpParent.SetActive(true);
            Time.timeScale = 1.6f;
        }

    }
    public void SpeedupOff()
    {
        SpeedUpParent.SetActive(false);
        Time.timeScale = 1;
    }

    /*public void GetDown()
    {
        float step = 10f * Time.deltaTime;
        Vector2 newpos = Vector2.MoveTowards(TargetParent.transform.position, new Vector2(TargetParent.transform.position.x, TargetParent.transform.position.y - 0.75f),step);
        TargetParent.transform.position = newpos;
        if (Vector2.Distance(TargetParent.transform.position, newpos) < 0.00001f) {
            moving = false;
            Wall.Shooting = false;
        }
    }*/
    public void ResetBalls()
    {
        Wall.firstHit = true;
        stop = true;
        foreach (GameObject go in list)
        {
            if (go != null)
            {
                go.GetComponent<CircleCollider2D>().enabled = false;
                go.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                go.GetComponent<BallMoveto>().Move = true;
            }
         }
        //tmp.text = BallCount+"x";
    }
   
}
