﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using HD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HD.Tests
{
  [TestClass()]
  public class CryptoNightTests
  {
    // {"params":{"job_id":"000000228b2212a9","blob":"0606a78ac3cf05a9fca0d1560eea0d51bf8338af0f988282522c05eb993d7ebd721856228374a20000007b3eee48e6e2b5744e9884d61631d7f9b043630c1c343d10066d563265264f039407","target":"b7d10000"},"jsonrpc":"2.0","method":"job"}
    // {"method":"submit","params":{"id":"421970933313569","job_id":"000000228b2212a9","nonce":"07110c7b","result":"e5d18403a03ff4fdc27152202482bed2dfc5fdfd09e77a6ea7d471f2fa0c0000"},"id":1}
    // {"id":1,"jsonrpc":"2.0","error":null,"result":{"status":"OK"}}


    // {"job_id":"000000228b11006d","blob":"0606f382c3cf053219fe60635f528da9f6732fc95f1e27585c552a2e62df8405243e0b731a59350000007bed6320ecf337b1818d9e9d5ce81964b9bc4a2af3535af3d22fd0ed9747d6490704","target":"b7d10000"},"jsonrpc":"2.0","method":"job"}
    // {"method":"submit","params":{"id":"421970933313569","job_id":"000000228b11006d","nonce":"0508047b","result":"edeabc3dd5ee2bb84fe36f107f302982fa3da606ece6db32ca9bf77820350000"},"id":1}
    // {"id":1,"jsonrpc":"2.0","error":null,"result":{"status":"OK"}}


    // {"params":{"job_id":"000000228b2ccd9f","blob":"0606f78ec3cf0577b744fab48ba56e724c38276897d36f8f1908362d85d65318f6d3b33cd76a3c0000007ba0ed3fee4f999e53f17114931d55b95553df90f7c55472f8adbbb0eac3ba47f403","target":"b7d10000"},"jsonrpc":"2.0","method":"job"}
    // {"method":"submit","params":{"id":"421970933313569","job_id":"000000228b2ccd9f","nonce":"9207147b","result":"525adce6a66339b8bce448b3121d415fa6777c1ca986413bf109acf3f6b20000"},"id":1}
    // {"id":1,"jsonrpc":"2.0","error":null,"result":{"status":"OK"}}
    // {"method":"submit","params":{"id":"421970933313569","job_id":"000000228b2ccd9f","nonce":"2232087b","result":"eabc0546b73de5909f9941c67cdf4095e39f339802229468ea741ff3f9770000"},"id":1}
    // {"id":1,"jsonrpc":"2.0","error":null,"result":{"status":"OK"}}
    // {"method":"submit","params":{"id":"421970933313569","job_id":"000000228b2ccd9f","nonce":"fb33147b","result":"79a0cc26cdbf1958c6370bd48de024dd1c5cf9c789cc98bdf1a8cda5e8190000"},"id":1}
    // {"id":1,"jsonrpc":"2.0","error":null,"result":{"status":"OK"}}


    [TestMethod()]
    public void AdHoc()
    {
      NewJob newJob = JsonConvert.DeserializeObject<NewJob>("{\"jsonrpc\":\"2.0\",\"id\":1,\"error\":null,\"result\":{\"id\":\"061389344606618\",\"status\":\"OK\",\"job\":{\"job_id\":\"000000228f0cc01c\",\"blob\":\"06068fbdc7cf051819ea9ae500a0d2e6c07041893cb893ae799b88ba6f9057ffbac9ae3bcd1823000000f8ea945b81c0bd0c94d90701fad3005acbcff27909b7c16cd52db0767f8beecfbd12\",\"target\":\"b7d10000\"}}}");
      CryptoNight night = new CryptoNight();
      night.Process(newJob);
      night.ProcessStep2();
      night.ProcessStep3();
      night.ProcessStep4();
    }

    /// <summary>
    /// Up to while(iGlobalJobNo.load(std::memory_order_relaxed) == iJobNo)
    /// </summary>
    [TestMethod()]
    public void ProcessTest()
    {
      NewJob newJob = JsonConvert.DeserializeObject<NewJob>("{\"jsonrpc\":\"2.0\",\"id\":1,\"error\":null,\"result\":{\"id\":\"831431040790814\",\"status\":\"OK\",\"job\":{\"job_id\":\"000000228b507492\",\"blob\":\"0606aea6c3cf055b878e5f92902e6691cb6be34012b2d20f6cd1fe74ebc7306b35e3cb55e4782000000092444031f7a6023a17838d25035743300cf2b8db6dcb539e1fc6e303475c5d763501\",\"target\":\"b7d10000\"}}}");
      CryptoNight night = new CryptoNight();
      night.Process(newJob);

      Assert.IsTrue(night.result.sJobID == "000000228b507492");
      Assert.IsTrue(night.bWorkBlob.Length == 112);
      Assert.IsTrue(night.bWorkBlob[0] == 6);
      Assert.IsTrue(night.bWorkBlob[5] == 207);
      Assert.IsTrue(night.bWorkBlob[73] == 118);
      Assert.IsTrue(night.bWorkBlob[75] == 1);
      Assert.IsTrue(night.bWorkBlob[76] == 0);
      Assert.IsTrue(night.bWorkBlob[109] == 0);
      Assert.IsTrue(night.bWorkBlob[111] == 0);
      Assert.IsTrue(night.iWorkSize == 76);
      Assert.IsTrue(night.iTarget == (ulong)230584300921369);
      Assert.IsTrue(night.iJobDiff == (ulong)80000);
      Assert.IsTrue(night.iCount == 0);
      Assert.IsTrue(night.piNonce == 2449473536);
      Assert.IsTrue(night.result.iNonce == 2449473536);
    }



    [TestMethod()]
    public void ProcessTest2()
    {
      NewJob newJob = JsonConvert.DeserializeObject<NewJob>("{\"jsonrpc\":\"2.0\",\"id\":1,\"error\":null,\"result\":{\"id\":\"905120989173307\",\"status\":\"OK\",\"job\":{\"job_id\":\"000000228b601b20\",\"blob\":\"0405eeafc3cf0584f60a594f4850e6315aae83ae95cf980d8407346037895c65a2e324e858e18a0000006a3c7ebcfcf867030b6e03b52c736cfecdf3f1a9ccc06bb69382669b3b49c6905f02\",\"target\":\"b7d10000\"}}}");
      CryptoNight night = new CryptoNight();
      night.Process(newJob);

      // Passing
      Assert.AreEqual(night.bWorkBlob.Length, 112);
      Assert.AreEqual(night.bWorkBlob[0], 4);
      Assert.AreEqual(night.bWorkBlob[5], 207);
      Assert.AreEqual(night.bWorkBlob[73], 144);
      Assert.AreEqual(night.bWorkBlob[75], 2);
      Assert.AreEqual(night.bWorkBlob[76], 0);
      Assert.AreEqual(night.bWorkBlob[109], 0);
      Assert.AreEqual(night.bWorkBlob[111], 0);
      Assert.AreEqual(night.piNonce, (uint)1778384896);

      night.ProcessStep2();

      Assert.IsTrue(night.piNonce == 1778384897);
      Assert.IsTrue(night.result.iNonce == 1778384897);
      Assert.IsTrue(night.bWorkBlob[38] == 138);
      Assert.IsTrue(night.bWorkBlob[39] == 1);
      Assert.IsTrue(night.bWorkBlob[42] == 106);

      night.ProcessStep3();

      Assert.IsTrue(night.ctx.hash_state[0] == 79);
      Assert.IsTrue(night.ctx.hash_state[10] == 11);
      Assert.IsTrue(night.ctx.hash_state[129] == 59);
      Assert.IsTrue(night.ctx.hash_state[199] == 76);
    }
  }
}