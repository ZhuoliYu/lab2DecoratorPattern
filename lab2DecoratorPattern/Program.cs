var Paladin = new Player();
var littleDevil = new LittleDevil();
var deathWing = new DeathWing();
var fightsystem = new SettingOfFighting();
fightsystem.fight(Paladin, deathWing);
//output part to consoleApp

public class SettingOfFighting
{
    public void fight(Player p, Enemy enemy)
    {
        while (p.HP > 0 && enemy.HP > 0)
        {
            p.Armor(enemy);
            var previousHP = enemy.HP;
            enemy.Armor(p);
            var HPAfterFighting = enemy.HP;
            if (HPAfterFighting >= previousHP)
            {
                Console.WriteLine("Only certain weapon can damage to this monster, otherwise HP of monster will increase after attacking");
                Console.WriteLine("Select a speacial weapon please");
                Console.WriteLine("1. Frostmourne");
                Console.WriteLine("2. TriForce");
                var selectedWeopon = Console.ReadLine();
                if (selectedWeopon == "1")
                {
                    var frostmourne = new Frostmourne(p);
                    Console.WriteLine("Frostmourne has been equipped");
                }
                else if (selectedWeopon == "2")
                {
                    var triForce = new TriForce(p);
                    Console.WriteLine("TriForce  has been equipped");
                }
            }
        }
        if (p.HP < 0)
        {
            Console.WriteLine("WASTED");

        }
        else
        {
            Console.WriteLine("Congraduation! Monster has been slained");
        }
    }
}

public class Player//player's basic properties setting
{
    public virtual int Strength { get; set; }
    public int HP { get; set; }

    public int ArmorPoint { get; set; }

    public virtual int WeaponEnchant { get; set; }

    public Player()
    {
        Strength = 35;
        HP = 200;
        ArmorPoint = 5;
        WeaponEnchant = 0;//special ability of weapon only can use to certain enemy
    }
    public void Armor(Enemy enemy)
    {
        HP -= (enemy.Strength - ArmorPoint);
        Console.WriteLine($"Paladin dealed {(enemy.Strength - ArmorPoint)} damage, (remained HP :{HP})");
    }
}

public abstract class Enemy//monster's properties setting
    //use abstract type to set parent class called enemy
{
    public int Strength { get; set; }
    public int HP { get; set; }
    public int ArmorPoint { get; set; }

    public virtual void Armor(Player player)//this part is confusing because as enemy they will only
                                            //be attacked by player, not attack player initiatively
    {
        HP -= (player.Strength - ArmorPoint);
        Console.WriteLine($" damaged {(player.Strength - ArmorPoint)} (remained HP :{HP})");
    }

}

public class LittleDevil : Enemy//child class,inherit from enemy class
{
    public LittleDevil()
    {
        Strength = 7;
        HP = 20;
        ArmorPoint = 1;
    }
    public override void Armor(Player player)
      
    {
        //Console.WriteLine("LittleDevil");
        base.Armor(player);

    }
}

public class DeathWing : Enemy
{
    public DeathWing()
    {
        Strength = 15;
        HP = 99;
        ArmorPoint = 8;
    }

    public override void Armor(Player player)
    {
        HP -= (player.WeaponEnchant - ArmorPoint);

       //using decoration pattern(weapon enchant) to dmg to enemy
        Console.WriteLine($"DeathWing dealed {(player.WeaponEnchant - ArmorPoint)} damage, (remained HP :{HP})");
    }
}

public abstract class Weapon : Player
{
    public Player player { get; set; }

    public override int WeaponEnchant { get => base.WeaponEnchant; set => base.WeaponEnchant = value; }
    public override int Strength { get => base.Strength; set => base.Strength = value; }

}

public class Frostmourne : Weapon
{
    public Frostmourne(Player p)
    {
        player = p;
        player.WeaponEnchant += 66;
    }
}


public class TriForce : Weapon
{
    public TriForce(Player p)
    {
        player = p;
        player.Strength += 88;
    }
}