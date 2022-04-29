using App.Samples.UI;
using NUnit.Framework;
using Samples.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
/// <summary>
/// Tests te sample UI script
/// </summary>
public class SampleUITests
{
    private SampleUI sampleUI;
    private GameObject manager;
    private GameObject contentParent;
    private Transform contentParentTransform;
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene(3);
        yield return null;
        yield return null;
        manager = GameObject.Find("Core/RetrieveManager");

        yield return null;
        sampleUI = manager.GetComponent<SampleUI>();
        yield return null;
      contentParent=  GameObject.Find("Canvas_UserSamples/Panel_Scroll/Scroll_View/Viewport/Content");
    }
    /// <summary>
    /// tests that AddTextAndPrefab adds a child to the content parent
    /// when passed a single sample
    /// </summary>
    [Test]
    public void SingleSample_TextAndPrefab_PrefabTest()
    {
        Assert.AreEqual(0,contentParent.transform.childCount);
        sampleUI.AddTextAndPrefab(new Sample());
        Assert.AreEqual(1, contentParent.transform.childCount);
    }
    /// <summary>
    /// tests that AddTextAndPrefab adds the correct number of children to the content parent
    /// when passed a sample list
    /// </summary>
    [Test]
    public void ListSamples_TextAndPrefab_PrefabTest()
    {
        List<Sample> samples = new List<Sample>();
        samples.Add(new Sample());
        samples.Add(new Sample());
        samples.Add(new Sample());
        Assert.AreEqual(0, contentParent.transform.childCount);
        sampleUI.AddTextAndPrefab(samples);
        Assert.AreEqual(3, contentParent.transform.childCount);
    }
    /// <summary>
    /// tests that AddTextAndPrefab destroys previously loaded children attached
    /// to the content parent
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator ListSamples_TextAndPrefab_DestroyChildrenTest()
    {
        List<Sample> samples = new List<Sample>();
        samples.Add(new Sample());
        samples.Add(new Sample());
        samples.Add(new Sample());
        Assert.AreEqual(0, contentParent.transform.childCount);

        //Adding the 3 above Samples / Children to the "contentParent"
        sampleUI.AddTextAndPrefab(samples);
        Assert.AreEqual(3, contentParent.transform.childCount);

        List<Sample> samples2 = new List<Sample>();
        samples2.Add(new Sample());
        samples2.Add(new Sample());
        //adding the 2 samples from samples2 to the "contentParent"
        //AddTextAndPrefab should destroy the previous 3 children
        sampleUI.AddTextAndPrefab(samples2);
        yield return null;//wait for next frame

        Assert.AreEqual(2, contentParent.transform.childCount);
    }
  
}
