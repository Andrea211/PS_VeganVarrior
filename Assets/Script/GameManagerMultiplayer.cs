using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class GameManagerMultiplayer : MonoBehaviour, IPunObservable
{
    public static GameManagerMultiplayer Instance { set; get; }

    private PhotonView PV;

    public Text pointsPlayer1;
    public Text pointsPlayer2;

    private const float REQUIRED_SLICEFORCE = 200.0f;

    // vegetables prefabs
    public GameObject potatoPrefab1;
    public GameObject cabbagePrefab1;
    public GameObject eggplantPrefab1;
    public GameObject redbeanPrefab1;
    public GameObject beanPrefab1;
    public GameObject peaPrefab1;
    public GameObject vegetablePrefab1;
    public GameObject onionPrefab1;
    public GameObject maizePrefab1;
    public GameObject pumpkinPrefab1;

    public GameObject potatoPrefab2;
    public GameObject cabbagePrefab2;
    public GameObject eggplantPrefab2;
    public GameObject redbeanPrefab2;
    public GameObject beanPrefab2;
    public GameObject peaPrefab2;
    public GameObject vegetablePrefab2;
    public GameObject onionPrefab2;
    public GameObject maizePrefab2;
    public GameObject pumpkinPrefab2;

    public Transform trail;

    public bool isPaused;
    public Button escapeButton;

    // lists of vegetables
    private List<Potato1> potatoes1 = new List<Potato1>();
    private List<Cabbage1> cabbages1 = new List<Cabbage1>();
    private List<Eggplant1> eggplants1 = new List<Eggplant1>();
    private List<Redbean1> redbeans1 = new List<Redbean1>();
    private List<Bean1> beans1 = new List<Bean1>();
    private List<Pea1> peas1 = new List<Pea1>();
    private List<Vegetable1> veggies1 = new List<Vegetable1>();
    private List<Onion1> onions1 = new List<Onion1>();
    private List<Maize1> maizes1 = new List<Maize1>();
    private List<Pumpkin1> pumpkins1 = new List<Pumpkin1>();

    private List<Potato2> potatoes2 = new List<Potato2>();
    private List<Cabbage2> cabbages2 = new List<Cabbage2>();
    private List<Eggplant2> eggplants2 = new List<Eggplant2>();
    private List<Redbean2> redbeans2 = new List<Redbean2>();
    private List<Bean2> beans2 = new List<Bean2>();
    private List<Pea2> peas2 = new List<Pea2>();
    private List<Vegetable2> veggies2 = new List<Vegetable2>();
    private List<Onion2> onions2 = new List<Onion2>();
    private List<Maize2> maizes2 = new List<Maize2>();
    private List<Pumpkin2> pumpkins2 = new List<Pumpkin2>();

    private float lastSpawn;
    private float deltaSpawn = 1.0f;
    private Vector3 lastMousePos;

    private Collider2D[] potatoesCols1;
    private Collider2D[] cabbagesCols1;
    private Collider2D[] eggplantsCols1;
    private Collider2D[] redbeansCols1;
    private Collider2D[] beansCols1;
    private Collider2D[] peasCols1;
    private Collider2D[] veggiesCols1;
    private Collider2D[] onionsCols1;
    private Collider2D[] maizesCols1;
    private Collider2D[] pumpkinsCols1;

    private Collider2D[] potatoesCols2;
    private Collider2D[] cabbagesCols2;
    private Collider2D[] eggplantsCols2;
    private Collider2D[] redbeansCols2;
    private Collider2D[] beansCols2;
    private Collider2D[] peasCols2;
    private Collider2D[] veggiesCols2;
    private Collider2D[] onionsCols2;
    private Collider2D[] maizesCols2;
    private Collider2D[] pumpkinsCols2;

    // UI part of the game
    private int score1;
    private int score2;
    public Image[] lifepoints;
    public GameObject pauseMenu;
    public GameObject deathMenu;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        PhotonNetwork.SendRate = 20;

        veggiesCols1 = new Collider2D[0];
        NewGame();

        pointsPlayer1 = GameSetup.GS.pointsPlayer1;
        pointsPlayer2 = GameSetup.GS.pointsPlayer2;
        escapeButton = GameSetup.GS.escapeButton;
    }

    public void NewGame()
    {
        score1 = 0;
        score2 = 0;
        pauseMenu.SetActive(false);
        pointsPlayer1.text = score1.ToString();
        pointsPlayer2.text = score2.ToString();
        Time.timeScale = 1;
        isPaused = false;

        foreach(Image i in lifepoints)
        {
            i.enabled = true;
        }

        // for each potato
        foreach (Potato1 v in potatoes1)
        {
            Destroy(v.gameObject);
        }
        potatoes1.Clear();
        foreach (Potato2 v in potatoes2)
        {
            Destroy(v.gameObject);
        }
        potatoes2.Clear();

        // for each cabbage
        foreach (Cabbage1 v in cabbages1)
        {
            Destroy(v.gameObject);
        }
        cabbages1.Clear();
        foreach (Cabbage2 v in cabbages2)
        {
            Destroy(v.gameObject);
        }
        cabbages2.Clear();

        // for each eggplant
        foreach (Eggplant1 v in eggplants1)
        {
            Destroy(v.gameObject);
        }
        eggplants1.Clear();
        foreach (Eggplant2 v in eggplants2)
        {
            Destroy(v.gameObject);
        }
        eggplants2.Clear();

        // for each redbean
        foreach (Redbean1 v in redbeans1)
        {
            Destroy(v.gameObject);
        }
        redbeans1.Clear();
        foreach (Redbean2 v in redbeans2)
        {
            Destroy(v.gameObject);
        }
        redbeans2.Clear();

        // for each bean
        foreach (Bean1 v in beans1)
        {
            Destroy(v.gameObject);
        }
        beans1.Clear();
        foreach (Bean2 v in beans2)
        {
            Destroy(v.gameObject);
        }
        beans2.Clear();

        // for each pea
        foreach (Pea1 v in peas1)
        {
            Destroy(v.gameObject);
        }
        peas1.Clear();
        foreach (Pea2 v in peas2)
        {
            Destroy(v.gameObject);
        }
        peas2.Clear();

        // for each carrot
        foreach (Vegetable1 v in veggies1)
        {
            Destroy(v.gameObject);
        }
        veggies1.Clear();
        foreach (Vegetable2 v in veggies2)
        {
            Destroy(v.gameObject);
        }
        veggies2.Clear();

        // for each onion
        foreach (Onion1 v in onions1)
        {
            Destroy(v.gameObject);
        }
        onions1.Clear();
        foreach (Onion2 v in onions2)
        {
            Destroy(v.gameObject);
        }
        onions2.Clear();

        // for each maize
        foreach (Maize1 v in maizes1)
        {
            Destroy(v.gameObject);
        }
        maizes1.Clear();
        foreach (Maize2 v in maizes2)
        {
            Destroy(v.gameObject);
        }
        maizes2.Clear();
        
        // for each pumpkin
        foreach (Pumpkin1 v in pumpkins1)
        {
            Destroy(v.gameObject);
        }
        pumpkins1.Clear();
        foreach (Pumpkin2 v in pumpkins2)
        {
            Destroy(v.gameObject);
        }
        pumpkins2.Clear();

        deathMenu.SetActive(false);
    }

    private void Update()
    {
        if (isPaused)
            return;

        if(Time.time - lastSpawn > deltaSpawn)
        {
            // get vegetables
            Potato1 potato1 = GetPotato1();
            Cabbage1 cabbage1 = GetCabbage1();
            Eggplant1 eggplant1 = GetEggplant1();
            Redbean1 redbean1 = GetRedbean1();
            Bean1 bean1 = GetBean1();
            Pea1 pea1 = GetPea1();
            Vegetable1 v1 = GetVegetable1();
            Onion1 onion1 = GetOnion1();
            Maize1 maize1 = GetMaize1();
            Pumpkin1 pumpkin1 = GetPumpkin1();

            Potato2 potato2 = GetPotato2();
            Cabbage2 cabbage2 = GetCabbage2();
            Eggplant2 eggplant2 = GetEggplant2();
            Redbean2 redbean2 = GetRedbean2();
            Bean2 bean2 = GetBean2();
            Pea2 pea2 = GetPea2();
            Vegetable2 v2 = GetVegetable2();
            Onion2 onion2 = GetOnion2();
            Maize2 maize2 = GetMaize2();
            Pumpkin2 pumpkin2 = GetPumpkin2();

            // random seed
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            UnityEngine.Random.seed = unixTimestamp;

            float randomX = UnityEngine.Random.Range(-1.65f, 1.65f);
      
            //v.LaunchVegetable(Random.Range(2.5f, 3.5f),randomX, -randomX);
            //lastSpawn = Time.time;

            // randomize vegetables
            int randomVegetable = UnityEngine.Random.Range(0, 20);
            
            // launch potato
            if(!isPaused){
            if(randomVegetable == 0)
            {
                potato1.LaunchPotato1(UnityEngine.Random.Range(2.5f, 3.5f),randomX, -randomX);
                lastSpawn = Time.time;
            }
            if(randomVegetable == 1)
            {
                potato2.LaunchPotato2(UnityEngine.Random.Range(2.5f, 3.5f),randomX, -randomX);
                lastSpawn = Time.time;
            }

            // launch cabbage
            else if(randomVegetable == 2)
            {
                cabbage1.LaunchCabbage1(UnityEngine.Random.Range(2.5f, 3.5f),randomX, -randomX);
                lastSpawn = Time.time;
            }
            else if(randomVegetable == 3)
            {
                cabbage2.LaunchCabbage2(UnityEngine.Random.Range(2.5f, 3.5f),randomX, -randomX);
                lastSpawn = Time.time;
            }

            // launch eggplant
            else if (randomVegetable == 4)
            {
                eggplant1.LaunchEggplant1(UnityEngine.Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }
            else if (randomVegetable == 5)
            {
                eggplant2.LaunchEggplant2(UnityEngine.Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }

            // launch redbean
            else if (randomVegetable == 6)
            {
                redbean1.LaunchRedbean1(UnityEngine.Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }
            else if (randomVegetable == 7)
            {
                redbean2.LaunchRedbean2(UnityEngine.Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }

            // launch bean
            else if (randomVegetable == 8)
            {
                bean1.LaunchBean1(UnityEngine.Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }
            else if (randomVegetable == 9)
            {
                bean2.LaunchBean2(UnityEngine.Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }

            // launch pea
            else if (randomVegetable == 10)
            {
                pea1.LaunchPea1(UnityEngine.Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }
            else if (randomVegetable == 11)
            {
                pea2.LaunchPea2(UnityEngine.Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }

            // launch carrot
            else if (randomVegetable == 12)
            {
                v1.LaunchVegetable1(UnityEngine.Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }
            else if (randomVegetable == 13)
            {
                v2.LaunchVegetable2(UnityEngine.Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }

            // launch onion
            else if (randomVegetable == 14)
            {
                onion1.LaunchOnion1(UnityEngine.Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }
            else if (randomVegetable == 15)
            {
                onion2.LaunchOnion2(UnityEngine.Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }

            // launch maize
            else if (randomVegetable == 16)
            {
                maize1.LaunchMaize1(UnityEngine.Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }
            else if (randomVegetable == 17)
            {
                maize2.LaunchMaize2(UnityEngine.Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }

            // launch pumpkin
            else if (randomVegetable == 18)
            {
                pumpkin1.LaunchPumpkin1(UnityEngine.Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }
            else if (randomVegetable == 19)
            {
                pumpkin2.LaunchPumpkin2(UnityEngine.Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }
            }
        }

        if (Input.GetMouseButton(0))
        {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = -1;
                trail.position = pos;

                Collider2D[] thisFramesPotato1 = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Potato"));
                Collider2D[] thisFramesCabbage1 = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Cabbage"));
                Collider2D[] thisFramesEggplant1 = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Eggplant"));
                Collider2D[] thisFramesRedbean1 = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Redbean"));
                Collider2D[] thisFramesBean1 = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Bean"));
                Collider2D[] thisFramesPea1 = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Pea"));
                Collider2D[] thisFramesVegetable1 = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Vegetable"));
                Collider2D[] thisFramesOnion1 = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Onion"));
                Collider2D[] thisFramesMaize1 = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Maize"));
                Collider2D[] thisFramesPumpkin1 = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Pumpkin"));

                Collider2D[] thisFramesPotato2 = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Potato2"));
                Collider2D[] thisFramesCabbage2 = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Cabbage2"));
                Collider2D[] thisFramesEggplant2 = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Eggplant2"));
                Collider2D[] thisFramesRedbean2 = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Redbean2"));
                Collider2D[] thisFramesBean2 = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Bean2"));
                Collider2D[] thisFramesPea2 = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Pea2"));
                Collider2D[] thisFramesVegetable2 = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Vegetable2"));
                Collider2D[] thisFramesOnion2 = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Onion2"));
                Collider2D[] thisFramesMaize2 = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Maize2"));
                Collider2D[] thisFramesPumpkin2 = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Pumpkin2"));

            if ((Input.mousePosition - lastMousePos).sqrMagnitude > REQUIRED_SLICEFORCE)
                    
                    // potatoes
                    foreach (Collider2D c2 in thisFramesPotato1)
                    {
                        for (int i = 0; i < potatoesCols1.Length; i++)
                        {
                            if (c2 == potatoesCols1[i])
                            {
                                c2.GetComponent<Potato1>().Slice();
                                Debug.Log("potato1 sliced");

                                pointsPlayer1.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
                                pointsPlayer2.text = PhotonNetwork.PlayerList[1].GetScore().ToString();
                            }
                        }
                    }
                    foreach (Collider2D c2 in thisFramesPotato2)
                    {
                        for (int i = 0; i < potatoesCols2.Length; i++)
                        {
                            if (c2 == potatoesCols2[i])
                            {
                                c2.GetComponent<Potato2>().Slice();
                                Debug.Log("potato2 sliced");

                                pointsPlayer1.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
                                pointsPlayer2.text = PhotonNetwork.PlayerList[1].GetScore().ToString();
                            }
                        }
                    }

                    // cabbages
                    foreach (Collider2D c2 in thisFramesCabbage1)
                    {
                        for (int i = 0; i < cabbagesCols1.Length; i++)
                        {
                            if (c2 == cabbagesCols1[i])
                            {
                                c2.GetComponent<Cabbage1>().Slice();
                                Debug.Log("cabbage1 sliced");

                                pointsPlayer1.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
                                pointsPlayer2.text = PhotonNetwork.PlayerList[1].GetScore().ToString();
                            }
                        }
                    }
                    foreach (Collider2D c2 in thisFramesCabbage2)
                    {
                        for (int i = 0; i < cabbagesCols2.Length; i++)
                        {
                            if (c2 == cabbagesCols2[i])
                            {
                                c2.GetComponent<Cabbage2>().Slice();
                                Debug.Log("cabbage2 sliced");

                                pointsPlayer1.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
                                pointsPlayer2.text = PhotonNetwork.PlayerList[1].GetScore().ToString();
                            }
                        }
                    }

                    // eggplants
                    foreach (Collider2D c2 in thisFramesEggplant1)
                    {
                        for (int i = 0; i < eggplantsCols1.Length; i++)
                        {
                            if (c2 == eggplantsCols1[i])
                            {
                                c2.GetComponent<Eggplant1>().Slice();
                                Debug.Log("eggplant1 sliced");

                                pointsPlayer1.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
                                pointsPlayer2.text = PhotonNetwork.PlayerList[1].GetScore().ToString();
                            }
                        }
                    }
                    foreach (Collider2D c2 in thisFramesEggplant2)
                    {
                        for (int i = 0; i < eggplantsCols2.Length; i++)
                        {
                            if (c2 == eggplantsCols2[i])
                            {
                                c2.GetComponent<Eggplant2>().Slice();
                                Debug.Log("eggplant2 sliced");

                                pointsPlayer1.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
                                pointsPlayer2.text = PhotonNetwork.PlayerList[1].GetScore().ToString();
                            }
                        }
                    }

                    // redbeans
                    foreach (Collider2D c2 in thisFramesRedbean1)
                    {
                        for (int i = 0; i < redbeansCols1.Length; i++)
                        {
                            if (c2 == redbeansCols1[i])
                            {
                                c2.GetComponent<Redbean1>().Slice();
                                Debug.Log("redbean1 sliced");

                                pointsPlayer1.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
                                pointsPlayer2.text = PhotonNetwork.PlayerList[1].GetScore().ToString();
                            }
                        }
                    }
                    foreach (Collider2D c2 in thisFramesRedbean2)
                    {
                        for (int i = 0; i < redbeansCols2.Length; i++)
                        {
                            if (c2 == redbeansCols2[i])
                            {
                                c2.GetComponent<Redbean2>().Slice();
                                Debug.Log("redbean2 sliced");

                                pointsPlayer1.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
                                pointsPlayer2.text = PhotonNetwork.PlayerList[1].GetScore().ToString();
                            }
                        }
                    }

                    // beans
                    foreach (Collider2D c2 in thisFramesBean1)
                    {
                        for (int i = 0; i < beansCols1.Length; i++)
                        {
                            if (c2 == beansCols1[i])
                            {
                                c2.GetComponent<Bean1>().Slice();
                                Debug.Log("bean1 sliced");

                                //int currentScore1 = PhotonNetwork.LocalPlayer.GetScore();
                                //Debug.Log("Previous playerscore: " + currentScore1);
                                //PhotonNetwork.LocalPlayer.AddScore(2);

                                //int currentScore2 = PhotonNetwork.LocalPlayer.GetScore();
                                //Debug.Log("Current playerscore: " + currentScore2);

                                //PhotonNetwork.LocalPlayer.SetScore(currentScore2);
                                //Debug.Log("Score set");

                                pointsPlayer1.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
                                pointsPlayer2.text = PhotonNetwork.PlayerList[1].GetScore().ToString();
                            }
                        }
                    }
                    foreach (Collider2D c2 in thisFramesBean2)
                    {
                        for (int i = 0; i < beansCols2.Length; i++)
                        {
                            if (c2 == beansCols2[i])
                            {
                                c2.GetComponent<Bean2>().Slice();
                                Debug.Log("bean2 sliced");

                                pointsPlayer1.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
                                pointsPlayer2.text = PhotonNetwork.PlayerList[1].GetScore().ToString();
                            }
                        }
                    }

                    // peas
                    foreach (Collider2D c2 in thisFramesPea1)
                    {
                        for (int i = 0; i < peasCols1.Length; i++)
                        {
                            if (c2 == peasCols1[i])
                            {
                                c2.GetComponent<Pea1>().Slice();
                                Debug.Log("pea1 sliced");

                                pointsPlayer1.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
                                pointsPlayer2.text = PhotonNetwork.PlayerList[1].GetScore().ToString();
                            }
                        }
                    }
                    foreach (Collider2D c2 in thisFramesPea2)
                    {
                        for (int i = 0; i < peasCols2.Length; i++)
                        {
                            if (c2 == peasCols2[i])
                            {
                                c2.GetComponent<Pea2>().Slice();
                                Debug.Log("pea2 sliced");

                                pointsPlayer1.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
                                pointsPlayer2.text = PhotonNetwork.PlayerList[1].GetScore().ToString();
                            }
                        }
                    }

                    // carrots
                    foreach (Collider2D c2 in thisFramesVegetable1)
                    {
                        for (int i = 0; i < veggiesCols1.Length; i++)
                        {
                            if (c2 == veggiesCols1[i])
                            {
                                c2.GetComponent<Vegetable1>().Slice();
                                Debug.Log("carrot1 sliced");

                                pointsPlayer1.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
                                pointsPlayer2.text = PhotonNetwork.PlayerList[1].GetScore().ToString();
                            }
                        }
                    }
                    foreach (Collider2D c2 in thisFramesVegetable2)
                    {
                        for (int i = 0; i < veggiesCols2.Length; i++)
                        {
                            if (c2 == veggiesCols2[i])
                            {
                                c2.GetComponent<Vegetable2>().Slice();
                                Debug.Log("carrot2 sliced");

                                pointsPlayer1.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
                                pointsPlayer2.text = PhotonNetwork.PlayerList[1].GetScore().ToString();
                            }
                        }
                    }

                    // onions
                    foreach (Collider2D c2 in thisFramesOnion1)
                    {
                        for (int i = 0; i < onionsCols1.Length; i++)
                        {
                            if (c2 == onionsCols1[i])
                            {
                                c2.GetComponent<Onion1>().Slice();
                                Debug.Log("onion1 sliced");

                                pointsPlayer1.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
                                pointsPlayer2.text = PhotonNetwork.PlayerList[1].GetScore().ToString();
                            }
                        }
                    }
                    foreach (Collider2D c2 in thisFramesOnion2)
                    {
                        for (int i = 0; i < onionsCols2.Length; i++)
                        {
                            if (c2 == onionsCols2[i])
                            {
                                c2.GetComponent<Onion2>().Slice();
                                Debug.Log("onion2 sliced");

                                pointsPlayer1.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
                                pointsPlayer2.text = PhotonNetwork.PlayerList[1].GetScore().ToString();
                            }
                        }
                    }

                    // maizes
                    foreach (Collider2D c2 in thisFramesMaize1)
                    {
                        for (int i = 0; i < maizesCols1.Length; i++)
                        {
                            if (c2 == maizesCols1[i])
                            {
                                c2.GetComponent<Maize1>().Slice();
                                Debug.Log("maize1 sliced");

                                pointsPlayer1.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
                                pointsPlayer2.text = PhotonNetwork.PlayerList[1].GetScore().ToString();
                            }
                        }
                    }
                    foreach (Collider2D c2 in thisFramesMaize2)
                    {
                        for (int i = 0; i < maizesCols2.Length; i++)
                        {
                            if (c2 == maizesCols2[i])
                            {
                                c2.GetComponent<Maize2>().Slice();
                                Debug.Log("maize2 sliced");

                                pointsPlayer1.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
                                pointsPlayer2.text = PhotonNetwork.PlayerList[1].GetScore().ToString();
                            }
                        }
                    }

                    // pumpkins
                    foreach (Collider2D c2 in thisFramesPumpkin1)
                    {
                        for (int i = 0; i < pumpkinsCols1.Length; i++)
                        {
                            if (c2 == pumpkinsCols1[i])
                            {
                                c2.GetComponent<Pumpkin1>().Slice();
                                Debug.Log("pumpkin1 sliced");

                                pointsPlayer1.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
                                pointsPlayer2.text = PhotonNetwork.PlayerList[1].GetScore().ToString();
                            }
                        }
                    }
                    foreach (Collider2D c2 in thisFramesPumpkin2)
                    {
                        for (int i = 0; i < pumpkinsCols2.Length; i++)
                        {
                            if (c2 == pumpkinsCols2[i])
                            {
                                c2.GetComponent<Pumpkin2>().Slice();
                                Debug.Log("pumpkin2 sliced");

                                pointsPlayer1.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
                                pointsPlayer2.text = PhotonNetwork.PlayerList[1].GetScore().ToString();
                            }
                        }
                    }

            lastMousePos = Input.mousePosition;

            potatoesCols1 = thisFramesPotato1;
            cabbagesCols1 = thisFramesCabbage1;
            eggplantsCols1 = thisFramesEggplant1;
            redbeansCols1 = thisFramesRedbean1;
            beansCols1 = thisFramesBean1;
            peasCols1 = thisFramesPea1;
            veggiesCols1 = thisFramesVegetable1;
            onionsCols1 = thisFramesOnion1;
            maizesCols1 = thisFramesMaize1;
            pumpkinsCols1 = thisFramesPumpkin1;

            potatoesCols2 = thisFramesPotato2;
            cabbagesCols2 = thisFramesCabbage2;
            eggplantsCols2 = thisFramesEggplant2;
            redbeansCols2 = thisFramesRedbean2;
            beansCols2 = thisFramesBean2;
            peasCols2 = thisFramesPea2;
            veggiesCols2 = thisFramesVegetable2;
            onionsCols2 = thisFramesOnion2;
            maizesCols2 = thisFramesMaize2;
            pumpkinsCols2 = thisFramesPumpkin2;
        }
    }

    // potatoes
    [PunRPC]
    private Potato1 GetPotato1(){
        Potato1 v = potatoes1.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(potatoPrefab1).GetComponent<Potato1>();
            potatoes1.Add(v);
        }

        return v;
    }
    [PunRPC]
    private Potato2 GetPotato2(){
        Potato2 v = potatoes2.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(potatoPrefab2).GetComponent<Potato2>();
            potatoes2.Add(v);
        }

        return v;
    }

    // cabbages
    [PunRPC]
    private Cabbage1 GetCabbage1(){
        Cabbage1 v = cabbages1.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(cabbagePrefab1).GetComponent<Cabbage1>();
            cabbages1.Add(v);
        }

        return v;
    }
    [PunRPC]
    private Cabbage2 GetCabbage2(){
        Cabbage2 v = cabbages2.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(cabbagePrefab2).GetComponent<Cabbage2>();
            cabbages2.Add(v);
        }

        return v;
    }

    // eggplants
    [PunRPC]
    private Eggplant1 GetEggplant1(){
        Eggplant1 v = eggplants1.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(eggplantPrefab1).GetComponent<Eggplant1>();
            eggplants1.Add(v);
        }

        return v;
    }
    [PunRPC]
    private Eggplant2 GetEggplant2(){
        Eggplant2 v = eggplants2.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(eggplantPrefab2).GetComponent<Eggplant2>();
            eggplants2.Add(v);
        }

        return v;
    }

    // redbeans
    [PunRPC]
    private Redbean1 GetRedbean1(){
        Redbean1 v = redbeans1.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(redbeanPrefab1).GetComponent<Redbean1>();
            redbeans1.Add(v);
        }

        return v;
    }
    [PunRPC]
    private Redbean2 GetRedbean2(){
        Redbean2 v = redbeans2.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(redbeanPrefab2).GetComponent<Redbean2>();
            redbeans2.Add(v);
        }

        return v;
    }

    // beans
    [PunRPC]
    private Bean1 GetBean1(){
        Bean1 v = beans1.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(beanPrefab1).GetComponent<Bean1>();
            beans1.Add(v);
        }

        return v;
    }
    [PunRPC]
    private Bean2 GetBean2(){
        Bean2 v = beans2.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(beanPrefab2).GetComponent<Bean2>();
            beans2.Add(v);
        }

        return v;
    }

    // peas
    [PunRPC]
    private Pea1 GetPea1(){
        Pea1 v = peas1.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(peaPrefab1).GetComponent<Pea1>();
            peas1.Add(v);
        }

        return v;
    }
    [PunRPC]
    private Pea2 GetPea2(){
        Pea2 v = peas2.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(peaPrefab2).GetComponent<Pea2>();
            peas2.Add(v);
        }

        return v;
    }

    // carrots
    [PunRPC]
    private Vegetable1 GetVegetable1(){
        Vegetable1 v = veggies1.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(vegetablePrefab1).GetComponent<Vegetable1>();
            veggies1.Add(v);
        }

        return v;
    }
    [PunRPC]
    private Vegetable2 GetVegetable2(){
        Vegetable2 v = veggies2.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(vegetablePrefab2).GetComponent<Vegetable2>();
            veggies2.Add(v);
        }

        return v;
    }

    // onions
    [PunRPC]
    private Onion1 GetOnion1(){
        Onion1 v = onions1.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(onionPrefab1).GetComponent<Onion1>();
            onions1.Add(v);
        }

        return v;
    }
    [PunRPC]
    private Onion2 GetOnion2(){
        Onion2 v = onions2.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(onionPrefab2).GetComponent<Onion2>();
            onions2.Add(v);
        }

        return v;
    }

    // maizes
    [PunRPC]
    private Maize1 GetMaize1(){
        Maize1 v = maizes1.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(maizePrefab1).GetComponent<Maize1>();
            maizes1.Add(v);
        }

        return v;
    }
    [PunRPC]
    private Maize2 GetMaize2(){
        Maize2 v = maizes2.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(maizePrefab2).GetComponent<Maize2>();
            maizes2.Add(v);
        }

        return v;
    }
    
    // pumpkins
    [PunRPC]
    private Pumpkin1 GetPumpkin1(){
        Pumpkin1 v = pumpkins1.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(pumpkinPrefab1).GetComponent<Pumpkin1>();
            pumpkins1.Add(v);
        }

        return v;
    }
    [PunRPC]
    private Pumpkin2 GetPumpkin2(){
        Pumpkin2 v = pumpkins2.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(pumpkinPrefab2).GetComponent<Pumpkin2>();
            pumpkins2.Add(v);
        }

        return v;
    }

    [PunRPC]
    public void IncrementScore1(int scoreAmount)
    {
        score1 += scoreAmount;
        pointsPlayer1.text = score1.ToString();
    }

    [PunRPC]
    public void IncrementScore2(int scoreAmount)
    {
        score2 += scoreAmount;
        pointsPlayer2.text = score2.ToString();
    }

    public void DecrementScore1(int scoreAmount)
    {
        score1 -= scoreAmount;
        pointsPlayer1.text = score1.ToString();
    }

    public void DecrementScore2(int scoreAmount)
    {
        score2 -= scoreAmount;
        pointsPlayer2.text = score2.ToString();
    }

    public void PauseGame()
    {
        PhotonRoom.isGameLoaded = false;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        isPaused = pauseMenu.activeSelf;
        Time.timeScale = (Time.timeScale==0) ? 1 : 0;
    }

    public void ToMenu()
    {
        // disconnect when going back to menu
        PhotonNetwork.LeaveRoom();
        Debug.Log("Left the room");
        SceneManager.LoadScene("Menu");
    }

    public void Multiplayer()
    {
        SceneManager.LoadScene("Connection");
    }

    public static String GetTimestamp(DateTime value)
{
    return value.ToString("yyyyMMddHHmmssffff");
}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        
        Debug.Log("We're in OnPhotonSerializeView if statement");
        
        if(stream.IsWriting)
        {
            stream.SendNext(isPaused);
            Debug.Log("Is the game paused? " + isPaused);
        }
        else 
        {
            Debug.Log("We're in OnPhotonSerializeView else statement");
            isPaused = (bool) stream.ReceiveNext();
        }
    }

}
