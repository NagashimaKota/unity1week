using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CnControls;

public class player : MonoBehaviour {

    public GameObject bullet;
    public int count = 0;
    public int damege = 0;
    public int enemy_level = 0;
    public Text text,text2;
    public Text[] point;  //エンディングのスコア
    public ParticleSystem gun;
    public GameObject enemy;
    public GameObject title;
    public AudioSource gunshot;
    public AudioSource title_bgm;
    public GameObject endPanel;
    public GameObject score;

    private Touch touchOn;
    private float time_ui = 60;  // 制限時間
    private float time = 2;      // 弾のディレイのための変数
    private float hibullet_tap = 0;
    private float speed = 100f;
    private float horizontal, hori_tap; // キー、スマホの入力用
    private float[] rote = { 60, 300};  // 回転制限(90,270) だとわかりやすい

	void Start ()
    {
        
    }
	
	
	void Update ()
    {
        horizontal = Input.GetAxis("Horizontal");
        hori_tap = CnInputManager.GetAxis("Horizontal");
        
        this.GetComponent<Transform>().Rotate(0, 0, -horizontal * 5 + (-hori_tap * 5));
        
        if (rote[0] < this.transform.rotation.eulerAngles.z && this.transform.rotation.eulerAngles.z < rote[1])
        {
            this.GetComponent<Transform>().Rotate(0, 0, horizontal * 5 + (hori_tap * 5));
        }

        if (touchOn.tapCount > 0)
        {
            OffTitle();
        }
        if (touchOn.phase == TouchPhase.Stationary)
        {
            hibullet_tap += Time.deltaTime;
        }
        else
        {
            hibullet_tap = 0;
        }

        // 弾の発射処理
        if ((Input.GetKeyUp(KeyCode.Space) || touchOn.phase == TouchPhase.Ended) && time > 0.6f)
        {
            GameObject bul = Instantiate(bullet);
            float direction = (this.transform.rotation.eulerAngles.z / 180 ) * Mathf.PI;
            
            bul.GetComponent<Transform>().position = new Vector3(-Mathf.Sin(direction), -5.25f + Mathf.Cos(direction), 0);
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || hibullet_tap < 0.5f)
            {
                bul.GetComponent<Rigidbody>().AddForce(-Mathf.Sin(direction) * speed * 2, Mathf.Cos(direction) * speed * 2, 0);
            }
            else
            {
                bul.GetComponent<Rigidbody>().AddForce(-Mathf.Sin(direction) * speed, Mathf.Cos(direction) * speed, 0);
            }

            gun.Play();
            gunshot.Play();
            time = 0;
            
        }
        if (enemy.active)
        {
            time += Time.deltaTime;
            time_ui -= Time.deltaTime;
            text.text = "撃墜数:" + count.ToString();
            text2.text = "時間:" + time_ui.ToString("N0");
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            OffTitle();
        }

        // GameEnd の処理
        if (time_ui < 0)
        {
            enemy.SetActive(false);
            endPanel.SetActive(true);

            point[0].text = count.ToString();
            point[1].text = damege.ToString();
            point[2].text = ( enemy_level + (count - damege) * 10).ToString();

            GameObject[] enemys = GameObject.FindGameObjectsWithTag("enemy");
            foreach (GameObject en in enemys)
            {
                Destroy(en);
            }
        }
    }


    public void OffTitle()
    {
        title.SetActive(false);
        score.SetActive(true);
        enemy.SetActive(true);
        title_bgm.Stop();
    }

    public void OnTitle()
    {
        title.SetActive(true);
        score.SetActive(false);
        enemy.SetActive(false);
        endPanel.SetActive(false);
        count = 0;
        time = 0;
        time_ui = 60;
        enemy_level = 0;
        title_bgm.Play();
    }
}
