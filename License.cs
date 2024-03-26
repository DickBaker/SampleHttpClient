//
// To parse this JSON data, add NuGet 'System.Text.Json' then do:
//
//    using GitHubOrganization;
//
//    var dick = GitHubOrganization.FromJson(jsonString);

using System;
using System.Text.Json.Serialization;

namespace SampleHttpClient;

public class License
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("key")]
    public required string Key { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("spdx_id")]
    public required string SpdxId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("url")]
    public required Uri Url { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("node_id")]
    public required string NodeId { get; set; }
}
