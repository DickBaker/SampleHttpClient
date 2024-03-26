//
// To parse this JSON data, add NuGet 'System.Text.Json' then do:
//
//    using GitHubOrganization;
//
//    var dick = GitHubOrganization.FromJson(jsonString);

using System.Text.Json.Serialization;

namespace SampleHttpClient;

public class Permissions
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("admin")]
    public bool? Admin { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("maintain")]
    public bool? Maintain { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("push")]
    public bool? Push { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("pull")]
    public bool? Pull { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("triage")]
    public bool? Triage { get; set; }
}
