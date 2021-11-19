using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace MultiMcLibrary
{
    /// <summary>
    /// instgroups.json file
    /// </summary>
    [UsedImplicitly]
    public class InstanceGroups
    {
        [JsonProperty("formatVersion")]
        public string FormatVersion { get; private set; } = null!;

        /// <summary>
        /// Mapping of group display name -> group
        /// </summary>
        [JsonProperty("groups")]
        public IReadOnlyDictionary<string, InstanceGroup> Groups { get; private set; } = null!;
    }

    [UsedImplicitly]
    public class InstanceGroup
    {
        [JsonProperty("hidden")]
        public bool Hidden { get; private set; }

        /// <summary>
        /// Folder names, not display names
        /// </summary>
        [JsonProperty("instances")]
        public string[] Instances { get; private set; } = null!;
    }
    
    /// <summary>
    /// mmc-pack.json file
    /// </summary>
    [UsedImplicitly]
    public class MultiMcPack
    {
        [JsonProperty("components")]
        public PackComponent[] Components { get; private set; } = null!;

        [JsonProperty("formatVersion")]
        public long FormatVersion { get; private set; }

        public PackComponent? GetComponentById(string uid)
        {
            return Components.FirstOrDefault(e => e.Uid == uid);
        }
    }

    [UsedImplicitly]
    public class PackComponent
    {
        [JsonProperty("cachedName")]
        public string CachedName { get; private set; } = null!;

        [JsonProperty("cachedVersion")]
        public string? CachedVersion { get; private set; }

        [JsonProperty("cachedVolatile")]
        public bool? CachedVolatile { get; private set; }

        [JsonProperty("dependencyOnly")]
        public bool? DependencyOnly { get; private set; }

        /// <summary>
        /// Mandatory
        /// </summary>
        [JsonProperty("uid")]
        public string Uid { get; private set; } = null!;

        [JsonProperty("version")]
        public string? Version { get; private set; }

        [JsonProperty("cachedRequires")]
        public CachedRequire[]? CachedRequires { get; private set; }

        [JsonProperty("important")]
        public bool? Important { get; private set; }

        /// <summary>
        /// False if null
        /// </summary>
        [JsonProperty("disabled")]
        public bool? Disabled { get; private set; }
    }

    [UsedImplicitly]
    public class CachedRequire
    {
        /// <summary>
        /// Optional, suggested version
        /// </summary>
        [JsonProperty("suggests")]
        public string? Suggests { get; private set; }

        /// <summary>
        /// Mandatory
        /// </summary>
        [JsonProperty("uid")]
        public string Uid { get; private set; } = null!;

        /// <summary>
        /// Optional, mandatory version
        /// </summary>
        [JsonProperty("equals")]
        public string? EqualsVersion { get; private set; }
    }
    
    /// <summary>
    /// Each element inside an mcmod.info file 
    /// </summary>
    public class McModInfo
    {
        [JsonProperty("modid")]
        public string Modid { get; private set; } = null!;

        [JsonProperty("name")]
        public string Name { get; private set; } = null!;

        [JsonProperty("version")]
        public string? Version { get; private set; }

        [JsonProperty("mcversion")]
        public string? Mcversion { get; private set; }

        // [JsonProperty("description")]
        // public string? Description { get; private set; }

        // [JsonProperty("credits")]
        // public string? Credits { get; private set; }

        // [JsonProperty("logoFile")]
        // public string? LogoFile { get; private set; }

        // [JsonProperty("url")]
        // public string? Url { get; private set; }

        // [JsonProperty("updateUrl")]
        // public string? UpdateUrl { get; private set; }

        // [JsonProperty("updateJSON")]
        // public string? UpdateJson { get; private set; }

        // [JsonProperty("authorList")]
        // public string[]? AuthorList { get; private set; }

        // [JsonProperty("parent")]
        // public string? Parent { get; private set; }

        // [JsonProperty("screenshots")]
        // public object[]? Screenshots { get; private set; }

        // [JsonProperty("dependencies")]
        // public string[]? Dependencies { get; private set; }
    }

    /// <summary>
    /// Old, object-styled mcmod.info format 
    /// </summary>
    public class OldMcmodInfo
    {
        [JsonProperty("modListVersion")]
        public long ModListVersion { get; set; }

        [JsonProperty("modList")]
        public ModListEntry[] ModList { get; set; } = null!;
    }

    public class ModListEntry
    {
        [JsonProperty("modid")]
        public string Modid { get; set; } = null!;

        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("version")]
        public string? Version { get; set; }

        // [JsonProperty("mcversion")]
        // public string? Mcversion { get; set; }

        // [JsonProperty("url")]
        // public string? Url { get; set; }

        // [JsonProperty("authorList")]
        // public string[]? AuthorList { get; set; }

        // [JsonProperty("logoFile")]
        // public string? LogoFile { get; set; }

        // [JsonProperty("parent")]
        // public string? Parent { get; set; }

        // [JsonProperty("screenshots")]
        // public object[]? Screenshots { get; set; }

        // [JsonProperty("dependencies")]
        // public object[]? Dependencies { get; set; }
    }
    
    /// <summary>
    /// fabric.mod.json file
    /// </summary>
    public class FabricMod
    {
        [JsonProperty("schemaVersion")]
        public long SchemaVersion { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        [JsonProperty("version")]
        public string? Version { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        // [JsonProperty("description")]
        // public string? Description { get; set; }

        // [JsonProperty("authors")]
        // public string[]? Authors { get; set; }

        // [JsonProperty("contact")]
        // public Contact? Contact { get; set; }

        // [JsonProperty("license")]
        // public string? License { get; set; }

        // [JsonProperty("icon")]
        // public string? Icon { get; set; }

        // [JsonProperty("environment")]
        // public string? Environment { get; set; }

        // [JsonProperty("entrypoints")]
        // public Entrypoints? Entrypoints { get; set; }

        // [JsonProperty("mixins")]
        // public string[]? Mixins { get; set; }

        // [JsonProperty("depends")]
        // public IReadOnlyDictionary<string, string>? Depends { get; set; }

        // [JsonProperty("suggests")]
        // public IReadOnlyDictionary<string, string>? Suggests { get; set; }
    }

    // public class Contact
    // {
    //     [JsonProperty("homepage")]
    //     public string? Homepage { get; set; }
    //
    //     [JsonProperty("sources")]
    //     public string? Sources { get; set; }
    // }

    // public class Entrypoints
    // {
    //     [JsonProperty("main")]
    //     public string[]? Main { get; set; }
    // }
}