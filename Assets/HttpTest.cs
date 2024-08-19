using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HttpTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public PostInfoArray allPostInfo;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            HttpInfo info = new HttpInfo();
            info.url = "http://mtvs.helloworldlabs.kr:7771/api/string?parameter=�ȳ��ϼ���";
            info.onComplete = (DownloadHandler downloadHandler) =>
            {
                print(downloadHandler.text);

                //string jsonData = "{ \"data\" : "  + downloadHandler.text + "}";
                //// jsonData �� PostInfoArray ������ �ٲ���.
                //allPostInfo = JsonUtility.FromJson<PostInfoArray>(jsonData);
            };
            StartCoroutine(HttpManager.GetInstance().Get(info));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HttpInfo info = new HttpInfo();
            info.url = "https://ssl.pstatic.net/melona/libs/1506/1506331/059733bdf9e9e6dc85ce_20240813151029214.jpg";
            info.onComplete = (DownloadHandler downloadHandler) =>
            {
                // �ٿ�ε� �� �����͸� Texture2D �� ��ȯ.
                DownloadHandlerTexture handler = downloadHandler as DownloadHandlerTexture;
                Texture2D texture = handler.texture;

                // texture �� �̿��ؼ� Sprite �� ��ȯ.
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                Image image = GameObject.Find("Image").GetComponent<Image>();
                image.sprite = sprite;
            };
            StartCoroutine(HttpManager.GetInstance().DownloadSprite(info));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // ������ ���� �����͸� ������.
            UserInfo userInfo = new UserInfo();
            userInfo.name = "��Ÿ����";
            userInfo.age = 3;
            userInfo.height = 185.5f;

            HttpInfo info = new HttpInfo();
            info.url = "http://mtvs.helloworldlabs.kr:7771/api/json";
            info.body = JsonUtility.ToJson(userInfo);
            info.contentType = "application/json";
            info.onComplete = (DownloadHandler downloadHandler) =>
            {
                print(downloadHandler.text);
            };
            StartCoroutine(HttpManager.GetInstance().Post(info));
        }

        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            HttpInfo info = new HttpInfo();
            info.url = "http://mtvs.helloworldlabs.kr:7771/api/file";
            info.contentType = "multipart/form-data";
            info.body = "C:\\Users\\Admin\\Downloads\\Karina.jpg";
            info.onComplete = (DownloadHandler downloadHandler) =>
            {
                File.WriteAllBytes(Application.dataPath + "/aespa.jpg", downloadHandler.data);
            };
            StartCoroutine(HttpManager.GetInstance().UploadFileByFormData(info));
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            HttpInfo info = new HttpInfo();
            info.url = "http://mtvs.helloworldlabs.kr:7771/api/byte";
            info.contentType = "image/jpg";
            info.body = "C:\\Users\\Admin\\Downloads\\Cat03.jpg";
            info.onComplete = (DownloadHandler downloadHandler) =>
            {
                File.WriteAllBytes(Application.dataPath + "/cat.jpg", downloadHandler.data);
            };
            StartCoroutine(HttpManager.GetInstance().UploadFileByByte(info));
        }
    }  
}

[System.Serializable]
public struct UserInfo
{
    public string name;
    public int age;
    public float height;
}
