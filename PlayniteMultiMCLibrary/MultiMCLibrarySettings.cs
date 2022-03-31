using Playnite.SDK;
using Playnite.SDK.Data;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MultiMcLibrary;

public class MultiMcLibrarySettings : ObservableObject
{
    private string _multiMcPath = string.Empty;
    private string _instanceNameFormat = "Minecraft {InstanceName}";
    private bool _updatePlaytimeOnClose = true;
    private bool _updateDescriptionOnClose = true;
    private bool _useVersionCovers = true;
    private bool _useDefaultCover = true;
    private bool _useVersionBackgrounds = true;
    private bool _useDefaultBackground = true;

    [JsonProperty("MultiMcFolder")]
    public string MultiMcPath { get => _multiMcPath; set => SetValue(ref _multiMcPath, value); }
    public string InstanceNameFormat { get => _instanceNameFormat; set => SetValue(ref _instanceNameFormat, value); }
    public bool UpdatePlaytimeOnClose { get => _updatePlaytimeOnClose; set => SetValue(ref _updatePlaytimeOnClose, value); }
    public bool UpdateDescriptionOnClose { get => _updateDescriptionOnClose; set => SetValue(ref _updateDescriptionOnClose, value); }
    public bool UseVersionCovers { get => _useVersionCovers; set => SetValue(ref _useVersionCovers, value); }
    public bool UseDefaultCover { get => _useDefaultCover; set => SetValue(ref _useDefaultCover, value); }
    public bool UseVersionBackgrounds { get => _useVersionBackgrounds; set => SetValue(ref _useVersionBackgrounds, value); }
    public bool UseDefaultBackground { get => _useDefaultBackground; set => SetValue(ref _useDefaultBackground, value); }
}

public class MultiMcLibrarySettingsViewModel : ObservableObject, ISettings
{
    private readonly MultiMcLibrary _plugin;
    private MultiMcLibrarySettings EditingClone { get; set; } = null!;

    private MultiMcLibrarySettings _settings = null!;
    public MultiMcLibrarySettings Settings
    {
        get => _settings;
        set
        {
            _settings = value;
            OnPropertyChanged();
        }
    }

    public string CommaSeparatedValidTokens { get; } = string.Join(", ", TokenFormatter.ValidTokens);

    public MultiMcLibrarySettingsViewModel(MultiMcLibrary plugin)
    {
        // Injecting your plugin instance is required for Save/Load method because Playnite saves data to a location based on what plugin requested the operation.
        _plugin = plugin;

        // Load saved settings.
        var savedSettings = plugin.LoadPluginSettings<MultiMcLibrarySettings>();

        // LoadPluginSettings returns null if not saved data is available.
        Settings = savedSettings ?? new MultiMcLibrarySettings();
    }

    public void BeginEdit()
    {
        // Code executed when settings view is opened and user starts editing values.
        EditingClone = Serialization.GetClone(Settings);
    }

    public void CancelEdit()
    {
        // Code executed when user decides to cancel any changes made since BeginEdit was called.
        // This method should revert any changes made to Option1 and Option2.
        Settings = EditingClone;
    }

    public void EndEdit()
    {
        // Code executed when user decides to confirm changes made since BeginEdit was called.
        // This method should save settings made to Option1 and Option2.
        _plugin.SavePluginSettings(Settings);
        _plugin.SettingsChanged(EditingClone, Settings);
    }

    public bool VerifySettings(out List<string> errors)
    {
        // Code execute when user decides to confirm changes made since BeginEdit was called.
        // Executed before EndEdit is called and EndEdit is not called if false is returned.
        // List of errors is presented to user if verification fails.
        errors = new List<string>();
        return true;
    }
}