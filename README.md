# SampleHttpClient
A simple .NET Core console application to demonstrate using HttpClient

## Back-Story ...

### Newtonsoft.Json (aka Json.NET)
Written by James Newton-King (JamesNK) : earliest version showing on NuGet was 3.5.8 [8-Jan-2011]
The latest release was 13.0.3 [8-March-2023], and has proved very popular

Cumulative downloads on NuGet now exceed 4.5B, so you can see its colossal momentum. BTW James posted on Twitter (now X)
- 3-May-2021 "One BILLION Newtonsoft.Json downloads on NuGet!" (15 years after its launch)
- 28-Sep-2019 "Two billion Newtonsoft.Json downloads! The second billion only took 13 months."

Json.NET versions prior to 13.0.1 [22-Mar-2021] are flagged by NuGet to exhibit high vulnerability
"Newtonsoft.Json prior to version 13.0.1 is vulnerable to Insecure Defaults due to improper handling of expressions with high nesting level that lead to StackOverFlow exception or high CPU and RAM usage. Exploiting this vulnerability results in Denial Of Service (DoS)."
https://github.com/advisories/GHSA-5crp-9r3c-p9vr/

There is also a separate Newtonsoft.Json.Schema package.

### System.Text.Json
Microsoft has produced an alternative high-performance System.Text.Json package (aka STJ) available on NuGet
earliest shown is 4.6.0 [23-Sep-2019], latest 8.0.3 [12-Mar-2024] and cumulative downloads exceed 2B

Microsoft continues to invest in developing this package, and strives for parity with the legacy Json.NET packages
recent .NET releases trumpet System.Text.Json performance and allocation characteristics
https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/
https://learn.microsoft.com/en-gb/dotnet/core/whats-new/dotnet-7#systemtextjson-serialization
https://learn.microsoft.com/en-gb/dotnet/core/whats-new/dotnet-8/runtime#serialization

### James Newton-King
From Wellington (New Zealand) James joined Microsoft Q2 2018 and works as PM/Principal Software Engineer in Singapore, kept busy on other duties although he continues as owner of these repos, probably unrealistic to expect JN-K to expend more significant effort
obviously as OSS there are many other contributors and experts worldwide who will continue to carry the flame
https://github.com/JamesNK/Newtonsoft.Json/
https://github.com/JamesNK/Newtonsoft.Json.Schema/

## SampleHttpClient repo (OSS)
This repo was originally written by Jeffrey T. Fritz in 2018 based on netcoreapp2.2 and Json.NET 12.0.1 (27-Nov-2018)
however [given the above directions] it seemed appropriate to modernise the repo to STJ, hence this Issue and PR

This revamp is intended to still be simple (replacing the Json.NET attributes/methods by STJ equivalents)
but also accommodates the latest GitHub org schema. Hopefully this will help other developers who want to follow best practices.

Developers may be aware of the Visual Studio menu options *E*dit, Paste *S*pecial, Paste **JSON** as classes [and equivalent Paste **XML** as classes], which is quick but
- property names are lowercase, raising IDE1006 "Naming rule violation: These words must begin with upper case characters: X" warnings
- lacks property attributes [e.g. **JsonProperty** for Json.NET or **JsonPropertyName** for STJ]
- raises CS8618 "Non-nullable property 'X' must contain a non-null value when exiting constructor. Consider declaring the property as nullable"

For a better experience visit
https://quicktype.io
that was noted by Jeff Fritz on the original repo release. My PR also uses this excellent site with these settings

| **parameter** | **value** | 
| -|- | 
| Language | C# | 
| serialization framework | System Text Json | 
| Generated namespace | GitHubOrganization | 
| Use T[] or List<T> | Array | 
| Output features | Complete | 
| Generate virtual properties | off | 
| Fail if required properties are missing | off | 
| Make all properties optional | ON  |  

however that final parameter pop-up claims "Make all properties optional" but it only makes value properties nullable, e.g.
- bool?, DateTimeOffset?, long?
- CultureInfo, DateTimeStyles, License, object, Owner, Permissions, string, Uri

regardless whether the json sample was always/sometimes populated. However having the "#pragma warning disable/restore CS8618" obviates the analyzer forcing in "required" keyword
and the actual reflection mechanisms bypasses the enforcement of such required rules [i.e. the execution succeeds!]

Enjoy ! (Dick in UK)
