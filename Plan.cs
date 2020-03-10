using Godot;
using System;
using System.Collections.Generic;

public class Plan : Node2D
{

    private Random rand;
    private TileMap _tileMap1;
    private TileMap _tileMap2;
    private int max_x = 31;
    private int min_x = -16;
    private int max_y = 19;
    private int min_y = -29;
    private int max_flaque_eau = 4;
    private int min_flaque_eau = 2;
    private int max_block_flaque_eau = 10;
    private int min_block_flaque_eau = 5;


    public override void _Ready()
    {
        rand = new Random();
        int nbr_flaque_eau = rand.Next(min_flaque_eau, max_flaque_eau+1);
        _tileMap1 = (TileMap) GetNode("Navigation2D/TileMap1");
        _tileMap2 = (TileMap) GetNode("Navigation2D/TileMap2");
        List<(int, int)> coordonnes_base_flaques = new List<(int, int)>();
        for (int i = 0; i < nbr_flaque_eau; i++)
        {
            int rand_x = rand.Next(min_x, max_x+1);
            int rand_y = rand.Next(min_y, max_y+1);
            int indexe = _tileMap1.GetCell(rand_x, rand_y);
            if (indexe == 0)
            {
                coordonnes_base_flaques.Add((rand_x, rand_y));
                int nbr_block_eau = rand.Next(min_block_flaque_eau, max_block_flaque_eau+1);
                _tileMap1.SetCell(rand_x, rand_y, 2);
                nbr_block_eau++;
                int j = 0;
                while (j < nbr_block_eau)
                {
                    int deplacement = rand.Next(0, 4);

                    switch (deplacement)
                    {
                        case 0:
                        {
                            rand_x++;
                            if (_tileMap1.GetCell(rand_x, rand_y) == 0)
                            {
                                _tileMap1.SetCell(rand_x, rand_y, 2);
                            }
                            if (_tileMap1.GetCell(rand_x+1, rand_y) == 0)
                            {
                                
                                _tileMap1.SetCell(rand_x, rand_y, 2);
                                j++;
                                if (_tileMap1.GetCell(rand_x+1, rand_y) == 0)
                                {
                                    _tileMap1.SetCell(rand_x+1, rand_y, 2);
                                    j++;
                                }
                                if (_tileMap1.GetCell(rand_x, rand_y+1) == 0)
                                {
                                    _tileMap1.SetCell(rand_x, rand_y+1, 2);
                                    j++;
                                }

                                if (_tileMap1.GetCell(rand_x, rand_y - 1) == 0)
                                {
                                    _tileMap1.SetCell(rand_x, rand_y - 1, 2);
                                    j++;
                                }
                            }
                            break;
                        }

                        case 1:
                        {
                            rand_x--;
                            if (_tileMap1.GetCell(rand_x, rand_y) == 0)
                            {
                                _tileMap1.SetCell(rand_x, rand_y, 2);
                            }

                            if (_tileMap1.GetCell(rand_x-1, rand_y) == 0)
                            {
                                
                                _tileMap1.SetCell(rand_x-1, rand_y, 2);
                                j++;
                                if (_tileMap1.GetCell(rand_x-1, rand_y) == 0)
                                {
                                    _tileMap1.SetCell(rand_x-1, rand_y, 2);
                                    j++;
                                }
                                if (_tileMap1.GetCell(rand_x, rand_y+1) == 0)
                                {
                                    _tileMap1.SetCell(rand_x, rand_y+1, 2);
                                    j++;
                                }
                                if (_tileMap1.GetCell(rand_x, rand_y-1) == 0)
                                {
                                    _tileMap1.SetCell(rand_x, rand_y-1, 2);
                                    j++;
                                }
                            }
                            break;
                        }

                        case 2:
                        {
                            rand_y++;
                            if (_tileMap1.GetCell(rand_x, rand_y) == 0)
                            {
                                _tileMap1.SetCell(rand_x, rand_y, 2);
                            }

                            if (_tileMap1.GetCell(rand_x, rand_y+1) == 0)
                            {
                                _tileMap1.SetCell(rand_x, rand_y, 2);
                                j++;
                                if (_tileMap1.GetCell(rand_x+1, rand_y) == 0)
                                {
                                    _tileMap1.SetCell(rand_x+1, rand_y, 2);
                                    j++;
                                }
                                if (_tileMap1.GetCell(rand_x-1, rand_y) == 0)
                                {
                                    _tileMap1.SetCell(rand_x-1, rand_y, 2);
                                    j++;
                                }
                                if (_tileMap1.GetCell(rand_x, rand_y+1) == 0)
                                {
                                    _tileMap1.SetCell(rand_x, rand_y+1, 2);
                                    j++;
                                }
                            }
                            break;
                        }
                        case 3:
                        {
                            rand_y--;
                            if (_tileMap1.GetCell(rand_x, rand_y) == 0)
                            {
                                _tileMap1.SetCell(rand_x, rand_y, 2);
                            }

                            if (_tileMap1.GetCell(rand_x, rand_y-1) == 0)
                            {
                                _tileMap1.SetCell(rand_x, rand_y, 2);
                                j++;
                                if (_tileMap1.GetCell(rand_x+1, rand_y) == 0)
                                {
                                    _tileMap1.SetCell(rand_x+1, rand_y, 2);
                                    j++;
                                }
                                if (_tileMap1.GetCell(rand_x-1, rand_y) == 0)
                                {
                                    _tileMap1.SetCell(rand_x-1, rand_y, 2);
                                    j++;
                                }
                                if (_tileMap1.GetCell(rand_x, rand_y-1) == 0)
                                {
                                    _tileMap1.SetCell(rand_x, rand_y-1, 2);
                                    j++;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                i--;
            }
        }

        foreach (var coord in coordonnes_base_flaques)
        {
            (int x, int y) = coord;
            GD.Print(coord);
            
 
            SableGauche(x, y);
            SableHaut(x, y);
            SableBas(x, y);
            SableDroite(x, y);
        }
        
        int x2 = rand.Next(min_x +8, max_x -8);
        int y2 = rand.Next(min_y +8, max_y -8);
        int acc = 0;
        for (int i = x2; i < x2+5; i+= 2) //cONSTRUCTION DES BATIMENTS
        {
            for (int j = y2; j < y2 + 5; j+= 2)
            {
                if (_tileMap1.GetCell(i,j) == 0)
                {
                    _tileMap2.SetCell(i,j,1);
                }
            }

        }

        for (int i = x2+1; i < x2+6; i++)
        {
            for (int j = y2+1; j < y2 + 6; j++)
            {
                if (_tileMap1.GetCell(i,j) == 0 )
                {
                    _tileMap1.SetCell(i,j,4);
                }
            }
        }
           
           
        for (int i = min_x ; i < max_x; i++)
        {
            /*if (_tileMap1.GetCell(i,y2 +2) == 0)
             {
                 _tileMap1.SetCell(i,y2+2 ,3);
             }*/
            if (avant(i,y2))
            {
                if (gauche(i,y2))
                {
                    if (gauche(i-1,y2))
                    {
                        
                    }
                }
                else
                {
                    _tileMap1.SetCell(i-1,y2-1,4);
                    y2 -= 1;
                    i -= 1;
                }
            }
            else
            {
                if (_tileMap1.GetCell(i,y2) == 0)
                {
                    _tileMap1.SetCell(i,y2, 4);
                }
            }

        }
        for (int i = min_y ; i < max_y; i++)
        {
            /* if (_tileMap1.GetCell(x2+2,i) == 0)
             {
                 _tileMap1.SetCell(x2+2,i ,3);
             }*/
               
        }
    }

    private bool avant(int x, int y)
    {
        return _tileMap1.GetCell(x , y) == 6;
    }
    private bool gauche(int x, int y)
    {
        return _tileMap1.GetCell(x , y-1) == 6;
    }
    private bool droite(int x, int y)
    {
        return _tileMap1.GetCell(x , y+1) == 6;
    }

    private void SableDroite(int x, int y)
    {
        if (_tileMap1.GetCell(x, y) == 0 || _tileMap1.GetCell(x, y) == 6)
        {
            _tileMap1.SetCell(x, y, 6);
        }
        else 
        {
            if (_tileMap1.GetCell(x, y) == 2)
            {
                SableDroite(x+1, y+1);
                SableDroite(x+1, y);
                SableDroite(x+1, y-1);
            }
        }
    }

    private void SableGauche(int x, int y)
    {
        if (_tileMap1.GetCell(x, y) == 0 || _tileMap1.GetCell(x, y) == 6)
        {
            _tileMap1.SetCell(x, y, 6);
        }
        else 
        {
            if (_tileMap1.GetCell(x, y) == 2)
            {
                SableGauche(x-1, y+1);
                SableGauche(x-1, y);
                SableGauche(x-1, y-1);
            }
        }
    }
    
    private void SableHaut(int x, int y)
    {
        if (_tileMap1.GetCell(x, y) == 0 || _tileMap1.GetCell(x, y) == 6)
        {
            _tileMap1.SetCell(x, y, 6);
        }

        if (_tileMap1.GetCell(x, y) == 2)
        {
            SableHaut(x+1, y+1);
            SableHaut(x, y+1);
            SableHaut(x-1, y+1);
        }
        
    }
    
    private void SableBas(int x, int y)
    {
        if (_tileMap1.GetCell(x, y) == 0 || _tileMap1.GetCell(x, y) == 6)
        {
            _tileMap1.SetCell(x, y, 6);
        }
        else 
        {
            if (_tileMap1.GetCell(x, y) == 2)
            {
                SableBas(x+1, y-1);
                SableBas(x, y-1);
                SableBas(x-1, y-1);
            }
        }
    }

    public override void _Process(float delta)
    {
        if (Input.IsKeyPressed((int)KeyList.Space))
        {
            GetTree().ReloadCurrentScene();
        }
    }
}