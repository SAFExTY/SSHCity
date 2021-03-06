using SshCity.Game.Plan;

namespace SshCity.Game.Buildings.Characteristics
{
    public class Restaurant2 : IBuildingCharacteristics
    {
        public Restaurant2()
        {
            Bloc = new[] {Ref_donnees.restaurant2, Ref_donnees.burgeur};
            Cost = new[] {5000, 6000};
            Earn = new[] {10, 15};
            Titre = new[] {"Restaurant", "Burgeur"};
            Lvl = 0;
            GainXp = new[] {10, 15};
            energy = new[] {1, 3};
            water = new[] {2, 4};
            Image = new[] {"res://assets/ImageSized/restaurant2.png", "res://assets/ImageSized/iso resto burger.png"};
            NbrAmeliorations = 1;
            NbCar = 0;
            Population = new[] {0, 0};
        }

        public int[] Bloc { get; }
        public int[] Cost { get; }
        public int[] Earn { get; }
        public string[] Titre { get; }
        public int Lvl { get; set; }
        public int[] GainXp { get; }
        public int[] energy { get; }
        public int[] water { get; }
        public string[] Image { get; }
        public int NbrAmeliorations { get; }
        public int NbCar { get; }
        public int[] Population { get; }
    }
}