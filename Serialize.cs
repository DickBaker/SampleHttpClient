//
// To parse this JSON data, add NuGet 'System.Text.Json' then do:
//
//    using GitHubOrganization;
//
//    var dick = GitHubOrganization.FromJson(jsonString);

using System.Text.Json;

namespace SampleHttpClient;

public static class Serialize
{
    public static string ToJson(this GitHubOrganization[] self) =>
        JsonSerializer.Serialize(self, Converter.Settings);
}