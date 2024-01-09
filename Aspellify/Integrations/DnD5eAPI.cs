using Aspellify.Models;
using System;
using System.Net;
using System.Text.Json;
using System.Net.Http;
using System.Linq;

namespace Aspellify.Integrations
{
    public interface IDnD5eAPI
    {
        Task<SpellDnD5e[]> GetSpellsAsync();
    }
    public class DnD5eAPI : IDnD5eAPI
    {
        private readonly HttpClient _httpClient;

        public DnD5eAPI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<SpellDnD5e> GetSpellAsync(string index)
        {
            var response = await _httpClient.GetAsync($"https://www.dnd5eapi.co/api/spells/{index}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception();
            }

            var content = await response.Content.ReadAsStringAsync();

            // Deserialize directly into SpellDnD5e
            SpellDnD5e spell = JsonSerializer.Deserialize<SpellDnD5e>(content);

            return spell;
        }

        public async Task<SpellDnD5e[]> GetSpellsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://www.dnd5eapi.co/api/spells/");

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                SpellListResponse spellListResponse = JsonSerializer.Deserialize<SpellListResponse>(content);

                // Fetch additional details for each spell in parallel
                var spellDetailsTasks = spellListResponse.Results
                    .Select(result => GetSpellAsync(result.Index))
                    .ToArray();

                await Task.WhenAll(spellDetailsTasks);

                // Update the original objects with the fetched details
                for (int i = 0; i < spellListResponse.Results.Count; i++)
                {
                    spellListResponse.Results[i] = await spellDetailsTasks[i];
                }

                return spellListResponse?.Results?.ToArray();
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                //_logger.LogError($"Error retrieving spells: {ex.Message}");
                throw;
            }
        }
    }
}
