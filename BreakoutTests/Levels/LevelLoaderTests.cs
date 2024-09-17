namespace BreakoutTests.Levels;

using System;
using System.IO;
using Breakout.Entities.Blocks.Factories;
using Breakout.Levels;
using DIKUArcade.Utilities;

[TestFixture]
public class LevelLoaderTests {

    [Test]
    public void TestLoadFromConfigurationFileNotExists() {
        var guid = Guid.NewGuid().ToString();
        Assert.That(
            LevelLoader.LoadFromConfiguration(guid),
            Is.Null);
    }

    [Test]
    public void TestLoadFromConfigurationInvalidFormat() {
        var invalidFilePath = Path.Combine(FileIO.GetProjectPath(), "LevelLoadingTests", "invalid-format.txt");
        Assert.That(
            LevelLoader.LoadFromConfiguration(invalidFilePath),
            Is.Null);
    }

    [Test]
    public void TestLoadFromConfigurationValidFormat() {
        var validFilePath = Path.Combine(FileIO.GetProjectPath(), "LevelLoadingTests", "valid-format.txt");
        var data = LevelLoader.LoadFromConfiguration(validFilePath);
        Assert.Multiple(() => {
            Assert.That(data, Is.Not.Null);

            // Map
            Assert.That(data!.AsciiMap.Count, Is.Zero);
            // Meta
            Assert.That(data!.Metadata["Name"], Is.EqualTo("Empty Map"));
            Assert.That(data!.Metadata["Author"], Is.EqualTo("John Doe"));
            Assert.That(data!.Metadata["Hardened"], Is.EqualTo("j"));
            Assert.That(data!.Metadata.Factories['j'][0], Is.TypeOf<HardenedFactory>());
            // Legend
            Assert.That(data!.LegendData.ContainsKey('i'), Is.True);
        });
    }
}
