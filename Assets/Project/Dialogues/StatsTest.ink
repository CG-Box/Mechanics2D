INCLUDE globals.ink


Hi player. Money {money} Your stats: Charisma {Charisma}, Manipulation {Manipulation}, Intelligence {Intelligence}
-> main


=== main ===
Can you give me a little bit of money? {Intelligence}
{ Intelligence > 5 :
    + [I'm smart man  @color: \#3e2286]
         -> var_intel
- else:
    + [It's only game start]
        -> var_point
}
{ Charisma == 0 :
    + [Here your 25$ @color: red @style: cursive]
         -> var_0
- else:
    + [Ok take 3$  @color: blue]
         -> var_1
}

{ Charisma > 0 :
    + [I think you have enough money @color: green]
       -> var_2
}

{Charisma > 9 :
    + [May be better you give me some money]
        -> var_3
}

{Charisma > 10 :
    + [Get away from me @color: yellow]#sound: angryScream
       -> var_4
}

=== var_point ===
~AddPoints(10)
Oh it realy is. Ok, so here take some present from me.
-> END
=== var_intel ===
You are <color=\#5B81FF><b>MEGA</b> mind</color>
-> END

=== var_0 ===
~ShowInfo("You have no money")
~AddMoney(-25)
~ money -= 25
Thank you mister
-> END

=== var_1 ===
~AddMoney(-3)
~ money -= 3
Thank you mister. Money {money}
-> END

=== var_2 ===
{ Charisma > 5:Why i say it, but not mind |It was bad}
You are too smart
-> END

=== var_3 ===
~AddMoney(50)
~ money += 50
Ok, here is your money
-> END

=== var_4 ===
Sorry
  -> END