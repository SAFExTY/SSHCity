using System;
using System.Collections.Generic;
using Godot;
using SshCity.Game.Buildings;
using SshCity.Game.Plan;

public class Interface : CanvasLayer
{
    private const string _str_shop = "Boutique";
    private const string _str_button_shop = "ButtonShop";
    private const string _str_money_couleur = "MoneyColor";
    private const string _str_money_text = "MoneyColor/MoneyText";
    private const string _str_buttonRoute = "ButtonAjoutRoute";
    private const string _str_buttonDelete = "ButtonDelete";
    private const string _str_sonouverture = "ButtonShop/Ouverture";
    private const string _str_bulldozerMouse = "BulldozerMouse";
    private const string _strButtonEau = "ButtonEau";
    private const string _str_croix = "CroixRouge";
    private const string _str_croixJaune = "CroixJaune";
    private const string _str_infos = "Infos";
    private const string _str_timer = "Timer";
    private const string _strButtonTuyaux = "ButtonTuyaux";
    private const string _strButonExit = "ButtonExitEau";
    private static bool _interdit = false;
    private static bool _interdiMoney = false;
    private static Infos _infos;

    private static bool _infosBool = false;
    private static PlanInitial _planInitial;
    private static Button _buttonTuyaux;
    private static int _money = Ref_donnees.argent;
    private static int _energy = Ref_donnees.energy;
    private static int _water = Ref_donnees.water;
    private static bool _hide = true;
    private static Button _button_shop;
    private static Button _buttonDelete;
    private static Button _buttonRoute;
    private static Button _buttonEau;
    private static bool _delete = false;
    private static Panel _money_couleur;
    private static Label _money_text;

    /* ScoreBar */
    private static TextureProgress ScoreBar;
    private static Label Score;
    private static int _xp = 0;
    public static int _level = 1; // niveau
    private static int XpMax = 30;
    public static bool levelup = false;

    private static int moneyWin = 0;
    private static int energyused = 0;
    private static int waterused = 0;
    private static bool _moneyAutomatique = true;

    public static List<Vector2> ListTuyaux = new List<Vector2>();
    private bool _achatRoute = false;
    private bool _achatTuyaux = false;
    private Sprite _bulldozerMouse;

    private Button _buttonExit;
    public Sprite _croix;
    private Sprite _croixJaune;
    private Label _energy_text;
    private AudioStreamPlayer _ouvertureboutique;
    private Sprite _rouages;
    private Boutique _shop;
    private Timer _timer;
    private bool _visible;
    private Label _water_text;

    public static bool MoneyAutomatique
    {
        get => _moneyAutomatique;
        set => _moneyAutomatique = value;
    }

    public static int MoneyWin => moneyWin;

    public static int Energyused => energyused;

    public static int Waterused => waterused;

    public static bool Delete
    {
        get => _delete;
        set => _delete = value;
    }

    public static bool InfosBool
    {
        get => _infosBool;
        set => _infosBool = value;
    }

    public static bool InterdiMoney
    {
        get => _interdiMoney;
        set => _interdiMoney = value;
    }

    public static bool Interdit
    {
        get => _interdit;
        set => _interdit = value;
    }

    public static bool Hide
    {
        get => _hide;
        set => _hide = value;
    }

    public static int Money
    {
        get => _money;
        set => _money = value;
    }

    public static int Xp
    {
        get => _xp;
        set => _xp = value;
    }

    public static int Energy
    {
        get => _energy;
        set => _energy = value;
    }

    public static int Water
    {
        get => _water;
        set => _water = value;
    }

    public static void Init(PlanInitial planInitial)
    {
        _planInitial = planInitial;
    }

