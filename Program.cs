using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using SampleHttpClient;

GitHubOrganization[] fritzAndFriends;       // now an array

using (var client =
    new HttpClient
    {
        BaseAddress = new Uri("https://api.github.com")
    })
{
    client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
    client.DefaultRequestHeaders.UserAgent.Add(ProductInfoHeaderValue.Parse("HttpClient"));

    var responseString = await client.GetStringAsync("orgs/fritzandfriends/repos");
    fritzAndFriends = GitHubOrganization.FromJson(responseString)
        .OrderBy(ff => ff.FullName)
        .ToArray();
}

// Cheer 600 lannonbr 12/27/2018
// Cheer 100 nodebotanist 12/27/2018
// Cheer 1000 printscharming 12/27/2018

foreach (GitHubOrganization fritzAndFriend in fritzAndFriends)
{
    Console.WriteLine($"Found the organization {fritzAndFriend.FullName}");
}
Console.ReadLine();
