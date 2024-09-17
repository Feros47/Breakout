namespace Breakout.Entities.Blocks.Factories;

using System.Reflection;

/// <summary>
/// Represents a registry mapping strings to factories.
/// We utilize reflection in order to adhere to the open-closed principle: If we didn't do this we'd have to use switch statements or conditionals
/// in order to map the metadata "blocktype" strings to <see cref="IBlockFactory"/> instances. This method will automatically register
/// a newly added factory type, with the correct interface, allowing us to extend existing functionality without modifying other classes.
/// </summary>
public class FactoryRegistry {
    private readonly IDictionary<string, IBlockFactory> factoryMap;
    /// <summary>
    /// Initializes the registry by querying the current assembly for implementations of <see cref="IBlockFactory"/>
    /// </summary>
    public FactoryRegistry() {
        var factoryType = typeof(IBlockFactory);
        var implementations = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t =>
                t.IsAssignableTo(factoryType) &&            // i.e. IBlockFactory obj = (type)MyObj wont fail
                !t.IsAbstract &&                            // Can't instantiate abstract types.
                !t.IsInterface &&                           // -||-
                t.GetConstructor(Type.EmptyTypes) != null);  // We can only instantiate objects with parameterless constructors.
        factoryMap = implementations
            .Select(i => (i.Name, Activator.CreateInstance(i)))
            .Where(i => i.Item2 is IBlockFactory)
            .Select(i => (i.Name, (i.Item2 as IBlockFactory)!))
            .ToDictionary(i => GetNameFromClassName(i.Name), i => i.Item2);
    }
    public IBlockFactory? this[string name] => factoryMap.ContainsKey(name) ? factoryMap[name] : null;

    public bool IsBlockTypeDesignator(string input) {
        return factoryMap.ContainsKey(input);
    }

    /// <summary>
    /// Get the name of a block type from the class's name. For example: "HardenedFactory" returns "Hardened".
    /// If there isn't "Factory" in the name, the class name is returned.
    /// </summary>
    /// <param name="className">The name of the class.</param>
    /// <returns></returns>
    public static string GetNameFromClassName(string className) {
        var inx = className.IndexOf("Factory");
        if (inx == -1) {
            return className;
        }
        return className.Substring(0, inx);
    }
}
