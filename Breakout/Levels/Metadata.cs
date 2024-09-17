namespace Breakout.Levels;

using System;
using System.Collections.Generic;

/// <summary>
/// The <see cref="Metadata"/> class represents all key-value attributes in a level configuration file.
/// Additionally, it holds a <see cref="BlockFactoryMap"/> since block type creation is determined by metadata fields.
/// </summary>
public class Metadata {
    private readonly Dictionary<string, string> attribtues;
    public Metadata() {
        attribtues = new Dictionary<string, string>();
        Factories = new BlockFactoryMap();
    }
    public BlockFactoryMap Factories {
        get;
    }
    public string this[string name] {
        get {
            return attribtues[name];
        }
        set {
            if (ContainsKey(name)) {
                throw new InvalidOperationException($"Metadata attributes already contains element '{name}'");
            }
            attribtues[name] = value;
        }
    }
    public int Count => attribtues.Count;
    public bool ContainsKey(string v) {
        return attribtues.ContainsKey(v);
    }
}
