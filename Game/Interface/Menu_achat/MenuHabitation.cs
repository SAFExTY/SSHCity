using Godot;
using SshCity.Game.Buildings;

public class MenuHabitation : Node
{
    private const string _str_menu_achat = "Menu_Achat";
    private const string _str_carteMaison = _str_menu_achat + "/Maison";
    private const string _str_carteMaison3 = _str_menu_achat + "/Maison2";
    private const string _str_carteMaison4 = _str_menu_achat + "/Maison3";
    private const string _str_carteMaison5 = _str_menu_achat + "/Maison5";
    private const string _str_carteHotel = _str_menu_achat + "/Hotel";
    private Carte _carteHotel;

    private Carte _carteMaison;
    private Carte _carteMaison3;
    private Carte _carteMaison4;
    private Carte _carteMaison5;
    private Menu_Achat _menu_achat;

    public override void _Ready()
    {
        _menu_achat = (Menu_Achat) GetNode(_str_menu_achat);
        _menu_achat.Connect("CloseShop", this, nameof(CloseShop));

        //Config _carteMaison
        _carteMaison = (Carte) GetNode(_str_carteMaison);
        var maison = BuildingCharacteristics.FromType(BuildingType.MAISON);
        _carteMaison.Bloc = maison.Bloc[0];
        _carteMaison.Cost = maison.Cost[0];
        _carteMaison.Titre(maison.Titre[0]);
        _carteMaison.Gain(maison.Earn[0]);
        _carteMaison.Prix(maison.Cost[0]);
        _carteMaison.Enrgie(maison.energy[0].ToString());
        _carteMaison.Eau(maison.water[0].ToString());

        _carteMaison.Connect("Achat", _menu_achat, nameof(Menu_Achat.AchatBatiment));


        //Config _carteMaison3
        _carteMaison3 = (Carte) GetNode(_str_carteMaison3);
        var maison3 = BuildingCharacteristics.FromType(BuildingType.MAISON3);
        _carteMaison3.Bloc = maison3.Bloc[0];
        _carteMaison3.Cost = maison3.Cost[0];
        _carteMaison3.Titre(maison3.Titre[0]);
        _carteMaison3.Gain(maison3.Earn[0]);
        _carteMaison3.Prix(maison3.Cost[0]);
        _carteMaison3.Enrgie(maison3.energy[0].ToString());
        _carteMaison3.Eau(maison3.water[0].ToString());
        _carteMaison3.Connect("Achat", _menu_achat, nameof(Menu_Achat.AchatBatiment));

        //Config _carteMaison4
        _carteMaison4 = (Carte) GetNode(_str_carteMaison4);
        var maison4 = BuildingCharacteristics.FromType(BuildingType.MAISON4);
        _carteMaison4.Bloc = maison4.Bloc[0];
        _carteMaison4.Cost = maison4.Cost[0];
        _carteMaison4.Titre(maison4.Titre[0]);
        _carteMaison4.Gain(maison4.Earn[0]);
        _carteMaison4.Prix(maison4.Cost[0]);
        _carteMaison4.Enrgie(maison4.energy[0].ToString());
        _carteMaison4.Eau(maison4.water[0].ToString());
        _carteMaison4.Connect("Achat", _menu_achat, nameof(Menu_Achat.AchatBatiment));

        //Config _carteMaison5
        _carteMaison5 = (Carte) GetNode(_str_carteMaison5);
        var maison5 = BuildingCharacteristics.FromType(BuildingType.MAISON5);
        _carteMaison5.Bloc = maison5.Bloc[0];
        _carteMaison5.Cost = maison5.Cost[0];
        _carteMaison5.Titre(maison5.Titre[0]);
        _carteMaison5.Gain(maison5.Earn[0]);
        _carteMaison5.Prix(maison5.Cost[0]);
        _carteMaison5.Enrgie(maison5.energy[0].ToString());
        _carteMaison5.Eau(maison5.water[0].ToString());
        _carteMaison5.Hide();
        _carteMaison5.Connect("Achat", _menu_achat, nameof(Menu_Achat.AchatBatiment));

        //Config _carteHotel
        _carteHotel = (Carte) GetNode(_str_carteHotel);
        var hotel = BuildingCharacteristics.FromType(BuildingType.HOTEL);
        _carteHotel.Bloc = hotel.Bloc[0];
        _carteHotel.Cost = hotel.Cost[0];
        _carteHotel.Titre(hotel.Titre[0]);
        _carteHotel.Gain(hotel.Earn[0]);
        _carteHotel.Prix(hotel.Cost[0]);
        _carteHotel.Enrgie(hotel.energy[0].ToString());
        _carteHotel.Eau(hotel.water[0].ToString());
        _carteHotel.Hide();
        _carteHotel.Connect("Achat", _menu_achat, nameof(Menu_Achat.AchatBatiment));

        _menu_achat.Hide();
        AddUserSignal("CloseShop");

        Carte[] menu1 = {_carteMaison, _carteMaison3, _carteMaison4};
        Carte[] menu2 = {_carteMaison5, _carteHotel};
        Carte[][] menus = {menu1, menu2};
        _menu_achat.Menus = menus;
    }

    public void Reset()
    {
        Carte[] menu1 = {_carteMaison, _carteMaison3, _carteMaison4};
        Carte[] menu2 = {_carteMaison5, _carteHotel};
        _carteMaison.Show();
        _carteMaison3.Show();
        _carteMaison4.Show();
        _carteMaison5.Hide();
        _carteHotel.Hide();
        Carte[][] menus = {menu1, menu2};
        _menu_achat._whichMenu = 0;
    }

    public void CloseMenuHabitation()
    {
        _menu_achat.Hide();
    }

    public void OpenMenuHabitation()
    {
        _menu_achat.Show();
    }

    public void CloseShop()
    {
        EmitSignal("CloseShop", false);
    }
}