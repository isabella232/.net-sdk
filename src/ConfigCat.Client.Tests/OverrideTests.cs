﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ConfigCat.Client.Tests
{
    [TestClass]
    public class OverrideTests
    {
        private static readonly string ComplexJsonPath = Path.Combine("data", "test_json_complex.json");
        private static readonly string SimpleJsonPath = Path.Combine("data", "test_json_simple.json");
        private static readonly string SampleFileToCreate = Path.Combine("data", "generated.json");

        [TestMethod]
        public void LocalFile()
        {
            using var client = new ConfigCatClient(options =>
            {
                options.SdkKey = "localhost";
                options.FlagOverrides = FlagOverrides.LocalFile(ComplexJsonPath, false, OverrideBehaviour.LocalOnly);
            });

            Assert.IsTrue(client.GetValue("enabledFeature", false));
            Assert.IsFalse(client.GetValue("disabledFeature", false));
            Assert.AreEqual(5, client.GetValue("intSetting", 0));
            Assert.AreEqual(3.14, client.GetValue("doubleSetting", 0.0));
            Assert.AreEqual("test", client.GetValue("stringSetting", string.Empty));
        }

        [TestMethod]
        public async Task LocalFileAsync()
        {
            using var client = new ConfigCatClient(options =>
            {
                options.SdkKey = "localhost";
                options.FlagOverrides = FlagOverrides.LocalFile(ComplexJsonPath, false, OverrideBehaviour.LocalOnly);
            });

            Assert.IsTrue(await client.GetValueAsync("enabledFeature", false));
            Assert.IsFalse(await client.GetValueAsync("disabledFeature", false));
            Assert.AreEqual(5, await client.GetValueAsync("intSetting", 0));
            Assert.AreEqual(3.14, await client.GetValueAsync("doubleSetting", 0.0));
            Assert.AreEqual("test", await client.GetValueAsync("stringSetting", string.Empty));
        }

        public void LocalFile_Parallel()
        {
            using var client = new ConfigCatClient(options =>
            {
                options.SdkKey = "localhost";
                options.FlagOverrides = FlagOverrides.LocalFile(ComplexJsonPath, false, OverrideBehaviour.LocalOnly);
            });

            Assert.IsTrue(client.GetValue("enabledFeature", false));
            Assert.IsFalse(client.GetValue("disabledFeature", false));
            Assert.AreEqual(5, client.GetValue("intSetting", 0));
            Assert.AreEqual(3.14, client.GetValue("doubleSetting", 0.0));
            Assert.AreEqual("test", client.GetValue("stringSetting", string.Empty));
        }

        [TestMethod]
        public void LocalFileAsync_Parallel()
        {
            var keys = new[]
            {
                "enabledFeature",
                "disabledFeature",
                "intSetting",
                "doubleSetting",
                "stringSetting",
            };

            using var client = new ConfigCatClient(options =>
            {
                options.SdkKey = "localhost";
                options.FlagOverrides = FlagOverrides.LocalFile(ComplexJsonPath, false, OverrideBehaviour.LocalOnly);
            });

            Parallel.ForEach(keys, async item =>
            {
                Assert.IsNotNull(await client.GetValueAsync<object>(item, null));
            });
        }

        [TestMethod]
        public void LocalFileAsync_Parallel_Sync()
        {
            var keys = new []
            {
                "enabledFeature",
                "disabledFeature",
                "intSetting",
                "doubleSetting",
                "stringSetting",
            };

            using var client = new ConfigCatClient(options =>
            {
                options.SdkKey = "localhost";
                options.FlagOverrides = FlagOverrides.LocalFile(ComplexJsonPath, false, OverrideBehaviour.LocalOnly);
            });

            Parallel.ForEach(keys, item =>
            {
                Assert.IsNotNull(client.GetValue<object>(item, null));
            });
        }

        [TestMethod]
        public void LocalFile_Default_WhenErrorOccures()
        {
            using var client = new ConfigCatClient(options =>
            {
                options.SdkKey = "localhost";
                options.FlagOverrides = FlagOverrides.LocalFile("something-not-existing", false, OverrideBehaviour.LocalOnly);
            });

            Assert.IsFalse(client.GetValue("enabledFeature", false));
            Assert.AreEqual("default", client.GetValue("stringSetting", "default"));
        }

        [TestMethod]
        public void LocalFile_Reload()
        {
            using var client = new ConfigCatClient(options =>
            {
                options.SdkKey = "localhost";
                options.FlagOverrides = FlagOverrides.LocalFile(ComplexJsonPath, true, OverrideBehaviour.LocalOnly);
            });

            Assert.IsTrue(client.GetValue("enabledFeature", false));
            Assert.IsFalse(client.GetValue("disabledFeature", false));
            Assert.AreEqual(5, client.GetValue("intSetting", 0));
            Assert.AreEqual(3.14, client.GetValue("doubleSetting", 0.0));
            Assert.AreEqual("test", client.GetValue("stringSetting", string.Empty));
        }

        [TestMethod]
        public async Task LocalFileAsync_Reload()
        {
            using var client = new ConfigCatClient(options =>
            {
                options.SdkKey = "localhost";
                options.FlagOverrides = FlagOverrides.LocalFile(ComplexJsonPath, true, OverrideBehaviour.LocalOnly);
            });

            Assert.IsTrue(await client.GetValueAsync("enabledFeature", false));
            Assert.IsFalse(await client.GetValueAsync("disabledFeature", false));
            Assert.AreEqual(5, await client.GetValueAsync("intSetting", 0));
            Assert.AreEqual(3.14, await client.GetValueAsync("doubleSetting", 0.0));
            Assert.AreEqual("test", await client.GetValueAsync("stringSetting", string.Empty));
        }

        [TestMethod]
        public void LocalFile_Default_WhenErrorOccures_Reload()
        {
            using var client = new ConfigCatClient(options =>
            {
                options.SdkKey = "localhost";
                options.FlagOverrides = FlagOverrides.LocalFile("something-not-existing", true, OverrideBehaviour.LocalOnly);
            });

            Assert.IsFalse(client.GetValue("enabledFeature", false));
            Assert.AreEqual("default", client.GetValue("stringSetting", "default"));
        }

        [TestMethod]
        public void LocalFile_Simple()
        {
            using var client = new ConfigCatClient(options =>
            {
                options.SdkKey = "localhost";
                options.FlagOverrides = FlagOverrides.LocalFile(SimpleJsonPath, false, OverrideBehaviour.LocalOnly);
            });

            Assert.IsTrue(client.GetValue("enabledFeature", false));
            Assert.IsFalse(client.GetValue("disabledFeature", false));
            Assert.AreEqual(5, client.GetValue("intSetting", 0));
            Assert.AreEqual(3.14, client.GetValue("doubleSetting", 0.0));
            Assert.AreEqual("test", client.GetValue("stringSetting", string.Empty));
        }

        [TestMethod]
        public async Task LocalFileAsync_Simple()
        {
            using var client = new ConfigCatClient(options =>
            {
                options.SdkKey = "localhost";
                options.FlagOverrides = FlagOverrides.LocalFile(SimpleJsonPath, false, OverrideBehaviour.LocalOnly);
            });

            Assert.IsTrue(await client.GetValueAsync("enabledFeature", false));
            Assert.IsFalse(await client.GetValueAsync("disabledFeature", false));
            Assert.AreEqual(5, await client.GetValueAsync("intSetting", 0));
            Assert.AreEqual(3.14, await client.GetValueAsync("doubleSetting", 0.0));
            Assert.AreEqual("test", await client.GetValueAsync("stringSetting", string.Empty));
        }

        [TestMethod]
        public void LocalFile_Dictionary()
        {
            var dict = new Dictionary<string, object>
            {
                {"enabledFeature", true},
                {"disabledFeature", false},
                {"intSetting", 5},
                {"doubleSetting", 3.14},
                {"stringSetting", "test"},
            };

            using var client = new ConfigCatClient(options =>
            {
                options.SdkKey = "localhost";
                options.FlagOverrides = FlagOverrides.LocalDictionary(dict, OverrideBehaviour.LocalOnly);
            });

            Assert.IsTrue(client.GetValue("enabledFeature", false));
            Assert.IsFalse(client.GetValue("disabledFeature", false));
            Assert.AreEqual(5, client.GetValue("intSetting", 0));
            Assert.AreEqual(3.14, client.GetValue("doubleSetting", 0.0));
            Assert.AreEqual("test", client.GetValue("stringSetting", string.Empty));
        }

        [TestMethod]
        public async Task LocalFileAsync_Dictionary()
        {
            var dict = new Dictionary<string, object>
            {
                {"enabledFeature", true},
                {"disabledFeature", false},
                {"intSetting", 5},
                {"doubleSetting", 3.14},
                {"stringSetting", "test"},
            };

            using var client = new ConfigCatClient(options =>
            {
                options.SdkKey = "localhost";
                options.FlagOverrides = FlagOverrides.LocalDictionary(dict, OverrideBehaviour.LocalOnly);
            });

            Assert.IsTrue(await client.GetValueAsync("enabledFeature", false));
            Assert.IsFalse(await client.GetValueAsync("disabledFeature", false));
            Assert.AreEqual(5, await client.GetValueAsync("intSetting", 0));
            Assert.AreEqual(3.14, await client.GetValueAsync("doubleSetting", 0.0));
            Assert.AreEqual("test", await client.GetValueAsync("stringSetting", string.Empty));
        }

        [TestMethod]
        public void LocalOverRemote()
        {
            var dict = new Dictionary<string, object>
            {
                {"fakeKey", true},
                {"nonexisting", true},
            };

            var fakeHandler = new FakeHttpClientHandler(System.Net.HttpStatusCode.OK);

            using var client = new ConfigCatClient(options =>
            {
                options.SdkKey = "localhost";
                options.FlagOverrides = FlagOverrides.LocalDictionary(dict, OverrideBehaviour.LocalOverRemote);
                options.HttpClientHandler = new FakeHttpClientHandler(System.Net.HttpStatusCode.OK, GetJsonContent("false"));
                options.PollingMode = PollingModes.ManualPoll;
            });

            client.ForceRefresh();

            Assert.IsTrue(client.GetValue("fakeKey", false));
            Assert.IsTrue(client.GetValue("nonexisting", false));
        }

        [TestMethod]
        public async Task LocalOverRemote_Async()
        {
            var dict = new Dictionary<string, object>
            {
                {"fakeKey", true},
                {"nonexisting", true},
            };

            var fakeHandler = new FakeHttpClientHandler(System.Net.HttpStatusCode.OK);

            using var client = new ConfigCatClient(options =>
            {
                options.SdkKey = "localhost";
                options.FlagOverrides = FlagOverrides.LocalDictionary(dict, OverrideBehaviour.LocalOverRemote);
                options.HttpClientHandler = new FakeHttpClientHandler(System.Net.HttpStatusCode.OK, GetJsonContent("false"));
                options.PollingMode = PollingModes.ManualPoll;
            });

            await client.ForceRefreshAsync();

            Assert.IsTrue(await client.GetValueAsync("fakeKey", false));
            Assert.IsTrue(await client.GetValueAsync("nonexisting", false));
        }

        [TestMethod]
        public void RemoteOverLocal()
        {
            var dict = new Dictionary<string, object>
            {
                {"fakeKey", true},
                {"nonexisting", true},
            };

            var fakeHandler = new FakeHttpClientHandler(System.Net.HttpStatusCode.OK);

            using var client = new ConfigCatClient(options =>
            {
                options.SdkKey = "localhost";
                options.FlagOverrides = FlagOverrides.LocalDictionary(dict, OverrideBehaviour.RemoteOverLocal);
                options.HttpClientHandler = new FakeHttpClientHandler(System.Net.HttpStatusCode.OK, GetJsonContent("false"));
                options.PollingMode = PollingModes.ManualPoll;
            });

            client.ForceRefresh();

            Assert.IsFalse(client.GetValue("fakeKey", false));
            Assert.IsTrue(client.GetValue("nonexisting", false));
        }

        [TestMethod]
        public async Task RemoteOverLocal_Async()
        {
            var dict = new Dictionary<string, object>
            {
                {"fakeKey", true},
                {"nonexisting", true},
            };

            var fakeHandler = new FakeHttpClientHandler(System.Net.HttpStatusCode.OK);

            using var client = new ConfigCatClient(options =>
            {
                options.SdkKey = "localhost";
                options.FlagOverrides = FlagOverrides.LocalDictionary(dict, OverrideBehaviour.RemoteOverLocal);
                options.HttpClientHandler = new FakeHttpClientHandler(System.Net.HttpStatusCode.OK, GetJsonContent("false"));
                options.PollingMode = PollingModes.ManualPoll;
            });

            await client.ForceRefreshAsync();

            Assert.IsFalse(await client.GetValueAsync("fakeKey", false));
            Assert.IsTrue(await client.GetValueAsync("nonexisting", false));
        }

        [TestMethod]
        public async Task LocalFile_Watcher_Reload()
        {
            await CreateFileAndWriteContent(SampleFileToCreate, "initial");

            using var client = new ConfigCatClient(options =>
            {
                options.SdkKey = "localhost";
                options.FlagOverrides = FlagOverrides.LocalFile(SampleFileToCreate, true, OverrideBehaviour.LocalOnly);
                options.Logger.LogLevel = LogLevel.Info;
            });

            Assert.AreEqual("initial", await client.GetValueAsync("fakeKey", string.Empty));

            await WriteContent(SampleFileToCreate, "modified");
            await Task.Delay(400);

            Assert.AreEqual("modified", await client.GetValueAsync("fakeKey", string.Empty));

            File.Delete(SampleFileToCreate);
        }

        private static string GetJsonContent(string value)
        {
            return $"{{ \"f\": {{ \"fakeKey\": {{ \"v\": \"{value}\", \"p\": [] ,\"r\": [] }} }} }}";
        }

        private static async Task CreateFileAndWriteContent(string path, string content)
        {
            using var stream = File.Create(path);
            using var writer = new StreamWriter(stream);
            await writer.WriteAsync(GetJsonContent(content));
        }

        private static async Task WriteContent(string path, string content)
        {
            using var stream = File.OpenWrite(path);
            using var writer = new StreamWriter(stream);
            await writer.WriteAsync(GetJsonContent(content));
        }
    }
}
