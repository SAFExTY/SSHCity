using System.Collections.Generic;
using Godot;
using SshCity.Game.Plan;
using SshCity.Game.Sauvegarde;

namespace SshCity.Game.Buildings
{
    public class Building : ISerializable
    {
        /// <summary>
        /// La list des buildings construits sur la map
        /// </summary>
        public static List<Building> ListBuildings = new List<Building>();

        private Building(BuildingType type, Vector2 position, int theLvl)
        {
            Type = type;
            Characteristics = BuildingCharacteristics.FromType(type);
            Position = position;
            Activated = true;
            Characteristics.Lvl = theLvl;
            Characteristics.Lvl = theLvl;

            PlanInitial.MAX_CAR += Characteristics.NbCar;
            ListBuildings.Add(this);
        }

        /// <summary>
        /// La position du building sur la map
        /// </summary>
        public Vector2 Position { get; }

        /// <summary>
        /// Si le batiment est activé
        /// </summary>
        public bool Activated { get; set; }

        /// <summary>
        /// Le type de Building
        /// </summary>
        public BuildingType Type { get; }

        /// <summary>
        /// Ses caractéristiques associées
        /// </summary>
        public IBuildingCharacteristics Characteristics { get; }

        /// <summary>
        /// Créer le dictionnaire qui store l'état du bâtiment pour le sauvegarder
        /// </summary>
        /// <returns>Le dictionnaire prêt à être sauvegarder</returns>
        public Godot.Collections.Dictionary<string, object> Save()
        {
            return new Godot.Collections.Dictionary<string, object>
            {
                {"PosX", Position.x},
                {"PosY", Position.y},
                {"Type", Type},
                {"Level", Characteristics.Lvl}
            };
        }

        /// <summary>
        /// Génère un nouveau bâtiment 
        /// </summary>
        /// <param name="type">Le type</param>
        /// <param name="position">La position</param>
        /// <param name="theLvl">Le niveau initial</param>
        /// <returns>Le bâtiment créé</returns>
        public static Building Create(BuildingType type, Vector2 position, int theLvl = 0)
        {
            verify(type);
            return new Building(type, position, theLvl);
        }

        public static void verify(BuildingType type)
        {
        }

        public void energyAndWater(Building type)
        {
            (int energy, int water) = (Characteristics.energy[Characteristics.Lvl],
                Characteristics.water[Characteristics.Lvl]);
            if (energy < 0)
            {
                Ref_donnees.energy -= energy;
                Interface.Water -= water;
            }
            else if (water < 0)
            {
                Ref_donnees.water -= water;
                Interface.Energy -= energy;
            }
            else
            {
                Interface.Energy -= energy;
                Interface.Water -= water;
            }

            Ref_donnees.population += Characteristics.Population[Characteristics.Lvl];
        }

        /// <summary>
        /// Supprime un bâtiment de la liste des bâtiments
        /// </summary>
        /// <param name="tile">L'emplacement du bâtiment à supprimer</param>
        /// <returns>Le bâtiment supprimé</returns>
        public static Building Delete(Vector2 tile)
        {
            var i = 0;
            var length = ListBuildings.Count;
            while (i < length)
            {
                var batiment = ListBuildings[i];
                if (batiment.Position == tile)
                {
                    ListBuildings.RemoveAt(i);
                    return batiment;
                }

                i++;
            }

            return null;
        }

        /// <summary>
        /// Augmente le niveau si possible d'un bâtiment à partir de ses coordonnées
        /// </summary>
        /// <param name="tile">L'emplacement du bâtiment à améliorer</param>
        /// <returns>(true, le nouveau niveau) si tout va bien</returns>
        public static (bool, int) Upgrade(Vector2 tile)
        {
            var batimentToUpgrade = Delete(tile);
            if (batimentToUpgrade == null) return (false, -1);
            batimentToUpgrade.Upgrade();
            ListBuildings.Add(batimentToUpgrade);
            return (true, batimentToUpgrade.Characteristics.Bloc[batimentToUpgrade.Characteristics.Lvl]);
        }

        /// <summary>
        /// Fonction interne pour mettre à jour un bâtiment
        /// </summary>
        private void Upgrade()
        {
            if (Characteristics.NbrAmeliorations <= Characteristics.Lvl || Characteristics.Lvl >= 2 ||
                Interface.Money < Characteristics.Cost[Characteristics.Lvl]) return;
            Interface.Money -= Characteristics.Cost[Characteristics.Lvl];
            Interface.Xp += Characteristics.GainXp[Characteristics.Lvl];
            Characteristics.Lvl += 1;
        }

        public static Building GetFromTile(Vector2 tile)
        {
            var i = 0;
            var length = ListBuildings.Count;
            while (i < length)
            {
                var batiment = ListBuildings[i];
                if (batiment.Position == tile)
                    return batiment;
                i++;
            }

            return null;
        }
    }
}