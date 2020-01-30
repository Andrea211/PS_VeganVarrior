using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { set; get; }

    private const float REQUIRED_SLICEFORCE = 200.0f;

    // vegetables prefabs
    public GameObject potatoPrefab;
    public GameObject cabbagePrefab;
    public GameObject eggplantPrefab;
    public GameObject redbeanPrefab;
    public GameObject beanPrefab;
    public GameObject peaPrefab;
    public GameObject vegetablePrefab;
    public GameObject onionPrefab;
    public GameObject maizePrefab;
    public GameObject pumpkinPrefab;

    public Transform trail;

    private bool isPaused;

    // lists of vegetables
    private List<Potato> potatoes = new List<Potato>();
    private List<Cabbage> cabbages = new List<Cabbage>();
    private List<Eggplant> eggplants = new List<Eggplant>();
    private List<Redbean> redbeans = new List<Redbean>();
    private List<Bean> beans = new List<Bean>();
    private List<Pea> peas = new List<Pea>();
    private List<Vegetable> veggies = new List<Vegetable>();
    private List<Onion> onions = new List<Onion>();
    private List<Maize> maizes = new List<Maize>();
    private List<Pumpkin> pumpkins = new List<Pumpkin>();

    private float lastSpawn;
    private float deltaSpawn = 1.0f;
    private Vector3 lastMousePos;

    private Collider2D[] potatoesCols;
    private Collider2D[] cabbagesCols;
    private Collider2D[] eggplantsCols;
    private Collider2D[] redbeansCols;
    private Collider2D[] beansCols;
    private Collider2D[] peasCols;
    private Collider2D[] veggiesCols;
    private Collider2D[] onionsCols;
    private Collider2D[] maizesCols;
    private Collider2D[] pumpkinsCols;

    // UI part of the game
    private int score;
    private int highscore;
    private int lifepoint;
    public Text scoreText;
    public Text highscoreText;
    public Image[] lifepoints;
    public GameObject pauseMenu;
    public GameObject deathMenu;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        veggiesCols = new Collider2D[0];
        NewGame();
    }

    public void NewGame()
    {
        score = 0;
        lifepoint = 3;
        pauseMenu.SetActive(false);
        scoreText.text = score.ToString();
        highscore = PlayerPrefs.GetInt("Score");
        highscoreText.text = "BEST: " + highscore.ToString();
        Time.timeScale = 1;
        isPaused = false;

        foreach(Image i in lifepoints)
        {
            i.enabled = true;
        }

        // for each potato
        foreach (Potato v in potatoes)
        {
            Destroy(v.gameObject);
        }
        potatoes.Clear();

        // for each cabbage
        foreach (Cabbage v in cabbages)
        {
            Destroy(v.gameObject);
        }
        cabbages.Clear();

        // for each eggplant
        foreach (Eggplant v in eggplants)
        {
            Destroy(v.gameObject);
        }
        eggplants.Clear();

        // for each redbean
        foreach (Redbean v in redbeans)
        {
            Destroy(v.gameObject);
        }
        redbeans.Clear();

        // for each bean
        foreach (Bean v in beans)
        {
            Destroy(v.gameObject);
        }
        beans.Clear();

        // for each pea
        foreach (Pea v in peas)
        {
            Destroy(v.gameObject);
        }
        peas.Clear();

        // for each carrot
        foreach (Vegetable v in veggies)
        {
            Destroy(v.gameObject);
        }
        veggies.Clear();

        // for each onion
        foreach (Onion v in onions)
        {
            Destroy(v.gameObject);
        }
        onions.Clear();

        // for each maize
        foreach (Maize v in maizes)
        {
            Destroy(v.gameObject);
        }
        maizes.Clear();
        
        // for each pumpkin
        foreach (Pumpkin v in pumpkins)
        {
            Destroy(v.gameObject);
        }
        pumpkins.Clear();

        deathMenu.SetActive(false);
    }

    private void Update()
    {
        if (isPaused)
            return;

        if(Time.time - lastSpawn > deltaSpawn)
        {
            // get vegetables
            Potato potato = GetPotato();
            Cabbage cabbage = GetCabbage();
            Eggplant eggplant = GetEggplant();
            Redbean redbean = GetRedbean();
            Bean bean = GetBean();
            Pea pea = GetPea();
            Vegetable v = GetVegetable();
            Onion onion = GetOnion();
            Maize maize = GetMaize();
            Pumpkin pumpkin = GetPumpkin();

            float randomX = Random.Range(-1.65f, 1.65f);
      
            //v.LaunchVegetable(Random.Range(2.5f, 3.5f),randomX, -randomX);
            //lastSpawn = Time.time;

            // randomize vegetables
            int randomVegetable = Random.Range(0, 10);
            if(randomVegetable == 0)
            {
                potato.LaunchPotato(Random.Range(2.5f, 3.5f),randomX, -randomX);
                lastSpawn = Time.time;
            }
            else if(randomVegetable == 1)
            {
                cabbage.LaunchCabbage(Random.Range(2.5f, 3.5f),randomX, -randomX);
                lastSpawn = Time.time;
            }
            else if (randomVegetable == 2)
            {
                eggplant.LaunchEggplant(Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }
            else if (randomVegetable == 3)
            {
                redbean.LaunchRedbean(Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }
            else if (randomVegetable == 4)
            {
                bean.LaunchBean(Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }
            else if (randomVegetable == 5)
            {
                pea.LaunchPea(Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }
            else if (randomVegetable == 6)
            {
                v.LaunchVegetable(Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }
            else if (randomVegetable == 7)
            {
                onion.LaunchOnion(Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }
            else if (randomVegetable == 8)
            {
                maize.LaunchMaize(Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }
            else if (randomVegetable == 9)
            {
                pumpkin.LaunchPumpkin(Random.Range(2.5f, 3.5f), randomX, -randomX);
                lastSpawn = Time.time;
            }
        }

        if (Input.GetMouseButton(0))
        {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = -1;
                trail.position = pos;

                Collider2D[] thisFramesPotato = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Potato"));
                Collider2D[] thisFramesCabbage = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Cabbage"));
                Collider2D[] thisFramesEggplant = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Eggplant"));
                Collider2D[] thisFramesRedbean = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Redbean"));
                Collider2D[] thisFramesBean = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Bean"));
                Collider2D[] thisFramesPea = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Pea"));
                Collider2D[] thisFramesVegetable = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Vegetable"));
                Collider2D[] thisFramesOnion = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Onion"));
                Collider2D[] thisFramesMaize = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Maize"));
                Collider2D[] thisFramesPumpkin = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Pumpkin"));

            if ((Input.mousePosition - lastMousePos).sqrMagnitude > REQUIRED_SLICEFORCE)
                    foreach (Collider2D c2 in thisFramesPotato)
                    {
                        for (int i = 0; i < potatoesCols.Length; i++)
                        {
                            if (c2 == potatoesCols[i])
                            {
                                c2.GetComponent<Potato>().Slice();
                                Debug.Log("potato sliced");
                            }
                        }
                    }
                    foreach (Collider2D c2 in thisFramesCabbage)
                    {
                        for (int i = 0; i < cabbagesCols.Length; i++)
                        {
                            if (c2 == cabbagesCols[i])
                            {
                                c2.GetComponent<Cabbage>().Slice();
                                Debug.Log("cabbage sliced");
                            }
                        }
                    }
                    foreach (Collider2D c2 in thisFramesEggplant)
                    {
                        for (int i = 0; i < eggplantsCols.Length; i++)
                        {
                            if (c2 == eggplantsCols[i])
                            {
                                c2.GetComponent<Eggplant>().Slice();
                                Debug.Log("eggplant sliced");
                            }
                        }
                    }
                    foreach (Collider2D c2 in thisFramesRedbean)
                    {
                        for (int i = 0; i < redbeansCols.Length; i++)
                        {
                            if (c2 == redbeansCols[i])
                            {
                                c2.GetComponent<Redbean>().Slice();
                                Debug.Log("redbean sliced");
                            }
                        }
                    }
                    foreach (Collider2D c2 in thisFramesBean)
                    {
                        for (int i = 0; i < beansCols.Length; i++)
                        {
                            if (c2 == beansCols[i])
                            {
                                c2.GetComponent<Bean>().Slice();
                                Debug.Log("bean sliced");
                            }
                        }
                    }
                    foreach (Collider2D c2 in thisFramesPea)
                    {
                        for (int i = 0; i < peasCols.Length; i++)
                        {
                            if (c2 == peasCols[i])
                            {
                                c2.GetComponent<Pea>().Slice();
                                Debug.Log("pea sliced");
                            }
                        }
                    }
                    foreach (Collider2D c2 in thisFramesVegetable)
                    {
                        for (int i = 0; i < veggiesCols.Length; i++)
                        {
                            if (c2 == veggiesCols[i])
                            {
                                c2.GetComponent<Vegetable>().Slice();
                                Debug.Log("carrot sliced");
                            }
                        }
                    }
                    foreach (Collider2D c2 in thisFramesOnion)
                    {
                        for (int i = 0; i < onionsCols.Length; i++)
                        {
                            if (c2 == onionsCols[i])
                            {
                                c2.GetComponent<Onion>().Slice();
                                Debug.Log("onion sliced");
                            }
                        }
                    }
                    foreach (Collider2D c2 in thisFramesMaize)
                    {
                        for (int i = 0; i < maizesCols.Length; i++)
                        {
                            if (c2 == maizesCols[i])
                            {
                                c2.GetComponent<Maize>().Slice();
                                Debug.Log("maize sliced");
                            }
                        }
                    }
                    foreach (Collider2D c2 in thisFramesPumpkin)
                    {
                        for (int i = 0; i < pumpkinsCols.Length; i++)
                        {
                            if (c2 == pumpkinsCols[i])
                            {
                                c2.GetComponent<Pumpkin>().Slice();
                                Debug.Log("pumpkin sliced");
                            }
                        }
                    }

            lastMousePos = Input.mousePosition;
            potatoesCols = thisFramesPotato;
            cabbagesCols = thisFramesCabbage;
            eggplantsCols = thisFramesEggplant;
            redbeansCols = thisFramesRedbean;
            beansCols = thisFramesBean;
            peasCols = thisFramesPea;
            veggiesCols = thisFramesVegetable;
            onionsCols = thisFramesOnion;
            maizesCols = thisFramesMaize;
            pumpkinsCols = thisFramesPumpkin;
        }
    }

    private Potato GetPotato()
    {
        Potato v = potatoes.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(potatoPrefab).GetComponent<Potato>();
            potatoes.Add(v);
        }

        return v;
    }

    private Cabbage GetCabbage()
    {
        Cabbage v = cabbages.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(cabbagePrefab).GetComponent<Cabbage>();
            cabbages.Add(v);
        }

        return v;
    }

    private Eggplant GetEggplant()
    {
        Eggplant v = eggplants.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(eggplantPrefab).GetComponent<Eggplant>();
            eggplants.Add(v);
        }

        return v;
    }

    private Redbean GetRedbean()
    {
        Redbean v = redbeans.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(redbeanPrefab).GetComponent<Redbean>();
            redbeans.Add(v);
        }

        return v;
    }

    private Bean GetBean()
    {
        Bean v = beans.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(beanPrefab).GetComponent<Bean>();
            beans.Add(v);
        }

        return v;
    }

    private Pea GetPea()
    {
        Pea v = peas.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(peaPrefab).GetComponent<Pea>();
            peas.Add(v);
        }

        return v;
    }

    private Vegetable GetVegetable()
    {
        Vegetable v = veggies.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(vegetablePrefab).GetComponent<Vegetable>();
            veggies.Add(v);
        }

        return v;
    }

    private Onion GetOnion()
    {
        Onion v = onions.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(onionPrefab).GetComponent<Onion>();
            onions.Add(v);
        }

        return v;
    }

    private Maize GetMaize()
    {
        Maize v = maizes.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(maizePrefab).GetComponent<Maize>();
            maizes.Add(v);
        }

        return v;
    }
    
    private Pumpkin GetPumpkin()
    {
        Pumpkin v = pumpkins.Find(x => !x.IsActive);

        if (v == null)
        {
            v = Instantiate(pumpkinPrefab).GetComponent<Pumpkin>();
            pumpkins.Add(v);
        }

        return v;
    }

    public void IncrementScore(int scoreAmount)
    {
        score += scoreAmount;
        scoreText.text = score.ToString();

        if(score > highscore)
        {
            highscore = score;
            highscoreText.text = "BEST: " + highscore.ToString();
            PlayerPrefs.SetInt("Score", highscore);
        }
    }

    public void LoseLP()
    {
        if(lifepoint == 0)
        {
            return;
        }
        lifepoint --;
        lifepoints[lifepoint].enabled = false;
        if(lifepoint == 0)
        {
            Death();
        }
    }

    public void Death()
    {
        isPaused = true;
        deathMenu.SetActive(true);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        isPaused = pauseMenu.activeSelf;
        Time.timeScale = (Time.timeScale==0) ? 1 : 0;
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void Multiplayer()
    {
        SceneManager.LoadScene("Networking");
    }

}
