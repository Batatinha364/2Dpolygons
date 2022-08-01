using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DemoScript : MonoBehaviour
{
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;

    void Text3()
    {
        text3.ForceMeshUpdate();
        var textInfo = text3.textInfo;
        float varX;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;
            }

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; j++)
            {
                var orig = verts[charInfo.vertexIndex + j];

                varX = Mathf.Sin(-Time.time) * 100f;

                verts[charInfo.vertexIndex + j] = orig + new Vector3(varX, 0, 0);
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            text3.UpdateGeometry(meshInfo.mesh, i);
        }
    }

    void Text2() 
    {
        text2.ForceMeshUpdate();
        var textInfo = text2.textInfo;
        float varX;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;
            }

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; j++)
            {
                var orig = verts[charInfo.vertexIndex + j];

                varX = Mathf.Sin(Time.time) * 80f;

                verts[charInfo.vertexIndex + j] = orig + new Vector3(varX, 0, 0);
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            text2.UpdateGeometry(meshInfo.mesh, i);
        }
    }

    void Text1() 
    {
        text1.ForceMeshUpdate();
        var textInfo = text1.textInfo;
        float varY;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;
            }

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; j++)
            {
                var orig = verts[charInfo.vertexIndex + j];

                varY = Mathf.PingPong(Time.time, 5f) * 1.5f;

                verts[charInfo.vertexIndex + j] = orig + new Vector3(0, varY, 0);
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            text1.UpdateGeometry(meshInfo.mesh, i);
        }
    }

    void Update()
    {
        Text1();
        Text2();
        Text3();

        if(Input.GetButton("Submit"))
            SceneManager.LoadScene("MainScene");
    }
}
