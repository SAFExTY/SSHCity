using Godot;

public class Accident : Area2D
{
    private const string _strSprite = "Sprite";
    private bool _mouseIn = false;
    private Sprite _sprite;
    public bool Visi = false;

    public override void _Ready()
    {
        Connect("mouse_entered", this, nameof(MouseEntered));
        Connect("mouse_exited", this, nameof(MouseExited));
        Connect("area_exited", this, nameof(AreaExited));
    }

    /// <summary>
    /// Lorsqu'une Area2D sort, supprime cette area2D. Permet lors de la resolution de l'accident d'arreter les bouchons 
    /// </summary>
    /// <param name="area2D">Area2D venant de sortir de cette area2D</param>
    public void AreaExited(Area2D area2D)
    {
        if (area2D.CollisionMask == 7
        ) // Verifie que l'area venant de sortir est une area2D de CollsionMask accident, afin de ne pas supprimer les collsionMask de vehicules. 
        {
            QueueFree();
        }
    }

    /// <summary>
    /// Lorsque la souris passe sur la zone de l'accident
    /// </summary>
    public void MouseEntered()
    {
        _mouseIn = true;
    }

    /// <summary>
    /// Lorsque la souris sors de la zone de l'accident 
    /// </summary>
    public void MouseExited()
    {
        _mouseIn = false;
    }


    /// <summary>
    /// Constructeur de la zone d'accident
    /// </summary>
    /// <param name="visi">Indique si l'on voit l'image de l'accident ou s'il s'gait juste d'une zone pour faire les bouchons</param>
    public void Init(bool visi)
    {
        _sprite = (Sprite) GetNode(_strSprite);
        _sprite.Visible = visi;
        Visi = visi;
    }
}