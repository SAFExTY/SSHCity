using Godot;
using System;
using SshCity.Scenes.Buildings;
using SshCity.Scenes.Plan;
using SshCity.Scenes.Buildings.BatimentsCaracteristiques;

public class Restaurant
{
    public static int[] _bloc = {Ref_donnees.restaurant};
    public static int[] _cost = {5000};
    public static int[] _earn = {10,15,20};
    public static string[] _titre = {"Restaurant"};
    public static int lvl = 0;
    public static readonly int[] gain_xp = {10, 100, 500};
    public static string[] _image = {"res://assets/ImageSized/isometric boutique5.png"};
    public static int nbrAmeliorations = 0;
    public static Batiments.Class _class = Batiments.Class.RESTAURANT;
    public static Caracteristiques.BatimentsCaracteristiques cara = new Caracteristiques.BatimentsCaracteristiques(nbrAmeliorations, _bloc, _cost, _earn, _titre, gain_xp, _image, _class);
}
