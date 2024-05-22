using Microsoft.AspNetCore.Mvc;
using PREFINAL_ASSIGNMENT_TWO_POKEMON.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace PREFINAL_ASSIGNMENT_TWO_POKEMON.Controllers
{
    public class PokemonController : Controller
    {
        private readonly HttpClient _httpClient;

        public PokemonController(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            try
            {
                var response = await _httpClient.GetAsync($"pokemon?offset={(page - 1) * 20}&limit=20");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var pokemonResponse = JsonConvert.DeserializeObject<PokemonResponse>(content);
                return View(pokemonResponse.Results);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error retrieving Pokemon data: {ex.Message}");
            }
        }

        public async Task<IActionResult> Details(string name)
        {
            try
            {
                var response = await _httpClient.GetAsync($"pokemon/{name}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var pokemon = JsonConvert.DeserializeObject<Pokemon>(content);

                return View(pokemon);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error retrieving Pokemon details: {ex.Message}");
            }
        }
    }

    internal class PokemonResponse
    {
        public string Results { get; internal set; }
    }
}
