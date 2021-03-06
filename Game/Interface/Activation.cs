﻿using System;
using Godot;
using SshCity.Game.Plan;

public class Activation
{
    public static bool isNextToRoad(PlanInitial planInitial, Vector2 tile, int bloc)
    {
        Func<Vector2, bool> NextToRoad = delegate(Vector2 vector2)
        {
            return Routes.IsRoute(planInitial.GetBlock(planInitial.TileMap2, (int) vector2.x - 1, (int) vector2.y)) ||
                   Routes.IsRoute(planInitial.GetBlock(planInitial.TileMap2, (int) vector2.x + 1, (int) vector2.y)) ||
                   Routes.IsRoute(planInitial.GetBlock(planInitial.TileMap2, (int) vector2.x, (int) vector2.y - 1)) ||
                   Routes.IsRoute(planInitial.GetBlock(planInitial.TileMap2, (int) vector2.x, (int) vector2.y + 1));
        };

        bool res = false;
        (int largeur, int longueur) dimensions = (1, 1);
        try
        {
            dimensions = Ref_donnees.dimensions[bloc];
        }
        catch (Exception)
        {
        }

        for (int i = 0; i < dimensions.longueur; i++)
        {
            for (int j = 0; j < dimensions.largeur; j++)
            {
                res = res || NextToRoad(tile + new Vector2(i, j));
            }
        }

        return res;
    }

    public static bool isRaccordeEnEau(PlanInitial planInitial, Vector2 tile, int bloc, int consomationEau)
    {
        if (bloc != Ref_donnees.stationEpuration)
        {
            if (consomationEau == 0)
            {
                return true;
            }

            return (planInitial.GetBlock(planInitial.TileMap0, (int) tile.x + 1, (int) tile.y + 1) ==
                    Ref_donnees.sol_maisonEau);
        }
        else
        {
            return (planInitial.GetBlock(planInitial.TileMap0, (int) tile.x + 1, (int) tile.y + 1) ==
                    Ref_donnees.sol_stationEpuration);
        }
    }
}