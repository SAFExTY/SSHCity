using Godot;
using SshCity.Game.Buildings;

public class MenuEconomie : Node
{
    private const string _str_menu_achat = "Menu_Achat";
    private const string _str_carteCafe = "Menu_Achat/Cafe";
    private const string _str_carteRestaurant = _str_menu_achat + "/Restaurant";
    private const string _str_carteRestaurant2 = _str_menu_achat + "/Restaurant2";
    private const string _str_carteFerme = _str_menu_achat + "/Ferme";
    private const string _str_carteFermeFrande = _str_menu_achat + "/FermeGrande";
    private const string _str_carteEssence = _str_menu_achat + "/Essence";
    private const string _str_carteUsine = _str_menu_achat + "/Usine";
    private Carte _carteCafe;
    private Carte _carteEssence;
    private Carte _carteFerme;
    private Carte _carteFermeGrande;
    private Carte _carteRestaurant;
    private Carte _carteRestaurant2;
    private Carte _carteUsine;
    private Menu_Achat _menu_achat;


    public override void _Ready()
    {
        _menu_achat = (Menu_Achat) GetNode(_str_menu_achat);

        //Config _carteCafe
        _carteCafe = (Carte) GetNode(_str_carteCafe);
        var cafe = BuildingCharacteristics.FromType(BuildingType.CAFE);
        _carteCafe.Bloc = cafe.Bloc[0];
        _carteCafe.Cost = cafe.Cost[0];
        _carteCafe.Titre(cafe.Titre[0]);
        _carteCafe.Gain(cafe.Earn[0]);
        _carteCafe.Prix(cafe.Cost[0]);
        _carteCafe.Enrgie(cafe.energy[0].ToString());
        _carteCafe.Eau(cafe.water[0].ToString());
        _carteCafe.Connect("Achat", _menu_achat, nameof(Menu_Achat.AchatBatiment));

        //Config _carteRestaurant
        _carteRestaurant = (Carte) GetNode(_str_carteRestaurant);
        var restaurant = BuildingCharacteristics.FromType(BuildingType.RESTAURANT);
        _carteRestaurant.Bloc = restaurant.Bloc[0];
        _carteRestaurant.Cost = restaurant.Cost[0];
        _carteRestaurant.Titre(restaurant.Titre[0]);
        _carteRestaurant.Gain(restaurant.Earn[0]);
        _carteRestaurant.Prix(restaurant.Cost[0]);
        _carteRestaurant.Enrgie(restaurant.energy[0].ToString());
        _carteRestaurant.Eau(restaurant.water[0].ToString());
        _carteRestaurant.Connect("Achat", _menu_achat, nameof(Menu_Achat.AchatBatiment));

        //Config _carteRestaurant2
        _carteRestaurant2 = (Carte) GetNode(_str_carteRestaurant2);
        var restaurant2 = BuildingCharacteristics.FromType(BuildingType.RESTAURANT2);
        _carteRestaurant2.Bloc = restaurant2.Bloc[0];
        _carteRestaurant2.Cost = restaurant2.Cost[0];
        _carteRestaurant2.Titre(restaurant2.Titre[0]);
        _carteRestaurant2.Gain(restaurant2.Earn[0]);
        _carteRestaurant2.Prix(restaurant2.Cost[0]);
        _carteRestaurant2.Enrgie(restaurant2.energy[0].ToString());
        _carteRestaurant2.Eau(restaurant2.water[0].ToString());
        _carteRestaurant2.Connect("Achat", _menu_achat, nameof(Menu_Achat.AchatBatiment));

        //Config _carteFerme
        _carteFerme = (Carte) GetNode(_str_carteFerme);
        var ferme = BuildingCharacteristics.FromType(BuildingType.FERME);
        _carteFerme.Bloc = ferme.Bloc[0];
        _carteFerme.Cost = ferme.Cost[0];
        _carteFerme.Titre(ferme.Titre[0]);
        _carteFerme.Gain(ferme.Earn[0]);
        _carteFerme.Prix(ferme.Cost[0]);
        _carteFerme.Enrgie(ferme.energy[0].ToString());
        _carteFerme.Eau(ferme.water[0].ToString());
        _carteFerme.Hide();
        _carteFerme.Connect("Achat", _menu_achat, nameof(Menu_Achat.AchatBatiment));

        //Config _carteFermeGrande
        _carteFermeGrande = (Carte) GetNode(_str_carteFermeFrande);
        var fermeGrande = BuildingCharacteristics.FromType(BuildingType.FERMEGRANDE);
        _carteFermeGrande.Bloc = fermeGrande.Bloc[0];
        _carteFermeGrande.Cost = fermeGrande.Cost[0];
        _carteFermeGrande.Titre(fermeGrande.Titre[0]);
        _carteFermeGrande.Gain(fermeGrande.Earn[0]);
        _carteFermeGrande.Prix(fermeGrande.Cost[0]);
        _carteFermeGrande.Enrgie(fermeGrande.energy[0].ToString());
        _carteFermeGrande.Eau(fermeGrande.water[0].ToString());
        _carteFermeGrande.Hide();
        _carteFermeGrande.Connect("Achat", _menu_achat, nameof(Menu_Achat.AchatBatiment));

        //Config _carteEssence
        _carteEssence = (Carte) GetNode(_str_carteEssence);
        var essence = BuildingCharacteristics.FromType(BuildingType.ESSENCE);
        _carteEssence.Bloc = essence.Bloc[0];
        _carteEssence.Cost = essence.Cost[0];
        _carteEssence.Titre(essence.Titre[0]);
        _carteEssence.Gain(essence.Earn[0]);
        _carteEssence.Prix(essence.Cost[0]);
        _carteEssence.Enrgie(essence.energy[0].ToString());
        _carteEssence.Eau(essence.water[0].ToString());
        _carteEssence.Hide();
        _carteEssence.Connect("Achat", _menu_achat, nameof(Menu_Achat.AchatBatiment));

        //Config _carteUsine
        _carteUsine = (Carte) GetNode(_str_carteUsine);
        var usine = BuildingCharacteristics.FromType(BuildingType.USINE);
        _carteUsine.Bloc = usine.Bloc[0];
        _carteUsine.Cost = usine.Cost[0];
        _carteUsine.Titre(usine.Titre[0]);
        _carteUsine.Gain(usine.Earn[0]);
        _carteUsine.Prix(usine.Cost[0]);
        _carteUsine.Enrgie(usine.energy[0].ToString());
        _carteUsine.Eau(usine.water[0].ToString());
        _carteUsine.Hide();
        _carteUsine.Connect("Achat", _menu_achat, nameof(Menu_Achat.AchatBatiment));

        _menu_achat.Connect("CloseShop", this, nameof(CloseShop));

        _menu_achat.Hide();
        AddUserSignal("CloseShop");

        Carte[] menu1 = {_carteCafe, _carteRestaurant, _carteRestaurant2};
        Carte[] menu2 = {_carteFerme, _carteFermeGrande, _carteEssence};
        Carte[] menu3 = {_carteUsine};
        Carte[][] menus = {menu1, menu2, menu3};
        _menu_achat.Menus = menus;
    }

    public void Reset()
    {
        Carte[] menu1 = {_carteCafe, _carteRestaurant, _carteRestaurant2};
        Carte[] menu2 = {_carteFerme, _carteFermeGrande, _carteEssence};
        Carte[] menu3 = {_carteUsine};
        Carte[][] menus = {menu1, menu2, menu3};
        _carteCafe.Show();
        _carteRestaurant.Show();
        _carteRestaurant2.Show();
        _carteFerme.Hide();
        _carteFermeGrande.Hide();
        _carteUsine.Hide();
        _carteEssence.Hide();
        _menu_achat.Menus = menus;
        _menu_achat._whichMenu = 0;
    }

    public void CloseMenuEconomie()
    {
        _menu_achat.Hide();
    }

    public void OpenMenuEconomie()
    {
        _menu_achat.Show();
    }

    public void CloseShop()
    {
        EmitSignal("CloseShop", false);
    }
}