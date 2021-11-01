using System;
using System.Globalization;
using System.Linq;
using System.Text;
using PokeApiNet;

namespace PokemonBot.Services
{
    public class PokeService
    {
        private readonly Pokemon _pokemon;
        private readonly TextInfo _upperCase;

        public PokeService(Pokemon pokemon)
        {
            _pokemon = pokemon;
            _upperCase = new CultureInfo("es-ES", false).TextInfo;
        }
        public string Image { get => _pokemon.Sprites.FrontDefault;}

        public string Name { get => String.Format("*Name*: {0}",_upperCase.ToTitleCase(_pokemon.Name));}

        public string Types { get => String.Format("*Type*: {0}",_upperCase.ToTitleCase(String.Join(" / ", _pokemon.Types.Select(x => x.Type.Name))));}

        public string GetPokemonInfo()
        {
            StringBuilder _stringBuilder = new StringBuilder();
            _stringBuilder.AppendLine(Name);
            _stringBuilder.AppendLine(Types);
            return _stringBuilder.ToString();
        }

    }
}