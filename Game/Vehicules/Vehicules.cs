using System;
using System.Collections.Generic;
using Godot;
using SshCity.Game.Plan;

namespace SshCity.Game.Vehicules
{
    public partial class Vehicules: Area2D
    {
        private const string _strAnimatedSprite = "AnimatedSprite";
        private const string _strCollsionShape2D = "CollisionShape2D";
        private Sprite _invincible;
        private const string _strInvincible = "Invincible";
        private AnimatedSprite _animatedSprite;
        private CollisionShape2D _collisionShape2D;
        private Vector2 _deplacement;
        private PlanInitial _planInitial;
        private Vector2 arrive;
        private bool Autonome;
        private static Random rand = new Random();
        private bool _paused = false;
        private bool _stopArea2DCreat = false;
        private Timer _timer;
        private const string _strTimer = "Timer";
        private bool _stopAccident = false;
        public enum Direction
        {
            TOP,
            LEFT,
            BOTTOM,
            RIGHT
        }
        
        public static List<Direction> ListDirection = new List<Direction>()
        {
            {Direction.TOP},
            {Direction.BOTTOM},
            {Direction.RIGHT},
            {Direction.LEFT}
        };

        public enum Type
        {
            CAMION,
            AMBULANCE,
            VOITURE,
            VOITURECOURSE,
            POLICE,
            SPORTIVE,
            LUXE,
            SUV,
            LIVRAISON,
            HATCHBACK,
            TAXI,
            TRACTEURPOLICE,
            TRACTOPEL,
            TRACTEUR,
            VOITURETRUCK,
            VAN
        }

        public static Vector2 DirectionToVector2(Direction dir)
        {
            return dir switch
            {
                Direction.BOTTOM => new Vector2(0, 1),
                Direction.TOP => new Vector2(0, -1),
                Direction.LEFT => new Vector2(-1, 0),
                Direction.RIGHT => new Vector2(1, 0),
                _ => throw new ArgumentException()
            };
        }

        //Choisis l'animation du vehicule (son orientation) par rapport à la route au depart
        Godot.Collections.Dictionary<int, string>  WhichAnimation = new Godot.Collections.Dictionary<int, string>()
        {
            {Ref_donnees.route_left, "NE"},
            {Ref_donnees.route_right, "SE"},
            {Ref_donnees.route_bord_haut_gauche, "NE"},
            {Ref_donnees.route_bord_haut_droit, "SE"},
            {Ref_donnees.route_bord_bas_gauche, "NW"},
            {Ref_donnees.route_bord_bas_droit, "SW"},
            {Ref_donnees.route_T_bas_droite, "SE"},
            {Ref_donnees.route_T_bas_gauche, "SW"},
            {Ref_donnees.route_T_haut_droit, "NE"},
            {Ref_donnees.route_T_haut_gauche, "NW"},
            {-1, "NE"}
        };
        
        private Vector2 Decallage = new Vector2(175, 150);

        Godot.Collections.Dictionary<string, Vector2> DecallageDico = new Godot.Collections.Dictionary<string, Vector2>()
        {
            {"NE", new Vector2(100, 230)},
            {"NW", new Vector2(70, 220)},
            {"SE", new Vector2(-20, 150)},
            {"SW", new Vector2(175, 150)}
        };

        Godot.Collections.Dictionary<string, int> CollisionAngle = new Godot.Collections.Dictionary<string, int>()
        {
            {"NE", 27},
            {"NW", -27},
            {"SE", -27},
            {"SW", 27}
        };
        Godot.Collections.Dictionary<Direction, string> DirectionToAnim = new Godot.Collections.Dictionary<Direction, string>()
        {
            {Direction.TOP, "NW"},
            {Direction.BOTTOM, "SE"},
            {Direction.LEFT, "SW"},
            {Direction.RIGHT, "NE"}
        };
        Godot.Collections.Dictionary<string, Direction> AnimToDirection = new Godot.Collections.Dictionary<string, Direction>()
        {
            {"NW", Direction.TOP},
            {"SE", Direction.BOTTOM},
            {"SW", Direction.LEFT},
            {"NE", Direction.RIGHT}
        };

