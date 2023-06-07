
namespace RpgMvc.Models
{
    public class ArmaViewModel

    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Dano { get; set; }
        public PersonagemHabilidadeViewModel Personagem {get; set;}
        public int PersonagemId {get; set;}
        public ArmaViewModel Arma {get; set;}
        public int ArmaId {get; set;}
    }
}