    public override void _Ready()
    {
        _money_couleur = (Panel) GetNode(_str_money_couleur);
        _money_text = (Label) GetNode(_str_money_text);
        _button_shop = (Button) GetNode(_str_button_shop);
        _buttonRoute = (Button) GetNode(_str_buttonRoute);
        _buttonDelete = (Button) GetNode(_str_buttonDelete);
        _buttonEau = (Button) GetNode(_strButtonEau);
        _shop = (Boutique) GetNode(_str_shop);
        _bulldozerMouse = (Sprite) GetNode(_str_bulldozerMouse);
        _bulldozerMouse.Hide();
        _croix = (Sprite) GetNode(_str_croix);
        _croixJaune = (Sprite) GetNode(_str_croixJaune);
        _infos = (Infos) GetNode(_str_infos);
        _timer = (Timer) GetNode(_str_timer);
        ScoreBar = (TextureProgress) GetNode("ScoreBar");
        Score = GetNode<Label>("Score");

        //Tuyaux
        _buttonExit = (Button) GetNode(_strButonExit);
        _buttonTuyaux = (Button) GetNode(_strButtonTuyaux);

        /* cache toute l interface au demarrage */
        _money_couleur.Hide();
        _money_text.Hide();
        _button_shop.Hide();
        _buttonDelete.Hide();
        _buttonEau.Hide();
        _buttonRoute.Hide();
        Score.Hide();
        ScoreBar.Hide();

        _croix.Hide();
        _croixJaune.Hide();
        _infos.Hide();

        _timer.Start();

        _ouvertureboutique = (AudioStreamPlayer) GetNode(_str_sonouverture);
        _button_shop.Connect("pressed", this, nameof(ButtonShopPressed));
        _buttonDelete.Connect("pressed", this, nameof(ButtonDeletePressed));
        _buttonRoute.Connect("pressed", this, nameof(ButtonRoutePressed));
        _buttonEau.Connect("pressed", this, nameof(PressedButtonEau));
        _buttonExit.Connect("pressed", this, nameof(ExitPressed));
        _buttonTuyaux.Connect("pressed", this, nameof(TuyauxPressde));

        _button_shop.Connect("mouse_entered", this, nameof(ButtonOver));
        _buttonDelete.Connect("mouse_entered", this, nameof(ButtonOver));
        _buttonRoute.Connect("mouse_entered", this, nameof(ButtonOver));
        _buttonEau.Connect("mouse_entered", this, nameof(ButtonOver));
        _buttonTuyaux.Connect("mouse_entered", this, nameof(ButtonOver));
        _buttonExit.Connect("mouse_entered", this, nameof(ButtonOver));

        _button_shop.Connect("mouse_exited", this, nameof(ButtonExited));
        _buttonRoute.Connect("mouse_exited", this, nameof(ButtonExited));
        _buttonDelete.Connect("mouse_exited", this, nameof(ButtonExited));
        _buttonEau.Connect("mouse_exited", this, nameof(ButtonExited));

        _buttonTuyaux.Connect("mouse_exited", this, nameof(ButtonExited));
        _buttonExit.Connect("mouse_exited", this, nameof(ButtonExited));

        _timer.Connect("timeout", this, nameof(WinMoney));
        _timer.Connect("timeout", this, nameof(EnergyWin));
        _timer.Connect("timeout", this, nameof(WaterWin));

        _xp = 0;
        _level = 1;
        ScoreBar.MaxValue = 30; //UpdateScoreValue(_level);
        ScoreBar.MinValue = 0;
        ScoreBar.Value = _xp;
        Score.Text = Convert.ToString(_level);
    }

    public void TuyauxPressde()
    {
        _achatTuyaux = !_achatTuyaux;
        PlanInitial.Tuyaux = _achatTuyaux;
    }

    public void ExitPressed()
    {
        _achatTuyaux = false;
        PlanInitial.Tuyaux = false;
        _planInitial.TileMapWithoutRoute.Hide();
        _planInitial.TileMap0.Hide();
        _planInitial.TileMap1.Show();
        _planInitial.TileMap2.Show();
        _button_shop.Show();
        _buttonDelete.Show();
        _buttonRoute.Show();
        _buttonEau.Show();
        _buttonExit.Hide();
        _buttonTuyaux.Hide();
    }

    public void PressedButtonEau()
    {
        _button_shop.Hide();
        _buttonDelete.Hide();
        _buttonRoute.Hide();
        _buttonEau.Hide();
        _buttonExit.Show();
        _buttonTuyaux.Show();
        _planInitial.TileMap0.Show();
        _planInitial.TileMap1.Hide();
        _planInitial.TileMap2.Hide();
        _planInitial.TileMapWithoutRoute.Show();
    }

    public static void ConfigInfos(Vector2 tile)
    {
        if (tile == MainPlan.MairiePosition)
        {
            MairieMenu.OpenMairieMenu = true;
        }
        else if (_infos.config(tile))
        {
            _infos.Show();
        }
    }

    public void WinMoney()
    {
        if (_moneyAutomatique)
        {
            Money += moneyWin;
        }
        else
        {
            MairieMenu.MoneyWinManuel += moneyWin;
        }
    }

    public void EnergyWin()
    {
        Energy -= energyused;
    }