        Godot.Collections.Dictionary<Type, SpriteFrames> AnimatedSpriteType = new Godot.Collections.Dictionary<Type, SpriteFrames>()
        {
            {Type.AMBULANCE, ResourceLoader.Load("res://Game/Vehicules/ANimatedSpriteVehicules/Ambulance_animatedSprite.tres") as SpriteFrames},
            {Type.CAMION, ResourceLoader.Load("res://Game/Vehicules/ANimatedSpriteVehicules/Camion_animatedSprite.tres") as SpriteFrames},
            {Type.VOITURE, ResourceLoader.Load("res://Game/Vehicules/ANimatedSpriteVehicules/Voiture_animatedSprite.tres") as SpriteFrames},
            {Type.POLICE, ResourceLoader.Load("res://Game/Vehicules/ANimatedSpriteVehicules/Police_animatedSprite.tres") as SpriteFrames},
            {Type.SPORTIVE, ResourceLoader.Load("res://Game/Vehicules/ANimatedSpriteVehicules/VoitureSport_animatedSprite.tres") as SpriteFrames},
            {Type.LUXE, ResourceLoader.Load("res://Game/Vehicules/ANimatedSpriteVehicules/Luxe_animatedSprite.tres") as SpriteFrames},
            {Type.SUV, ResourceLoader.Load("res://Game/Vehicules/ANimatedSpriteVehicules/SUV_animatedSprite.tres") as SpriteFrames},
            {Type.LIVRAISON, ResourceLoader.Load("res://Game/Vehicules/ANimatedSpriteVehicules/Livraison_animatedSprite.tres") as SpriteFrames},
            {Type.HATCHBACK, ResourceLoader.Load("res://Game/Vehicules/ANimatedSpriteVehicules/HatchBack_animatedSprite.tres") as SpriteFrames},
            {Type.VOITURECOURSE, ResourceLoader.Load("res://Game/Vehicules/ANimatedSpriteVehicules/VoitureCourse_animatedSprite.tres") as SpriteFrames},
            {Type.TAXI, ResourceLoader.Load("res://Game/Vehicules/ANimatedSpriteVehicules/Taxi_animatedSprite.tres") as SpriteFrames},
            {Type.TRACTEURPOLICE, ResourceLoader.Load("res://Game/Vehicules/ANimatedSpriteVehicules/TracteurPolice_animatedSprite.tres") as SpriteFrames},
            {Type.TRACTOPEL, ResourceLoader.Load("res://Game/Vehicules/ANimatedSpriteVehicules/Tractopel_animatedSprite.tres") as SpriteFrames},
            {Type.TRACTEUR, ResourceLoader.Load("res://Game/Vehicules/ANimatedSpriteVehicules/Tracteur_animatedSprite.tres") as SpriteFrames},
            {Type.VOITURETRUCK, ResourceLoader.Load("res://Game/Vehicules/ANimatedSpriteVehicules/VoitureTruck_animatedSprite.tres") as SpriteFrames},
            {Type.VAN, ResourceLoader.Load("res://Game/Vehicules/ANimatedSpriteVehicules/Van_animatedSprite.tres") as SpriteFrames},
        };
        
        private Direction direction;
        private bool isMoving = false;

        public void Init(PlanInitial planInitial, Vector2 position, Type type, bool autonome=false)
        {
            _animatedSprite = (AnimatedSprite) GetNode(_strAnimatedSprite);
            _collisionShape2D = (CollisionShape2D) GetNode(_strCollsionShape2D);
            Autonome = autonome;
            this._planInitial = planInitial;
            SpriteFrames spriteFrames = AnimatedSpriteType[type];
            _animatedSprite.Frames = spriteFrames;
            int blocRoute = planInitial.GetBlock(planInitial.TileMap2, (int) position.x, (int) position.y);
            _animatedSprite.Animation = WhichAnimation[blocRoute];
            Decallage = DecallageDico[_animatedSprite.Animation];
            _collisionShape2D.Rotation =  CollisionAngle[_animatedSprite.Animation];
            this.Connect("area_entered", this, nameof(Collision));
            Connect("area_exited", this, nameof(EndCollision));
            this.Position = planInitial.TileMap2.MapToWorld(position + new Vector2(1, 1)) + Decallage;
        }

        public override void _Ready()
        {
            base._Ready();
            _timer = (Timer) GetNode(_strTimer);
            _timer.Connect("timeout", this, nameof(TimeOut));
            _invincible = (Sprite) GetNode(_strInvincible);
        }

        public void Collision(Area2D area2D)
        {
            if (!_stopArea2DCreat && !_stopAccident)
            {
                if (area2D.CollisionMask == 1)
                {
                    Vector2 position = _planInitial.TileMap2.WorldToMap(Position) - new Vector2(1, 1);
                    PlanInitial.AddZoneAccident(Position, true);
                    QueueFree();
                }
                else
                {
                    PlanInitial.AddZoneAccident(Position, false);
                    _paused = true;
                    _stopArea2DCreat = true;
                }
            }
        }

        public void EndCollision(Area2D area2D)
        {
            if (area2D.CollisionMask == 3)
            {
                _stopArea2DCreat = false;
                _paused = false;
                _stopAccident = true;
                _invincible.Show();
                _timer.Start();
            }
        }

        public void TimeOut()
        {
            _stopAccident = false;
            _invincible.Hide();
        }
        
        public static List<Type> ListTypeVehicules = new List<Type>()
        {
            {Type.SUV},
            {Type.VAN},
            {Type.LUXE},
            {Type.TAXI},
            {Type.SPORTIVE},
            {Type.VOITURE},
            {Type.TRACTEUR},
            {Type.HATCHBACK},
            {Type.LIVRAISON},
            {Type.TRACTOPEL},
            {Type.VOITURETRUCK},
            {Type.VOITURECOURSE},
        };
    }
}