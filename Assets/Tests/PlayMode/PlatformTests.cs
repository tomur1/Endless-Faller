using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlatformTests
    {
        GameObject walls;
        GameObject platform;

        [SetUp]
        public void Setup()
        {
            walls = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Walls"));
            platform = MonoBehaviour.Instantiate(Resources.Load<GameObject>("MovingPlatform"));
        }


        [UnityTest]
        public IEnumerator CheckPlatformGoesUp()
        {
            // Assign
            Vector3 oldPos = platform.transform.position;
            // Act
            yield return null;
            // Assert
            Assert.Less(oldPos.y, platform.transform.position.y);

        }

        [UnityTest]
        public IEnumerator CheckPlatformDestroyedIfTooHigh()
        {
            //after waiting for 10 seconds the platform should be killed
            yield return new WaitForSeconds(7);
            
            Assert.False(platform);

        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(platform);
            Object.Destroy(walls);
        }
    }
}
 