    public void WaterWin()
    {
        Water -= Waterused;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        _money_text.Text = Convert.ToString(_money);

        /* incrementation de la barre de niveau */
        //ScoreBar.MaxValue = UpdateScoreValue(_level);
        if (_xp >= XpMax)
        {
            _xp -= XpMax;
            _level += 1;
            levelup = true;
        }

        ScoreBar.Value = _xp;
        Score.Text = Convert.ToString(_level);

        moneyWin = 0;
        energyused = 0;
        waterused = 0;
        foreach (var batiment in Building.ListBuildings)
        {
            if (Activation.isNextToRoad(_planInitial, batiment.Position,
                batiment.Characteristics.Bloc[batiment.Characteristics.Lvl]) && Activation.isRaccordeEnEau(
                _planInitial, batiment.Position, batiment.Characteristics.Bloc[batiment.Characteristics.Lvl],
                batiment.Characteristics.water[batiment.Characteristics.Lvl]))
            {
                bool energirvalide = false;
                bool eauvalide = false;
                if (!batiment.Activated)
                {
                    _planInitial.SetBlock(_planInitial.TileMap3, (int) batiment.Position.x, (int) batiment.Position.y,
                        -1);
                }

                batiment.Activated = true;
                if (energyused + batiment.Characteristics.energy[batiment.Characteristics.Lvl] <= Ref_donnees.energy)
                {
                    if (batiment.Characteristics.Bloc[batiment.Characteristics.Lvl] != Ref_donnees.centrale)
                    {
                        energyused += batiment.Characteristics.energy[batiment.Characteristics.Lvl];
                    }

                    energirvalide = true;
                }

                if (waterused + batiment.Characteristics.water[batiment.Characteristics.Lvl] <= Ref_donnees.water)
                {
                    waterused += batiment.Characteristics.water[batiment.Characteristics.Lvl];
                    eauvalide = true;
                }

                if (energirvalide && eauvalide)
                {
                    moneyWin += batiment.Characteristics.Earn[batiment.Characteristics.Lvl];
                }
            }
            else
            {
                if (!Activation.isNextToRoad(_planInitial, batiment.Position,
                    batiment.Characteristics.Bloc[batiment.Characteristics.Lvl]))
                {
                    _planInitial.SetBlock(_planInitial.TileMap3, (int) batiment.Position.x, (int) batiment.Position.y,
                        Ref_donnees.bulleRoute);
                }
                else
                {
                    _planInitial.SetBlock(_planInitial.TileMap3, (int) batiment.Position.x, (int) batiment.Position.y,
                        Ref_donnees.bulleEau);
                }

                batiment.Activated = false;
            }
        }

        if (PlanInitial.Delete)
        {
            Vector2 mousePosition = GetViewport().GetMousePosition();
            _bulldozerMouse.Position = new Vector2(mousePosition.x + 25, mousePosition.y + 25);
        }
        else
        {
            _bulldozerMouse.Hide();
        }

        if (_interdit)
        {
            _croix.Show();
            Vector2 mousePosition = GetViewport().GetMousePosition();
            _croix.Position = new Vector2(mousePosition.x, mousePosition.y);
        }
        else
        {
            _croix.Hide();
        }

        if (_interdiMoney)
        {
            _croixJaune.Show();
            Vector2 mousePosition = GetViewport().GetMousePosition();
            _croixJaune.Position = new Vector2(mousePosition.x, mousePosition.y);
        }
        else
        {
            _croixJaune.Hide();
        }
    }

    public override void _Input(InputEvent OneAction)
    {
        base._Input(OneAction);
        if (OneAction.IsActionPressed("OuvertureBoutique"))
        {
            ButtonShopPressed();
        }

        if (OneAction.IsActionPressed("Route"))
        {
            ButtonRoutePressed();
        }

        if (OneAction.IsActionPressed("Delete"))
        {
            ButtonDeletePressed();
        }
    }

    public void ButtonShopPressed()
    {
        //Ferme Achat Route
        _achatRoute = false;
        PlanInitial.AchatRoute(_achatRoute);

        _delete = false;
        PlanInitial.Delete = _delete;
        DeleteVerif.Verif = false;

        Infos.Close = true;

        _shop.ViewShop(_hide);
        if (_hide && Parametres.effets) // && verifie que le joueur n'est pas desactiver les effets sonores
        {
            _ouvertureboutique.Play();
        }
    }

    public void ButtonRoutePressed()
    {
        _achatRoute = !_achatRoute;
        PlanInitial.AchatRoute(_achatRoute);

        _delete = false;
        PlanInitial.Delete = _delete;
        DeleteVerif.Verif = false;
        Infos.Close = true;
        _hide = false;
        _shop.ViewShop(_hide);
    }

    public void ButtonDeletePressed()
    {
        _hide = false;
        _shop.ViewShop(_hide);

        _interdit = false;
        _achatRoute = false;
        PlanInitial.AchatRoute(_achatRoute);
        Infos.Close = true;
        _delete = !_delete;
        PlanInitial.Delete = _delete;
        _bulldozerMouse.Show();
    }

    public void ButtonOver()
    {
        Interdit = false;
        if (_achatTuyaux)
        {
            PlanInitial.Tuyaux = false;
        }

        if (_achatRoute)
            PlanInitial.AchatRoute(false);

        if (_delete)
        {
            PlanInitial.Delete = false;
            _bulldozerMouse.Hide();
        }
    }

    public void ButtonExited()
    {
        if (_achatTuyaux)
        {
            PlanInitial.Tuyaux = true;
        }

        if (_achatRoute)
        {
            PlanInitial.AchatRoute(true);
        }

        if (_delete)
        {
            PlanInitial.Delete = true;
            _bulldozerMouse.Show();
        }
    }

    public static void Start()
    {
        _money_couleur.Show();
        _money_text.Show();
        _button_shop.Show();
        _buttonDelete.Show();
        _buttonEau.Show();
        _buttonRoute.Show();
        Score.Show();
        ScoreBar.Show();
    }
}