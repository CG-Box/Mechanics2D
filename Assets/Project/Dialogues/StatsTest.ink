INCLUDE globals.ink

Hi player 
-> main


=== main ===
Can you give me a little bit of money? {playerCharisma}
{ playerCharisma == 0 :
    + [Here your 25$]
         -> var_0
- else:
    + [Ok take 3$]
         -> var_1
}

{ playerCharisma > 0 :
    + [I think you have enough money]
       -> var_2
}

{playerCharisma > 9 :
    + [May be better you give me some money]
        -> var_3
}

{playerCharisma > 10 :
    + [Get away from me]#sound: angryScream
       -> var_4
}

=== var_0 ===
~AddMoney(-25)
Thank you mister
-> END

=== var_1 ===
~AddMoney(-3)
Thank you mister
-> END

=== var_2 ===
{ playerCharisma > 5:Why i say it, but not mind |It was bad}
You are too smart
-> END

=== var_3 ===
~AddMoney(50)
Ok, here is your money
-> END

=== var_4 ===
Sorry
  -> END