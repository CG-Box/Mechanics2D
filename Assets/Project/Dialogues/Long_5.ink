INCLUDE globals.ink


-> start
=== start ===
Choose variant? #speaker:Random
    + [Add Money]
        -> var_1
    + [Add Item]
        -> var_2
    + [Remove Item taco]
        -> var_3
    + [Take Quest]
        -> var_4
    + [Remove Item jam]
        -> var_5
        

=== var_1 ===
~AddMoney(100)
~PlaySound("coins_rings")
You chose var_1, now see it
   + [1]
    -> Final
=== var_2() ===
You chose to take item, now choose the item
   + [1] 
     -> addItem(1)
   + [2]
    -> addItem(2)
   + [3]
    -> addItem(3)
   + [4]
    -> addItem(4)
    + [5]
    -> addItem(0)
=== addItem(itemId) ===
~AddItem(itemId)
Now you have new item.
-> Final
=== var_3 ===
~RemoveItem(4)
You chose var_3, now see it
   + [1]
        -> Final
    + [2]
        -> Final
     + [3]
        -> Final
=== var_4 ===
~TakeQuest("button mash")
You chose var_4, now see it
   + [1]
        -> Final
    + [2]
        -> Final
    + [3]
        -> Final
    + [4]
        -> Final
=== var_5 ===
~RemoveItem(0)
You chose var_5, now see it
   + [1]
        -> Final
    + [2]
        -> Final
    + [3]
        -> Final
    + [4]
        -> Final
    + [5]
        -> Final
=== Final ===
Nice choice, any questions
    + [No]
     -> bye
    + [Yes]
     -> start
=== bye ===
Ok, bye dude
-> END