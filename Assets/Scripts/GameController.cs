using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public int MA_len = 24;


    // 所有的Prefab，仅供生成物品用
    public GameObject MAPlatformPrefab;
    public GameObject MMPlatformPrefab;
    public GameObject CPlatformPrefab;
    public GameObject Text_MA_bit_Prefab;
    public GameObject Text_MM_wordPrefix_Prefab;
    public GameObject Text_MM_ellipsisDot_Prefab;
    public GameObject Text_C_ellipsisDot_Prefab;
    public GameObject Text_MM_brace_Prefab;
    public GameObject Text_C_brace_Prefab;
    public GameObject Text_MM_blockPrefix_Prefab;
    public GameObject Text_C_linePrefix_Prefab;
    public GameObject Text_MA_wordDelimiter_Prefab;
    public GameObject Text_MA_block_Prefab;
    public GameObject Text_MA_word_Prefab;
    public GameObject C_tag_Prefab;
    public GameObject Text_C_tag_Prefab;
    public GameObject MA_lineDelimiter_Prefab;
    public GameObject Text_MA_line_Prefab;
    public GameObject Text_MA_tag1_Prefab;
    public GameObject MM_blockPlatform_origin_Prefab;
    public GameObject Text_MM_smallEllipsisDot_Prefab;
    public GameObject Text_C_smallEllipsisDot_Prefab;
    public GameObject Text_brace_set_Prefab;
    public GameObject Text_C_setPrefix_Prefab;
    public GameObject Text_brace_wordInLine_Prefab;



    // 生成的所有物品，可获取并操控
    private GameObject[] MA_platform_array = new GameObject[30];
    private GameObject[] MA_bit_array = new GameObject[30];
    private GameObject[] MM_platform_array = new GameObject[15];
    private GameObject[] MM_word_array = new GameObject[15];
    private GameObject[] MM_ellipsisDot_array = new GameObject[10];
    private GameObject[] C_platform_array = new GameObject[30];
    private GameObject[] C_ellipsisDot_array = new GameObject[10];
    private GameObject[] C_tag_array = new GameObject[10];
    private GameObject[] MM_brace_array = new GameObject[10];
    private GameObject[] C_brace_array = new GameObject[10];
    private GameObject[] MM_block_array = new GameObject[10];
    private GameObject[] C_line_array = new GameObject[20];
    private GameObject MA_wordDelimiter;
    private GameObject MA_textBlock;
    private GameObject MA_textWord;
    private GameObject MA_lineDelimiter;
    private GameObject MA_textLine;
    private GameObject MA_textTag1;
    private GameObject[] MM_blockPlatform_origin_array = new GameObject[15];
    private GameObject[] MM_smallEllipsisDot_array = new GameObject[12];
    private GameObject[] C_linePlatform_array = new GameObject[15];
    private GameObject[] C_smallEllipsisDot_array = new GameObject[6];
    private GameObject[] C_tag_text_array = new GameObject[10];
    private GameObject[] C_braceSet_array = new GameObject[4];
    private GameObject[] C_set_array = new GameObject[4];
    private GameObject C_brace_wordInLine;

    // direct / set associative mapping动画3临时用
    private GameObject[] direct_blockPlatform_array = new GameObject[12];
    private GameObject[] direct_block_array = new GameObject[9];

    // associative mapping动画3临时用
    private GameObject[] MM_blockPlatform_copy = new GameObject[12];

    // 动画4 变量
    private string hexAddressString;
    private GameObject word_in_MM, word_text_in_MM;


    // 所有（一系列）物品的起始坐标
    // 主要起始坐标
    private Vector3 MA_platform_begin = new Vector3(40f,190f,0f);
    private Vector3 MM_platform_begin = new Vector3(600f,190f,0f);
    private Vector3 C_platform_begin = new Vector3(430f,190f,0f);
    private Vector3 MA_wordDelimiter_begin = new Vector3(301f,205f,0f);
    private Vector3 MA_textBlock_begin = new Vector3(148f,178f,0f); //s = 22
    private Vector3 MA_textWord_begin = new Vector3(308f,178f,0f); //w = 2
    private Vector3 MA_lineDelimiter_begin = new Vector3(132f,205f,0f);
    private Vector3 C_brace_wordInLine_begin = new Vector3(464f,92f,0f);
    // 相对于 主要起始坐标 关联形成的起始坐标
    private Vector3 MM_brace_begin; //y由下一行获取
    private float[] MM_brace_begin_y = new float[10];
    private Vector3 C_brace_begin; //y由下一行获取
    private float[] C_brace_begin_y = new float[10];
    private Vector3 MM_block_begin; //y由下一行获取
    private float[] MM_block_begin_y = new float[10];
    private Vector3 C_line_begin; //y由下一行获取
    private float[] C_line_begin_y = new float[10];
    private Vector3 C_tag_begin; //y由下一行获取
    private float[] C_tag_begin_y = new float[10];
    private Vector3 MA_textLine_begin = new Vector3(212f,178f,0f);
    private Vector3 MA_textTag1_begin = new Vector3(70f,178f,0f);
    private Vector3 MM_blockPlatform_origin_begin; //y由下一行获取
    private float[] MM_blockPlatform_origin_y = new float[10];
    private Vector3 MM_blockPlatform_compression_begin;
    private Vector3 MM_block_compression_begin;
    private Vector3 MM_smallEllipsisDot_begin;
    private Vector3 MM_expansion_begin;
    private Vector3 C_smallEllipsisDot_begin;
    private Vector3 C_expansion_begin;
    private Vector3 MM_blockPlatform_setAssociative_begin;
    private float MM_block_setAssociative_begin_y;
    private Vector3 C_braceSet_begin;
    private Vector3 C_set_begin;


    

    // 缝隙
    private float gap = 2f;

    Sequence s;
    // Start is called before the first frame update
    void Start()
    {

        //相对坐标的begin设置
        MM_brace_begin = new Vector3(MM_platform_begin.x + 33f,0f,0f);
        C_brace_begin = new Vector3(C_platform_begin.x + 33f,0f,0f);
        MM_block_begin = new Vector3(MM_brace_begin.x + 20f,0f,0f);
        C_line_begin = new Vector3(C_brace_begin.x + 20f,0f,0f);
        C_tag_begin = new Vector3(C_platform_begin.x - 42f, 0f, 0f);
        MM_blockPlatform_origin_begin = new Vector3(MM_platform_begin.x,0f,0f);
        MM_blockPlatform_compression_begin = new Vector3(MM_platform_begin.x,MM_platform_begin.y-2.5f,0f);
        MM_block_compression_begin = new Vector3(MM_platform_begin.x-7f,MM_platform_begin.y+5f,0f);
        MM_smallEllipsisDot_begin = new Vector3(MM_platform_begin.x-3f,MM_platform_begin.y+20f,0f);
        C_smallEllipsisDot_begin = new Vector3(C_platform_begin.x-3f,C_platform_begin.y+20f,0f);
        C_expansion_begin = C_smallEllipsisDot_begin + new Vector3(0f,-5f * 1 - (15f + gap) * 4,0f);
        MM_blockPlatform_setAssociative_begin = C_platform_begin - new Vector3(0f, 10.7f,0f);
        MM_block_setAssociative_begin_y = C_platform_begin.y - 4f;
        C_braceSet_begin = new Vector3(C_platform_begin.x + 33f, C_platform_begin.y + 10f, 0f);
        C_set_begin = new Vector3(C_braceSet_begin.x + 15f,C_braceSet_begin.y - 13f,0f);

        // 勿删~
        MM_blockPlatform_origin_Prefab.transform.localScale = new Vector3(60f,46f,10f);
        Text_MM_blockPrefix_Prefab.transform.localScale = new Vector3(1f,1f,1f);
        Text_C_linePrefix_Prefab.transform.localScale = new Vector3(1f,1f,1f);

        //开始设置
        //Memory Address
        for(int i = 0; i < MA_len; ++ i)
        {
            MA_platform_array[i] = Instantiate(MAPlatformPrefab, MA_platform_begin + new Vector3((gap + 10f) * i,0f,0f), Quaternion.Euler(0, 0, 0));
            MA_bit_array[i] = Instantiate(Text_MA_bit_Prefab, MA_platform_begin + new Vector3((gap + 10f) * i - 3.5f,7f,0f), Quaternion.Euler(0, 0, 0));
        }
            
        //Main Memory
        for(int i = 0; i < 8; ++ i)
        {
            MM_platform_array[i] = Instantiate(MMPlatformPrefab, MM_platform_begin - new Vector3(0f,(gap + 10f) * i,0f), Quaternion.Euler(0, 0, 0));
            MM_word_array[i] = Instantiate(Text_MM_wordPrefix_Prefab, MM_platform_begin - new Vector3(9f,(gap + 10f) * i - 6.5f,0f), Quaternion.Euler(0, 0, 0));
            TextMesh temp1 = MM_word_array[i].GetComponent<TextMesh>();
            temp1.text = "W" + i.ToString();
            if(i % 4 == 0)
            {
                MM_brace_begin_y[i / 4] = MM_platform_array[i].transform.position.y + 10f;
                MM_block_begin_y[i / 4] = MM_platform_array[i].transform.position.y - 10f;
                MM_blockPlatform_origin_y[i / 4] = MM_platform_array[i].transform.position.y - 18f;
            }
        }
        for(int i = 0; i < 3; ++ i)
        {
            MM_ellipsisDot_array[i] = Instantiate(Text_MM_ellipsisDot_Prefab, MM_platform_begin - new Vector3(3f,(gap + 10f) * (i + 8 - 1) - 8f,0f), Quaternion.Euler(0, 0, 0));
            if(i == 1)
            {
                MM_expansion_begin = MM_ellipsisDot_array[1].transform.position;
                word_in_MM = Instantiate(MMPlatformPrefab, MM_platform_begin - new Vector3(0,(gap + 10f) * 9, 0f), Quaternion.Euler(0, 0, 0));
                word_text_in_MM = Instantiate(Text_MM_wordPrefix_Prefab, MM_platform_begin - new Vector3(0,(gap + 10f) * 9,0f), Quaternion.Euler(0, 0, 0));
                word_in_MM.SetActive(false);
                word_text_in_MM.SetActive(false);
            }
        }
        for(int i = 11; i < 15; ++ i)
        {
            MM_platform_array[i - 3] = Instantiate(MMPlatformPrefab, MM_platform_begin - new Vector3(0f,(gap + 10f) * i,0f), Quaternion.Euler(0, 0, 0));
            MM_word_array[i - 3] = Instantiate(Text_MM_wordPrefix_Prefab, MM_platform_begin - new Vector3(27f,(gap + 10f) * i - 6.5f,0f), Quaternion.Euler(0, 0, 0));
            TextMesh temp2 = MM_word_array[i - 3].GetComponent<TextMesh>();
            temp2.text = "W(2^24-" + (15-i).ToString() + ")";
        }
        MM_brace_begin_y[2] = MM_platform_array[8].transform.position.y + 10f;
        MM_block_begin_y[2] = MM_platform_array[8].transform.position.y - 10f;
        MM_blockPlatform_origin_y[2] = MM_platform_array[8].transform.position.y - 18f;
        
        //Cache v1
        for(int i = 0; i < 8; ++ i)
        {
            C_platform_array[i] = Instantiate(CPlatformPrefab, C_platform_begin - new Vector3(0f,(gap + 10f) * i,0f), Quaternion.Euler(0, 0, 0));
            if(i % 4 == 0)
            {
                C_brace_begin_y[i / 4] = C_platform_array[i].transform.position.y + 10f;
                C_line_begin_y[i / 4] = C_platform_array[i].transform.position.y - 10f;
                C_tag_begin_y[i / 4] = C_platform_array[i].transform.position.y;
            }
        }
        for(int i = 0; i < 3; ++ i)
        {
            C_ellipsisDot_array[i] = Instantiate(Text_C_ellipsisDot_Prefab, C_platform_begin - new Vector3(3f,(gap + 10f) * (i + 8 - 1) - 8f,0f), Quaternion.Euler(0, 0, 0));
        }
        for(int i = 11; i < 15; ++ i)
        {
            C_platform_array[i - 3] = Instantiate(CPlatformPrefab, C_platform_begin - new Vector3(0f,(gap + 10f) * i,0f), Quaternion.Euler(0, 0, 0));
        }
        C_brace_begin_y[2] = C_platform_array[8].transform.position.y + 10f;
        C_line_begin_y[2] = C_platform_array[8].transform.position.y - 10f;
        C_tag_begin_y[2] = C_platform_array[8].transform.position.y;

        // ADDED
        for(int i = 3; i < 6; ++ i)
        {
            C_ellipsisDot_array[i] = Instantiate(Text_C_ellipsisDot_Prefab, C_platform_begin - new Vector3(3f,(gap + 10f) * (i + 12 - 1) - 8f,0f), Quaternion.Euler(0, 0, 0));
            C_ellipsisDot_array[i].SetActive(false);
        }
        for(int i = 18; i < 22; ++ i)
        {
            C_platform_array[i - 6] = Instantiate(CPlatformPrefab, C_platform_begin - new Vector3(0f,(gap + 10f) * i,0f), Quaternion.Euler(0, 0, 0));
            C_platform_array[i - 6].SetActive(false);
        }
        C_brace_begin_y[3] = C_platform_array[12].transform.position.y + 10f;
        C_line_begin_y[3] = C_platform_array[12].transform.position.y - 10f;
        C_tag_begin_y[3] = C_platform_array[12].transform.position.y;
        // ENDADD


        //Main Memory block braces
        for(int i = 0; i < 3; ++ i)
        {
            MM_brace_array[i] = Instantiate(Text_MM_brace_Prefab, MM_brace_begin + new Vector3(0f, MM_brace_begin_y[i], 0f), Quaternion.Euler(0, 0, 0));
        }

        //Cache v1 line braces
        // MODIFIED
        for(int i = 0; i < 4; ++ i)
        {
            // Debug.Log(i);
            C_brace_array[i] = Instantiate(Text_C_brace_Prefab, C_brace_begin + new Vector3(0f, C_brace_begin_y[i], 0f), Quaternion.Euler(0, 0, 0));
        }
        C_brace_array[3].SetActive(false);

        
        //Main Memory block
        for(int i = 0; i < 3; ++ i)
        {
            MM_block_array[i] = Instantiate(Text_MM_blockPrefix_Prefab, MM_block_begin + new Vector3(0f, MM_block_begin_y[i], 0f), Quaternion.Euler(0, 0, 0));
            TextMesh temp3 = MM_block_array[i].GetComponent<TextMesh>();
            if(i == 2) temp3.text = "B(2^22-1)";
            else temp3.text = "B" + i.ToString();
        }

        //Cache line
        // MODIFIED
        for(int i = 0; i < 4; ++ i)
        {
            C_line_array[i] = Instantiate(Text_C_linePrefix_Prefab, C_line_begin + new Vector3(0f, C_line_begin_y[i], 0f), Quaternion.Euler(0, 0, 0));
            TextMesh temp4 = C_line_array[i].GetComponent<TextMesh>();
            if(i == 2) temp4.text = "L(2^14-1)";
            else if(i < 3) temp4.text = "L" + i.ToString();
            else    C_line_array[i].SetActive(false);
        }

        //Cache tag
        for(int i = 0; i < 3; ++ i)
        {
            C_tag_array[i] = Instantiate(C_tag_Prefab, C_tag_begin + new Vector3(0f, C_tag_begin_y[i], 0f),Quaternion.Euler(0, 0, 0));
            C_tag_text_array[i] = Instantiate(Text_C_tag_Prefab, C_tag_begin + new Vector3(-7f, C_tag_begin_y[i] + 4f, 0f),Quaternion.Euler(0, 0, 0));
        }

        //开始动画
        s = DOTween.Sequence();
        StartCoroutine(p());
    }

    IEnumerator p(){
        // 动画1开始
        Init1();
        // StartCoroutine(Procedure1(1, 1, 0, 0, 1));
        // yield return new WaitForSeconds(6f);
        // StartCoroutine(Procedure1(1, 2, 0, 1, 1));
        // yield return new WaitForSeconds(6f);
        // StartCoroutine(Procedure1(1, 3, 0, 0, 1));
        // yield return new WaitForSeconds(6f);
        // StartCoroutine(Procedure1(1, 4, 1, 1, 1));
        // yield return new WaitForSeconds(6f);
        // StartCoroutine(Procedure1(1, 5, 0, 0, 1));
        // yield return new WaitForSeconds(6f);

        // 动画2开始
        Init2();
        StartCoroutine(Procedure2_1());
        yield return new WaitForSeconds(4f);
        StartCoroutine(exchangePosition());
        yield return new WaitForSeconds(1f);
        // StartCoroutine(Procedure1(2, 1, 0, 0, 1));
        // yield return new WaitForSeconds(6f);
        // StartCoroutine(Procedure1(2, 2, 0, 1, 1));
        // yield return new WaitForSeconds(6f);
        // StartCoroutine(Procedure1(2, 3, 0, 0, 1));
        // yield return new WaitForSeconds(6f);
        // StartCoroutine(Procedure1(2, 4, 1, 1, 1));
        // yield return new WaitForSeconds(6f);
        // StartCoroutine(Procedure1(2, 5, 0, 0, 1));
        // yield return new WaitForSeconds(6f);

        // direct mapping动画3开始
        // Init3();
        // StartCoroutine(Procedure3_1());
        // yield return new WaitForSeconds(3f);
        // StartCoroutine(Procedure3_2(1));
        // yield return new WaitForSeconds(3f);
        // StartCoroutine(D_tag_flip(1));
        // yield return new WaitForSeconds(2f);
        // StartCoroutine(Procedure3_3(1,1));
        // yield return new WaitForSeconds(3.5f);
        // StartCoroutine(D_tag_flip(2));
        // yield return new WaitForSeconds(2f);
        // StartCoroutine(Procedure3_3(1,2));
        // yield return new WaitForSeconds(3.5f);
        // StartCoroutine(D_tag_flip(3));
        // yield return new WaitForSeconds(2f);
        // StartCoroutine(Procedure3_3(1,3));
        // yield return new WaitForSeconds(3.5f);

        // direct mapping动画4开始
        // int tag = 155, line = 2006, word = 2;
        // bool isInCache = true;
        // Init4(tag, line, word, isInCache);
        // StartCoroutine(checkLine(line));
        // yield return new WaitForSeconds(4f);
        // StartCoroutine(checkTag(tag, isInCache));
        // yield return new WaitForSeconds(7f);
        // StartCoroutine(checkWord(word));
        // yield return new WaitForSeconds(5 + word);
        // end4(tag, line, word);
        
        // Init4(tag, line, word, !isInCache);
        // StartCoroutine(checkLine(line));
        // yield return new WaitForSeconds(4f);
        // StartCoroutine(checkTag(tag, isInCache));
        // yield return new WaitForSeconds(7f);
        // StartCoroutine(checkMainMemory(tag, line, word));
        // yield return new WaitForSeconds(7f);
        // end4(tag, line, word);

        // fully associative mapping动画3开始
        // Init3();
        // StartCoroutine(Pro3());
        // yield return new WaitForSeconds(18f);

        // fully associative mapping动画4开始
        // int tag1 = 390, word1 = 3;
        // bool isInCache1 = true;
        // Init5(tag1, word1, isInCache1);
        // StartCoroutine(checkTag1(tag1, isInCache1));
        // yield return new WaitForSeconds(4f);
        // StartCoroutine(checkWord(word1));
        // yield return new WaitForSeconds(5 + word1);
        // end5(tag1, word1);

        // Init5(tag1, word1, !isInCache1);
        // StartCoroutine(checkTag1(tag1, isInCache1));
        // yield return new WaitForSeconds(7f);
        // StartCoroutine(checkMainMemory1(tag1, word1));
        // yield return new WaitForSeconds(7f);
        // end5(tag1, word1);

        // set associative mapping动画3开始1
        Init3();
        StartCoroutine(Procedure3_SA_1());
        yield return new WaitForSeconds(7f);

        // set associative mapping动画3开始2
        // StartCoroutine(Procedure3_2(3));
        // yield return new WaitForSeconds(3f);
        // StartCoroutine(SA_tag_flip(1));
        // yield return new WaitForSeconds(2f);
        // StartCoroutine(Procedure3_3(3,1));
        // yield return new WaitForSeconds(3.5f);
        // StartCoroutine(SA_tag_flip(2));
        // yield return new WaitForSeconds(2f);
        // StartCoroutine(Procedure3_3(3,2));
        // yield return new WaitForSeconds(3.5f);
        // StartCoroutine(SA_tag_flip(3));
        // yield return new WaitForSeconds(2f);
        // StartCoroutine(Procedure3_3(3,3));
        // yield return new WaitForSeconds(3.5f);

        

        // set associative mapping动画4开始
        // int tag2 = 245, set = 679, word3 = 1;
        // bool isInCache2 = true;
        // StartCoroutine(Init6(tag2, set, word3, isInCache2));
        // yield return new WaitForSeconds(1f);
        // StartCoroutine(checkSet(set));
        // yield return new WaitForSeconds(4f);
        // StartCoroutine(checkTag2(tag2, isInCache2));
        // yield return new WaitForSeconds(4f);
        // yield return new WaitForSeconds(5 + word3);
        // end6(tag2, set, word3);


        // 不加
        // StartCoroutine(Init6(tag2, set, word3, !isInCache2));
        // yield return new WaitForSeconds(1f);
        // StartCoroutine(checkSet(set));
        // yield return new WaitForSeconds(4f);
        // StartCoroutine(checkTag2(tag2, isInCache2));
        // yield return new WaitForSeconds(7f);
        // StartCoroutine(checkMainMemory1(tag2, word3));
        // yield return new WaitForSeconds(7f);
        // end6(tag2, set, word3);

        yield return new WaitForSeconds(1f);

    }

    //memory address 与 所有标签 上下移位
    IEnumerator exchangePosition()
    {
        s.Append(MA_bit_array[23].transform.DOMove(new Vector3(MA_bit_array[23].transform.position.x,MA_bit_array[23].transform.position.y - 35f,0f),1));
        s.Join(MA_platform_array[23].transform.DOMove(new Vector3(MA_platform_array[23].transform.position.x,MA_platform_array[23].transform.position.y - 35f,0f),1));
        for(int i = 0; i < 23; ++ i)
        {
            s.Join(MA_bit_array[i].transform.DOMove(new Vector3(MA_bit_array[i].transform.position.x,MA_bit_array[i].transform.position.y - 35f,0f),1));
            s.Join(MA_platform_array[i].transform.DOMove(new Vector3(MA_platform_array[i].transform.position.x,MA_platform_array[i].transform.position.y - 35f,0f),1));
        }
        if(MA_textBlock.activeSelf)
        {
            s.Join(MA_textBlock.transform.DOMove(new Vector3(MA_textBlock.transform.position.x,MA_textBlock.transform.position.y + 17f,0f),1));
        }
        if(MA_textWord.activeSelf)
        {
            s.Join(MA_textWord.transform.DOMove(new Vector3(MA_textWord.transform.position.x,MA_textWord.transform.position.y + 17f,0f),1));
        }
        if(MA_wordDelimiter.activeSelf)
        {
            s.Join(MA_wordDelimiter.transform.DOMove(new Vector3(MA_wordDelimiter.transform.position.x,MA_wordDelimiter.transform.position.y - 35f,0f),1));
        }


        yield return new WaitForSeconds(1f);
    }

    IEnumerator SA_tag_flip(int past)
    {
        for(int i = 9; i < 24; ++ i)
        {
            MA_bit_array[i].GetComponent<TextMesh>().text = "x";
        }
        if(past == 1)
        {
            s.Append(MA_platform_array[8].transform.DORotate(new Vector3(180,0,0),2));
            yield return new WaitForSeconds(1f);
            MA_bit_array[8].GetComponent<TextMesh>().text = "1";
            MA_platform_array[8].GetComponent<MeshRenderer>().material.color = Color.red;
            yield return new WaitForSeconds(1f);
        }
        else if(past == 2)
        {
            s.Append(MA_platform_array[7].transform.DORotate(new Vector3(180,0,0),2));
            s.Append(MA_platform_array[8].transform.DORotate(new Vector3(360,0,0),2));
            yield return new WaitForSeconds(1f);
            MA_bit_array[8].GetComponent<TextMesh>().text = "0";
            MA_bit_array[7].GetComponent<TextMesh>().text = "1";
            MA_platform_array[7].GetComponent<MeshRenderer>().material.color = Color.red;
            yield return new WaitForSeconds(1f);
        }
        else if(past == 3)
        {
            s.Append(MA_platform_array[8].transform.DORotate(new Vector3(180,0,0),2));
            for(int i=0; i<=6; ++i)
            {
                s.Join(MA_platform_array[i].transform.DORotate(new Vector3(180,0,0),2));
            }
            yield return new WaitForSeconds(1f);
            for(int i = 0; i <= 8; ++i)
            {
                MA_bit_array[i].GetComponent<TextMesh>().text = "1";
                MA_platform_array[i].GetComponent<MeshRenderer>().material.color = Color.red;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator D_tag_flip(int past)
    {
        for(int i = 8; i < 24; ++ i)
        {
            MA_bit_array[i].GetComponent<TextMesh>().text = "x";
        }
        if(past == 1)
        {
            s.Append(MA_platform_array[7].transform.DORotate(new Vector3(180,0,0),2));
            yield return new WaitForSeconds(1f);
            MA_bit_array[7].GetComponent<TextMesh>().text = "1";
            MA_platform_array[7].GetComponent<MeshRenderer>().material.color = Color.red;
            yield return new WaitForSeconds(1f);
        }
        else if(past == 2)
        {
            s.Append(MA_platform_array[6].transform.DORotate(new Vector3(180,0,0),2));
            s.Append(MA_platform_array[7].transform.DORotate(new Vector3(360,0,0),2));
            yield return new WaitForSeconds(1f);
            MA_bit_array[7].GetComponent<TextMesh>().text = "0";
            MA_bit_array[6].GetComponent<TextMesh>().text = "1";
            MA_platform_array[6].GetComponent<MeshRenderer>().material.color = Color.red;
            yield return new WaitForSeconds(1f);
        }
        else if(past == 3)
        {
            s.Append(MA_platform_array[7].transform.DORotate(new Vector3(180,0,0),2));
            for(int i=0; i<=5; ++i)
            {
                s.Join(MA_platform_array[i].transform.DORotate(new Vector3(180,0,0),2));
            }
            yield return new WaitForSeconds(1f);
            for(int i = 0; i <= 7; ++i)
            {
                MA_bit_array[i].GetComponent<TextMesh>().text = "1";
                MA_platform_array[i].GetComponent<MeshRenderer>().material.color = Color.red;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator Pro3()
    {
        MA_textBlock.GetComponent<TextMesh>().text = "Tag = block\n    (s=22)";
        s.Append(MA_textBlock.transform.DOMove(MA_textBlock_begin + new Vector3(-10f,0f,0f),1));
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < 12; ++ i)
        {
            MM_platform_array[i].SetActive(false);
            MM_word_array[i].SetActive(false);
        }
        for(int i = 0; i < 3; ++ i)
        {
            MM_blockPlatform_origin_array[i] = Instantiate(MM_blockPlatform_origin_Prefab, MM_blockPlatform_origin_begin + new Vector3(0f,MM_blockPlatform_origin_y[i],0f),Quaternion.Euler(0, 0, 0));
            MM_blockPlatform_copy[i] = Instantiate(MM_blockPlatform_origin_Prefab, MM_blockPlatform_origin_begin + new Vector3(0f,MM_blockPlatform_origin_y[i],0f),Quaternion.Euler(0, 0, 0));
        }
        for(int i = 0; i < 3; ++ i)
        {
            MM_brace_array[i].SetActive(false);
        }

        yield return new WaitForSeconds(1f);

        s.Append(MM_block_array[0].transform.DOMove(MM_block_begin + new Vector3(-58f,MM_block_begin_y[0],0f), 1));
        s.Join(MM_block_array[1].transform.DOMove(MM_block_begin + new Vector3(-58f,MM_block_begin_y[1],0f), 1));
        s.Join(MM_block_array[2].transform.DOMove(MM_block_begin + new Vector3(-78f,MM_block_begin_y[2],0f), 1));
        yield return new WaitForSeconds(1f);

        s.Append(MA_platform_array[21].transform.DORotate(new Vector3(180,0,0),2));
        yield return new WaitForSeconds(1f);
        MA_bit_array[21].GetComponent<TextMesh>().text = "1";
        MA_platform_array[21].GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1f);

        Color c = MM_blockPlatform_origin_array[0].GetComponent<MeshRenderer>().material.color;
        MM_blockPlatform_origin_array[0].GetComponent<MeshRenderer>().material.color = Color.red;
        MM_blockPlatform_copy[0].GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        MM_blockPlatform_origin_array[0].GetComponent<MeshRenderer>().material.color = c;
        MM_blockPlatform_copy[0].GetComponent<MeshRenderer>().material.color = c;
        yield return new WaitForSeconds(0.1f);
        MM_blockPlatform_origin_array[0].GetComponent<MeshRenderer>().material.color = Color.red;
        MM_blockPlatform_copy[0].GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.8f);
        s.Append(MM_blockPlatform_copy[0].transform.DOScale(new Vector3(60,178,10),1));
        s.Join(MM_blockPlatform_copy[0].transform.DOMove(new Vector3(430f,106f,0f),1));
        //s.Join(MM_blockPlatform_origin_array[0].GetComponent<MeshRenderer>().material.DOColor(Color.white, 2));
        yield return new WaitForSeconds(2f);
        MM_blockPlatform_copy[0].SetActive(false);
        MM_blockPlatform_origin_array[0].GetComponent<MeshRenderer>().material.color = c;

        s.Append(MA_platform_array[20].transform.DORotate(new Vector3(180,0,0),2));
        s.Append(MA_platform_array[21].transform.DORotate(new Vector3(360,0,0),2));
        yield return new WaitForSeconds(1f);
        MA_bit_array[21].GetComponent<TextMesh>().text = "0";
        MA_bit_array[20].GetComponent<TextMesh>().text = "1";
        MA_platform_array[20].GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1f);

        MM_blockPlatform_origin_array[1].GetComponent<MeshRenderer>().material.color = Color.red;
        MM_blockPlatform_copy[1].GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        MM_blockPlatform_origin_array[1].GetComponent<MeshRenderer>().material.color = c;
        MM_blockPlatform_copy[1].GetComponent<MeshRenderer>().material.color = c;
        yield return new WaitForSeconds(0.1f);
        MM_blockPlatform_origin_array[1].GetComponent<MeshRenderer>().material.color = Color.red;
        MM_blockPlatform_copy[1].GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.8f);
        s.Append(MM_blockPlatform_copy[1].transform.DOScale(new Vector3(60,178,10),1));
        s.Join(MM_blockPlatform_copy[1].transform.DOMove(new Vector3(430f,106f,0f),1));
        yield return new WaitForSeconds(2f);
        MM_blockPlatform_copy[1].SetActive(false);
        MM_blockPlatform_origin_array[1].GetComponent<MeshRenderer>().material.color = c;

        s.Append(MA_platform_array[21].transform.DORotate(new Vector3(180,0,0),2));
        for(int i=0; i<=19; ++i)
        {
            s.Join(MA_platform_array[i].transform.DORotate(new Vector3(180,0,0),2));
        }
        yield return new WaitForSeconds(1f);
        for(int i = 0; i <= 21; ++i)
        {
            MA_bit_array[i].GetComponent<TextMesh>().text = "1";
            MA_platform_array[i].GetComponent<MeshRenderer>().material.color = Color.red;
        }
        yield return new WaitForSeconds(1f);

        MM_blockPlatform_origin_array[2].GetComponent<MeshRenderer>().material.color = Color.red;
        MM_blockPlatform_copy[2].GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        MM_blockPlatform_origin_array[2].GetComponent<MeshRenderer>().material.color = c;
        MM_blockPlatform_copy[2].GetComponent<MeshRenderer>().material.color = c;
        yield return new WaitForSeconds(0.1f);
        MM_blockPlatform_origin_array[2].GetComponent<MeshRenderer>().material.color = Color.red;
        MM_blockPlatform_copy[2].GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.8f);
        s.Append(MM_blockPlatform_copy[2].transform.DOScale(new Vector3(60,178,10),1));
        s.Join(MM_blockPlatform_copy[2].transform.DOMove(new Vector3(430f,106f,0f),1));
        yield return new WaitForSeconds(2f);
        MM_blockPlatform_copy[2].SetActive(false);
        MM_blockPlatform_origin_array[2].GetComponent<MeshRenderer>().material.color = c;
    }


    IEnumerator Procedure3_SA_1()
    {
        MA_textBlock.SetActive(false);
        s.Append(MA_platform_array[23].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap + 10f) * 25,0f,0f),1));
        s.Join(MA_bit_array[23].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap + 10f) * 25 - 3.5f,7f,0f),1));
        s.Join(MA_platform_array[22].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap + 10f) * 24,0f,0f),1));
        s.Join(MA_bit_array[22].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap + 10f) * 24 - 3.5f,7f,0f),1));
        for(int i = 21; i > 8; -- i)
        {
            s.Join(MA_platform_array[i].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap + 10f) * (i + 1),0f,0f),1));
            s.Join(MA_bit_array[i].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap + 10f) * (i + 1) - 3.5f,7f,0f),1));
        }
        s.Join(MA_wordDelimiter.transform.DOMove(MA_platform_array[0].transform.position + new Vector3(261f,15f,0f) + new Vector3(gap + 10f, 0f, 0f), 1));
        s.Join(MA_textWord.transform.DOMove(MA_textWord.transform.position + new Vector3(gap + 10f,0f,0f),1));
        yield return new WaitForSeconds(1f);
        MA_lineDelimiter = Instantiate(MA_lineDelimiter_Prefab, new Vector3( MA_lineDelimiter_begin.x, MA_wordDelimiter.transform.position.y, 0f) +new Vector3(12f,0f,0f), Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(1f);
        MA_textLine = Instantiate(Text_MA_line_Prefab, new Vector3( MA_textLine_begin.x, MA_textWord.transform.position.y, 0f), Quaternion.Euler(0, 0, 0));
        MA_textLine.GetComponent<TextMesh>().text = "   Set\n (d=13)";
        MA_textTag1 = Instantiate(Text_MA_tag1_Prefab, new Vector3( MA_textTag1_begin.x, MA_textWord.transform.position.y, 0f), Quaternion.Euler(0, 0, 0));
        MA_textTag1.GetComponent<TextMesh>().text = " Tag\n(s=9)";
        yield return new WaitForSeconds(1f);

        // 移动Cache符号
        for(int i = 0; i < 12; ++ i)
        {
            C_platform_array[i].SetActive(false);
        }
        MM_blockPlatform_origin_Prefab.transform.localScale = new Vector3(60f,46f,10f);
        for(int i = 0; i < 3; ++ i)
        {
            C_linePlatform_array[i] = Instantiate(MM_blockPlatform_origin_Prefab, new Vector3(C_platform_begin.x,MM_blockPlatform_origin_y[i],0f),Quaternion.Euler(0, 0, 0));
        }
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < 3; ++ i)
        {
            C_brace_array[i].SetActive(false);
            C_ellipsisDot_array[i].SetActive(false);
        }

        s.Append(C_line_array[0].transform.DOMove(C_line_begin + new Vector3(-58f,MM_block_begin_y[0],0f), 1));
        s.Join(C_line_array[1].transform.DOMove(C_line_begin + new Vector3(-58f,MM_block_begin_y[1],0f), 1));
        s.Join(C_line_array[2].transform.DOMove(C_line_begin + new Vector3(-78f,MM_block_begin_y[2],0f), 1));
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < 3; ++ i) C_tag_array[i].SetActive(false);
        // 开始压缩

        s.Append(C_linePlatform_array[0].transform.DOScale(new Vector3(60f,15f,10f), 1));
        s.Join(C_linePlatform_array[0].transform.DOMove(new Vector3(C_platform_begin.x,C_platform_begin.y-2.5f,0f), 1));
        s.Join(C_line_array[0].transform.DOMove(new Vector3(C_line_begin.x - 58f,C_platform_begin.y+5f,0f),1));
        s.Join(C_linePlatform_array[1].transform.DOScale(new Vector3(60f,15f,10f), 1));
        s.Join(C_linePlatform_array[1].transform.DOMove(new Vector3(C_platform_begin.x,C_platform_begin.y-2.5f,0f) - new Vector3(0f,(15f + gap) * 1,0f), 1));
        s.Join(C_line_array[1].transform.DOMove(new Vector3(C_line_begin.x - 58f,C_platform_begin.y+5f,0f) - new Vector3(0f,(15f + gap) * 1,0f),1));
        C_linePlatform_array[5] = C_linePlatform_array[2];
        s.Join(C_linePlatform_array[5].transform.DOScale(new Vector3(60f,15f,10f), 1));
        s.Join(C_linePlatform_array[5].transform.DOMove(new Vector3(C_platform_begin.x,C_platform_begin.y-2.5f,0f) - new Vector3(0f,(15f + gap) * 6,0f), 1));
        C_line_array[5] = C_line_array[2];
        s.Join(C_line_array[5].transform.DOMove(new Vector3(C_line_begin.x - 78f,C_platform_begin.y+5f,0f) - new Vector3(0f,(15f + gap) * 6,0f),1));
       
        // 压缩的linePlatform移动
        for(int i = 2; i < 5; ++ i)
        {
            // 文字生成
            Text_C_linePrefix_Prefab.transform.localScale = new Vector3(0f,0f,0f);
            C_line_array[i] = Instantiate(Text_C_linePrefix_Prefab, C_expansion_begin, Quaternion.Euler(0, 0, 0));
            s.Join(C_line_array[i].transform.DOScale(new Vector3(1f,1f,1f),1));
            // blockPlatform和文字的移动变换
            MM_blockPlatform_origin_Prefab.transform.localScale = new Vector3(0f,0f,0f);
            C_linePlatform_array[i] = Instantiate(MM_blockPlatform_origin_Prefab, C_expansion_begin, Quaternion.Euler(0, 0, 0));
            s.Join(C_linePlatform_array[i].transform.DOScale(new Vector3(60f,15f,10f), 1));
            if(i < 4)
            {
                s.Join(C_linePlatform_array[i].transform.DOMove(new Vector3(C_platform_begin.x,C_platform_begin.y-2.5f,0f) - new Vector3(0f,(15f + gap) * i,0f), 1));
                s.Join(C_line_array[i].transform.DOMove(new Vector3(C_line_begin.x - 58f,C_platform_begin.y+5f,0f) - new Vector3(0f,(15f + gap) * i,0f),1));
            }
            else
            {
                s.Join(C_linePlatform_array[i].transform.DOMove(new Vector3(C_platform_begin.x,C_platform_begin.y-2.5f,0f) - new Vector3(0f,(15f + gap) * (i + 1),0f), 1));
                s.Join(C_line_array[i].transform.DOMove(new Vector3(C_line_begin.x - 78f,C_platform_begin.y+5f,0f) - new Vector3(0f,(15f + gap) * (i + 1),0f),1));
            }
        }
        // 省略号移动
        C_smallEllipsisDot_array[0] = Instantiate(Text_C_smallEllipsisDot_Prefab, C_expansion_begin, Quaternion.Euler(0, 0, 0));
        s.Join(C_smallEllipsisDot_array[0].transform.DOMove(C_smallEllipsisDot_begin + new Vector3(0f,-5f * 0 - (15f + gap) * 4,0f),1));
        C_smallEllipsisDot_array[1] = Instantiate(Text_C_smallEllipsisDot_Prefab, C_expansion_begin, Quaternion.Euler(0, 0, 0));
        s.Join(C_smallEllipsisDot_array[1].transform.DOMove(C_smallEllipsisDot_begin + new Vector3(0f,-5f * 1 - (15f + gap) * 4,0f),1));
        C_smallEllipsisDot_array[2] = Instantiate(Text_C_smallEllipsisDot_Prefab, C_expansion_begin, Quaternion.Euler(0, 0, 0));
        s.Join(C_smallEllipsisDot_array[2].transform.DOMove(C_smallEllipsisDot_begin + new Vector3(0f,-5f * 2 - (15f + gap) * 4,0f),1));

        // 文字内容编辑
        C_line_array[2].GetComponent<TextMesh>().text = "L2";
        C_line_array[3].GetComponent<TextMesh>().text = "L3";
        C_line_array[4].GetComponent<TextMesh>().text = "L(2^14-2)";

        yield return new WaitForSeconds(1f);


        C_braceSet_array[0] = Instantiate(Text_brace_set_Prefab, C_braceSet_begin - new Vector3(0f, (15f + gap) * 0, 0f), Quaternion.Euler(0, 0, 0));
        C_braceSet_array[1] = Instantiate(Text_brace_set_Prefab, C_braceSet_begin - new Vector3(0f, (15f + gap) * 2, 0f), Quaternion.Euler(0, 0, 0));
        C_braceSet_array[2] = Instantiate(Text_brace_set_Prefab, C_braceSet_begin - new Vector3(0f, (15f + gap) * 5, 0f), Quaternion.Euler(0, 0, 0));
        C_set_array[0] = Instantiate(Text_C_setPrefix_Prefab, C_set_begin - new Vector3(0f, (15f + gap) * 0, 0f), Quaternion.Euler(0, 0, 0));
        C_set_array[1] = Instantiate(Text_C_setPrefix_Prefab, C_set_begin - new Vector3(0f, (15f + gap) * 2, 0f), Quaternion.Euler(0, 0, 0));
        C_set_array[2] = Instantiate(Text_C_setPrefix_Prefab, C_set_begin - new Vector3(0f, (15f + gap) * 5, 0f), Quaternion.Euler(0, 0, 0));
        C_set_array[0].GetComponent<TextMesh>().text = "S0";
        C_set_array[1].GetComponent<TextMesh>().text = "S1";
        C_set_array[2].GetComponent<TextMesh>().text = "S(2^13-1)";
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Procedure3_1()
    {
        MA_textBlock.SetActive(false);
        s.Append(MA_platform_array[23].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap + 10f) * 25,0f,0f),1));
        s.Join(MA_bit_array[23].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap + 10f) * 25 - 3.5f,7f,0f),1));
        s.Join(MA_platform_array[22].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap + 10f) * 24,0f,0f),1));
        s.Join(MA_bit_array[22].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap + 10f) * 24 - 3.5f,7f,0f),1));
        for(int i = 21; i > 7; -- i)
        {
            s.Join(MA_platform_array[i].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap + 10f) * (i + 1),0f,0f),1));
            s.Join(MA_bit_array[i].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap + 10f) * (i + 1) - 3.5f,7f,0f),1));
        }
        s.Join(MA_wordDelimiter.transform.DOMove(MA_platform_array[0].transform.position + new Vector3(261f,15f,0f) + new Vector3(gap + 10f, 0f, 0f), 1));
        s.Join(MA_textWord.transform.DOMove(MA_textWord.transform.position + new Vector3(gap + 10f,0f,0f),1));
        yield return new WaitForSeconds(1f);
        MA_lineDelimiter = Instantiate(MA_lineDelimiter_Prefab, new Vector3( MA_lineDelimiter_begin.x, MA_wordDelimiter.transform.position.y, 0f), Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(1f);
        MA_textLine = Instantiate(Text_MA_line_Prefab, new Vector3(MA_textLine_begin.x, MA_textWord.transform.position.y,0f), Quaternion.Euler(0, 0, 0));
        MA_textTag1 = Instantiate(Text_MA_tag1_Prefab, new Vector3(MA_textTag1_begin.x, MA_textWord.transform.position.y,0f), Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(1f);
    }



    // 压缩
    IEnumerator Procedure3_2(int mappingType)
    {
        // 移动Block符号
        for(int i = 0; i < 12; ++ i)
        {
            MM_platform_array[i].SetActive(false);
            MM_word_array[i].SetActive(false);
        }
        MM_blockPlatform_origin_Prefab.transform.localScale = new Vector3(60f,46f,10f);
        for(int i = 0; i < 3; ++ i)
        {
            MM_blockPlatform_origin_array[i] = Instantiate(MM_blockPlatform_origin_Prefab, MM_blockPlatform_origin_begin + new Vector3(0f,MM_blockPlatform_origin_y[i],0f),Quaternion.Euler(0, 0, 0));
        }
        yield return new WaitForSeconds(1f);

        for(int i = 0; i < 3; ++ i)
        {
            MM_brace_array[i].SetActive(false);
            MM_ellipsisDot_array[i].SetActive(false);
        }
        s.Append(MM_block_array[0].transform.DOMove(MM_block_begin + new Vector3(-58f,MM_block_begin_y[0],0f), 1));
        s.Join(MM_block_array[1].transform.DOMove(MM_block_begin + new Vector3(-58f,MM_block_begin_y[1],0f), 1));
        s.Join(MM_block_array[2].transform.DOMove(MM_block_begin + new Vector3(-78f,MM_block_begin_y[2],0f), 1));
        yield return new WaitForSeconds(1f);

        // 开始压缩

        s.Append(MM_blockPlatform_origin_array[0].transform.DOScale(new Vector3(60f,15f,10f), 1));
        s.Join(MM_blockPlatform_origin_array[0].transform.DOMove(MM_blockPlatform_compression_begin, 1));
        s.Join(MM_block_array[0].transform.DOMove(MM_block_compression_begin,1));
        s.Join(MM_blockPlatform_origin_array[1].transform.DOScale(new Vector3(60f,15f,10f), 1));
        s.Join(MM_blockPlatform_origin_array[1].transform.DOMove(MM_blockPlatform_compression_begin - new Vector3(0f,(15f + gap) * 1,0f), 1));
        s.Join(MM_block_array[1].transform.DOMove(MM_block_compression_begin - new Vector3(0f,(15f + gap) * 1,0f),1));
        MM_blockPlatform_origin_array[8] = MM_blockPlatform_origin_array[2];
        s.Join(MM_blockPlatform_origin_array[8].transform.DOScale(new Vector3(60f,15f,10f), 1));
        s.Join(MM_blockPlatform_origin_array[8].transform.DOMove(MM_blockPlatform_compression_begin - new Vector3(0f,(15f + gap) * 12,0f), 1));
        MM_block_array[8] = MM_block_array[2];
        s.Join(MM_block_array[8].transform.DOMove(MM_block_compression_begin - new Vector3(0f,(15f + gap) * 12,0f),1));
        // 压缩的blockPlatform移动
        for(int i = 2; i < 8; ++ i)
        {
             // 文字生成
            Text_MM_blockPrefix_Prefab.transform.localScale = new Vector3(0f,0f,0f);
            MM_block_array[i] = Instantiate(Text_MM_blockPrefix_Prefab, MM_expansion_begin, Quaternion.Euler(0, 0, 0));
            s.Join(MM_block_array[i].transform.DOScale(new Vector3(1f,1f,1f),1));
            // blockPlatform和文字的移动变换
            MM_blockPlatform_origin_Prefab.transform.localScale = new Vector3(0f,0f,0f);
            MM_blockPlatform_origin_array[i] = Instantiate(MM_blockPlatform_origin_Prefab, MM_expansion_begin, Quaternion.Euler(0, 0, 0));
            s.Join(MM_blockPlatform_origin_array[i].transform.DOScale(new Vector3(60f,15f,10f), 1));
            if(i < 5)
            {
                s.Join(MM_blockPlatform_origin_array[i].transform.DOMove(MM_blockPlatform_compression_begin - new Vector3(0f,(15f + gap) * (i + 1),0f), 1));
                s.Join(MM_block_array[i].transform.DOMove(MM_block_compression_begin - new Vector3(20f,(15f + gap) * (i + 1),0f),1));
            }
            else if(i < 6)
            {
                s.Join(MM_blockPlatform_origin_array[i].transform.DOMove(MM_blockPlatform_compression_begin - new Vector3(0f,(15f + gap) * (i + 2),0f), 1));
                s.Join(MM_block_array[i].transform.DOMove(MM_block_compression_begin - new Vector3(20f,(15f + gap) * (i + 2),0f),1));
            }
            else
            {
                s.Join(MM_blockPlatform_origin_array[i].transform.DOMove(MM_blockPlatform_compression_begin - new Vector3(0f,(15f + gap) * (i + 3),0f), 1));
                s.Join(MM_block_array[i].transform.DOMove(MM_block_compression_begin - new Vector3(20f,(15f + gap) * (i + 3),0f),1));
            }

        }
        // 省略号移动
        for(int i = 0; i < 4; ++ i)
        {
            for(int j = 0; j < 3; ++ j)
            {
                MM_smallEllipsisDot_array[i * 3 + j] = Instantiate(Text_MM_smallEllipsisDot_Prefab, MM_expansion_begin, Quaternion.Euler(0, 0, 0));
                if(i == 0)
                {
                    s.Join(MM_smallEllipsisDot_array[i * 3 + j].transform.DOMove(MM_smallEllipsisDot_begin + new Vector3(0f,-5f * j - (15f + gap) * 2,0f),1));
                }
                else if(i == 1)
                {
                    s.Join(MM_smallEllipsisDot_array[i * 3 + j].transform.DOMove(MM_smallEllipsisDot_begin + new Vector3(0f,-5f * j - (15f + gap) * 6,0f),1));
                }
                else if(i == 2)
                {
                    s.Join(MM_smallEllipsisDot_array[i * 3 + j].transform.DOMove(MM_smallEllipsisDot_begin + new Vector3(0f,-5f * j - (15f + gap) * 8,0f),1));
                }
                else if(i == 3)
                {
                    s.Join(MM_smallEllipsisDot_array[i * 3 + j].transform.DOMove(MM_smallEllipsisDot_begin + new Vector3(0f,-5f * j - (15f + gap) * 11,0f),1));
                }
            }
        }
        // 文字内容编辑
        if(mappingType == 1)
        {
            MM_block_array[2].GetComponent<TextMesh>().text = "B(2^14-1)";
            MM_block_array[3].GetComponent<TextMesh>().text = "B(2^14)";
            MM_block_array[4].GetComponent<TextMesh>().text = "B(2^14+1)";
            MM_block_array[5].GetComponent<TextMesh>().text = "B(2^15-1)";
            MM_block_array[6].GetComponent<TextMesh>().text = "B(2^14(2^8-1))";
            MM_block_array[7].GetComponent<TextMesh>().text = "B(2^14(2^8-1)+1)";
        }
        else if(mappingType == 3)
        {
            MM_block_array[2].GetComponent<TextMesh>().text = "B(2^13-1)";
            MM_block_array[3].GetComponent<TextMesh>().text = "B(2^13)";
            MM_block_array[4].GetComponent<TextMesh>().text = "B(2^13+1)";
            MM_block_array[5].GetComponent<TextMesh>().text = "B(2^14-1)";
            MM_block_array[6].GetComponent<TextMesh>().text = "B(2^13(2^9-1))";
            MM_block_array[7].GetComponent<TextMesh>().text = "B(2^13(2^9-1)+1)";
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Procedure3_3(int mappingType, int past)
    {
        Color c = C_platform_array[0].GetComponent<MeshRenderer>().material.color;
        for(int i = 3 * (past - 1); i < 3 * past; ++ i)
        {
            direct_blockPlatform_array[i] = Instantiate(MM_blockPlatform_origin_array[i]);
            direct_block_array[i] = Instantiate(MM_block_array[i]);
            MM_blockPlatform_origin_array[i].GetComponent<MeshRenderer>().material.color = Color.red;
        }
        yield return new WaitForSeconds(0.1f);
        for(int i = 3 * (past - 1); i < 3 * past; ++ i)
            MM_blockPlatform_origin_array[i].GetComponent<MeshRenderer>().material.color = c;
        yield return new WaitForSeconds(0.1f);
        for(int i = 3 * (past - 1); i < 3 * past; ++ i)
            MM_blockPlatform_origin_array[i].GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        for(int i = 3 * (past - 1); i < 3 * past; ++ i)
            MM_blockPlatform_origin_array[i].GetComponent<MeshRenderer>().material.color = c;
        yield return new WaitForSeconds(0.1f);
        for(int i = 3 * (past - 1); i < 3 * past; ++ i)
            MM_blockPlatform_origin_array[i].GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        if(mappingType == 1)
        {
            s.Append(direct_blockPlatform_array[3 * (past - 1)].transform.DOMove(new Vector3(C_platform_begin.x, MM_blockPlatform_origin_y[0],0f),1));
            s.Join(direct_blockPlatform_array[3 * (past - 1)].transform.DOScale(new Vector3(60f,46f,10f),1));
            s.Join(direct_blockPlatform_array[3 * (past - 1) + 1].transform.DOMove(new Vector3(C_platform_begin.x, MM_blockPlatform_origin_y[1],0f),1));
            s.Join(direct_blockPlatform_array[3 * (past - 1) + 1].transform.DOScale(new Vector3(60f,46f,10f),1));
            s.Join(direct_blockPlatform_array[3 * (past - 1) + 2].transform.DOMove(new Vector3(C_platform_begin.x, MM_blockPlatform_origin_y[2],0f),1));
            s.Join(direct_blockPlatform_array[3 * (past - 1) + 2].transform.DOScale(new Vector3(60f,46f,10f),1));
        }
        else if(mappingType == 3)
        {
            s.Append(direct_blockPlatform_array[3 * (past - 1)].transform.DOMove(MM_blockPlatform_setAssociative_begin - new Vector3(0f, (15f + gap) * 0,0f),1));
            s.Join(direct_blockPlatform_array[3 * (past - 1)].transform.DOScale(new Vector3(60f,32f,10f),1));
            s.Join(direct_blockPlatform_array[3 * (past - 1) + 1].transform.DOMove(MM_blockPlatform_setAssociative_begin - new Vector3(0f, (15f + gap) * 2,0f),1));
            s.Join(direct_blockPlatform_array[3 * (past - 1) + 1].transform.DOScale(new Vector3(60f,32f,10f),1));
            s.Join(direct_blockPlatform_array[3 * (past - 1) + 2].transform.DOMove(MM_blockPlatform_setAssociative_begin - new Vector3(0f, (15f + gap) * 5,0f),1));
            s.Join(direct_blockPlatform_array[3 * (past - 1) + 2].transform.DOScale(new Vector3(60f,32f,10f),1));
        }
        float y1, y2, y3;
        if(mappingType == 1)
        {
            y1 = MM_block_begin_y[0];
            y2 = MM_block_begin_y[1];
            y3 = MM_block_begin_y[2];
        }
        else
        {
            y1 = MM_block_setAssociative_begin_y;
            y2 = MM_block_setAssociative_begin_y - (15f + gap) * 2;
            y3 = MM_block_setAssociative_begin_y - (15f + gap) * 5;
        }
        if(past == 1)
        {
            s.Join(direct_block_array[3 * (past - 1)].transform.DOMove(new Vector3(C_platform_begin.x-7f, y1,0f),1));
            s.Join(direct_block_array[3 * (past - 1) + 1].transform.DOMove(new Vector3(C_platform_begin.x-7f, y2,0f),1));
        }
        else
        {
            s.Join(direct_block_array[3 * (past - 1)].transform.DOMove(new Vector3(C_platform_begin.x-27f, y1,0f),1));
            s.Join(direct_block_array[3 * (past - 1) + 1].transform.DOMove(new Vector3(C_platform_begin.x-27f, y2,0f),1));
        }
        s.Join(direct_block_array[3 * (past - 1) + 2].transform.DOMove(new Vector3(C_platform_begin.x-27f, y3,0f),1));
        yield return new WaitForSeconds(1f);
        if(mappingType == 3)
            for(int i = 0; i < 6; ++ i) C_line_array[i].SetActive(false);
        yield return new WaitForSeconds(1f);

        for(int i = 3 * (past - 1); i < 3 * past; ++ i)
        {
            direct_blockPlatform_array[i].SetActive(false);
            direct_block_array[i].SetActive(false);
            MM_blockPlatform_origin_array[i].GetComponent<MeshRenderer>().material.color = c;
        }
        // for(int i = 0; i < 6; ++ i) C_line_array[i].SetActive(true);
    }

    void Init1()
    {
        MM_platform_array[0].GetComponent<MeshRenderer>().material.color = Color.red;
        MA_bit_array[23].GetComponent<TextMesh>().text = "<color=#FF3333>0</color>";
        MA_bit_array[22].GetComponent<TextMesh>().text = "<color=#FF3333>0</color>";
        MA_bit_array[21].GetComponent<TextMesh>().text = "<color=#FF3333>0</color>";
        for(int i=0; i<3; ++i)
        {
            C_brace_array[i].SetActive(false);
            C_line_array[i].SetActive(false);
            MM_block_array[i].SetActive(false);
            MM_brace_array[i].SetActive(false);
            C_tag_array[i].SetActive(false);
        }
    }
    void Init2()
    {
        Color c = MM_platform_array[0].GetComponent<MeshRenderer>().material.color;
        MM_platform_array[5].GetComponent<MeshRenderer>().material.color = c;
        MM_platform_array[0].GetComponent<MeshRenderer>().material.color = Color.red;
        MA_bit_array[23].GetComponent<TextMesh>().text = "<color=#FF3333>0</color>";
        MA_bit_array[22].GetComponent<TextMesh>().text = "<color=#FF3333>0</color>";
        MA_bit_array[21].GetComponent<TextMesh>().text = "<color=#FF3333>0</color>";
        MM_block_array[0].GetComponent<TextMesh>().text = "<color=#FF3333>B0</color>";
        MM_brace_array[0].GetComponent<TextMesh>().text = "<color=#FF3333>}</color>";
        MM_block_array[1].GetComponent<TextMesh>().text = "<color=#07F152>B1</color>";
        MM_brace_array[1].GetComponent<TextMesh>().text = "<color=#07F152>}</color>";
    }

    void Init3()
    {
        MA_bit_array[23].GetComponent<TextMesh>().text = "<color=#FFFFFF>0</color>";
        MA_bit_array[22].GetComponent<TextMesh>().text = "<color=#FFFFFF>0</color>";
        MA_bit_array[21].GetComponent<TextMesh>().text = "<color=#FFFFFF>0</color>";
        MM_block_array[1].GetComponent<TextMesh>().text = "<color=#07F152>B1</color>";
        MM_brace_array[1].GetComponent<TextMesh>().text = "<color=#07F152>}</color>";
        Color c = MM_platform_array[0].GetComponent<MeshRenderer>().material.color;
        MM_platform_array[5].GetComponent<MeshRenderer>().material.color = c;
    }

    void setTag(int tag)
    {
        // tag: MA_platform_array[0:8]
        if(tag >= 0 && tag < (1 << 8))
        {
            for(int i = 7; i >= 0; i --)
            {
                if((tag & 1) == 1)
                    MA_bit_array[i].GetComponent<TextMesh>().text = "<color=#FFFFFF>1</color>";
                else   
                    MA_bit_array[i].GetComponent<TextMesh>().text = "<color=#FFFFFF>0</color>";
                tag >>= 1;
            }
        }
    }

    void setLine(int line)
    {
        // line: MA_platform_array[8:22]
        if(line >= 0 && line < (1 << 14))
        {
            for(int i = 21; i >= 8; i --)
            {
                if((line & 1) == 1)
                    MA_bit_array[i].GetComponent<TextMesh>().text = "<color=#FFFFFF>1</color>";
                else   
                    MA_bit_array[i].GetComponent<TextMesh>().text = "<color=#FFFFFF>0</color>";
                line >>= 1;
            }
        }

        
    }

    void setTag1(int tag1)
    {
         //Tag1: MA_platform_array[0:22]
         if(tag1 >= 0 && tag1 < (1 << 22))
        {
            for(int i = 21; i >= 0; i--)
            {
                if((tag1 & 1) == 1)
                    MA_bit_array[i].GetComponent<TextMesh>().text = "<color=#FFFFFF>1</color>";
                else
                    MA_bit_array[i].GetComponent<TextMesh>().text = "<color=#FFFFFF>0</color>";
                tag1 >>= 1;
            }

        }
    }
    void setTag2(int tag2)
    {
        //Tag1: MA_platform_array[0:9]
        if (tag2 >= 0 && tag2 < (1 << 9))
        {
            for (int i = 8; i >= 0; i--)
            {
                if ((tag2 & 1) == 1)
                    MA_bit_array[i].GetComponent<TextMesh>().text = "<color=#FFFFFF>1</color>";
                else
                    MA_bit_array[i].GetComponent<TextMesh>().text = "<color=#FFFFFF>0</color>";
                tag2 >>= 1;
            }

        }
    }

    void setWord(int word)
    {
        // word: MA_platform_array[22:24]
        if(word >= 0 && word < (1 << 2))
        {
            for(int i = 23; i >= 22; i --)
            {
                if((word & 1) == 1)
                    MA_bit_array[i].GetComponent<TextMesh>().text = "<color=#FFFFFF>1</color>";
                else   
                    MA_bit_array[i].GetComponent<TextMesh>().text = "<color=#FFFFFF>0</color>";
                word >>= 1;
            }
        }
    }

    void setSet(int set)
    {
        // set: MA_platform_array[9:22]
        if (set >= 0 && set < (1 << 14))
        {
            for (int i = 21; i >= 9; i--)
            {
                if ((set & 1) == 1)
                    MA_bit_array[i].GetComponent<TextMesh>().text = "<color=#FFFFFF>1</color>";
                else
                    MA_bit_array[i].GetComponent<TextMesh>().text = "<color=#FFFFFF>0</color>";
                set >>= 1;
            }
        }
    }

    string int2bstring(int num, int length)
    {
        string s = "";
        for(int i = 0; i < length; i ++)
        {
            if((num & 1) == 1)
                s = "1" + s;
            else
                s = "0" + s;
            num >>= 1;
        }
        return s;
    }
    
    void Init4(int tag, int line, int word, bool isInCache)
    {
        // 初始化memory address内容，tag、line、word分别是地址二进制转十进制后的值，注意范围不要溢出
        // isInCache: 地址对应内容是否在cache里

        setTag(tag);
        setLine(line);
        setWord(word);

        // 之前隐藏的一些要setactive
        for(int i = 3; i < 6; ++ i)
        {
            C_ellipsisDot_array[i].SetActive(true);
        }
        for(int i = 18; i < 22; ++ i)
        {
            C_platform_array[i - 6].SetActive(true);
        }
        C_brace_array[3].SetActive(true);
        C_line_array[3].SetActive(true);

        // cache大括号文本修改
        C_line_array[2].GetComponent<TextMesh>().text = "L" + line.ToString();
        C_line_array[3].GetComponent<TextMesh>().text = "L(2^14-1)";

        // // deliminiter
        // MA_wordDelimiter = Instantiate(Text_MA_wordDelimiter_Prefab, MA_wordDelimiter_begin + new Vector3(-6, 0, 0), Quaternion.Euler(0, 0, 0));
        // MA_lineDelimiter = Instantiate(MA_lineDelimiter_Prefab, MA_lineDelimiter_begin + new Vector3(-6, 0, 0), Quaternion.Euler(0, 0, 0));

        // instanciate对应的line
        if(isInCache)   // 在cache里，设置cache tag和tag相同
        {
            C_tag_text_array[2].GetComponent<TextMesh>().text = tag.ToString();
        }
        else    // 不在cache里，设置cache tag和tag不同
        {
            int randTag;
            do{
                randTag = UnityEngine.Random.Range(0, 256);
            } while(randTag == tag);
            C_tag_text_array[2].GetComponent<TextMesh>().text = randTag.ToString();
        }
    }
    
    void Init5(int tag, int word, bool isInCache)
    {
        //初始化fully associated's tag and word
        setTag1(tag);
        setWord(word);

        // 之前隐藏的一些要setactive
        for (int i = 3; i < 6; ++i)
        {
            C_ellipsisDot_array[i].SetActive(true);
        }
        for (int i = 18; i < 22; ++i)
        {
            C_platform_array[i - 6].SetActive(true);
        }
        C_brace_array[3].SetActive(true);
        C_line_array[3].SetActive(true);

        // cache大括号文本修改
        int randTag = UnityEngine.Random.Range(0, 2^14-1);
        C_line_array[2].GetComponent<TextMesh>().text = "L" + randTag.ToString();
        C_line_array[3].GetComponent<TextMesh>().text = "L(2^14-1)";

        // deliminiter
        MA_wordDelimiter = Instantiate(Text_MA_wordDelimiter_Prefab, MA_wordDelimiter_begin + new Vector3(-6, 0, 0), Quaternion.Euler(0, 0, 0));
        MA_lineDelimiter = Instantiate(MA_lineDelimiter_Prefab, MA_lineDelimiter_begin + new Vector3(-6, 0, 0), Quaternion.Euler(0, 0, 0));

        // instanciate对应的tag
        if (isInCache)   // 在cache里，设置cache tag和tag相同
        {
            C_tag_text_array[2].GetComponent<TextMesh>().text = tag.ToString();
        }
        else    // 不在cache里，设置cache tag和tag不同
        {
            do
            {
                randTag = UnityEngine.Random.Range(0, 256);
            } while (randTag == tag);
            C_tag_text_array[2].GetComponent<TextMesh>().text = randTag.ToString();
        }

    }

    IEnumerator Init6(int tag, int set,int word, bool isInCache)
    {
        setTag2(tag);
        setSet(set);
        setWord(word);

        //之前隐藏的一些要setactive
        C_smallEllipsisDot_array[3] = Instantiate(Text_C_smallEllipsisDot_Prefab, C_smallEllipsisDot_begin + new Vector3(0f, -5f * 0 - (15f + gap) * 7, 0f), Quaternion.Euler(0, 0, 0));
        C_smallEllipsisDot_array[4] = Instantiate(Text_C_smallEllipsisDot_Prefab, C_smallEllipsisDot_begin + new Vector3(0f, -5f * 1 - (15f + gap) * 7, 0f), Quaternion.Euler(0, 0, 0));
        C_smallEllipsisDot_array[5] = Instantiate(Text_C_smallEllipsisDot_Prefab, C_smallEllipsisDot_begin + new Vector3(0f, -5f * 2 - (15f + gap) * 7, 0f), Quaternion.Euler(0, 0, 0));

        //C_platform_array[9].SetActive(true);
        Text_C_linePrefix_Prefab.transform.localScale = new Vector3(1f, 1f, 1f);
        C_line_array[9] = Instantiate(Text_C_linePrefix_Prefab, new Vector3(C_line_begin.x - 78f, C_platform_begin.y + 5f, 0f) - new Vector3(0f, (15f + gap) * (6 + 2), 0f), Quaternion.Euler(0, 0, 0));
        C_line_array[10] = Instantiate(Text_C_linePrefix_Prefab, new Vector3(C_line_begin.x - 78f, C_platform_begin.y + 5f, 0f) - new Vector3(0f, (15f + gap) * (7 + 2), 0f), Quaternion.Euler(0, 0, 0));
       

        //C_platform_array[10].SetActive(true);

        C_braceSet_array[3] = Instantiate(Text_brace_set_Prefab, C_braceSet_begin - new Vector3(0f, (15f + gap) * 8, 0f), Quaternion.Euler(0, 0, 0));
        C_set_array[3] = Instantiate(Text_C_setPrefix_Prefab, C_set_begin - new Vector3(0f, (15f + gap) * 8, 0f), Quaternion.Euler(0, 0, 0));

        C_line_array[4].GetComponent<TextMesh>().text = "L(2*(679+1)-2)";
        C_line_array[5].GetComponent<TextMesh>().text = "L(2*(679+1)-1)";



        MM_blockPlatform_origin_Prefab.transform.localScale = new Vector3(60f, 15f, 10f);
        //s.Join(C_linePlatform_array[5].transform.DOMove(new Vector3(C_platform_begin.x, C_platform_begin.y - 2.5f, 0f) - new Vector3(0f, (15f + gap) * 6, 0f), 1));
        C_linePlatform_array[6] = Instantiate(MM_blockPlatform_origin_Prefab, new Vector3(C_platform_begin.x, C_platform_begin.y - 2.5f, 0f) - new Vector3(0f, (15f + gap) * 8, 0f), Quaternion.Euler(0, 0, 0));
        C_linePlatform_array[7] = Instantiate(MM_blockPlatform_origin_Prefab, new Vector3(C_platform_begin.x, C_platform_begin.y - 2.5f, 0f) - new Vector3(0f, (15f + gap) * 9, 0f), Quaternion.Euler(0, 0, 0));
        C_line_array[9].GetComponent<TextMesh>().text = "L(2^14-2)";
        C_line_array[10].GetComponent<TextMesh>().text = "L(2^14-1)";

        //// cache大括号文本修改
        C_set_array[2].GetComponent<TextMesh>().text = "S" + set.ToString();
        C_set_array[3].GetComponent<TextMesh>().text = "S(2^13-1)";

        yield return new WaitForSeconds(1f);
        C_smallEllipsisDot_array[3].SetActive(false);
        C_smallEllipsisDot_array[4].SetActive(false);
        C_smallEllipsisDot_array[5].SetActive(false);
        C_line_array[9].SetActive(false);
        C_line_array[10].SetActive(false);
        C_linePlatform_array[6].SetActive(false);
        C_linePlatform_array[7].SetActive(false);
        C_set_array[3].SetActive(false);
        C_braceSet_array[3].SetActive(false);

  
        C_linePlatform_array[8] = Instantiate(MM_blockPlatform_origin_Prefab, new Vector3(C_platform_begin.x, C_platform_begin.y - 2.5f, 0f) - new Vector3(0f, (15f + gap) * 7, 0f), Quaternion.Euler(0, 0, 0));
        C_linePlatform_array[9] = Instantiate(MM_blockPlatform_origin_Prefab, new Vector3(C_platform_begin.x, C_platform_begin.y - 2.5f, 0f) - new Vector3(0f, (15f + gap) * 8, 0f), Quaternion.Euler(0, 0, 0));
        C_linePlatform_array[10] = Instantiate(MM_blockPlatform_origin_Prefab, new Vector3(C_platform_begin.x, C_platform_begin.y - 2.5f, 0f) - new Vector3(0f, (15f + gap) * 9, 0f), Quaternion.Euler(0, 0, 0));

        yield return new WaitForSeconds(4f);
        C_linePlatform_array[8].GetComponent<MeshRenderer>().material.color = Color.red;
        if (isInCache)   // 在cache里，设置cache tag和tag相同
        {
            C_tag_text_array[2].GetComponent<TextMesh>().text = tag.ToString();
        }
        else    // 不在cache里，设置cache tag和tag不同
        {
            int randTag;
            do
            {
                randTag = UnityEngine.Random.Range(0, 256);
            } while (randTag == tag);
            C_tag_text_array[2].GetComponent<TextMesh>().text = randTag.ToString();
        }

        yield return new WaitForSeconds(7f);
        C_linePlatform_array[8].GetComponent<MeshRenderer>().material.color = Color.grey;

        //// deliminiter
        //MA_wordDelimiter = Instantiate(Text_MA_wordDelimiter_Prefab, MA_wordDelimiter_begin + new Vector3(-6, 0, 0), Quaternion.Euler(0, 0, 0));
        //MA_lineDelimiter = Instantiate(MA_lineDelimiter_Prefab, MA_lineDelimiter_begin + new Vector3(-6, 0, 0), Quaternion.Euler(0, 0, 0));

        // instanciate对应的line
        if (isInCache)   // 在cache里，设置cache tag和tag相同
        {
            C_tag_text_array[2].GetComponent<TextMesh>().text = tag.ToString();
        }
        else    // 不在cache里，设置cache tag和tag不同
        {
            int randTag;
            do
            {
                randTag = UnityEngine.Random.Range(0, 256);
            } while (randTag == tag);
            C_tag_text_array[2].GetComponent<TextMesh>().text = randTag.ToString();
        }
    }
    IEnumerator Procedure2_1()
    {
        s.Append(MA_platform_array[23].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap + 10f) * 24,0f,0f),1));
        s.Join(MA_bit_array[23].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap + 10f) * 24 - 3.5f,7f,0f),1));
        s.Join(MA_platform_array[22].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap + 10f) * 23,0f,0f),1));
        s.Join(MA_bit_array[22].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap + 10f) * 23 - 3.5f,7f,0f),1));
        yield return new WaitForSeconds(1f);
        MA_wordDelimiter = Instantiate(Text_MA_wordDelimiter_Prefab, MA_platform_array[0].transform.position + new Vector3(261f,15f,0f), Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(1f);
        MA_textBlock = Instantiate(Text_MA_block_Prefab, MA_textBlock_begin, Quaternion.Euler(0, 0, 0));
        MA_textWord = Instantiate(Text_MA_word_Prefab, MA_textWord_begin, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(1f);
        for(int i=0; i<3; ++i)
        {
            C_brace_array[i].SetActive(true);
            C_line_array[i].SetActive(true);
            MM_block_array[i].SetActive(true);
            MM_brace_array[i].SetActive(true);
            C_tag_array[i].SetActive(true);
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Procedure1(int anim, int past, int flag21, int flag22, int flag23)
    {
        int time = (past-1) * 6;
        s.Append(MA_platform_array[23].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap+10f)*25,-29f,0f),1));
        s.Join(MA_bit_array[23].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap+10f)*25- 3.5f,-22f,0f),1));
        s.Join(MA_platform_array[22].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap+10f)*22,-29f,0f),1));
        s.Join(MA_bit_array[22].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap+10f)*22- 3.5f,-22f,0f),1));
        s.Join(MA_platform_array[21].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap+10f)*19,-29f,0f),1));
        s.Join(MA_bit_array[21].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap+10f)*19- 3.5f,-22f,0f),1));

        yield return new WaitForSeconds(1f);
        
        if(flag23 == 1)
        {
            s.Append(MA_platform_array[23].transform.DOScale(new Vector3(20, 20, 20), 1));
            //文字飞出
            s.Join(MA_bit_array[23].transform.DOScale(new Vector3(2,2,2),1));
            s.Join(MA_bit_array[23].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap+10f)*25-6f,-16.5f,0f),1));
            if(flag22 == 1)
            {
                s.Join(MA_platform_array[22].transform.DOScale(new Vector3(20, 20, 20), 1));
                s.Join(MA_bit_array[22].transform.DOScale(new Vector3(2,2,2),1));
                s.Join(MA_bit_array[22].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap+10f)*22-6.5f,-16.5f,0f),1));
            }
            if(flag21 == 1)
            {
                s.Join(MA_platform_array[21].transform.DOScale(new Vector3(20, 20, 20), 1));
                s.Join(MA_bit_array[21].transform.DOScale(new Vector3(2,2,2),1));
                s.Join(MA_bit_array[21].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap+10f)*19-7f,-16.5f,0f),1));
            }

            yield return new WaitForSeconds(1f);

            if(MA_platform_array[23].transform.localEulerAngles.x != 0)
            {
                s.Append(MA_platform_array[23].transform.DORotate(new Vector3(0, 0, 0), 2));
            }
            else
            {
                s.Append(MA_platform_array[23].transform.DORotate(new Vector3(180, 0, 0), 2));
            }
            //Debug.Log(MA_platform_array[23].transform.localEulerAngles.x);
            if(flag22 == 1)
            {
                if(MA_platform_array[22].transform.localEulerAngles.x != 0)
                {
                    s.Join(MA_platform_array[22].transform.DORotate(new Vector3(0, 0, 0), 2));
                }
                else
                {
                    s.Join(MA_platform_array[22].transform.DORotate(new Vector3(180, 0, 0), 2));
                }
            }
            if(flag21 == 1)
            {
                if(MA_platform_array[21].transform.localEulerAngles.x != 0)
                {
                    s.Join(MA_platform_array[21].transform.DORotate(new Vector3(0, 0, 0), 2));
                }
                else
                {
                    s.Join(MA_platform_array[21].transform.DORotate(new Vector3(180, 0, 0), 2));
                }
            }
                     
            yield return new WaitForSeconds(1f);

            TextMesh t1 = MA_bit_array[23].GetComponent<TextMesh>();
            t1.text = (t1.text == "<color=#FF3333>1</color>") ? "<color=#FF3333>0</color>" : "<color=#FF3333>1</color>";
            Color c = MM_platform_array[past].GetComponent<MeshRenderer>().material.color;
            MM_platform_array[past-1].GetComponent<MeshRenderer>().material.color = c;
            if(flag22 == 1)
            {
                TextMesh t2 = MA_bit_array[22].GetComponent<TextMesh>();
                t2.text = (t2.text == "<color=#FF3333>1</color>") ? "<color=#FF3333>0</color>" : "<color=#FF3333>1</color>";
            }
            if(flag21 == 1)
            {
                TextMesh t3 = MA_bit_array[21].GetComponent<TextMesh>();
                t3.text = (t3.text == "<color=#FF3333>1</color>") ? "<color=#FF3333>0</color>" : "<color=#FF3333>1</color>";
            }

            yield return new WaitForSeconds(0.5f);
            if(past == 4)
            {
                MM_block_array[1].GetComponent<TextMesh>().text = "<color=#FF3333>B1</color>";
                MM_brace_array[1].GetComponent<TextMesh>().text = "<color=#FF3333>}</color>";
                MM_block_array[0].GetComponent<TextMesh>().text = "<color=#07F152>B0</color>";
                MM_brace_array[0].GetComponent<TextMesh>().text = "<color=#07F152>}</color>";
            }
            MM_platform_array[past].GetComponent<MeshRenderer>().material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            MM_platform_array[past].GetComponent<MeshRenderer>().material.color = c;
            yield return new WaitForSeconds(0.1f);
            MM_platform_array[past].GetComponent<MeshRenderer>().material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            MM_platform_array[past].GetComponent<MeshRenderer>().material.color = c;
            yield return new WaitForSeconds(0.1f);
            MM_platform_array[past].GetComponent<MeshRenderer>().material.color = Color.red;
            yield return new WaitForSeconds(0.1f);

            s.Append(MA_platform_array[23].transform.DOScale(new Vector3(10, 10, 10), 1));
            s.Join(MA_bit_array[23].transform.DOScale(new Vector3(1,1,1),1));
            s.Join(MA_bit_array[23].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap+10f)*25- 3.5f,-22f,0f),1));
            //
            if(flag22 == 1)
            {
                s.Join(MA_platform_array[22].transform.DOScale(new Vector3(10, 10, 10), 1));           
                s.Join(MA_bit_array[22].transform.DOScale(new Vector3(1,1,1),1));
                s.Join(MA_bit_array[22].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap+10f)*22- 3.5f,-22f,0f),1));
            }
            if(flag21 == 1)
            {
                s.Join(MA_platform_array[21].transform.DOScale(new Vector3(10, 10, 10), 1));           
                s.Join(MA_bit_array[21].transform.DOScale(new Vector3(1,1,1),1));
                s.Join(MA_bit_array[21].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap+10f)*19- 3.5f,-22f,0f),1));
            }

            yield return new WaitForSeconds(1f);
        }
        if(anim == 1)
        {
            s.Append(MA_platform_array[23].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap+10f)*23,0f,0f),1));
            s.Join(MA_bit_array[23].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap+10f)*23- 3.5f,7f,0f),1));       
            s.Join(MA_platform_array[22].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap+10f)*22,0f,0f),1));
            s.Join(MA_bit_array[22].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap+10f)*22- 3.5f,7f,0f),1));      
            s.Join(MA_platform_array[21].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap+10f)*21,0f,0f),1));
            s.Join(MA_bit_array[21].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap+10f)*21 - 3.5f,7f,0f),1));
        }
        else if(anim == 2)
        {
            s.Append(MA_platform_array[23].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap + 10f) * 24,0f,0f),1));
            s.Join(MA_bit_array[23].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap + 10f) * 24 - 3.5f,7f,0f),1));
            s.Join(MA_platform_array[22].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap + 10f) * 23,0f,0f),1));
            s.Join(MA_bit_array[22].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap + 10f) * 23 - 3.5f,7f,0f),1));
            s.Join(MA_platform_array[21].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap+10f)*21,0f,0f),1));
            s.Join(MA_bit_array[21].transform.DOMove(MA_platform_array[0].transform.position + new Vector3((gap+10f)*21 - 3.5f,7f,0f),1));
        }

        yield return new WaitForSeconds(1f);
    }

    IEnumerator checkLine(int line)
    {
        // Line下移
        float offset = 30;
        Vector3 tmp1 = MA_platform_array[8].transform.position;
        s.Append(MA_platform_array[8].transform.DOMove(tmp1 + new Vector3(0, -offset, 0), 1));
        Vector3 tmp2 = MA_bit_array[8].transform.position;
        s.Join(MA_bit_array[8].transform.DOMove(tmp2 + new Vector3(0, -offset, 0), 1));
        for(int i = 9; i < 22; i ++)
        {
            tmp1 = MA_platform_array[i].transform.position;
            s.Join(MA_platform_array[i].transform.DOMove(tmp1 + new Vector3(0, -offset, 0), 1));
            tmp2 = MA_bit_array[i].transform.position;
            s.Join(MA_bit_array[i].transform.DOMove(tmp2 + new Vector3(0, -offset, 0), 1));
        }

        yield return new WaitForSeconds(1f);

        // 显示十进制值
        GameObject decimalVal = Instantiate(Text_MA_bit_Prefab, (MA_bit_array[14].transform.position + MA_bit_array[15].transform.position) / 2 + new Vector3(0, -15, 0), Quaternion.Euler(0, 0, 0));
        string tmpStr = "<color=#0000FF>" + line.ToString() + "</color>";
        decimalVal.GetComponent<TextMesh>().text = tmpStr;

        yield return new WaitForSeconds(1f);

        // 对应的line变蓝
        for(int i = 11; i < 15; ++ i)
        {
            C_platform_array[i - 3].GetComponent<MeshRenderer>().material.color = Color.blue;
        }

        yield return new WaitForSeconds(1f);

        // line回去
        tmp1 = MA_platform_array[8].transform.position;
        s.Append(MA_platform_array[8].transform.DOMove(tmp1 + new Vector3(0, offset, 0), 1));
        tmp2 = MA_bit_array[8].transform.position;
        s.Join(MA_bit_array[8].transform.DOMove(tmp2 + new Vector3(0, offset, 0), 1));
        for(int i = 9; i < 22; i ++)
        {
            tmp1 = MA_platform_array[i].transform.position;
            s.Join(MA_platform_array[i].transform.DOMove(tmp1 + new Vector3(0, offset, 0), 1));
            tmp2 = MA_bit_array[i].transform.position;
            s.Join(MA_bit_array[i].transform.DOMove(tmp2 + new Vector3(0, offset, 0), 1));
        }

        // 十进制消失
        for(int i = 11; i < 15; i ++)
        {
            C_platform_array[i - 3].GetComponent<MeshRenderer>().material.color = C_platform_array[0].GetComponent<MeshRenderer>().material.color;
        }
        decimalVal.SetActive(false);

        yield return new WaitForSeconds(1f);
    }

    IEnumerator checkSet(int set)

    {
        Vector3 newLinePosition = new Vector3(485f,66f,0f);
        // Set下移
        float offset = 80;
        Vector3 tmp1 = MA_platform_array[9].transform.position;
        s.Append(MA_platform_array[9].transform.DOMove(tmp1 + new Vector3(0, -offset, 0), 1));
        Vector3 tmp2 = MA_bit_array[9].transform.position;
        s.Join(MA_bit_array[9].transform.DOMove(tmp2 + new Vector3(0, -offset, 0), 1));
        for (int i = 9; i < 22; i++)
        {
            tmp1 = MA_platform_array[i].transform.position;
            s.Join(MA_platform_array[i].transform.DOMove(tmp1 + new Vector3(0, -offset, 0), 1));
            tmp2 = MA_bit_array[i].transform.position;
            s.Join(MA_bit_array[i].transform.DOMove(tmp2 + new Vector3(0, -offset, 0), 1));
        }
        C_set_array[2].SetActive(false);
        C_braceSet_array[2].SetActive(false);
        C_brace_wordInLine = Instantiate(Text_brace_wordInLine_Prefab,C_brace_wordInLine_begin,Quaternion.Euler(0, 0, 0));
        s.Join(C_line_array[5].transform.DOMove(newLinePosition, 1));

        yield return new WaitForSeconds(1f);

        // 显示十进制值
        GameObject decimalVal = Instantiate(Text_MA_bit_Prefab, (MA_bit_array[14].transform.position + MA_bit_array[15].transform.position) / 2 + new Vector3(0, -15, 0), Quaternion.Euler(0, 0, 0));
        string tmpStr = "<color=#0000FF>" + set.ToString() + "</color>";
        decimalVal.GetComponent<TextMesh>().text = tmpStr;

        yield return new WaitForSeconds(1f);

        // 对应的Set变蓝

        C_set_array[2].GetComponent<MeshRenderer>().material.color = Color.blue;
      
        yield return new WaitForSeconds(1f);

        // Set回去
        tmp1 = MA_platform_array[9].transform.position;
        s.Append(MA_platform_array[9].transform.DOMove(tmp1 + new Vector3(0, offset, 0), 1));
        tmp2 = MA_bit_array[9].transform.position;
        s.Join(MA_bit_array[9].transform.DOMove(tmp2 + new Vector3(0, offset, 0), 1));

        for (int i = 9; i < 22; i++)
        {
            tmp1 = MA_platform_array[i].transform.position;
            s.Join(MA_platform_array[i].transform.DOMove(tmp1 + new Vector3(0, offset, 0), 1));
            tmp2 = MA_bit_array[i].transform.position;
            s.Join(MA_bit_array[i].transform.DOMove(tmp2 + new Vector3(0, offset, 0), 1));
        }

        // 十进制消失
        for (int i = 11; i < 15; i++)
        {
            C_platform_array[i - 3].GetComponent<MeshRenderer>().material.color = C_platform_array[0].GetComponent<MeshRenderer>().material.color;
        }
        decimalVal.SetActive(false);

        yield return new WaitForSeconds(1f);
    }

    IEnumerator checkTag(int tag, bool isInCache)
    {
        // tag下移
        float offset = 30;
        Vector3 tmp1 = MA_platform_array[0].transform.position;
        s.Append(MA_platform_array[0].transform.DOMove(tmp1 + new Vector3(0, -offset, 0), 1));
        Vector3 tmp2 = MA_bit_array[0].transform.position;
        s.Join(MA_bit_array[0].transform.DOMove(tmp2 + new Vector3(0, -offset, 0), 1));
        for(int i = 0; i < 8; i ++)
        {
            tmp1 = MA_platform_array[i].transform.position;
            s.Join(MA_platform_array[i].transform.DOMove(tmp1 + new Vector3(0, -offset, 0), 1));
            tmp2 = MA_bit_array[i].transform.position;
            s.Join(MA_bit_array[i].transform.DOMove(tmp2 + new Vector3(0, -offset, 0), 1));
        }

        yield return new WaitForSeconds(1f);

        // 显示tag十进制值
        GameObject decimalVal = Instantiate(Text_MA_bit_Prefab, (MA_bit_array[3].transform.position + MA_bit_array[4].transform.position) / 2 + new Vector3(0, -15, 0), Quaternion.Euler(0, 0, 0));
        string tmpStr = "<color=#FF0000>" + tag.ToString() + "</color>";
        decimalVal.GetComponent<TextMesh>().text = tmpStr;

        yield return new WaitForSeconds(1f);

        // 对应tag变红
        C_tag_array[2].GetComponent<MeshRenderer>().material.color = Color.red;

        yield return new WaitForSeconds(1f);

        // tag回去
        tmp1 = MA_platform_array[0].transform.position;
        s.Append(MA_platform_array[0].transform.DOMove(tmp1 + new Vector3(0, offset, 0), 1));
        tmp2 = MA_bit_array[0].transform.position;
        s.Join(MA_bit_array[0].transform.DOMove(tmp2 + new Vector3(0, offset, 0), 1));
        for(int i = 0; i < 8; i ++)
        {
            tmp1 = MA_platform_array[i].transform.position;
            s.Join(MA_platform_array[i].transform.DOMove(tmp1 + new Vector3(0, offset, 0), 1));
            tmp2 = MA_bit_array[i].transform.position;
            s.Join(MA_bit_array[i].transform.DOMove(tmp2 + new Vector3(0, offset, 0), 1));
        }

        // 十进制移动到cache tag旁边
        // decimalVal.SetActive(false);
        s.Join(decimalVal.transform.DOMove(C_tag_array[2].transform.position + new Vector3(-50, 6, 0), 1));

        yield return new WaitForSeconds(1f);

        // 比较tag
        if(isInCache)
            decimalVal.GetComponent<TextMesh>().text += " == ";
        else
            decimalVal.GetComponent<TextMesh>().text += " != ";
        yield return new WaitForSeconds(2f);

        // cache tag颜色变回去
        C_tag_array[2].GetComponent<MeshRenderer>().material.color = C_tag_array[1].GetComponent<MeshRenderer>().material.color;
        decimalVal.SetActive(false);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator checkTag1(int tag, bool isInCache)
    {
        // tag下移
        float offset = 80;
        Vector3 tmp1 = MA_platform_array[0].transform.position;
        s.Append(MA_platform_array[0].transform.DOMove(tmp1 + new Vector3(0, -offset, 0), 1));
        Vector3 tmp2 = MA_bit_array[0].transform.position;
        s.Join(MA_bit_array[0].transform.DOMove(tmp2 + new Vector3(0, -offset, 0), 1));
        for (int i = 0; i < 22; i++)
        {
            tmp1 = MA_platform_array[i].transform.position;
            s.Join(MA_platform_array[i].transform.DOMove(tmp1 + new Vector3(0, -offset, 0), 1));
            tmp2 = MA_bit_array[i].transform.position;
            s.Join(MA_bit_array[i].transform.DOMove(tmp2 + new Vector3(0, -offset, 0), 1));
        }

        yield return new WaitForSeconds(1f);

        // 显示tag十进制值
        GameObject decimalVal = Instantiate(Text_MA_bit_Prefab, (MA_bit_array[3].transform.position + MA_bit_array[4].transform.position) / 2 + new Vector3(0, -15, 0), Quaternion.Euler(0, 0, 0));
        string tmpStr = "<color=#FF0000>" + tag.ToString() + "</color>";
        decimalVal.GetComponent<TextMesh>().text = tmpStr;

        yield return new WaitForSeconds(1f);

        // 对应tag变红
        C_tag_array[2].GetComponent<MeshRenderer>().material.color = Color.red;

        yield return new WaitForSeconds(1f);

        // tag回去
        tmp1 = MA_platform_array[0].transform.position;
        s.Append(MA_platform_array[0].transform.DOMove(tmp1 + new Vector3(0, offset, 0), 1));
        tmp2 = MA_bit_array[0].transform.position;
        s.Join(MA_bit_array[0].transform.DOMove(tmp2 + new Vector3(0, offset, 0), 1));
        for (int i = 0; i < 22; i++)
        {
            tmp1 = MA_platform_array[i].transform.position;
            s.Join(MA_platform_array[i].transform.DOMove(tmp1 + new Vector3(0, offset, 0), 1));
            tmp2 = MA_bit_array[i].transform.position;
            s.Join(MA_bit_array[i].transform.DOMove(tmp2 + new Vector3(0, offset, 0), 1));
        }

        // 十进制移动到cache tag旁边
        // decimalVal.SetActive(false);
        s.Join(decimalVal.transform.DOMove(C_tag_array[2].transform.position + new Vector3(-50, 6, 0), 1));

        yield return new WaitForSeconds(1f);

        // 比较tag
        if (isInCache)
            decimalVal.GetComponent<TextMesh>().text += " == ";
        else
            decimalVal.GetComponent<TextMesh>().text += " != ";
        yield return new WaitForSeconds(2f);

        // cache tag颜色变回去
        C_tag_array[2].GetComponent<MeshRenderer>().material.color = C_tag_array[1].GetComponent<MeshRenderer>().material.color;
        decimalVal.SetActive(false);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator checkTag2(int tag, bool isInCache)
    {
        // tag下移
        float offset = 80;
        Vector3 tmp1 = MA_platform_array[0].transform.position;
        s.Append(MA_platform_array[0].transform.DOMove(tmp1 + new Vector3(0, -offset, 0), 1));
        Vector3 tmp2 = MA_bit_array[0].transform.position;
        s.Join(MA_bit_array[0].transform.DOMove(tmp2 + new Vector3(0, -offset, 0), 1));
        for (int i = 0; i < 9; i++)
        {
            tmp1 = MA_platform_array[i].transform.position;
            s.Join(MA_platform_array[i].transform.DOMove(tmp1 + new Vector3(0, -offset, 0), 1));
            tmp2 = MA_bit_array[i].transform.position;
            s.Join(MA_bit_array[i].transform.DOMove(tmp2 + new Vector3(0, -offset, 0), 1));
        }

        yield return new WaitForSeconds(1f);

        // 显示tag十进制值
        GameObject decimalVal = Instantiate(Text_MA_bit_Prefab, (MA_bit_array[3].transform.position + MA_bit_array[4].transform.position) / 2 + new Vector3(0, -15, 0), Quaternion.Euler(0, 0, 0));
        string tmpStr = "<color=#FF0000>" + tag.ToString() + "</color>";
        decimalVal.GetComponent<TextMesh>().text = tmpStr;

        yield return new WaitForSeconds(1f);

        // 对应tag变红
        C_tag_array[3].GetComponent<MeshRenderer>().material.color = Color.red;

        yield return new WaitForSeconds(1f);

        // tag回去
        tmp1 = MA_platform_array[0].transform.position;
        s.Append(MA_platform_array[0].transform.DOMove(tmp1 + new Vector3(0, offset, 0), 1));
        tmp2 = MA_bit_array[0].transform.position;
        s.Join(MA_bit_array[0].transform.DOMove(tmp2 + new Vector3(0, offset, 0), 1));
        for (int i = 0; i < 9; i++)
        {
            tmp1 = MA_platform_array[i].transform.position;
            s.Join(MA_platform_array[i].transform.DOMove(tmp1 + new Vector3(0, offset, 0), 1));
            tmp2 = MA_bit_array[i].transform.position;
            s.Join(MA_bit_array[i].transform.DOMove(tmp2 + new Vector3(0, offset, 0), 1));
        }

        // 十进制移动到cache tag旁边
        // decimalVal.SetActive(false);
        s.Join(decimalVal.transform.DOMove(C_tag_array[2].transform.position + new Vector3(-50, 6, 0), 1));

        yield return new WaitForSeconds(1f);

        // 比较tag
        if (isInCache)
            decimalVal.GetComponent<TextMesh>().text += " == ";
        else
            decimalVal.GetComponent<TextMesh>().text += " != ";
        yield return new WaitForSeconds(2f);

        // cache tag颜色变回去
        C_tag_array[2].GetComponent<MeshRenderer>().material.color = C_tag_array[1].GetComponent<MeshRenderer>().material.color;
        decimalVal.SetActive(false);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator checkWord(int word)
    {
        // word下移
        float offset = 30;
        Vector3 tmp1 = MA_platform_array[22].transform.position;
        s.Append(MA_platform_array[22].transform.DOMove(tmp1 + new Vector3(0, -offset, 0), 1));
        Vector3 tmp2 = MA_bit_array[22].transform.position;
        s.Join(MA_bit_array[22].transform.DOMove(tmp2 + new Vector3(0, -offset, 0), 1));
        tmp1 = MA_platform_array[23].transform.position;
        s.Join(MA_platform_array[23].transform.DOMove(tmp1 + new Vector3(0, -offset, 0), 1));
        tmp2 = MA_bit_array[23].transform.position;
        s.Join(MA_bit_array[23].transform.DOMove(tmp2 + new Vector3(0, -offset, 0), 1));

        yield return new WaitForSeconds(1f);

        // 显示word十进制值
        GameObject decimalVal = Instantiate(Text_MA_bit_Prefab, (MA_bit_array[22].transform.position + MA_bit_array[23].transform.position) / 2 + new Vector3(0, -15, 0), Quaternion.Euler(0, 0, 0));
        string tmpStr = "<color=#000000>" + word.ToString() + "</color>";
        decimalVal.GetComponent<TextMesh>().text = tmpStr;

        yield return new WaitForSeconds(1f);

        // word回去
        tmp1 = MA_platform_array[22].transform.position;
        s.Append(MA_platform_array[22].transform.DOMove(tmp1 + new Vector3(0, offset, 0), 1));
        tmp2 = MA_bit_array[22].transform.position;
        s.Join(MA_bit_array[22].transform.DOMove(tmp2 + new Vector3(0, offset, 0), 1));
        tmp1 = MA_platform_array[23].transform.position;
        s.Join(MA_platform_array[23].transform.DOMove(tmp1 + new Vector3(0, offset, 0), 1));
        tmp2 = MA_bit_array[23].transform.position;
        s.Join(MA_bit_array[23].transform.DOMove(tmp2 + new Vector3(0, offset, 0), 1));

        // 对应
        for(int i = 0; i <= word; i ++)
        {
            if(i > 0)
                C_platform_array[7 + i].GetComponent<MeshRenderer>().material.color = C_platform_array[8 + i].GetComponent<MeshRenderer>().material.color;
            C_platform_array[8 + i].GetComponent<MeshRenderer>().material.color = Color.blue;
            yield return new WaitForSeconds(1f);
        }

        C_platform_array[8 + word].GetComponent<MeshRenderer>().material.color = C_platform_array[0].GetComponent<MeshRenderer>().material.color;
        yield return new WaitForSeconds(1f);

        decimalVal.SetActive(false);
    }

    IEnumerator checkMainMemory(int tag, int line, int word)
    {
        // 去除delimiter
        MA_lineDelimiter.SetActive(false);
        MA_wordDelimiter.SetActive(false);

        // 整段地址下移
        float offset = 30;
        Vector3 tmp1 = MA_platform_array[0].transform.position;
        s.Append(MA_platform_array[0].transform.DOMove(tmp1 + new Vector3(-30, -offset, 0), 1));
        Vector3 tmp2 = MA_bit_array[0].transform.position;
        s.Join(MA_bit_array[0].transform.DOMove(tmp2 + new Vector3(-30, -offset, 0), 1));
        for(int i = 0; i < 24; i ++)
        {
            tmp1 = MA_platform_array[i].transform.position;
            s.Join(MA_platform_array[i].transform.DOMove(tmp1 + new Vector3(i / 4 * 10 - 30, -offset, 0), 1));
            tmp2 = MA_bit_array[i].transform.position;
            s.Join(MA_bit_array[i].transform.DOMove(tmp2 + new Vector3(i / 4 * 10 - 30, -offset, 0), 1));
        }

        yield return new WaitForSeconds(1f);

        GameObject[] hexAddress = new GameObject[10];
        hexAddressString = "";
        // 计算十六进制的地址 & 显示
        int address = (tag << 16) + (line << 2) + word;
        for(int i = 0; i < 6; i ++)
        {
            int sum = (address & 1) + (address & 2) + (address & 4) + (address & 8);
            hexAddress[i] = Instantiate(Text_MA_bit_Prefab, (MA_bit_array[24 - i * 4 - 3].transform.position + MA_bit_array[24 - i * 4 - 2].transform.position) / 2 + new Vector3(0, -15, 0), Quaternion.Euler(0, 0, 0));
            string tmpStr;
            if(sum <= 9)
            {
                tmpStr = "<color=#000000>" + sum.ToString() + "</color>";
                hexAddressString = sum.ToString() + hexAddressString;
            }
            else
            {
                tmpStr = "<color=#000000>" + (char)(sum - 10 + 'A') + "</color>";
                hexAddressString = (char)(sum - 10 + 'A') + hexAddressString;
            }
            hexAddress[i].GetComponent<TextMesh>().text = tmpStr;
            address >>= 4;
        }

        yield return new WaitForSeconds(1f);

        // 地址回到原位
        tmp1 = MA_platform_array[0].transform.position;
        s.Append(MA_platform_array[0].transform.DOMove(tmp1 - new Vector3(-30, -offset, 0), 1));
        tmp2 = MA_bit_array[0].transform.position;
        s.Join(MA_bit_array[0].transform.DOMove(tmp2 - new Vector3(-30, -offset, 0), 1));
        for(int i = 0; i < 24; i ++)
        {
            tmp1 = MA_platform_array[i].transform.position;
            s.Join(MA_platform_array[i].transform.DOMove(tmp1 - new Vector3(i / 4 * 10 - 30, -offset, 0), 1));
            tmp2 = MA_bit_array[i].transform.position;
            s.Join(MA_bit_array[i].transform.DOMove(tmp2 - new Vector3(i / 4 * 10 - 30, -offset, 0), 1));
        }

        // 十六进制地址靠拢
        tmp1 = hexAddress[0].transform.position;
        s.Join(hexAddress[0].transform.DOMove(tmp1 + new Vector3(-120, 0, 0), 1));
        tmp2 = hexAddress[0].transform.position;
        s.Join(hexAddress[0].transform.DOMove(tmp2 + new Vector3(-120, 0, 0), 1));
        for(int i = 1; i < 6; i ++)
        {
            tmp1 = hexAddress[i].transform.position;
            s.Join(hexAddress[i].transform.DOMove(tmp1 + new Vector3(i * 40 - 120, 0, 0), 1));
            tmp2 = hexAddress[i].transform.position;
            s.Join(hexAddress[i].transform.DOMove(tmp2 + new Vector3(i * 40 - 120, 0, 0), 1));
        }

        yield return new WaitForSeconds(1f);

        // 显示main memory里对应的word
        MM_ellipsisDot_array[1].SetActive(false);
        word_in_MM.SetActive(true);
        word_text_in_MM.SetActive(true);
        word_text_in_MM.transform.position += new Vector3(-25, 6, 0);
        word_text_in_MM.GetComponent<TextMesh>().text = hexAddressString + "(H)";
        word_in_MM.GetComponent<MeshRenderer>().material.color = Color.green;

        yield return new WaitForSeconds(1f);

        // 十六进制地址消失
        for(int i = 0; i < 6; i ++)
        {
            hexAddress[i].SetActive(false);
            word_text_in_MM.SetActive(false);
            MM_ellipsisDot_array[1].SetActive(true);
        }

        yield return new WaitForSeconds(1f);

        // main memory 的内容移到cache里
        s.Append(word_in_MM.transform.DOMove(C_platform_array[8 + word].transform.position + new Vector3(0, 0, -0.5f), 1));

        yield return new WaitForSeconds(1f);

        // tag 修改
        C_tag_text_array[2].GetComponent<TextMesh>().text = tag.ToString();
        C_tag_array[2].GetComponent<MeshRenderer>().material.color = Color.green;
    }

    void end4(int tag, int line, int word)
    {
        word_in_MM.transform.position = MM_platform_begin - new Vector3(0,(gap + 10f) * 9, 0f);
        word_in_MM.SetActive(false);

        C_tag_array[2].GetComponent<MeshRenderer>().material.color = C_tag_array[0].GetComponent<MeshRenderer>().material.color;
        C_tag_text_array[2].GetComponent<TextMesh>().text = "";

        for(int i = 3; i < 6; ++ i)
        {
            C_ellipsisDot_array[i].SetActive(false);
        }
        for(int i = 18; i < 22; ++ i)
        {
            C_platform_array[i - 6].SetActive(false);
        }
        C_line_array[3].SetActive(false);
        C_line_array[2].GetComponent<TextMesh>().text = "L(2^14-1)";
        C_brace_array[3].SetActive(false);
    }

    IEnumerator checkMainMemory1(int tag, int word)
    {
        // 去除delimiter
        MA_lineDelimiter.SetActive(false);
        MA_wordDelimiter.SetActive(false);

        // 整段地址下移
        float offset = 30;
        Vector3 tmp1 = MA_platform_array[0].transform.position;
        s.Append(MA_platform_array[0].transform.DOMove(tmp1 + new Vector3(-30, -offset, 0), 1));
        Vector3 tmp2 = MA_bit_array[0].transform.position;
        s.Join(MA_bit_array[0].transform.DOMove(tmp2 + new Vector3(-30, -offset, 0), 1));
        for (int i = 0; i < 24; i++)
        {
            tmp1 = MA_platform_array[i].transform.position;
            s.Join(MA_platform_array[i].transform.DOMove(tmp1 + new Vector3(i / 4 * 10 - 30, -offset, 0), 1));
            tmp2 = MA_bit_array[i].transform.position;
            s.Join(MA_bit_array[i].transform.DOMove(tmp2 + new Vector3(i / 4 * 10 - 30, -offset, 0), 1));
        }

        yield return new WaitForSeconds(1f);

        GameObject[] hexAddress = new GameObject[10];
        hexAddressString = "";
        // 计算十六进制的地址 & 显示
        int address = (tag << 16) + word;
        for (int i = 0; i < 6; i++)
        {
            int sum = (address & 1) + (address & 2) + (address & 4) + (address & 8);
            hexAddress[i] = Instantiate(Text_MA_bit_Prefab, (MA_bit_array[24 - i * 4 - 3].transform.position + MA_bit_array[24 - i * 4 - 2].transform.position) / 2 + new Vector3(0, -15, 0), Quaternion.Euler(0, 0, 0));
            string tmpStr;
            if (sum <= 9)
            {
                tmpStr = "<color=#000000>" + sum.ToString() + "</color>";
                hexAddressString = sum.ToString() + hexAddressString;
            }
            else
            {
                tmpStr = "<color=#000000>" + (char)(sum - 10 + 'A') + "</color>";
                hexAddressString = (char)(sum - 10 + 'A') + hexAddressString;
            }
            hexAddress[i].GetComponent<TextMesh>().text = tmpStr;
            address >>= 4;
        }

        yield return new WaitForSeconds(1f);

        // 地址回到原位
        tmp1 = MA_platform_array[0].transform.position;
        s.Append(MA_platform_array[0].transform.DOMove(tmp1 - new Vector3(-30, -offset, 0), 1));
        tmp2 = MA_bit_array[0].transform.position;
        s.Join(MA_bit_array[0].transform.DOMove(tmp2 - new Vector3(-30, -offset, 0), 1));
        for (int i = 0; i < 24; i++)
        {
            tmp1 = MA_platform_array[i].transform.position;
            s.Join(MA_platform_array[i].transform.DOMove(tmp1 - new Vector3(i / 4 * 10 - 30, -offset, 0), 1));
            tmp2 = MA_bit_array[i].transform.position;
            s.Join(MA_bit_array[i].transform.DOMove(tmp2 - new Vector3(i / 4 * 10 - 30, -offset, 0), 1));
        }

        // 十六进制地址靠拢
        tmp1 = hexAddress[0].transform.position;
        s.Join(hexAddress[0].transform.DOMove(tmp1 + new Vector3(-120, 0, 0), 1));
        tmp2 = hexAddress[0].transform.position;
        s.Join(hexAddress[0].transform.DOMove(tmp2 + new Vector3(-120, 0, 0), 1));
        for (int i = 1; i < 6; i++)
        {
            tmp1 = hexAddress[i].transform.position;
            s.Join(hexAddress[i].transform.DOMove(tmp1 + new Vector3(i * 40 - 120, 0, 0), 1));
            tmp2 = hexAddress[i].transform.position;
            s.Join(hexAddress[i].transform.DOMove(tmp2 + new Vector3(i * 40 - 120, 0, 0), 1));
        }

        yield return new WaitForSeconds(1f);

        // 显示main memory里对应的word
        MM_ellipsisDot_array[1].SetActive(false);
        word_in_MM.SetActive(true);
        word_text_in_MM.SetActive(true);
        word_text_in_MM.transform.position += new Vector3(-25, 6, 0);
        word_text_in_MM.GetComponent<TextMesh>().text = hexAddressString + "(H)";
        word_in_MM.GetComponent<MeshRenderer>().material.color = Color.green;

        yield return new WaitForSeconds(1f);

        // 十六进制地址消失
        for (int i = 0; i < 6; i++)
        {
            hexAddress[i].SetActive(false);
            word_text_in_MM.SetActive(false);
            MM_ellipsisDot_array[1].SetActive(true);
        }

        yield return new WaitForSeconds(1f);

        // main memory 的内容移到cache里
        s.Append(word_in_MM.transform.DOMove(C_platform_array[8 + word].transform.position + new Vector3(0, 0, -0.5f), 1));

        yield return new WaitForSeconds(1f);

        // tag 修改
        C_tag_text_array[2].GetComponent<TextMesh>().text = tag.ToString();
        C_tag_array[2].GetComponent<MeshRenderer>().material.color = Color.green;
    }

    void end5(int tag,int word)
    {
        word_in_MM.transform.position = MM_platform_begin - new Vector3(0, (gap + 10f) * 9, 0f);
        word_in_MM.SetActive(false);

        C_tag_array[2].GetComponent<MeshRenderer>().material.color = C_tag_array[0].GetComponent<MeshRenderer>().material.color;
        C_tag_text_array[2].GetComponent<TextMesh>().text = "";

        for (int i = 3; i < 6; ++i)
        {
            C_ellipsisDot_array[i].SetActive(false);
        }
        for (int i = 18; i < 22; ++i)
        {
            C_platform_array[i - 6].SetActive(false);
        }
        C_line_array[3].SetActive(false);
        C_brace_array[3].SetActive(false);
        MA_wordDelimiter.SetActive(false);
        MA_lineDelimiter.SetActive(false);
    }

    IEnumerator checkMainMemory2(int tag, int set, int word)
    {
        // 去除delimiter
        MA_lineDelimiter.SetActive(false);
        MA_wordDelimiter.SetActive(false);

        // 整段地址下移
        float offset = 30;
        Vector3 tmp1 = MA_platform_array[0].transform.position;
        s.Append(MA_platform_array[0].transform.DOMove(tmp1 + new Vector3(-30, -offset, 0), 1));
        Vector3 tmp2 = MA_bit_array[0].transform.position;
        s.Join(MA_bit_array[0].transform.DOMove(tmp2 + new Vector3(-30, -offset, 0), 1));
        for (int i = 0; i < 24; i++)
        {
            tmp1 = MA_platform_array[i].transform.position;
            s.Join(MA_platform_array[i].transform.DOMove(tmp1 + new Vector3(i / 4 * 10 - 30, -offset, 0), 1));
            tmp2 = MA_bit_array[i].transform.position;
            s.Join(MA_bit_array[i].transform.DOMove(tmp2 + new Vector3(i / 4 * 10 - 30, -offset, 0), 1));
        }

        yield return new WaitForSeconds(1f);

        GameObject[] hexAddress = new GameObject[10];
        hexAddressString = "";
        // 计算十六进制的地址 & 显示
        int address = (tag << 16) + (set << 2) + word;
        for (int i = 0; i < 6; i++)
        {
            int sum = (address & 1) + (address & 2) + (address & 4) + (address & 8);
            hexAddress[i] = Instantiate(Text_MA_bit_Prefab, (MA_bit_array[24 - i * 4 - 3].transform.position + MA_bit_array[24 - i * 4 - 2].transform.position) / 2 + new Vector3(0, -15, 0), Quaternion.Euler(0, 0, 0));
            string tmpStr;
            if (sum <= 9)
            {
                tmpStr = "<color=#000000>" + sum.ToString() + "</color>";
                hexAddressString = sum.ToString() + hexAddressString;
            }
            else
            {
                tmpStr = "<color=#000000>" + (char)(sum - 10 + 'A') + "</color>";
                hexAddressString = (char)(sum - 10 + 'A') + hexAddressString;
            }
            hexAddress[i].GetComponent<TextMesh>().text = tmpStr;
            address >>= 4;
        }

        yield return new WaitForSeconds(1f);

        // 地址回到原位
        tmp1 = MA_platform_array[0].transform.position;
        s.Append(MA_platform_array[0].transform.DOMove(tmp1 - new Vector3(-30, -offset, 0), 1));
        tmp2 = MA_bit_array[0].transform.position;
        s.Join(MA_bit_array[0].transform.DOMove(tmp2 - new Vector3(-30, -offset, 0), 1));
        for (int i = 0; i < 24; i++)
        {
            tmp1 = MA_platform_array[i].transform.position;
            s.Join(MA_platform_array[i].transform.DOMove(tmp1 - new Vector3(i / 4 * 10 - 30, -offset, 0), 1));
            tmp2 = MA_bit_array[i].transform.position;
            s.Join(MA_bit_array[i].transform.DOMove(tmp2 - new Vector3(i / 4 * 10 - 30, -offset, 0), 1));
        }

        // 十六进制地址靠拢
        tmp1 = hexAddress[0].transform.position;
        s.Join(hexAddress[0].transform.DOMove(tmp1 + new Vector3(-120, 0, 0), 1));
        tmp2 = hexAddress[0].transform.position;
        s.Join(hexAddress[0].transform.DOMove(tmp2 + new Vector3(-120, 0, 0), 1));
        for (int i = 1; i < 6; i++)
        {
            tmp1 = hexAddress[i].transform.position;
            s.Join(hexAddress[i].transform.DOMove(tmp1 + new Vector3(i * 40 - 120, 0, 0), 1));
            tmp2 = hexAddress[i].transform.position;
            s.Join(hexAddress[i].transform.DOMove(tmp2 + new Vector3(i * 40 - 120, 0, 0), 1));
        }

        yield return new WaitForSeconds(1f);

        // 显示main memory里对应的word
        MM_ellipsisDot_array[1].SetActive(false);
        word_in_MM.SetActive(true);
        word_text_in_MM.SetActive(true);
        word_text_in_MM.transform.position += new Vector3(-25, 6, 0);
        word_text_in_MM.GetComponent<TextMesh>().text = hexAddressString + "(H)";
        word_in_MM.GetComponent<MeshRenderer>().material.color = Color.green;

        yield return new WaitForSeconds(1f);

        // 十六进制地址消失
        for (int i = 0; i < 6; i++)
        {
            hexAddress[i].SetActive(false);
            word_text_in_MM.SetActive(false);
            MM_ellipsisDot_array[1].SetActive(true);
        }

        yield return new WaitForSeconds(1f);

        // main memory 的内容移到cache里
        s.Append(word_in_MM.transform.DOMove(C_platform_array[8 + word].transform.position + new Vector3(0, 0, -0.5f), 1));

        yield return new WaitForSeconds(1f);

        // tag 修改
        C_tag_text_array[2].GetComponent<TextMesh>().text = tag.ToString();
        C_tag_array[2].GetComponent<MeshRenderer>().material.color = Color.green;
    }

    void end6(int tag, int set, int word)
    {
        word_in_MM.transform.position = MM_platform_begin - new Vector3(0, (gap + 10f) * 9, 0f);
        word_in_MM.SetActive(false);

        C_tag_array[2].GetComponent<MeshRenderer>().material.color = C_tag_array[0].GetComponent<MeshRenderer>().material.color;
        C_tag_text_array[2].GetComponent<TextMesh>().text = "";

        for (int i = 3; i < 6; ++i)
        {
            C_ellipsisDot_array[i].SetActive(false);
        }
        for (int i = 18; i < 22; ++i)
        {
            C_platform_array[i - 6].SetActive(false);
        }
        C_line_array[3].SetActive(false);
        C_line_array[2].GetComponent<TextMesh>().text = "L(2^14-1)";
        C_brace_array[3].SetActive(false);
    }

    void Update()
    {
        
    }
}
