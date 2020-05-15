using SshCity.Game.Buildings;
using SshCity.Game.Buildings.BatimentsCaracteristiques;
using SshCity.Game.Plan;

public class Cafe : Caracteristiques
{
    public static Batiments.Class _class = Batiments.Class.CAFE;
    public static int nbrAmeliorations = 0;
    public static int[] _bloc = {Ref_donnees.cafe};
    public static int[] _cost = {1000, 1500};
    public static int[] _earn = {1, 2, 5};
    public static int[] _consomationelec = {1};
    public static int[] _consomationeau = {2};
    public static string[] _titre = {"Cafe"};
    public static readonly int[] gain_xp = {10, 100, 500};

    public static BatimentsCaracteristiques cara =
        new BatimentsCaracteristiques(nbrAmeliorations,
            _bloc,
            _cost,
            _earn,
            _titre,
            gain_xp,
            _image,
            _class,
            _consomationelec,
            _consomationeau);

    public static string[] _image = {"res://assets/ImageSized/isometric magasin6.png"};
}